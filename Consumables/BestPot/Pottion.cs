using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace LukyMon.Consumables.BestPot
{
    public class Pottion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Buffer"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Get all Buffs in one.");
        }

        public override void SetDefaults()
        {
     
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.value = 1000;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item3;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.potion = false;
            Item.maxStack = 30;
            



        }
        public override bool? UseItem(Player player)
        {
            player.AddBuff(BuffID.AmmoBox,10000);
            player.AddBuff(BuffID.Archery,10000);
            player.AddBuff(BuffID.Endurance,10000);
            player.AddBuff(BuffID.Heartreach,10000);
            player.AddBuff(BuffID.Inferno,10000);
            player.AddBuff(BuffID.Ironskin,10000);
            player.AddBuff(BuffID.Lifeforce,10000);
            player.AddBuff(BuffID.MagicPower,10000);
            player.AddBuff(BuffID.Lucky,10000);
            player.AddBuff(BuffID.ManaRegeneration,10000);
            player.AddBuff(BuffID.Mining,10000);
            player.AddBuff(BuffID.ObsidianSkin,10000);
            player.AddBuff(BuffID.Thorns,10000);
            player.AddBuff(BuffID.Swiftness,10000);
            player.AddBuff(BuffID.Wrath,10000);
            player.AddBuff(BuffID.WellFed3,10000);
            player.AddBuff(BuffID.WeaponImbueIchor,10000);
            player.AddBuff(BuffID.Bewitched,10000);
            player.AddBuff(BuffID.Campfire,10000);
            player.AddBuff(BuffID.Clairvoyance,10000);
            player.AddBuff(BuffID.SugarRush,10000);
            player.AddBuff(BuffID.Sharpened,10000);
            player.AddBuff(BuffID.DryadsWard,10000);
            player.AddBuff(BuffID.HeartLamp,10000);
            player.AddBuff(BuffID.Honey,10000);
            player.AddBuff(BuffID.StarInBottle,10000);
            player.AddBuff(BuffID.CatBast,10000);
            player.AddBuff(110,10000);
            

            

           return true;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gel, 15);
            recipe.AddTile(TileID.Bottles);
            recipe.HasResult(this);
            recipe.Register();
        }




    }
}