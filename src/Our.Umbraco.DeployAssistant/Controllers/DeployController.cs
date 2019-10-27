using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Our.Umbraco.DeployAssistant.Controllers
{
    [PluginController("DeployAssistant")]
    public class DeployController : UmbracoAuthorizedApiController
    {
        [HttpPost]
        public bool Import()
        {
            var deployPath = HttpContext.Current.Server.MapPath("~/data/deploy");
            File.WriteAllText(deployPath, "");

            return File.Exists(deployPath);
        }
        [HttpPost]
        public bool ExportUdas()
        {
            var deployPath = HttpContext.Current.Server.MapPath("~/data/deploy-export");
            File.WriteAllText(deployPath, "");

            return File.Exists(deployPath);
        }

        [HttpGet]
        public string GetSettings()
        {
            var settingsPath = HttpContext.Current.Server.MapPath("~/config/UmbracoDeploy.Settings.config");
            return (File.Exists(settingsPath) ? string.Join("<br/>", File.ReadAllLines(settingsPath).Select(x => HttpContext.Current.Server.HtmlEncode(x))) : "/config/UmbracoDeploy.Settings.config not found!");
        }
    }
}
