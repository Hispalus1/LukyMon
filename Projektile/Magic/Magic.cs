using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace LukyMon.Projektile.Magic
{
    public class Magic : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic missile");
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Magic;
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 600;
            Projectile.light = 0.25f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.Center, 1, 1, DustID.MagicMirror, 0f, 0f, 0, default(Color), 1f);
            Main.dust[dust].velocity *= 0.3f;
            Main.dust[dust].scale = (float)Main.rand.Next(100, 135) * 0.013f;
            Main.dust[dust].noGravity = true;
            
            

            int dust2 = Dust.NewDust(Projectile.Center, 1, 1, DustID.IcyMerman, 0f, 0f, 0, default(Color), 1f);
            Main.dust[dust2].velocity *= 0.3f;
            Main.dust[dust2].scale = (float)Main.rand.Next(80, 115) * 0.013f;
            Main.dust[dust2].noGravity = true;
            
        }
    }
}
