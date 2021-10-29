using System;
using System.Collections.Generic;

namespace Honey.Core.Modrinth.Models
{
    /// <summary>
    /// The mod result
    /// </summary>
    /// <param name="ModId">The id of the mod; prefixed with 'local-'</param>
    /// <param name="ProjectType">The project type of the mod</param>
    /// <param name="Author">The username of the author of the mod</param>
    /// <param name="Title">The name of the mod</param>
    /// <param name="Description">A short description of the mod</param>
    /// <param name="Categories">A list of the categories the mod is in</param>
    /// <param name="Versions">A list of the minecraft versions supported by the mod</param>
    /// <param name="Downloads">The total number of downloads for the mod</param>
    /// <param name="PageUrl">A link to the mod's main page</param>
    /// <param name="IconUrl">The url of the mod's icon</param>
    /// <param name="AuthorUrl">The url of the mod's author</param>
    /// <param name="DateCreated">The date that the mod was originally created</param>
    /// <param name="DateModified">The date that the mod was last modified</param>
    /// <param name="LatestVersion">The latest version of minecraft that this mod supports</param>
    /// <param name="License">The id of the license this mod follows</param>
    /// <param name="ClientSide">The side type id that this mod is on the client</param>
    /// <param name="ServerSide">The side type id that this mod is on the server</param>
    /// <param name="Host">The host that this mod is from, always modrinth</param>
    public record ModResult(string ModId,
        string ProjectType,
        string Author,
        string Title,
        string Description,
        List<string> Categories,
        List<string> Versions,
        int Downloads,
        string PageUrl,
        string IconUrl,
        string AuthorUrl,
        DateTime DateCreated,
        DateTime DateModified,
        string LatestVersion,
        string License,
        string ClientSide,
        string ServerSide,
        string Host
    );
}