using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace whereThat1percentAt.Content
{
    public class crimsonCompassInfo : InfoDisplay
    {
        public override string Texture => Textures.crimsonCompassInfo;
        public override string Name => Language.GetTextValue("Mods.whereThat1percentAt.info.cr");

        public int range = 125;

        public override bool Active()
        {
            return Main.LocalPlayer.GetModPlayer<CustomPlayer>().showCrimsonCompass;
        }

        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            Tuple<Tile, Vector2, float> ret = Scripts.getClosestTileOfType(
                Main.LocalPlayer,
                Lists.Crimson,
                Tuple.Create(range, range)
            );

            if (ret == null || (int)Math.Round(ret.Item3 / 16) > range)
                if (Main.LocalPlayer.GetModPlayer<CustomPlayer>().showPercentages)
                    try
                    {
                        return Language.GetTextValue(
                            "Mods.whereThat1percentAt.percentageCr",
                            Main.LocalPlayer.GetModPlayer<CustomPlayer>()
                                .percentages["cr"]
                                .ToString(),
                            Main.worldName
                        );
                    }
                    catch (KeyNotFoundException)
                    {
                        return _FixDisplayValue(ref displayColor, ref displayShadowColor);
                    }
                else
                    return "No crimson nearby";

            int distance = (int)Math.Round(ret.Item3 / 16);
            if (distance > 5)
                return $"Crimson {distance} tiles away";
            else
                return $"{Language.GetTextValue(TileID.Search.GetName(ret.Item1.TileType))} nearby";
        }

        bool fixFailed = false;
        bool reportMessageSent = false;

        public string _FixDisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            CustomPlayer player = Main.LocalPlayer.GetModPlayer<CustomPlayer>();
            try
            {
                if (fixFailed)
                {
                    if (!reportMessageSent)
                    {
                        ChatHelper.DisplayMessageOnClient(
                            NetworkText.FromKey("Mods.whereThat1percentAt.reportErr"),
                            Color.Cyan,
                            Main.LocalPlayer.whoAmI
                        );
                        reportMessageSent = true;
                    }
                    throw new KeyNotFoundException(
                        "Automatic fix failed, preventing lag (by updating on each frame)"
                    );
                }
                player.forceUpdate = true;
                player.PostUpdate();
                return Language.GetTextValue(
                    "Mods.whereThat1percentAt.percentageCr",
                    player.percentages["cr"].ToString(),
                    Main.worldName
                );
            }
            catch (KeyNotFoundException)
            {
                fixFailed = true;
                return Language.GetTextValue("Mods.whereThat1percentAt.restartErr");
            }
        }
    }
}
