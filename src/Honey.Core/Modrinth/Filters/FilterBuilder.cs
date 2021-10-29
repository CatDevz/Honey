using System;
using System.Collections.Generic;
using System.Linq;

namespace Honey.Core.Modrinth.Filters
{
    public class FilterBuilder
    {
        private readonly List<string> _statements = new List<string>();
        private readonly List<SubStatement> _subStatements = new List<SubStatement>();

        public FilterBuilder Filter(string name, string value) => InternalFilter(string.Empty, name, value, And);
        public FilterBuilder FilterNot(string name, string value) => InternalFilter("NOT", name, value, AndNot);
        public FilterBuilder And(string name, string value) => InternalAddStatement("AND", name, value);
        public FilterBuilder Or(string name, string value) => InternalAddStatement("OR", name, value);
        public FilterBuilder AndNot(string name, string value) => InternalAddStatement("AND NOT", name, value);
        public FilterBuilder OrNot(string name, string value) => InternalAddStatement("OR NOT", name, value);
        public FilterBuilder And(FilterBuilder builder) => InternalAddSubStatement("AND", builder);
        public FilterBuilder AndNot(FilterBuilder builder) => InternalAddSubStatement("AND NOT", builder);
        public FilterBuilder Or(FilterBuilder builder) => InternalAddSubStatement("OR", builder);
        public FilterBuilder OrNot(FilterBuilder builder) => InternalAddSubStatement("OR NOT", builder);
        
        public string BuildStatement()
        {
            string initialStatement = string.Join(" ", _statements);

            var subStatementBuilder = _subStatements.Select(subStatement =>
                $"{subStatement.Statement} ({subStatement.Builder.BuildStatement()})").ToList();
            
            var subStatement = string.Join(" ", subStatementBuilder);
            return subStatement == string.Empty ? initialStatement : $"{initialStatement} {subStatement}";
        }
        
        private FilterBuilder InternalFilter(string operation,
            string name,
            string value,
            Func<string, string, FilterBuilder> nonEmptyStatementsFunc)
        {
            if (_statements.Count > 0)
            {
                return nonEmptyStatementsFunc(name, value);
            }

            string statement = string.IsNullOrEmpty(operation) ? $"{name}={value}" : $"{operation} {name}={value}";
            _statements.Add(statement);
            return this;
        }
        
        private FilterBuilder InternalAddStatement(string statement, string name, string value)
        {
            _statements.Add($"{statement} {name}={value}");
            return this;
        }

        private FilterBuilder InternalAddSubStatement(string statement, FilterBuilder builder)
        {
            _subStatements.Add(new SubStatement
            {
                Statement = statement,
                Builder = builder
            });
            return this;
        }
    }
}