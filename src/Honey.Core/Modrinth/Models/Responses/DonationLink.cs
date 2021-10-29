namespace Honey.Core.Modrinth.Models
{
    /// <summary>
    /// The donation link of a mod, representing the platform id, platform name, and URL.
    /// </summary>
    /// <param name="Id">The platform id of a mod, retrieved from the donation platforms get route</param>
    /// <param name="Platform">The long for name of a platform</param>
    /// <param name="Url">The URL to this donation link</param>
    public record DonationLink(
        string Id,
        string Platform,
        string Url
    );
}