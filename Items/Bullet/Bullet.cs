using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LukyMon.Items.Bullet
{
    public class Bullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gel bullets");
            Tooltip.SetDefault("Bounces off walls");
        }

        public override void SetDefaults()
        {
            Item.damage = 4;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 40;
            Item.knockBack = 2;
            Item.value = 50;
            Item.rare = 1;
            Item.consumable = true;
            Item.shoot = Mod.Find<ModProjectile>("GelBullet").Type;
            Item.ammo = AmmoID.Bullet;
            Item.maxStack = 999;
            Item.shootSpeed = 4.5f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(25);
            recipe.AddIngredient(ItemID.Gel, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.HasResult(25);
            recipe.Register();
        }
    }
}
