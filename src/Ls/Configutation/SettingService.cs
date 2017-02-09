using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Ls.Application;
using Ls.Caching;
using Ls.Domain.Repositories;
using Ls.Domain.UnitOfWork;
using Ls.Utilities;

namespace Ls.Configutation
{
    public class SettingService : ApplicationServiceBase,ISettingService
    {
        #region Constants

        /// <summary>
        /// 缓存设置键
        /// </summary>
        private const string SETTINGS_ALL_KEY = "Nop.setting.all";
        /// <summary>
        /// 清除缓存键
        /// </summary>
        private const string SETTINGS_PATTERN_KEY = "Nop.setting.";

        #endregion

        #region Fields
        private readonly IRepository<Setting> _settingRepository;
        private readonly ICacheManager _cacheManager;
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="settingRepository"></param>
        /// <param name="cacheManager"></param>
        public SettingService(IRepository<Setting> settingRepository,
            ICacheManager cacheManager)
        {
            _settingRepository = settingRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Nested classes

        [Serializable]
        public class SettingForCaching
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
        }

        #endregion

        #region Utilities


        /// <summary>
        /// 获取所有的Setting
        /// </summary>
        /// <returns>Settings</returns>
        [UnitOfWork]
        protected virtual IDictionary<string, SettingForCaching> GetAllSettingsCached()
        {
            //cache
            string key = string.Format(SETTINGS_ALL_KEY);
            return _cacheManager.Get(key, () =>
            {
                //we use no tracking here for performance optimization
                //anyway records are loaded only for read-only operations
                var query = from s in _settingRepository.GetTable()
                            orderby s.Name
                            select s;
                var settings = query.ToList();
                var dictionary = new Dictionary<string, SettingForCaching>();
                foreach (var s in settings)
                {
                    var resourceName = s.Name.ToLowerInvariant();
                    var settingForCaching = new SettingForCaching
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Value = s.Value
                    };
                    if (!dictionary.ContainsKey(resourceName))
                    {
                        //first setting
                        dictionary.Add(resourceName, settingForCaching);
                    }

                }
                return dictionary;
            });
        }

        #endregion
        /// <summary>
        /// 新增一个
        /// </summary>
        /// <param name="setting">Setting</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        [UnitOfWork]
        public virtual void InsertSetting(Setting setting, bool clearCache = true)
        {
            if(setting==null) throw new  LsException("Setting对象为空");
            setting.Id = LsIdGenerator.CreateIdentity();
            _settingRepository.Add(setting);
            if (clearCache)
                _cacheManager.RemoveByPattern(SETTINGS_PATTERN_KEY);
        }

        /// <summary>
        /// 更新 a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
       [UnitOfWork]
        public virtual void UpdateSetting(Setting setting, bool clearCache = true)
        {
            _settingRepository.Update(setting);
            if (clearCache)
                _cacheManager.RemoveByPattern(SETTINGS_PATTERN_KEY);

        }

        [UnitOfWork]
        public Setting GetSettingById(long settingId)
        {
            return _settingRepository.Get(settingId);
        }

        [UnitOfWork]
        public void DeleteSetting(long settingId)
        {
           
            var setting = GetSettingById(settingId);
            _settingRepository.Delete(setting);

            //cache
            _cacheManager.RemoveByPattern(SETTINGS_PATTERN_KEY);
        }
        [UnitOfWork]
        public void DeleteSetting(Setting setting)
        {
           
            _settingRepository.Delete(setting);

            //cache
            _cacheManager.RemoveByPattern(SETTINGS_PATTERN_KEY);

            //event notification
        }


        public Setting GetSetting(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            var settings = GetAllSettingsCached();
            key = key.Trim().ToLowerInvariant();
            if (settings.ContainsKey(key))
            {
                var setting = settings[key];

                if (setting != null)
                    return GetSettingById(setting.Id);
            }

            return null;
        }

        public T GetSettingByKey<T>(string key, T defaultValue = default(T))
        {
            if (string.IsNullOrEmpty(key))
                return defaultValue;

            var settings = GetAllSettingsCached();
            key = key.Trim().ToLowerInvariant();
            if (settings.ContainsKey(key))
            {
                var setting = settings[key];


                if (setting != null)
                    return setting.Value.CastTo<T>();
            }

            return defaultValue;
        }

