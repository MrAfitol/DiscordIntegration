using PluginAPI.Core.Attributes;
using PluginAPI.Enums;

namespace DiscordIntegration.Events;

public class VeryHelpful
{
    private readonly DiscordIntegration Plugin;
    public VeryHelpful(DiscordIntegration plugin)
    {
        Plugin = plugin;
    }
    
    public static ushort GeneratorCount = 0;
    
    [PluginEvent(ServerEventType.MapGenerated)]
    void OnGenerateMap()
    {
        GeneratorCount = 0;
    }
}