using Terraria;
using Terraria.ID;
using whereThat1percentAt.Content;

namespace whereThat1percentAt.Items
{
    public class crimsonCompass : baseModCompass
    {
        public override string Texture => Textures.crimsonCompass;
        public override short CompassItem => ItemID.Vertebrae;

        public override void UpdateInfoAccessory(Player player)
        {
            player.GetModPlayer<CustomPlayer>().showCrimsonCompass = true;
        }
    }
}
