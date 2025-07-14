using System.ComponentModel;
using Terraria.ModLoader.Config;

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

namespace whereThat1percentAt.Content;

class CustomConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    [DefaultValue(true)]
    [ReloadRequired]
    public bool randomTp;

    [DefaultValue(120)]
    [Range(5, 600)]
    public int percentageUpdateInterval;

    [DefaultValue(true)]
    public bool updateOnDisplay;
}
