using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace whereThat1percentAt.Content
{
    public class corruptionCompassInfo : InfoDisplay
    {
        public override string Texture => Textures.corruptionCompassInfo;
        public override string Name => Language.GetTextValue("Mods.whereThat1percentAt.info.co");

        public int range = 125;

        public override bool Active()
        {
            return Main.LocalPlayer.GetModPlayer<CustomPlayer>().showCorruptionCompass;
        }

        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            Tuple<Tile, Vector2, float> ret = Scripts.getClosestTileOfType(
                Main.LocalPlayer,
                Lists.Corruption,
                Tuple.Create(range, range)
            );

            if (ret == null || (int)Math.Round(ret.Item3 / 16) > range)
                if (Main.LocalPlayer.GetModPlayer<CustomPlayer>().showPercentages)
                    try
                    {
                        return Language.GetTextValue(
                            "Mods.whereThat1percentAt.percentageCo",
                            Main.LocalPlayer.GetModPlayer<CustomPlayer>()
                                .percentages["co"]
                                .ToString(),
                            Main.worldName
                        );
                    }
                    catch (KeyNotFoundException)
                    {
                        return _FixDisplayValue(ref displayColor, ref displayShadowColor);
                    }
                else
                    return "No corruption nearby";

            int distance = (int)Math.Round(ret.Item3 / 16);
            if (distance > 5)
                return $"Corruption {distance} tiles away";
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
                    "Mods.whereThat1percentAt.percentageCo",
                    player.percentages["co"].ToString(),
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
