using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace whereThat1percentAt.Content;

public class CorruptionCompassInfo : BaseModCompassInfo
{
    public override string Texture => Textures.CORRUPTION_COMPASS_INFO;
    public override List<int> Tiles => TileID.Sets.CorruptCountCollection;
    public override string TargetName => Language.GetTextValue("Mods.whereThat1percentAt.co"); // TODO: Replace with localization from game
    public override string Percentage =>
        Language.GetTextValue(
            "Mods.whereThat1percentAt.percentage.co",
            _PercentageString(Main.LocalPlayer.GetModPlayer<CustomPlayer>().percentages.Corruption),
            Main.worldName
        );

    public override bool Active() =>
        Main.LocalPlayer.GetModPlayer<CustomPlayer>().showCorruptionCompass;
}
