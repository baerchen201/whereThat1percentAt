using Terraria;
using Terraria.ID;
using whereThat1percentAt.Content;

namespace whereThat1percentAt.Items;

public class HallowCompass : BaseModCompass
{
    public override string Texture => Textures.HALLOW_COMPASS;
    public override short CompassItem => ItemID.LightShard;

    public override void UpdateInfoAccessory(Player player)
    {
        player.GetModPlayer<CustomPlayer>().showHallowCompass = true;
    }
}
