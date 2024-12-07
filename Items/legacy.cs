using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace whereThat1percentAt.Items
{
    static class LegacyReplacements
    {
        [Obsolete("Unstable, use replaceItemTypeIfLegacyItem instead")]
        public static Item findReplacementOrKeep(Item i)
        {
            if (i.type == itype<crimsonCompass>())
            {
                return nitem<CrimsonCompass>();
            }
            else if (i.type == itype<corruptionCompass>())
            {
                return nitem<CorruptionCompass>();
            }
            else if (i.type == itype<hallowCompass>())
            {
                return nitem<HallowCompass>();
            }
            else if (i.type == itype<crimsonMirror>())
            {
                return nitem<CrimsonMirror>();
            }
            else if (i.type == itype<corruptionMirror>())
            {
                return nitem<CorruptionMirror>();
            }
            else if (i.type == itype<hallowMirror>())
            {
                return nitem<HallowCompass>();
            }
            else if (i.type == itype<rainbowCompass>())
            {
                return nitem<RainbowCompass>();
            }
            else
            {
                return i;
            }
        }

        [Obsolete("Makes items unstable, use replacePlayerItemIfLegacyItem instead")]
        public static void replaceItemTypeIfLegacyItem(Item i)
        {
            if (i.type == itype<crimsonCompass>())
            {
                i.type = itype<CrimsonCompass>();
            }
            else if (i.type == itype<corruptionCompass>())
            {
                i.type = itype<CorruptionCompass>();
            }
            else if (i.type == itype<hallowCompass>())
            {
                i.type = itype<HallowCompass>();
            }
            else if (i.type == itype<crimsonMirror>())
            {
                i.type = itype<CrimsonMirror>();
            }
            else if (i.type == itype<corruptionMirror>())
            {
                i.type = itype<CorruptionMirror>();
            }
            else if (i.type == itype<hallowMirror>())
            {
                i.type = itype<HallowCompass>();
            }
            else if (i.type == itype<rainbowCompass>())
            {
                i.type = itype<RainbowCompass>();
            }
        }

        public static void replacePlayerItemIfLegacyItem(Player p, int i)
        {
            Item item = p.inventory[i];
            if (item.type == itype<crimsonCompass>())
            {
                p.inventory[i] = nitem<CrimsonCompass>();
                item.TurnToAir();
            }
            else if (item.type == itype<corruptionCompass>())
            {
                p.inventory[i] = nitem<CorruptionCompass>();
                item.TurnToAir();
            }
            else if (item.type == itype<hallowCompass>())
            {
                p.inventory[i] = nitem<HallowCompass>();
                item.TurnToAir();
            }
            else if (item.type == itype<crimsonMirror>())
            {
                p.inventory[i] = nitem<CrimsonMirror>();
                item.TurnToAir();
            }
            else if (item.type == itype<corruptionMirror>())
            {
                p.inventory[i] = nitem<CorruptionMirror>();
                item.TurnToAir();
            }
            else if (item.type == itype<hallowMirror>())
            {
                p.inventory[i] = nitem<HallowCompass>();
                item.TurnToAir();
            }
            else if (item.type == itype<rainbowCompass>())
            {
                p.inventory[i] = nitem<RainbowCompass>();
                item.TurnToAir();
            }
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
