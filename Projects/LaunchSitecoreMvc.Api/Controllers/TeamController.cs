using LaunchSitecoreMvc.Api.Controllers.Abstracts;
using LaunchSitecoreMvc.Api.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LaunchSitecoreMvc.Api.Controllers
{
    [RoutePrefix("api/team")]
    public class TeamController : BaseController
    {
        [Route("members")]
        public JsonResult GetMembers()
        {
            var items = Sitecore.Context.Database
                .GetItem("/sitecore/content/Home/Team")
                .Children
                .Select(x => new TeamMember(x));

            return Json(items);
        }

        [Route("members/{id}")]
        public JsonResult GetMember(string id)
        {
            var item = new TeamMember(Sitecore.Context.Database
                .GetItem(Sitecore.Data.ID.Parse(id)));

            return Json(item);
        }
    }
}
