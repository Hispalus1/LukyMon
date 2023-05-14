using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using LukyMon;



namespace LukyMon.Accessory.Boots
{
    public class GelBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sticky Boots");
            Tooltip.SetDefault("Try to run like slime!!");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 0;
            Item.rare = ItemRarityID.Gray;
            Item.accessory = true;
        }
        

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += 0.3f;
            player.accRunSpeed += 0.2f;
            player.jumpBoost = true;
            player.GetModPlayer<WoodenSlime>().stickyShoes = true;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gel, 25);
            recipe.AddTile(TileID.Anvils);
            recipe.HasResult(this);
            recipe.Register();
        }
    }
}
