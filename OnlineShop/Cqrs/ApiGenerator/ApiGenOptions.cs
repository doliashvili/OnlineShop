namespace Cqrs.ApiGenerator
{
    public class ApiGenOptions
    {
        public string ControllersPath { get; set; }
        public string ControllersNamespace { get; set; }

        public bool IgnoreGenerationIfControllerExists { get; set; }

        public bool IsFinished { get; set; }
    }
}