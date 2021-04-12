using System;
using System.Collections.Generic;

namespace Cqrs.ApiGenerator
{
    /// <summary>
    /// API generator controller level settings
    /// </summary>
    public class ApiGenAttribute : Attribute
    {
        /// <summary>
        /// Controller, Default: name of handler
        /// </summary>
        public string ControllerName { get; }

        /// <summary>
        /// Default: name of controller
        /// </summary>
        public string ControllerRoute { get; }
        
        /// <summary>
        /// Api version Default: 1
        /// </summary>
        public int Version { get; }
        
        /// <summary>
        /// Add versioning to the path
        /// </summary>
        public bool AllowVersioning { get; }
        
        public ApiGenAttribute(string controllerName = default, string controllerRoute = default, int version = 1, bool allowVersioning = true)
        {
            if (controllerName != null && !controllerName.EndsWith("Controller"))
                controllerName = controllerName + "Controller";
            
            ControllerName = controllerName;
            ControllerRoute = controllerRoute;
            Version = version < 1 ? 1 : version;
            AllowVersioning = allowVersioning;
        }
    }
}