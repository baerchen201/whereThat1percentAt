using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace whereThat1percentAt.Content;

public static class TilemapExtensions
{
    public static bool GetClosestTileOfType(
        this Tilemap tilemap,
        Vector2 origin,
        List<int> tiles,
        out TileInfo closestTile,
        out float closestDistance
    )
    {
        closestTile = null!;
        closestDistance = float.MaxValue;
        for (int x = 0; x < Main.maxTilesX; x++)
        for (int y = 0; y < Main.maxTilesY; y++)
        {
            Tile tile = tilemap[x, y];
            if (tile.HasTile && tiles.Contains(tile.TileType))
            {
                Vector2 tilePosition = new Vector2(x, y);
                float distance = Vector2.Distance(origin, tilePosition);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTile = new TileInfo(tile, tilePosition);
                }
            }
        }

        return closestTile != null!;
    }

    public static bool GetClosestTileOfType(
        this Tilemap tilemap,
        Vector2 origin,
        List<int> tiles,
        int range,
        out TileInfo closestTile,
        out float closestDistance
    )
    {
        if (range < 1)
            throw new ArgumentException("Range has to be positive integer", nameof(range));

        closestTile = null!;
        closestDistance = float.MaxValue;

        int xUpperBound = Math.Min((int)origin.X + range, tilemap.Width),
            yUpperBound = Math.Min((int)origin.Y + range, tilemap.Height);
        for (int x = Math.Max((int)origin.X - range, 0); x < xUpperBound; x++)
        for (int y = Math.Max((int)origin.Y - range, 0); y < yUpperBound; y++)
        {
            Tile tile = tilemap[x, y];
            if (tile.HasTile && tiles.Contains(tile.TileType))
            {
                Vector2 tilePosition = new Vector2(x, y);
                float distance = Vector2.Distance(origin, tilePosition);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTile = new TileInfo(tile, tilePosition);
                }
            }
        }

        return closestTile != null!;
    }

    public static bool GetRandomTileOfType(
        this Tilemap tilemap,
        List<int> tiles,
        out TileInfo randomTile
    )
    {
        randomTile = null!;
        List<TileInfo> choices = [];

        for (int x = 0; x < Main.maxTilesX; x++)
        for (int y = 0; y < Main.maxTilesY; y++)
        {
            Tile tile = tilemap[x, y];
            if (tile.HasTile && tiles.Contains(tile.TileType))
                choices.Add(new TileInfo(tile, new Vector2(x, y)));
        }

        if (choices.Count == 0)
            return false;

        randomTile = choices[new Random().Next(choices.Count)];
        return true;
    }

    public static bool GetClosestTeleportSpace(
        this Tilemap tilemap,
        Vector2 origin,
        out TileInfo closestTile,
        out float closestDistance
    )
    {
        closestTile = null!;
        closestDistance = float.MaxValue;
        for (int x = 0; x < Main.maxTilesX; x++)
        for (int y = 0; y < Main.maxTilesY; y++)
        {
            if (tilemap[x, y].HasTile)
                continue;

            float distance = Vector2.Distance(origin, new Vector2(x, y));
            if (distance < closestDistance)
            {
                bool isValid = true;
                for (int localX = x - 1; localX <= x + 1; localX++)
                {
                    if (!isValid || localX < 0 || localX >= tilemap.Width)
                    {
                        isValid = false;
                        break;
                    }

                    for (int localY = y - 1; localY <= y + 1; localY++)
                    {
                        if (localY < 0 || localY >= tilemap.Height)
                        {
                            isValid = false;
                            break;
                        }

                        Tile tile = tilemap[localX, localY];
                        if (
                            tile.HasTile
                            || tile
                                is {
                                    CheckingLiquid: true,
                                    LiquidType: LiquidID.Lava or LiquidID.Shimmer
                                }
                        )
                            isValid = false;
                    }
                }

                if (isValid)
                {
                    closestDistance = distance;
                    closestTile = new TileInfo(tilemap[x, y], new Vector2(x, y));
                }
            }
        }

        return closestTile != null!;
    }

    public static double CountWorldTilePercentage(this Tilemap tilemap, List<int> tiles)
    {
        double empty = 0,
            matches = 0;

        for (int x = 0; x < tilemap.Width; x++)
        for (int y = 0; y < tilemap.Height; y++)
        {
            Tile tile = tilemap[x, y];
            if (!tile.HasTile)
                empty++;
            else if (tiles.Contains(tile.TileType))
                matches++;
        }

        return matches / (tilemap.Height * tilemap.Width - empty);
    }
}
