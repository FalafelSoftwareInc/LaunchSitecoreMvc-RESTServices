using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System;
using System.Web;

namespace LaunchSitecoreMvc.Core.Application
{
    /// <summary>
    /// Base class for application start up.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public abstract class BaseStartup<T> : IHttpModule where T : IHttpModule
    {
        private static volatile bool _started = false;
        private static object _startLock = new object();

        /// <summary>
        /// Gets the <see cref="T:System.Type" /> of the current instance.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Type" /> instance that represents the exact runtime type of the
        /// current instance.
        /// </returns>
        public static new Type GetType()
        {
            return typeof(T);
        }

        /// <summary>
        /// Will register when the application is starting (same as Application_Start)
        /// Called by the assembly PreApplicationStartMethod attribute.
        /// </summary>
        public static void RegisterStartup()
        {
            // Register any modules to hook into app lifecycle
            // Registers itself so it can subscribe to more events
            DynamicModuleUtility.RegisterModule(GetType());
        }

        /// <summary>
        /// Inits the specified application. Triggered due to DynamicModuleUtility in PreInit.
        /// </summary>
        /// <param name="context">The application context that instantiated and will be running this module.</param>
        public void Init(HttpApplication context)
        {
            // Ensure OnStart gets called only once
            // http://erraticdev.blogspot.com/2011/01/how-to-correctly-use-ihttpmodule-to.html
            if (!_started)
            {
                lock (_startLock)
                {
                    if (!_started)
                    {
                        // This will run only once per application start
                        this.OnStart(context);
                        _started = true;
                    }
                }
            }

            // This will run on every HttpApplication initialization in the application pool
            this.OnInit(context);
        }

        /// <summary>Initializes any data/resources on application start. Only runs once!</summary>
        /// <param name="context">The application context that instantiated and will be running this module.</param>
        public virtual void OnStart(HttpApplication context)
        {

        }

        /// <summary>Initializes any data/resources on HTTP module start. Runs multiple times!</summary>
        /// <param name="context">The application context that instantiated and will be running this module.</param>
        public virtual void OnInit(HttpApplication context)
        {

        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements
        /// <see cref="T:System.Web.IHttpModule" />.
        /// </summary>
        public virtual void Dispose()
        {
        }
    }
}
