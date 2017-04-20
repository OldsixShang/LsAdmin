using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Example.Areas.SystemManage
{
    public class SystemManageRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "SystemManage"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("SystemManage_default"
                , "SystemManage/{controller}/{action}/{id}"
                , new {action="Index",id=UrlParameter.Optional });
        }
    }
}