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
    public abstract class BaseModCompassInfo : InfoDisplay
    {
        public virtual int Range => 125;

        public abstract List<int> Tiles { get; }
        public abstract override string Name { get; }
        public string RealName => Language.GetTextValue("Mods.whereThat1percentAt." + Name);

        public override LocalizedText DisplayName =>
            Language.GetText("Mods.whereThat1percentAt.compassInfo").WithFormatArgs(RealName);

        public abstract override bool Active();

        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            Tuple<Tile, Vector2, float> ret = Scripts.getClosestTileOfType(
                Main.LocalPlayer,
                Tiles,
                Tuple.Create(Range, Range)
            );

            if (ret == null || (int)Math.Round(ret.Item3 / 16) > Range)
                if (Main.LocalPlayer.GetModPlayer<CustomPlayer>().showPercentages)
                    try
                    {
                        return Language.GetTextValue(
                            "Mods.whereThat1percentAt.percentage." + Name,
                            Main.LocalPlayer.GetModPlayer<CustomPlayer>()
                                .percentages[Name]
                                .ToString(),
                            Main.worldName
                        );
                    }
                    catch (KeyNotFoundException)
                    {
                        return _FixDisplayValue();
                    }
                else
                    return $"No {RealName} nearby";

            int distance = (int)Math.Round(ret.Item3 / 16);
            if (distance > 5)
                return $"{RealName} {distance} tiles away";
            else
                return $"{Language.GetTextValue(TileID.Search.GetName(ret.Item1.TileType))} nearby";
        }

        bool fixFailed = false;
        bool reportMessageSent = false;

        string _FixDisplayValue()
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
                        "Automatic fix failed, preventing lag from updating on each frame"
                    );
                }
                player.forceUpdate = true;
                player.PostUpdate();
                return Language.GetTextValue(
                    "Mods.whereThat1percentAt.percentage." + Name,
                    player.percentages[Name].ToString(),
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
