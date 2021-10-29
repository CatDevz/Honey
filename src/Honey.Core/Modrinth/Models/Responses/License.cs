namespace Honey.Core.Modrinth.Models
{
    /// <summary>
    /// The license of a mod, representing the short id, long form name, and URL.
    /// </summary>
    /// <param name="Id">license id of a mod, retrieved from the licenses get route</param>
    /// <param name="Name">The long form name of a license</param>
    /// <param name="Url">The URL to this license</param>
    public record License(
        string Id,
        string Name,
        string Url
    );
}