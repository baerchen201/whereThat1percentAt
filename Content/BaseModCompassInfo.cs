using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace whereThat1percentAt.Content
{
    public abstract class BaseModCompassInfo : InfoDisplay
    {
        public virtual int Range => 125;
        public abstract List<int> Tiles { get; }
        public abstract string TargetName { get; }
        public abstract string Percentage { get; }

        public override LocalizedText DisplayName =>
            Language.GetText("Mods.whereThat1percentAt.compassInfo").WithFormatArgs(TargetName);

        public abstract override bool Active();

        private bool hasUpdated;

        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            if (
                !Main.tile.GetClosestTileOfType(
                    Main.LocalPlayer.Center.WorldToTileSpace(),
                    Tiles,
                    Range,
                    out var tile,
                    out var distance
                )
            )
                if (Main.LocalPlayer.GetModPlayer<CustomPlayer>().showPercentages)
                {
                    if (!hasUpdated && ModContent.GetInstance<CustomConfig>().updateOnDisplay)
                    {
                        Main.LocalPlayer.GetModPlayer<CustomPlayer>().ForceUpdate = true;
                        hasUpdated = true;
                    }

                    return Percentage;
                }
                else
                    return $"No {TargetName} nearby";
            else
                hasUpdated = false;

            if (distance > 5)
                return $"{TargetName} {Math.Ceiling(distance)} tiles away";

            StringBuilder blockName = new StringBuilder();
            foreach (char i in TileID.Search.GetName(tile.Tile.TileType))
                if (char.IsUpper(i) && blockName.Length > 0)
                    blockName.Append(" " + i);
                else
                    blockName.Append(i);
            return $"{blockName} nearby";
        }

        protected static string _PercentageString(double? percentage)
        {
            if (percentage == null)
                return "null";
            percentage *= 100d;
            var roundedPercentage = Math.Round(percentage.Value, 2);
            if (roundedPercentage <= 0 && percentage > 0)
                return "<0.01";
            return roundedPercentage.ToString(CultureInfo.CurrentCulture);
        }
    }
}
