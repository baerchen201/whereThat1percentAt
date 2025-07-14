using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace whereThat1percentAt.Content;

public class HallowCompassInfo : BaseModCompassInfo
{
    public override string Texture => Textures.HALLOW_COMPASS_INFO;
    public override List<int> Tiles => TileID.Sets.HallowCountCollection;
    public override string TargetName => Language.GetTextValue("Mods.whereThat1percentAt.h"); // TODO: Replace with localization from game
    public override string Percentage =>
        Language.GetTextValue(
            "Mods.whereThat1percentAt.percentage.h",
            _PercentageString(Main.LocalPlayer.GetModPlayer<CustomPlayer>().percentages.Hallow),
            Main.worldName
        );

    public override bool Active() =>
        Main.LocalPlayer.GetModPlayer<CustomPlayer>().showHallowCompass;
}
