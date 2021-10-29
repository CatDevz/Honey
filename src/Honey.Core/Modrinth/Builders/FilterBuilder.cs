using System.Collections.Generic;
using System.Linq;

namespace Honey.Core.Modrinth.Builders
{
    public class SubStatement
    {
        public string Statement { get; set; } = string.Empty;
        public FilterBuilder Builder { get; set; } = new FilterBuilder();
    }
    
    public class FilterBuilder
    {
        private readonly List<string> _statements = new List<string>();
        private readonly List<SubStatement> _subStatements = new List<SubStatement>();
        public FilterBuilder Filter(string name, string value)
        {
            if (_statements.Count > 0)
            {
                return And(name, value);
            }

            _statements.Add($"{name}={value}");
            return this;
        }
        
        public FilterBuilder FilterNot(string name, string value)
        {
            if (_statements.Count > 0)
            {
                return AndNot(name, value);
            }

            _statements.Add($"NOT {name}={value}");
            return this;
        }

        public FilterBuilder And(string name, string value)
        {
            _statements.Add($"AND {name}={value}");
            return this;
        }

        public FilterBuilder Or(string name, string value)
        {
            _statements.Add($"OR {name}={value}");
            return this;
        }

        public FilterBuilder AndNot(string name, string value)
        {
            _statements.Add($"AND NOT {name}={value}");
            return this;
        }
        
        public FilterBuilder OrNot(string name, string value)
        {
            _statements.Add($"OR NOT {name}={value}");
            return this;
        }

        public FilterBuilder And(FilterBuilder builder)
        {
            _subStatements.Add(new SubStatement
            {
                Statement = "AND",
                Builder = builder
            });
            return this;
        }
        
        public FilterBuilder AndNot(FilterBuilder builder)
        {
            _subStatements.Add(new SubStatement
            {
                Statement = "AND NOT",
                Builder = builder
            });
            return this;
        }

        public FilterBuilder Or(FilterBuilder builder)
        {
            _subStatements.Add(new SubStatement
            {
                Statement = "OR",
                Builder = builder
            });
            return this;
        }
        
        public FilterBuilder OrNot(FilterBuilder builder)
        {
            _subStatements.Add(new SubStatement
            {
                Statement = "OR",
                Builder = builder
            });
            return this;
        }

        public string BuildStatement()
        {
            string initialStatement = string.Join(" ", _statements);
            
            var subStatementBuilder = _subStatements.Select(subStatement => 
                $"{subStatement.Statement} ({subStatement.Builder.BuildStatement()})")
                .ToList() ?? new List<string>();
            
            var subStatement = string.Join(" ", subStatementBuilder);
            return subStatement == string.Empty ? initialStatement : $"{initialStatement} {subStatement}";
        }
    }
}