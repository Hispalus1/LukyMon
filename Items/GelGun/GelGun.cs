using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using LukyMon.Projektile.GelBullet;
using System;


namespace LukyMon.Items.GelGun
{
    public class GelGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gel Revolver"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Shoot gel projectiles at cost of 0 ammunition.");
        }

        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0.6f;
            Item.value = 2500;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = 1;
            Item.useAmmo = AmmoID.Bullet;
            Item.shootSpeed = 5f;
            Item.noMelee = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 offset = new Vector2(velocity.X * 5, velocity.Y * 5);
            position += offset;
            if(type == ProjectileID.Bullet)
            {
                type = ModContent.ProjectileType<GelBullet>();
            }
            Projectile.NewProjectile(source, position, velocity, type, damage, (int)knockback, player.whoAmI);
            return false;
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