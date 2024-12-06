using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using whereThat1percentAt.Content;

namespace whereThat1percentAt.Items
{
    public class CrimsonMirror : ModItem
    {
        public override string Texture => Textures.crimsonMirror;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.MagicMirror);
            Item.value = Item.value + new Item(ItemID.Vertebrae).value + 1;
            Item.rare = ItemRarityID.Green;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MagicMirror);
            recipe.AddIngredient(ItemID.Vertebrae, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }

        public override bool? UseItem(Player player)
        {
            Tuple<Tile, Vector2> tile = null;
            if (ModContent.GetInstance<CustomConfig>().randomTp)
            {
                tile = Scripts.getRandomTileOfType(Lists.Crimson, player);
            }
            else
            {
                Tuple<Tile, Vector2, float> tmp = Scripts.getClosestTileOfType(
                    player,
                    Lists.Crimson
                );
                tile = new Tuple<Tile, Vector2>(tmp.Item1, tmp.Item2);
            }
            if (tile != null)
            {
                Tuple<Tile, Vector2, float> targetPos = Scripts.getClosestValid3x3Space(tile.Item2);
                if (targetPos != null)
                {
                    player.Teleport(targetPos.Item2);
                }
                else
                {
                    ChatHelper.DisplayMessageOnClient(
                        NetworkText.FromKey(
                            "Mods.whereThat1percentAt.noTargetPos",
                            Language.GetTextValue("Mods.whereThat1percentAt.cr")
                        ),
                        Color.Red,
                        player.whoAmI
                    );
                }
            }
            else
            {
                ChatHelper.DisplayMessageOnClient(
                    NetworkText.FromKey(
                        "Mods.whereThat1percentAt.noTarget",
                        Language.GetTextValue("Mods.whereThat1percentAt.cr")
                    ),
                    Color.Yellow,
                    player.whoAmI
                );
            }

            return true;
        }
    }
}
