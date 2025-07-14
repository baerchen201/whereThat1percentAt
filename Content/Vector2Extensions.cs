using System;
using Microsoft.Xna.Framework;

namespace whereThat1percentAt.Content;

public static class Vector2Extensions
{
    public static Vector2 TileToWorldSpace(this Vector2 position) =>
        position * 16f + new Vector2(8f);

    public static Vector2 WorldToTileSpace(this Vector2 position) =>
        new((float)Math.Floor(position.X / 16f), (float)Math.Floor(position.Y / 16f));
}
