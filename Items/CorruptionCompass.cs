using Terraria;
using Terraria.ID;
using whereThat1percentAt.Content;

namespace whereThat1percentAt.Items;

public class CorruptionCompass : BaseModCompass
{
    public override string Texture => Textures.CORRUPTION_COMPASS;
    public override short CompassItem => ItemID.RottenChunk;

    public override void UpdateInfoAccessory(Player player)
    {
        player.GetModPlayer<CustomPlayer>().showCorruptionCompass = true;
    }
}
