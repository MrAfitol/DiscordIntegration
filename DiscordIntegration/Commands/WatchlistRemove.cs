// -----------------------------------------------------------------------
// <copyright file="PlayerList.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

using DiscordIntegration.Dependency.Database;
using NWAPIPermissionSystem;
using PluginAPI.Core;

namespace DiscordIntegration.Commands
{
    using System;
    using CommandSystem;
    using static DiscordIntegration;

    /// <summary>
    /// Removes a user from the watchlist.
    /// </summary>
    internal sealed class WatchlistRemove : ICommand
    {
        public static WatchlistRemove Instance { get; } = new WatchlistRemove();

        public string Command { get; } = "watchrem";

        public string[] Aliases { get; } = new[] { "wlr" };

        public string Description { get; } = Language.WatchlistRemoveDescription;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("di.watchlistremove"))
            {
                response = string.Format(Language.NotEnoughPermissions, "di.watchlistremove");
                return false;
            }

            Player player = Player.Get(arguments.ElementAt(2));
            DatabaseHandler.RemoveEntry(player.UserId);
            response = $"{player.Nickname} removed from watchlist.";
            return true;
        }
    }
}