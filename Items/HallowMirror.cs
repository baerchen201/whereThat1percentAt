using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;
using whereThat1percentAt.Content;

namespace whereThat1percentAt.Items;

public class HallowMirror : BaseModMirror
{
    public override string Texture => Textures.HALLOW_MIRROR;

    public override short MirrorItem => ItemID.LightShard;
    public override List<int> Tiles => TileID.Sets.HallowCountCollection;
    public override string TargetName => Language.GetTextValue("Mods.whereThat1percentAt.h"); // TODO: Replace with localization from game
}
