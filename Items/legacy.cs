using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace whereThat1percentAt.Items
{
    static class LegacyReplacements
    {
        public static void replacePlayerLegacyItem(Player p, int i)
        {
            Item item = p.inventory[i];
            if (item.type == itype<crimsonCompass>())
                p.inventory[i] = nitem<CrimsonCompass>();
            else if (item.type == itype<corruptionCompass>())
                p.inventory[i] = nitem<CorruptionCompass>();
            else if (item.type == itype<hallowCompass>())
                p.inventory[i] = nitem<HallowCompass>();
            else if (item.type == itype<crimsonMirror>())
                p.inventory[i] = nitem<CrimsonMirror>();
            else if (item.type == itype<corruptionMirror>())
                p.inventory[i] = nitem<CorruptionMirror>();
            else if (item.type == itype<hallowMirror>())
                p.inventory[i] = nitem<HallowCompass>();
            else if (item.type == itype<rainbowCompass>())
                p.inventory[i] = nitem<RainbowCompass>();
            else
                return;
            item.TurnToAir();
        }

        private static int itype<T>()
            where T : ModItem
        {
            return ModContent.ItemType<T>();
        }

        private static Item nitem<T>()
            where T : ModItem
        {
            return Main.item[Item.NewItem(new EntitySource_Misc(""), 0, 0, 0, 0, itype<T>())];
        }
    }

    class crimsonCompass : CrimsonCompass
    {
        public override void AddRecipes() { }
    }

    class corruptionCompass : CorruptionCompass
    {
        public override void AddRecipes() { }
    }

    class hallowCompass : HallowCompass
    {
        public override void AddRecipes() { }
    }

    class crimsonMirror : CrimsonMirror
    {
        public override void AddRecipes() { }
    }

    class corruptionMirror : CorruptionMirror
    {
        public override void AddRecipes() { }
    }

    class hallowMirror : HallowMirror
    {
        public override void AddRecipes() { }
    }

    class rainbowCompass : RainbowCompass
    {
        public override void AddRecipes() { }
    }
}
