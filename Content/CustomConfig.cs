using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace whereThat1percentAt.Content
{
    class CustomConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool randomTp;

        [DefaultValue(120)]
        [Range(5, 600)]
        public int percentageUpdateInterval;
    }
}
