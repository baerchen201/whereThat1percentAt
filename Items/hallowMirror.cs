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
    public class hallowMirror : ModItem
    {
        public override string Texture => Textures.hallowMirror;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.MagicMirror);
            Item.value = Item.value + new Item(ItemID.LightShard).value + 1;
            Item.rare = ItemRarityID.LightRed;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MagicMirror);
            recipe.AddIngredient(ItemID.LightShard, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }

        public override bool? UseItem(Player player)
        {
            Tuple<Tile, Vector2> tile = null;
            if (ModContent.GetInstance<CustomConfig>().randomTp)
            {
                tile = Scripts.getRandomTileOfType(Lists.Hallow, player);
            }
            else
            {
                Tuple<Tile, Vector2, float> tmp = Scripts.getClosestTileOfType(
                    player,
                    Lists.Hallow
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
                            Language.GetTextValue("Mods.whereThat1percentAt.h")
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
                        Language.GetTextValue("Mods.whereThat1percentAt.h")
                    ),
                    Color.Yellow,
                    player.whoAmI
                );
            }

            return true;
        }
    }
}
