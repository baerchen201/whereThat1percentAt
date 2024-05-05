using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using whereThat1percentAt.Content;

namespace whereThat1percentAt.Items
{
    public class crimsonCompass : ModItem
    {
        public override string Texture => Textures.crimsonCompass;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Compass);
            Item.value = Item.value + new Item(ItemID.Vertebrae).value * 5 + 1;
            Item.rare = ItemRarityID.Green;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Compass);
            recipe.AddIngredient(ItemID.Vertebrae, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }

        public override void UpdateInfoAccessory(Player player)
        {
            player.GetModPlayer<CustomPlayer>().showCrimsonCompass = true;
        }
    }
}
