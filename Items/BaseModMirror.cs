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
                    ? Main.tile.GetRandomTileOfType(Tiles, out var tile)
                    : Main.tile.GetClosestTileOfType(
                        player.Center.WorldToTileSpace(),
                        Tiles,
                        out tile,
                        out _
                    )
            )
            && Main.tile.GetClosestTeleportSpace(
                tile.TilePosition,
                out TileInfo targetPosition,
                out _
            )
        )
            player.Teleport(targetPosition.WorldPosition);
        else
            ChatHelper.DisplayMessageOnClient(
                NetworkText.FromKey("Mods.whereThat1percentAt.noTarget", TargetName),
                Color.Yellow,
                player.whoAmI
            );
        return true;
    }
}
