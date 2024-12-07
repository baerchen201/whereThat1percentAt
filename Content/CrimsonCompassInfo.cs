using System.Collections.Generic;
using Terraria;

namespace whereThat1percentAt.Content
{
    public class CrimsonCompassInfo : BaseModCompassInfo
    {
        public override string Texture => Textures.crimsonCompassInfo;

        public override List<int> Tiles => Lists.Crimson;
        public override string TileName => "cr";

        public override bool Active()
        {
            return Main.LocalPlayer.GetModPlayer<CustomPlayer>().showCrimsonCompass;
        }
    }
}
