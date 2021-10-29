using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Honey.Core.Modrinth.Models
{
    /// <summary>
    /// The mod lookup response.
    /// </summary>
    /// <param name="Hits">The list of results</param>
    /// <param name="Offset">The number of results that were skipped by the query</param>
    /// <param name="Limit">The number of mods returned by the query</param>
    /// <param name="TotalHits">The total number of mods that the query found</param>
    public record SearchResponse(List<ModResult> Hits, int Offset, int Limit, int TotalHits);
}