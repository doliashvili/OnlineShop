using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cqrs.ApiGenerator.SrcGen
{
    internal class GeneratedMethod
    {
        public HashSet<string> Attributes { get; set; }
        public bool IsAsync { get; set; }
        public string ReturnType { get; set; }
        public string Name { get; set; }
        public List<string> Parameters { get; set; }
        public List<string> BodyLines { get; set; }
        public string ReturnValue { get; set; }

        
        private StringBuilder _stringBuilder;
        public GeneratedMethod() => _stringBuilder = new StringBuilder();

        public override string ToString()
        {
            _stringBuilder.Clear();

            _stringBuilder.Append("\n\n");
            if (Attributes?.FirstOrDefault() != null)
            {
                foreach (var attribute in Attributes) 
                    _stringBuilder.Append("\t\t" + attribute + "\n");
            }

            _stringBuilder.Append("\t\tpublic ");
            if (IsAsync) _stringBuilder.Append("async ");

            _stringBuilder.Append($"{ReturnType} ");

            _stringBuilder.Append(Name + "(");

            if (Parameters?.FirstOrDefault() != null) 
                _stringBuilder.Append(string.Join(", ", Parameters));

            _stringBuilder.Append(") \n\t\t{");
            
            BodyLines.ForEach(b => _stringBuilder.Append("\n\t\t\t" + b));

            if (!string.IsNullOrWhiteSpace(ReturnValue)) 
                _stringBuilder.Append("\n\t\t\t" + ReturnValue);

            _stringBuilder.Append("\n\t\t}");
            return _stringBuilder.ToString();
        }
    }
}