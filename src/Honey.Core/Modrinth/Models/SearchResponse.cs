using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Honey.Core.Modrinth.Models
{
    public record SearchResponse(List<ModResult> Hits, int Offset, int Limit, int TotalHits);
}