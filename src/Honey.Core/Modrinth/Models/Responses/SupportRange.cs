namespace Honey.Core.Modrinth.Models
{
    /// <summary>
    /// The support range for the client/server mod - approved, rejected, draft, unlisted, processing, or unknown
    /// </summary>
    public enum SupportRange
    {
        Required,
        Optional,
        Unsupported,
        Unknown
    }
}