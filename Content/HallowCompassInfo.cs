using System.Collections.Generic;
using Terraria;

namespace whereThat1percentAt.Content
{
    public class HallowCompassInfo : BaseModCompassInfo
    {
        public override string Texture => Textures.hallowCompassInfo;

        public override List<int> Tiles => Lists.Hallow;
        public override string TileName => "h";

        public override bool Active()
        {
            return Main.LocalPlayer.GetModPlayer<CustomPlayer>().showHallowCompass;
        }
    }
}
