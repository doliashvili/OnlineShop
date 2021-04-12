using System;
using System.Collections.Generic;

namespace Cqrs.ApiGenerator
{
    /// <summary>
    /// specific settings for attributed action
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ApiGenActionAttribute : Attribute
    {
        /// <summary>
        /// Default: POST. Only affects for Commands, Queries always are GET
        /// </summary>
        public ApiMethods Method { get; }
        
        /// <summary>
        /// Default: name of Command/Query without Command/Query
        /// </summary>
        public string ActionName { get; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="method"></param>
        /// <param name="actionName"></param>
        public ApiGenActionAttribute(ApiMethods method = default, string actionName = default)
        {
            Method = method;
            ActionName = actionName;
        }
    }
}