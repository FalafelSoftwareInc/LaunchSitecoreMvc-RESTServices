using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Web.Mvc;

namespace LaunchSitecoreMvc.Api.Formatters
{
    /// <summary>
    /// Encapsulates the result of a JSON net.
    /// </summary>
    public class JsonNetResult : JsonResult
    {
        public JsonSerializerSettings Settings { get; set; }
        public Formatting Formatting { get; set; }

        public JsonNetResult()
        {
            Settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            Formatting = Formatting.None;
            JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet
                && String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("JSON GET is not allowed");
            }

            var response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                var serializer = JsonSerializer.Create(Settings);
                var writer = new JsonTextWriter(response.Output)
                {
                    Formatting = Formatting
                };

                serializer.Serialize(writer, Data);
                writer.Flush();
            }
        }
    }
}
