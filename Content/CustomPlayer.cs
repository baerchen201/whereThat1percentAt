using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace whereThat1percentAt.Content
{
    public class CustomPlayer : ModPlayer
    {
        public bool showCorruptionCompass;
        public bool showCrimsonCompass;
        public bool showHallowCompass;
        public bool showPercentages;

        private int updateCooldown = 0;
        public bool forceUpdate = true;

        public Dictionary<string, string> percentages = new Dictionary<string, string>();

        public override void ResetInfoAccessories()
        {
            showCorruptionCompass = false;
            showCrimsonCompass = false;
            showHallowCompass = false;
            showPercentages = false;
        }

        public override void RefreshInfoAccessoriesFromTeamPlayers(Player otherPlayer)
        {
            if (otherPlayer.GetModPlayer<CustomPlayer>().showCorruptionCompass)
                showCorruptionCompass = true;
            if (otherPlayer.GetModPlayer<CustomPlayer>().showCrimsonCompass)
                showCrimsonCompass = true;
            if (otherPlayer.GetModPlayer<CustomPlayer>().showHallowCompass)
                showHallowCompass = true;
            if (otherPlayer.GetModPlayer<CustomPlayer>().showPercentages)
                showPercentages = true;
        }

        public override void PostUpdate()
        {
            _PostUpdate();
            forceUpdate = false;
        }

        public void _PostUpdate()
        {
            if (updateCooldown < 1 || forceUpdate)
            {
                updateCooldown =
                    ModContent.GetInstance<CustomConfig>().percentageUpdateInterval * 60;

                if (!showPercentages && !forceUpdate)
                    return;

                if (!percentages.ContainsKey("co"))
                    percentages.Add("co", null);
                if (!percentages.ContainsKey("cr"))
                    percentages.Add("cr", null);
                if (!percentages.ContainsKey("h"))
                    percentages.Add("h", null);

                Tuple<List<List<Tuple<Tuple<int, int>, Tile>>>, Tuple<int, int>> ret =
                    Scripts.countWorldTilePercentage(Lists.Corruption, Lists.Crimson, Lists.Hallow);
                int total = ret.Item2.Item1;
                int empty = ret.Item2.Item2;

                double co = ret.Item1[0].Count / (double)(total - empty) * 100;
                double cr = ret.Item1[1].Count / (double)(total - empty) * 100;
                double h = ret.Item1[2].Count / (double)(total - empty) * 100;
                percentages["co"] = Math.Round(co, 2).ToString();
                if (co > 0 && Math.Round(co, 2) == 0)
                    percentages["co"] = "<0.01";

                percentages["cr"] = Math.Round(cr, 2).ToString();
                if (cr > 0 && Math.Round(cr, 2) == 0)
                    percentages["cr"] = "<0.01";

                percentages["h"] = Math.Round(h, 2).ToString();
                if (h > 0 && Math.Round(h, 2) == 0)
                    percentages["h"] = "<0.01";
            }
            else
                updateCooldown--;
        }
    }
}
