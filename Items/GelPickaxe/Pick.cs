using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace LukyMon.Items.GelPickaxe
{
    public class Pick : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gel Pickaxe"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Gel Pickaxe crafted from gel.");
        }

        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1;
            Item.value = 1000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.pick = 53;
            Item.useTurn = true;
            Item.scale = 0.75f;
           

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gel, 15);
            recipe.AddIngredient(ItemID.Wood, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.HasResult(this);
            recipe.Register();
        }






    }
}