        public void SetSetting<T>(string key, T value, bool clearCache = true)
        {
            //if (!_workContent.User.IsAdmin) throw new TtsException("只有系统管理员可以配置，你没有权限配置");
            if (key == null)
                throw new ArgumentNullException("key");
            key = key.Trim().ToLowerInvariant();
            string valueStr = CommonHelper.GetNopCustomTypeConverter(typeof(T)).ConvertToInvariantString(value);

            var allSettings = GetAllSettingsCached();
            var settingForCaching = allSettings.ContainsKey(key) ?
                allSettings[key] : null;
            if (settingForCaching != null)
            {
                //update
                var setting = GetSettingById(settingForCaching.Id);
                setting.Value = valueStr;
                UpdateSetting(setting, clearCache);
            }
            else
            {
                //insert
                var setting = new Setting
                {
                    Name = key,
                    Value = valueStr
                };
                InsertSetting(setting, clearCache);
            }
        }

         [UnitOfWork]
        public IList<Setting> GetAllSettings(string settingName = null)
        {
            var query = from s in _settingRepository.GetTable()
                        where string.IsNullOrEmpty(settingName) || s.Name.Contains(settingName)
                        //orderby s.Name
                        select s;
            query = query.OrderBy(x => x.Name);
            //if (!string.IsNullOrEmpty(settingName))
            //    query = query.Where(x => x.Name.Equals(settingName, StringComparison.OrdinalIgnoreCase));
            var settings = query.ToList();
            return settings;
        }

        public bool SettingExists<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
        {
            string key = settings.GetSettingKey(keySelector);

            var setting = GetSettingByKey<string>(key);
            return setting != null;
        }

        public T LoadSetting<T>() where T : ISettings, new()
        {
            var settings = Activator.CreateInstance<T>();

            foreach (var prop in typeof(T).GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var key = typeof(T).Name + "." + prop.Name;
                var setting = GetSettingByKey<string>(key);
                if (setting == null)
                    continue;

                if (!CommonHelper.GetNopCustomTypeConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                if (!CommonHelper.GetNopCustomTypeConverter(prop.PropertyType).IsValid(setting))
                    continue;

                object value = CommonHelper.GetNopCustomTypeConverter(prop.PropertyType).ConvertFromInvariantString(setting);

                //set property
                prop.SetValue(settings, value, null);
            }

            return settings;
        }

        public void SaveSetting<T>(T settings) where T : ISettings, new()
        {
            /* We do not clear cache after each setting update.
            * This behavior can increase performance because cached settings will not be cleared 
            * and loaded from database after each update */
            foreach (var prop in typeof(T).GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                if (!CommonHelper.GetNopCustomTypeConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                string key = typeof(T).Name + "." + prop.Name;
                //Duck typing is not supported in C#. That's why we're using dynamic type
                dynamic value = prop.GetValue(settings, null);
                if (value != null)
                    SetSetting(key, value, false);
                else
                    SetSetting(key, "", false);
            }

            //and now clear cache
            ClearCache();
        }

        public void SaveSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector, bool clearCache = true) where T : ISettings, new()
        {
            var member = keySelector.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    keySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                       "Expression '{0}' refers to a field, not a property.",
                       keySelector));
            }

            string key = settings.GetSettingKey(keySelector);
            //Duck typing is not supported in C#. That's why we're using dynamic type
            dynamic value = propInfo.GetValue(settings, null);
            if (value != null)
                SetSetting(key, value, clearCache);
            else
                SetSetting(key, "", clearCache);
        }

        public void DeleteSetting<T>() where T : ISettings, new()
        {
            var settingsToDelete = new List<Setting>();
            var allSettings = GetAllSettings();
            foreach (var prop in typeof(T).GetProperties())
            {
                string key = typeof(T).Name + "." + prop.Name;
                settingsToDelete.AddRange(allSettings.Where(x => x.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase)));
            }

            foreach (var setting in settingsToDelete)
                DeleteSetting(setting);
        }

        public void DeleteSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
        {
            string key = settings.GetSettingKey(keySelector);
            key = key.Trim().ToLowerInvariant();

            var allSettings = GetAllSettingsCached();
            var settingForCaching = allSettings.ContainsKey(key) ?
                allSettings[key] : null;
            if (settingForCaching != null)
            {
                //update
                var setting = GetSettingById(settingForCaching.Id);
                DeleteSetting(setting);
            }
        }

        public void ClearCache()
        {
            _cacheManager.RemoveByPattern(SETTINGS_PATTERN_KEY);
        }
    }
}