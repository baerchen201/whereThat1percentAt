using Microsoft.Xna.Framework;
using Terraria;

namespace whereThat1percentAt.Content;

public class TileInfo(Tile tile, Vector2 tilePosition)
{
    public Tile Tile { get; } = tile;
    public Vector2 TilePosition { get; } = tilePosition;
    public Vector2 WorldPosition => TilePosition.TileToWorldSpace();
}
