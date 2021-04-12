using System;

namespace Cqrs.ApiGenerator
{
    /// <summary>
    /// Ignore code generation for selected action
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ApiGenIgnoreAttribute : Attribute
    {
    }
}