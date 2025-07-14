using Microsoft.Xna.Framework;
using Terraria;

namespace whereThat1percentAt.Content;

public class TileInfo(Tile tile, Vector2 position)
{
    public Tile Tile { get; } = tile;
    public Vector2 Position { get; } = position;
    public Vector2 TruePosition => Position * 16f + new Vector2(8f);
}
