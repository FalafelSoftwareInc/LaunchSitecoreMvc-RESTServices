using LaunchSitecoreMvc.Api.Formatters;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LaunchSitecoreMvc.Api.Controllers.Abstracts
{
    public abstract class BaseJsonController : Controller
    {
        public BaseJsonController()
            : base()
        {

        }

        /// <summary>
        /// Uses JSON.NET for serializing, which correctly serializes Dates and other data
        /// </summary>
        protected JsonResult JsonNet(object data)
        {
            return new JsonNetResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// Creates a JSON result with the given data as its content.
        /// Override Json to allow GET request by default.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// A JsonResult.
        /// </returns>
        protected new JsonResult Json(object data)
        {
            return JsonNet(data);
        }

        /// <summary>
        /// Creates a JSON result with the given data as its content. Override Json to allow GET request
        /// by default.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// A JsonResult.
        /// </returns>
        protected JsonResult Json(object data, HttpStatusCode status)
        {
            // Update status code on response
            UpdateStatusCode(status);

            return Json(data);
        }

        /// <summary>
        /// Creates a JSON result with the given data as its content.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="errors">The errors.</param>
        /// <param name="status">(Optional) The status.</param>
        /// <returns>
        /// A JsonResult.
        /// </returns>
        protected JsonResult Json(IEnumerable<object> data, object errors, HttpStatusCode status = HttpStatusCode.OK)
        {
            // Set status automatically if error message exists
            if (errors != null && status == HttpStatusCode.OK)
            {
                status = HttpStatusCode.InternalServerError;
            }

            // Construct data for response
            return Json(new
            {
                Data = data,
                Errors = errors,
                Total = data.Count()
            }, status);
        }

        /// <summary>
        /// Creates a JSON result with the given data as its content.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="status">(Optional) The status.</param>
        /// <returns>
        /// A JsonResult.
        /// </returns>
        protected JsonResult Json(IEnumerable<object> data, HttpStatusCode status = HttpStatusCode.OK)
        {
            // Construct data for response
            return Json(data, null, status);
        }

        /// <summary>
        /// Updates the status code described by status.
        /// </summary>
        /// <param name="status">The status.</param>
        private void UpdateStatusCode(HttpStatusCode status)
        {
            if (status != HttpStatusCode.OK)
            {
                Response.StatusCode = (int)status;
            }
        }
    }
}
