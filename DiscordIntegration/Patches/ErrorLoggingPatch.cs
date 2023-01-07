// -----------------------------------------------------------------------
// <copyright file="ErrorLoggingPatch.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

using PluginAPI.Core;

namespace DiscordIntegration.Patches
{
#pragma warning disable SA1118

    using System.Collections.Generic;
    using System.Reflection.Emit;
    using global::DiscordIntegration.Dependency;

    using HarmonyLib;

    using NorthwoodLib.Pools;

    using static HarmonyLib.AccessTools;

    [HarmonyPatch(typeof(Log), nameof(Log.Error))]
    public class LogErrorPatch
    {
        // I dont know transpilers so... yeah im bad in harmony 
        public static void PostFix(string message, string prefix = null)
        {
            if (DiscordIntegration.Instance.Config.LogErrors)
                _ = DiscordIntegration.Network.SendAsync(new RemoteCommand(ActionType.Log, ChannelType.Errors, message));
        }
    }
    
    /*internal class ErrorLoggingPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            int offset = -2;
            int index = newInstructions.FindLastIndex(i => i.opcode == OpCodes.Call) + offset;
            Label nullLabel = generator.DefineLabel();
            Label continueLabel = generator.DefineLabel();
            LocalBuilder message = generator.DeclareLocal(typeof(object));

            newInstructions.InsertRange(index, new CodeInstruction[]
            {
                new (OpCodes.Dup),
                new (OpCodes.Call, Method(typeof(ErrorLoggingPatch), nameof(LogError))),
            });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }

        private static void LogError(object message)
        {
            if (DiscordIntegration.Instance.Config.LogErrors)
                _ = DiscordIntegration.Network.SendAsync(new RemoteCommand(ActionType.Log, ChannelType.Errors, message));
        }
    }*/
}