using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using whereThat1percentAt.Content;

namespace whereThat1percentAt.Items
{
    public class rainbowCompass : ModItem
    {
        public override string Texture => Textures.rainbowCompass;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Compass);
            Item.value =
                Item.value * 3
                + new Item(ItemID.RottenChunk).value * 5
                + new Item(ItemID.Vertebrae).value * 5
                + new Item(ItemID.LightShard).value * 5
                + 4;
            Item.rare = ItemRarityID.Pink;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<corruptionCompass>());
            recipe.AddIngredient(ModContent.ItemType<crimsonCompass>());
            recipe.AddIngredient(ModContent.ItemType<hallowCompass>());
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateInfoAccessory(Player player)
        {
            player.GetModPlayer<CustomPlayer>().showCorruptionCompass = true;
            player.GetModPlayer<CustomPlayer>().showCrimsonCompass = true;
            player.GetModPlayer<CustomPlayer>().showHallowCompass = true;
            player.GetModPlayer<CustomPlayer>().showPercentages = true;
        }
    }
}
