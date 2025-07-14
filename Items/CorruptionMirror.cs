using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;
using whereThat1percentAt.Content;

namespace whereThat1percentAt.Items;

public class CorruptionMirror : BaseModMirror
{
    public override string Texture => Textures.corruptionMirror;

    public override short MirrorItem => ItemID.RottenChunk;
    public override List<int> Tiles => TileID.Sets.CorruptCountCollection;
    public override string TargetName => Language.GetTextValue("Mods.whereThat1percentAt.co"); // TODO: Replace with localization from game
}
