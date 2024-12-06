using Terraria;
using Terraria.ID;
using whereThat1percentAt.Content;

namespace whereThat1percentAt.Items
{
    public class corruptionCompass : baseModCompass
    {
        public override string Texture => Textures.corruptionCompass;
        public override short CompassItem => ItemID.RottenChunk;

        public override void UpdateInfoAccessory(Player player)
        {
            player.GetModPlayer<CustomPlayer>().showCorruptionCompass = true;
        }
    }
}
