using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

//global player

namespace LukyMon
{
	public class GlobalPlayer : ModPlayer
	{
		public bool stickyShoes = false;

        

        public override void PostUpdate()
        {
            if (Player.velocity.X != 0 && stickyShoes) 
            { 
                int dust = Dust.NewDust(Player.position + new Vector2(0,Player.height), Player.width,4, DustID.Cloud, 0f, 0f, 0, Colors.RarityBlue, 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0;
            }
        }

        public override void ResetEffects()
        {
            stickyShoes = false;
            
        }
    }


  
}
