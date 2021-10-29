using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Honey.Core.Modrinth.Models
{
    /// <summary>
    /// A minecraft mod
    /// </summary>
    /// <param name="Id">The ID of the mod, encoded as a base62 string</param>
    /// <param name="Slug">The slug of a mod, used for vanity URLs</param>
    /// <param name="Team">The id of the team that has ownership of this mod</param>
    /// <param name="Title">The title or name of the mod</param>
    /// <param name="Description">A short description of the mod</param>
    /// <param name="Body">A long form description of the mod</param>
    /// <param name="Published">The date at which the mod was first published</param>
    /// <param name="Updated">The date at which the mod was updated</param>
    /// <param name="Status">The status of the mod - approved, rejected, draft, unlisted, processing, or unknown</param>
    /// <param name="License">The license of the mod</param>
    /// <param name="ClientSide">The support range for the client mod - required, optional, unsupported, or unknown</param>
    /// <param name="ServerSide">The support range for the server mod - required, optional, unsupported, or unknown</param>
    /// <param name="Downloads">The total number of downloads the mod has</param>
    /// <param name="Categories">A list of the categories that the mod is in</param>
    /// <param name="Versions">A list of ids for versions of the mod</param>
    /// <param name="IconUrl">The URL of the icon of the mod</param>
    /// <param name="IssuesUrl">An optional link to where to submit bugs or issues with the mod</param>
    /// <param name="SourceUrl">An optional link to the source code for the mod</param>
    /// <param name="WikiUrl">An optional link to the mod's wiki page or other relevant information</param>
    /// <param name="DiscordUrl">An optional link to the mod's discord</param>
    /// <param name="DonationUrls">An optional list of all donation links the mod has</param>
    public record Mod(
        string Id,
        string Slug,
        string Team,
        string Title,
        string Description,
        string Body,
        DateTime Published,
        DateTime Updated,
        ModStatus Status,
        License License,
        SupportRange ClientSide,
        SupportRange ServerSide,
        int Downloads,
        List<string> Categories,
        List<string> Versions,
        string? IconUrl,
        string? IssuesUrl,
        string? SourceUrl,
        string? WikiUrl,
        string? DiscordUrl,
        List<DonationLink>? DonationUrls
    );
}