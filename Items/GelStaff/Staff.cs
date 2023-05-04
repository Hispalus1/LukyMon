using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using LukyMon.Projektile.Magic;


namespace LukyMon.Items.GelStaff
{
    public class Staff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gel Staff"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Gel staff crafted from gel.");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.mana = 4;
            Item.DamageType = DamageClass.Magic;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 29;
            Item.useAnimation = 29;
            Item.useStyle = 5;
            Item.knockBack = 2;
            Item.value = 1200;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType <Magic>();
            Item.shootSpeed = 5f;
            Item.noMelee = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 offset = new Vector2(velocity.X * (float)8.5, velocity.Y * (float)8.5);
            position += offset;
            Projectile.NewProjectile(source, position, velocity, type, damage, (int)knockback, player.whoAmI);
            return false;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gel, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.HasResult(this);
            recipe.Register();
        }






    }
}