using System;
using System.Collections.Generic;
using System.Linq;

namespace Honey.Core.Modrinth.Filters
{
    /// <summary>
    /// This class allows consumers to build a filtered query for the Modrinth API
    /// </summary>
    public class FilterBuilder
    {
        private readonly List<string> _statements = new List<string>();

        /// <summary>
        /// Creates a key-value-pair filter [name = value]
        /// If a filter statement already exists, this will append a <see cref="And(string,string)"/> filter
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="value">The property value</param>
        /// <returns>The modified <see cref="FilterBuilder"/></returns>
        public FilterBuilder Filter(string name, string value) => InternalFilter(string.Empty, name, value, And);
        
        /// <summary>
        /// Creates a logical NOT statement [NOT name = value]
        /// If a filter statement already exists, this will append a <see cref="AndNot(string,string)"/> filter
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="value">The property value</param>
        /// <returns>The modified <see cref="FilterBuilder"/></returns>
        public FilterBuilder FilterNot(string name, string value) => InternalFilter("NOT", name, value, AndNot);
        
        /// <summary>
        /// Appends a logical AND statement [... AND name = value]
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="value">The property value</param>
        /// <returns>The modified <see cref="FilterBuilder"/></returns>
        public FilterBuilder And(string name, string value) => InternalAddStatement("AND", name, value);
        
        /// <summary>
        /// Prepends a logical AND statement to a new sub-statement [... AND (statement)]
        /// </summary>
        /// <param name="builder">The sub-statement <see cref="FilterBuilder"/></param>
        /// <returns>The modified <see cref="FilterBuilder"/></returns>
        public FilterBuilder And(FilterBuilder builder) => InternalAddSubStatement("AND", builder);

        /// <summary>
        /// Appends a logical OR statement [... OR name = value]
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="value">The property value</param>
        /// <returns>The modified <see cref="FilterBuilder"/></returns>
        public FilterBuilder Or(string name, string value) => InternalAddStatement("OR", name, value);
        
        /// <summary>
        /// Prepends a logical OR statement to a new sub-statement [... OR (statement)]
        /// </summary>
        /// <param name="builder">The sub-statement <see cref="FilterBuilder"/></param>
        /// <returns>The modified <see cref="FilterBuilder"/></returns>
        public FilterBuilder Or(FilterBuilder builder) => InternalAddSubStatement("OR", builder);
        
        /// <summary>
        /// Appends a logical AND NOT statement [... AND NOT name = value]
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="value">The property value</param>
        /// <returns>The modified <see cref="FilterBuilder"/></returns>
        public FilterBuilder AndNot(string name, string value) => InternalAddStatement("AND NOT", name, value);
        
        /// <summary>
        /// Prepends a logical AND NOT statement to a new sub-statement [... AND NOT (statement)]
        /// </summary>
        /// <param name="builder">The sub-statement <see cref="FilterBuilder"/></param>
        /// <returns>The modified <see cref="FilterBuilder"/></returns>
        public FilterBuilder AndNot(FilterBuilder builder) => InternalAddSubStatement("AND NOT", builder);

        /// <summary>
        /// Appends a logical OR NOT statement [... OR NOT name = value]
        /// </summary>
        /// <param name="name">The property name</param>
        /// <param name="value">The property value</param>
        /// <returns>The modified <see cref="FilterBuilder"/></returns>
        public FilterBuilder OrNot(string name, string value) => InternalAddStatement("OR NOT", name, value);
        
        /// <summary>
        /// Prepends a logical OR NOT statement to a new sub-statement [... AND (statement)]
        /// </summary>
        /// <param name="builder">The sub-statement <see cref="FilterBuilder"/></param>
        /// <returns>The modified <see cref="FilterBuilder"/></returns>
        public FilterBuilder OrNot(FilterBuilder builder) => InternalAddSubStatement("OR NOT", builder);
        
        internal string BuildStatement()
        {
            return string.Join(" ", _statements);
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
            _statements.Add($"{statement} ({builder.BuildStatement()})");
            return this;
        }
    }
}