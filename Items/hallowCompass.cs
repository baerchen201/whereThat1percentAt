using Terraria;
using Terraria.ID;
using whereThat1percentAt.Content;

namespace whereThat1percentAt.Items
{
    public class hallowCompass : baseModCompass
    {
        public override string Texture => Textures.hallowCompass;
        public override short CompassItem => ItemID.LightShard;

        public override void UpdateInfoAccessory(Player player)
        {
            player.GetModPlayer<CustomPlayer>().showHallowCompass = true;
        }
    }
}
