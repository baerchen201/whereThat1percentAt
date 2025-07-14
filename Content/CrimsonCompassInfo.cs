using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace whereThat1percentAt.Content;

public class CrimsonCompassInfo : BaseModCompassInfo
{
    public override string Texture => Textures.CRIMSON_COMPASS_INFO;
    public override List<int> Tiles => TileID.Sets.CrimsonCountCollection;
    public override string TargetName => Language.GetTextValue("Mods.whereThat1percentAt.cr"); // TODO: Replace with localization from game
    public override string Percentage =>
        Language.GetTextValue(
            "Mods.whereThat1percentAt.percentage.cr",
            _PercentageString(Main.LocalPlayer.GetModPlayer<CustomPlayer>().percentages.Crimson),
            Main.worldName
        );

    public override bool Active() =>
        Main.LocalPlayer.GetModPlayer<CustomPlayer>().showCrimsonCompass;
}
