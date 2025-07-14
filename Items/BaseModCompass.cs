using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace whereThat1percentAt.Items;

public abstract class BaseModCompass : ModItem
{
    public abstract short CompassItem { get; }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.Compass);
        Item.value = Item.value + new Item(CompassItem).value * 5 + 1;
        Item.rare = ItemRarityID.Green;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Compass);
        recipe.AddIngredient(CompassItem, 5);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}
