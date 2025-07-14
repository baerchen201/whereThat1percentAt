using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using whereThat1percentAt.Content;

namespace whereThat1percentAt.Items;

public abstract class BaseModMirror : ModItem
{
    public abstract short MirrorItem { get; }
    public abstract List<int> Tiles { get; }
    public abstract string TargetName { get; }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.MagicMirror);
        Item.value = Item.value + new Item(MirrorItem).value + 1;
        Item.rare = ItemRarityID.Green;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MagicMirror);
        recipe.AddIngredient(MirrorItem, 5);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }

    public override bool? UseItem(Player player)
    {
        if (
            (
                ModContent.GetInstance<CustomConfig>().randomTp
                    ? GetRandomTileOfType(Tiles, out var tile)
                    : GetClosestTileOfType(player.Center, Tiles, out tile, out _)
            ) && GetClosestTeleportSpace(tile.Position, out TileInfo targetPosition, out _)
        )
            player.Teleport(targetPosition.TruePosition);
        else
            ChatHelper.DisplayMessageOnClient(
                NetworkText.FromKey("Mods.whereThat1percentAt.noTarget", TargetName),
                Color.Yellow,
                player.whoAmI
            );
        return true;
    }

    private static bool GetClosestTileOfType(
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
            Tile tile = Main.tile[x, y];
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

    private static bool GetRandomTileOfType(List<int> tiles, out TileInfo randomTile)
    {
        randomTile = null!;
        List<TileInfo> choices = [];

        for (int x = 0; x < Main.maxTilesX; x++)
        for (int y = 0; y < Main.maxTilesY; y++)
        {
            Tile tile = Main.tile[x, y];
            if (tile.HasTile && tiles.Contains(tile.TileType))
                choices.Add(new TileInfo(tile, new Vector2(x, y)));
        }

        if (choices.Count == 0)
            return false;

        randomTile = choices[new Random().Next(choices.Count)];
        return true;
    }

    private static bool GetClosestTeleportSpace(
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
            if (Main.tile[x, y].HasTile)
                continue;

            float distance = Vector2.Distance(origin, new Vector2(x, y));
            if (distance < closestDistance)
            {
                bool isValid = true;
                for (int localX = x - 1; localX <= x + 1; localX++)
                {
                    if (!isValid || localX < 0 || localX >= Main.tile.Width)
                    {
                        isValid = false;
                        break;
                    }

                    for (int localY = y - 1; localY <= y + 1; localY++)
                    {
                        if (localY < 0 || localY >= Main.tile.Height)
                        {
                            isValid = false;
                            break;
                        }

                        Tile tile = Main.tile[localX, localY];
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
                    closestTile = new TileInfo(Main.tile[x, y], new Vector2(x, y));
                }
            }
        }

        return closestTile != null!;
    }
}
