using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using whereThat1percentAt.Content;

namespace whereThat1percentAt.Items
{
    public class hallowCompass : ModItem
    {
        public override string Texture => Textures.hallowCompass;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Compass);
            Item.value = Item.value + new Item(ItemID.LightShard).value * 5 + 1;
            Item.rare = ItemRarityID.LightRed;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Compass);
            recipe.AddIngredient(ItemID.LightShard, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }

        public override void UpdateInfoAccessory(Player player)
        {
            player.GetModPlayer<CustomPlayer>().showHallowCompass = true;
        }
    }
}
