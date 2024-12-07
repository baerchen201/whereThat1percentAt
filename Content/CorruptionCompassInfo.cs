using System.Collections.Generic;
using Terraria;

namespace whereThat1percentAt.Content
{
    public class CorruptionCompassInfo : BaseModCompassInfo
    {
        public override string Texture => Textures.corruptionCompassInfo;

        public override List<int> Tiles => Lists.Corruption;
        public override string TileName => "co";

        public override bool Active()
        {
            return Main.LocalPlayer.GetModPlayer<CustomPlayer>().showCorruptionCompass;
        }
    }
}
