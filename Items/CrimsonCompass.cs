using Terraria;
using Terraria.ID;
using whereThat1percentAt.Content;

namespace whereThat1percentAt.Items;

public class CrimsonCompass : BaseModCompass
{
    public override string Texture => Textures.CRIMSON_COMPASS;
    public override short CompassItem => ItemID.Vertebrae;

    public override void UpdateInfoAccessory(Player player)
    {
        player.GetModPlayer<CustomPlayer>().showCrimsonCompass = true;
    }
}
