using Honey.Core.Modrinth.Enums;
using Honey.Core.Modrinth.Filters;

namespace Honey.Core.Modrinth.Models
{
    public class SearchOptions
    {
        /// <summary>
        /// The query to search for.
        /// </summary>
        public string Query { get; set; } = "";
        
        /// <summary>
        /// A list of filters relating to the categories of a mod
        /// </summary>
        public FilterBuilder Filters { get; set; } = new FilterBuilder();
        
        /// <summary>
        /// What the results are sorted by.
        /// </summary>
        public SortBy SortBy { get; set; } = SortBy.Relevance;

        /// <summary>
        /// The offset into the search; skips this number of results
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// The number of mods returned by the search
        /// </summary>
        public int Limit { get; set; } = 10;
    }
}