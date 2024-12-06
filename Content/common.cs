using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;

namespace whereThat1percentAt.Content
{
    class Scripts
    {
        public static void SendDebugChatMessage(string text, Color? color)
        {
            if (color == null)
                color = Color.Gold;
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), (Color)color);
        }

        public static void SendDebugChatMessage(NetworkText text, Color? color)
        {
            if (color == null)
                color = Color.Gold;
            ChatHelper.BroadcastChatMessage(text, (Color)color);
        }

        public static void SendDebugChatMessage(string text)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.Gold);
        }

        public static void SendDebugChatMessage(NetworkText text)
        {
            ChatHelper.BroadcastChatMessage(text, Color.Gold);
        }

        public static Tuple<Tile, Vector2, float> getClosestTileOfType(
            Player player,
            List<int> types,
            Tuple<int, int> limit = null
        )
        {
            float closestDistance = float.MaxValue;

            Tuple<Tile, Vector2, float> ret = null;

            if (limit == null)
                for (int x = 0; x < Main.maxTilesX; x++)
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    Tile tile = Main.tile[x, y];
                    if (tile.HasTile && types.Contains(tile.TileType))
                    {
                        Vector2 tilePosition = new Vector2(x * 16, y * 16);
                        float distance = Vector2.Distance(player.Center, tilePosition);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            ret = Tuple.Create(tile, tilePosition, closestDistance);
                        }
                    }
                }
            else
                for (
                    int x = (int)(player.Center.X / 16) - limit.Item1;
                    x < (int)(player.Center.X / 16) + limit.Item1;
                    x++
                )
                    if (x < 0 || x >= Main.tile.Width)
                        continue;
                    else
                        for (
                            int y = (int)(player.Center.Y / 16) - limit.Item2;
                            y < (int)(player.Center.Y / 16) + limit.Item2;
                            y++
                        )
                        {
                            if (y < 0 || y >= Main.tile.Height)
                                continue;
                            Tile tile = Main.tile[x, y];
                            if (tile.HasTile && types.Contains(tile.TileType))
                            {
                                Vector2 tilePosition = new Vector2(x * 16, y * 16);
                                float distance = Vector2.Distance(player.Center, tilePosition);
                                if (distance < closestDistance)
                                {
                                    closestDistance = distance;
                                    ret = Tuple.Create(tile, tilePosition, closestDistance);
                                }
                            }
                        }

            return ret;
        }

        public static Tuple<Tile, Vector2> getRandomTileOfType(
            List<int> types,
            Player player,
            Tuple<int, int> limit = null
        )
        {
            List<Tuple<Tile, Vector2>> tiles = new List<Tuple<Tile, Vector2>>();

            if (limit == null)
                for (int x = 0; x < Main.maxTilesX; x++)
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    Tile tile = Main.tile[x, y];
                    if (tile.HasTile && types.Contains(tile.TileType))
                    {
                        Vector2 tilePosition = new Vector2(x * 16, y * 16);
                        tiles.Add(Tuple.Create(tile, tilePosition));
                    }
                }
            else
                for (
                    int x = (int)(player.Center.X / 16) - limit.Item1;
                    x < (int)(player.Center.X / 16) + limit.Item1;
                    x++
                )
                    if (x < 0 || x >= Main.tile.Width)
                        continue;
                    else
                        for (
                            int y = (int)(player.Center.Y / 16) - limit.Item2;
                            y < (int)(player.Center.Y / 16) + limit.Item2;
                            y++
                        )
                        {
                            if (y < 0 || y >= Main.tile.Height)
                                continue;
                            Tile tile = Main.tile[x, y];
                            if (tile.HasTile && types.Contains(tile.TileType))
                            {
                                Vector2 tilePosition = new Vector2(x * 16, y * 16);
                                tiles.Add(Tuple.Create(tile, tilePosition));
                            }
                        }

            if (tiles.Count == 0)
                return null;
            return tiles[new Random().Next(tiles.Count)];
        }

        public static Tuple<Tile, Vector2, float> getClosestValid3x3Space(Vector2 trueCenter)
        {
            float closestDistance = float.MaxValue;
            Vector2 center = trueCenter / 16;

            Tuple<Tile, Vector2, float> ret = null;

            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    Tile tile = Main.tile[x, y];

                    if (!tile.HasTile)
                    {
                        Vector2 tilePosition = new Vector2(x, y);
                        float distance = Vector2.Distance(center, tilePosition);
                        if (distance < closestDistance)
                        {
                            bool isValid = true;
                            for (int x2 = x - 1; x2 <= x + 1; x2++)
                            {
                                if (x2 < 0 || x2 >= Main.tile.Width || !isValid)
                                {
                                    isValid = false;
                                    break;
                                }
                                for (int y2 = y - 1; y2 <= y + 1; y2++)
                                {
                                    if (y2 < 0 || y2 >= Main.tile.Height)
                                    {
                                        isValid = false;
                                        break;
                                    }
                                    if (
                                        Main.tile[x2, y2].HasTile
                                        || (
                                            Main.tile[x2, y2].CheckingLiquid
                                            && new List<int>()
                                            {
                                                LiquidID.Lava,
                                                LiquidID.Shimmer
                                            }.Contains(Main.tile[x2, y2].LiquidType)
                                        )
                                    )
                                    {
                                        isValid = false;
                                    }
                                }
                            }
                            try
                            {
                                if (y + 2 >= Main.tile.Height || !Main.tile[x, y + 2].HasTile)
                                {
                                    isValid = false;
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                                isValid = false;
                            }

                            if (isValid)
                            {
                                closestDistance = distance;
                                ret = Tuple.Create(
                                    tile,
                                    new Vector2((int)tilePosition.X * 16, (int)tilePosition.Y * 16),
                                    closestDistance
                                );
                            }
                        }
                    }
                }
            }

            return ret;
        }

        public static Tuple<
            List<List<Tuple<Tuple<int, int>, Tile>>>,
            Tuple<int, int>
        > countWorldTilePercentage(params List<int>[] types)
        {
            int empty = 0;
            int total = Main.tile.Height * Main.tile.Width;

            List<List<Tuple<Tuple<int, int>, Tile>>> ret =
                new List<List<Tuple<Tuple<int, int>, Tile>>>();
            foreach (var i in types)
                ret.Add(new List<Tuple<Tuple<int, int>, Tile>>());

            for (int x = 0; x < Main.tile.Width; x++)
            for (int y = 0; y < Main.tile.Height; y++)
            {
                Tile tile = Main.tile[x, y];
                if (!tile.HasTile)
                    empty++;
                else
                    for (int i = 0; i < types.Length; i++)
                    {
                        List<int> type_tiles = types[i];
                        if (type_tiles.Contains(tile.TileType))
                            ret[i].Add(Tuple.Create(Tuple.Create(x, y), tile));
                    }
            }

            return Tuple.Create(ret, Tuple.Create(total, empty));
        }

        public static List<List<Vector2>> findBlockClustersOfType(params List<int>[] types)
        {
            List<List<Vector2>> ret = new List<List<Vector2>>();
            List<Vector2> pass = new List<Vector2>();
            foreach (var i in types)
                ret.Add(new List<Vector2>());

            for (int x = 0; x < Main.tile.Width; x++)
            for (int y = 0; y < Main.tile.Height; y++)
            {
                Tile tile = Main.tile[x, y];
                if (tile.HasTile)
                    for (int i = 0; i < types.Length; i++)
                    {
                        List<int> type_tiles = types[i];
                        if (type_tiles.Contains(tile.TileType) && !pass.Contains(new Vector2(x, y)))
                        {
                            ret[i].Add(new Vector2(x, y));
                            _recursiveCheck(new Vector2(x, y), type_tiles, ref pass);
                        }
                    }
            }

            return ret;
        }

        private static void _recursiveCheck(
            Vector2 root,
            List<int> type,
            ref List<Vector2> exclude_list
        )
        {
            for (int x = (int)root.X - 1; x <= (int)root.X + 1; x++)
            {
                if (x < 0 || x >= Main.tile.Width)
                    continue;
                for (int y = (int)root.Y - 1; y <= (int)root.Y + 1; y++)
                {
                    if (
                        y < 0
                        || y >= Main.tile.Height
                        || (x == root.X && y == root.Y)
                        || exclude_list.Contains(new Vector2(x, y))
                    )
                        continue;
                    if (Main.tile[x, y].HasTile && type.Contains(Main.tile[x, y].TileType))
                    {
                        exclude_list.Add(new Vector2(x, y));
                        _recursiveCheck(new Vector2(x, y), type, ref exclude_list);
                    }
                }
            }
        }
    }

    class Lists
    {
        public static List<int> Corruption = TileID.Sets.CorruptCountCollection;
        public static List<int> Crimson = TileID.Sets.CrimsonCountCollection;
        public static List<int> Hallow = TileID.Sets.HallowCountCollection;
    }

    class Textures
    {
        public static string placeholder = "whereThat1percentAt/default";
        public static string corruptionCompassInfo =
            "whereThat1percentAt/Textures/Corruption_Compass_toggle";
        public static string corruptionCompass = "whereThat1percentAt/Textures/CorruptionCompass";
        public static string crimsonCompassInfo =
            "whereThat1percentAt/Textures/Crimson_Compass_toggle";
        public static string crimsonCompass = "whereThat1percentAt/Textures/CrimsonCompass";
        public static string hallowCompassInfo =
            "whereThat1percentAt/Textures/Hallow_Compass_toggle";
        public static string hallowCompass = "whereThat1percentAt/Textures/HallowCompass";
        public static string corruptionMirror =
            "whereThat1percentAt/Textures/Corruption_Magic_Mirror";
        public static string crimsonMirror = "whereThat1percentAt/Textures/Crimson_Magic_Mirror";
        public static string hallowMirror = "whereThat1percentAt/Textures/Hallow_Magic_Mirror";
        public static string rainbowCompass = "whereThat1percentAt/Textures/RainbowCompass";
        public static string mapHighlightToggle = "whereThat1percentAt/Textures/MapHToggle";
        public static string mapHighlightToggleCo = "whereThat1percentAt/Textures/MapHToggleCo";
        public static string mapHighlightToggleCr = "whereThat1percentAt/Textures/MapHToggleCr";
        public static string mapHighlightToggleH = "whereThat1percentAt/Textures/MapHToggleH";
        public static string mapHighlightToggleAll = "whereThat1percentAt/Textures/MapHToggleOn";
    }
}
