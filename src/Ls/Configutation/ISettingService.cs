using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ls.Configutation
{
    /// <summary>
    /// Setting service interface
    /// </summary>
    public partial interface ISettingService
    {
        /// <summary>
        /// 获取setting
        /// </summary>
        /// <param name="settingId">Setting identifier</param>
        /// <returns>Setting</returns>
        Setting GetSettingById(long settingId);

        /// <summary>
        /// 删除setting
        /// </summary>
        /// <param name="settingId">settingId</param>
        void DeleteSetting(long settingId);

        /// <summary>
        /// 删除setting
        /// </summary>
        /// <param name="setting">settingId</param>
        void DeleteSetting(Setting setting);

        /// <summary>
        /// 通过一个key获取 Setting
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Setting</returns>
        Setting GetSetting(string key);

        /// <summary>
        /// 通过一个key获取 Setting
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Setting value</returns>
        T GetSettingByKey<T>(string key, T defaultValue = default(T));

        /// <summary>
        ///设置一个Setting值
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        void SetSetting<T>(string key, T value, bool clearCache = true);

       /// <summary>
        /// 获取所有的Setting
       /// </summary>
       /// <param name="page"></param>
       /// <param name="size"></param>
       /// <param name="settingName"></param>
       /// <returns></returns>

        IList<Setting> GetAllSettings( string settingName = null);

        /// <summary>
        /// 判断一个Setting是否存在
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="settings">Settings</param>
        /// <param name="keySelector">Key selector</param>
        /// <returns>true -setting exists; false - does not exist</returns>
        bool SettingExists<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector)
            where T : ISettings, new();

        /// <summary>
        /// 加载一个类型的Setting
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        T LoadSetting<T>() where T : ISettings, new();

        /// <summary>
        /// 保存一个Setting
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="storeId">Store identifier</param>
        /// <param name="settings">Setting instance</param>
        void SaveSetting<T>(T settings) where T : ISettings, new();

        /// <summary>
        /// 保存一个Setting对象
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="settings">Settings</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        void SaveSetting<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector, bool clearCache = true) where T : ISettings, new();

        /// <summary>
        /// Delete all settings
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        void DeleteSetting<T>() where T : ISettings, new();

        /// <summary>
        /// Delete settings object
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="settings">Settings</param>
        /// <param name="keySelector">Key selector</param>
        void DeleteSetting<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector) where T : ISettings, new();

        /// <summary>
        /// Clear cache
        /// </summary>
        void ClearCache();
    }
}