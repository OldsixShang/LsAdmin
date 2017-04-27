using System.Web;
using System.Web.Optimization;

namespace Example.Mvc
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //js 基础组件
            bundles.Add(new ScriptBundle("~/bundles/baseComponents")
                .Include(
                "~/Scripts/jquery-1.10.2.min.js",
                "~/Scripts/easyUI/jquery.easyui.min.js",
                "~/Scripts/easyUI/easyui-lang-zh_CN.js",
                "~/Scripts/json2.js",
                "~/Components/artDialog/js/dialog-plus-min.js"));
            //ls相关控件
            bundles.Add(new ScriptBundle("~/bundles/ls")
                .Include(
                "~/Scripts/ls/jquery.ls.extension.js",
                "~/Scripts/ls/ls.common.js",
                "~/Scripts/ls/ls.form.js",
                "~/Scripts/ls/ls.controls.js"));
            //css 基础组件
            bundles.Add(new StyleBundle("~/bundles/baseComponentsStyle")
                .Include(
                "~/Content/bootstrap.css",
                "~/Content/easyui/themes/material/easyui.css",
                "~/Content/font-awesome/css/font-awesome.css",
                "~/Components/artDialog/css/ui-dialog.css"));
        }
    }
}
