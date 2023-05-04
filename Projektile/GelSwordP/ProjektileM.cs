using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace LukyMon.Projektile.GelSwordP
{
    public class ProjektileM : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Test Projectil");
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = 2;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 600;
            Projectile.light = 0.25f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;

        }

        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.Center, 1, 1, DustID.MagicMirror, 0f, 0f, 0, default, 1f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 1.2f;
            Main.dust[dust].scale = Main.rand.Next(100, 135) * 0.013f;

            int dust2 = Dust.NewDust(Projectile.Center, 1, 1, DustID.IcyMerman, 0f, 0f, 0, default, 1f);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].velocity *= 1.2f;
            Main.dust[dust2].scale = Main.rand.Next(100, 135) * 0.013f;

        }
    }
}
