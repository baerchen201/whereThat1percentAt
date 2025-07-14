using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace whereThat1percentAt.Content
{
    public class CustomPlayer : ModPlayer
    {
        public bool showCorruptionCompass;
        public bool showCrimsonCompass;
        public bool showHallowCompass;
        public bool showPercentages;

        public readonly Percentages percentages = new();
        public bool ForceUpdate
        {
            get => forceUpdate;
            set => forceUpdate = true;
        }
        private bool forceUpdate = true;

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

        private int updateCooldown;

        public override void PostUpdate()
        {
            if (updateCooldown <= 0 || forceUpdate)
            {
                updateCooldown =
                    ModContent.GetInstance<CustomConfig>().percentageUpdateInterval * 60;

                if (!showPercentages && !forceUpdate)
                    return;

                percentages.Corruption = Main.tile.CountWorldTilePercentage(
                    TileID.Sets.CorruptCountCollection
                );
                percentages.Crimson = Main.tile.CountWorldTilePercentage(
                    TileID.Sets.CrimsonCountCollection
                );
                percentages.Hallow = Main.tile.CountWorldTilePercentage(
                    TileID.Sets.HallowCountCollection
                );
            }
            else
                updateCooldown--;

            forceUpdate = false;
        }
    }
}
