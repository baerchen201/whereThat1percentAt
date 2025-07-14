using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;
using whereThat1percentAt.Content;

namespace whereThat1percentAt.Items;

public class CrimsonMirror : BaseModMirror
{
    public override string Texture => Textures.crimsonMirror;
    public override short MirrorItem => ItemID.Vertebrae;
    public override List<int> Tiles => TileID.Sets.CrimsonCountCollection;
    public override string TargetName => Language.GetTextValue("Mods.whereThat1percentAt.cr"); // TODO: Replace with localization from game
}
