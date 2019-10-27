using Ecreo.DeployAssistant.Models;
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

namespace Ecreo.DeployAssistant.Controllers
{
    [PluginController("DeployAssistant")]
    public class StatusController : UmbracoAuthorizedApiController
    {
        [HttpGet]
        public Status Get()
        {
            var status = new Status();

            var dataPath = HttpContext.Current.Server.MapPath("~/data/");

            var deployCompleteFilePath = dataPath + "deploy-complete";
            var deployProgressFilePath = dataPath + "deploy-progress";
            var deployFailedFilePath = dataPath + "deploy-failed";
            var deployExportFilePath = dataPath + "deploy-export";

            status.Complete = File.Exists(deployCompleteFilePath);
            status.InProgress = File.Exists(deployProgressFilePath);
            status.Failed = File.Exists(deployFailedFilePath);
            status.Export = File.Exists(deployExportFilePath);

            var lastWriteTime = DateTime.MinValue;
            var currentLastWriteTime = DateTime.MinValue;

            if (status.Complete)
            {
                currentLastWriteTime = File.GetLastWriteTime(deployCompleteFilePath);

                if (currentLastWriteTime > lastWriteTime)
                {
                    lastWriteTime = currentLastWriteTime;
                }
            }
            if (status.InProgress)
            {
                currentLastWriteTime = File.GetLastWriteTime(deployProgressFilePath);

                if (currentLastWriteTime > lastWriteTime)
                {
                    lastWriteTime = currentLastWriteTime;
                }
            }
            if (status.Failed)
            {
                currentLastWriteTime = File.GetLastWriteTime(deployFailedFilePath);

                if (currentLastWriteTime > lastWriteTime)
                {
                    lastWriteTime = currentLastWriteTime;
                }

                status.Content = File.ReadAllText(deployFailedFilePath);
            }

            status.LastEdit = lastWriteTime;

            return status;
        }
    }
}
