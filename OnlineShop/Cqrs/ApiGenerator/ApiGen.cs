using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Cqrs.ApiGenerator.SrcGen;
using Helpers.TypeExtensions;

namespace Cqrs.ApiGenerator
{
    internal class ApiGen
    {
        private readonly ApiGenOptions _options;
        private readonly List<GeneratedClassModel> _controllerGenerated;
        private readonly StringBuilder _stringBuilder;

        private const string HandlerMethodName = "HandleAsync";

        public ApiGen(ApiGenOptions options)
        {
            _options = options;
            _stringBuilder = new();
            _controllerGenerated = new();
        }

        public void RegisterCommandEndpoints(List<Type> handlers)
        {
            if(handlers == null)
                return;
            
            foreach (var handler in handlers.Where(h => h.GetCustomAttribute<ApiGenAttribute>() != null))
            {
                var temp = handler.GetMethods()
                    .Where(x => x.IsPublic && x.Name == HandlerMethodName).ToList();
                
                if(temp.Count == 0)
                    continue;
                
                var handlerAttr = handler.GetCustomAttribute<ApiGenAttribute>();
                
                var controllerName = handlerAttr?.ControllerName ?? MapHandlerNameToControllerName(handler.Name);
                var generatedControllerIndex = AddControllerIfNotExist(controllerName, handlerAttr, handler);
                
                foreach (var methodInfo in temp)
                {
                    if (methodInfo.GetCustomAttribute<ApiGenIgnoreAttribute>() != null)
                        continue;

                    var commandType = methodInfo.GetParameters()[0].ParameterType;
                    var attr = methodInfo.GetCustomAttribute<ApiGenActionAttribute>() ?? new ApiGenActionAttribute();

                    var originalName = commandType.Name.EndsWith("Command")
                        ? commandType.Name.Substring(0,
                            commandType.Name.Length - 7)
                        : commandType.Name;
                    
                    var actionName = attr.ActionName ?? originalName;
                    var httpMethod = attr.Method == default ? ApiMethods.Post : attr.Method;

                    var method = new GeneratedMethod
                    {
                        Attributes = new HashSet<string>() {$"[Http{httpMethod.ToString()}(\"{actionName}\")]"},
                        IsAsync = true,
                        ReturnType = "Task<IActionResult>",
                        Name = originalName,
                        Parameters = new List<string>() {$"[FromBody] {commandType.FullName} command"},
                        BodyLines = new List<string>() { "await Mediator.SendAsync(command);" },
                        ReturnValue = "return Ok();"
                    };
                    _controllerGenerated[generatedControllerIndex].Methods.Add(method);
                }
            }
        }


        public void RegisterQueryEndpoints(List<Type> handlers)
        {
            foreach (var handler in handlers)
            {
                var temp = handler.GetMethods()
                    .Where(x => x.IsPublic && x.Name == HandlerMethodName).ToList();
                
                if(temp.Count == 0)
                    continue;
                
                var handlerAttr = handler.GetCustomAttribute<ApiGenAttribute>();
                var controllerName = handlerAttr?.ControllerName ?? MapHandlerNameToControllerName(handler.Name);

                var generatedControllerIndex = AddControllerIfNotExist(controllerName, handlerAttr, handler);

                foreach (var methodInfo in temp)
                {
                    if (methodInfo.GetCustomAttribute<ApiGenIgnoreAttribute>() != null)
                        continue;

                    var queryType = methodInfo.GetParameters()[0].ParameterType;
                    var attr = methodInfo.GetCustomAttribute<ApiGenActionAttribute>() ?? new ApiGenActionAttribute();

                    var originalName = queryType.Name.EndsWith("Query")
                        ? queryType.Name.Substring(0,
                            queryType.Name.Length - 5)
                        : queryType.Name;
                    
                    var actionName = attr.ActionName ?? originalName;
                    
                    var method = new GeneratedMethod
                    {
                        Attributes = new HashSet<string>()
                        {
                            $"[HttpGet(\"{actionName}\")]",
                            $"[ProducesResponseType(typeof({methodInfo.ReturnType.GetGenericArguments()[0].GetFriendlyFullNameForType()}), 200)]"
                        },
                        IsAsync = true,
                        ReturnType = "Task<IActionResult>",
                        Name = originalName,
                        Parameters = new List<string>() {$"[FromQuery] {queryType.FullName} query"},
                        BodyLines = new List<string>() { "var data = await Mediator.QueryAsync(query);" },
                        ReturnValue = "return Ok(data);"
                    };
                    _controllerGenerated[generatedControllerIndex].Methods.Add(method);
                }
            }
        }

        private int AddControllerIfNotExist(string controllerName, ApiGenAttribute handlerAttr, Type handler)
        {
            var generatedControllerIndex = _controllerGenerated.FindIndex(x =>
                x.ClassName == controllerName && x.ClassNamespace == _options.ControllersNamespace);

            if (generatedControllerIndex == -1)
            {
                var generatedClass = new GeneratedClassModel
                {
                    ClassName = controllerName,
                    Namespaces = new()
                    {
                        "using Microsoft.AspNetCore.Mvc;",
                        "using System.Threading.Tasks;",
                        "using ApiCommon.BaseControllers;"
                    },
                    ClassNamespace = _options.ControllersNamespace,
                    IsPartial = false,
                    Parents = new() { "BaseApiController" },
                    Methods = new(),
                };


                var route = handlerAttr.ControllerRoute ??
                            MapHandlerNameToControllerName(handler.Name).Replace("Controller", "");
                generatedClass.Attributes = new();
                if (!handlerAttr.AllowVersioning)
                    generatedClass.Attributes.Add($"[Route(\"{route}\")]");
                else
                    generatedClass.Attributes.Add($"[Route(\"v{handlerAttr.Version}/{route}\")]");

                _controllerGenerated.Add(generatedClass);
                generatedControllerIndex = _controllerGenerated.IndexOf(generatedClass);
            }

            return generatedControllerIndex;
        }


        public void GenerateControllerFiles()
        {
            foreach (var controller in _controllerGenerated)
            {
                var fullPath = Path.Combine(_options.ControllersPath, controller.ClassName + ".cs" );
                if (File.Exists(fullPath))
                {
                    if (_options.IgnoreGenerationIfControllerExists)
                        continue;
                    File.Delete(fullPath);
                }

                using var streamWriter = new StreamWriter(fullPath);
                streamWriter.Write(controller.ToString());
                streamWriter.Flush();
            }
            
            _controllerGenerated?.Clear();
        }
        
        
        private string MapHandlerNameToControllerName(string handlerName)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(handlerName[0]);
            foreach (var c in handlerName.ToCharArray(1, handlerName.Length - 1))
            {
                if (!char.IsUpper(c))
                {
                    _stringBuilder.Append(c);
                }
                else
                {
                    break;
                }
            }

            _stringBuilder.Append("Controller");
            return _stringBuilder.ToString();
        }
    }
}