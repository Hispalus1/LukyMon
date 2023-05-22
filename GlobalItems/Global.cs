using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
//global item
namespace LukyMon.Global
{
	public class GlobalItemList : GlobalItem
	{

        public override void SetDefaults(Item item)
        {
            if(item.DamageType == DamageClass.Melee) item.autoReuse = true;
            if(item.type == ItemID.CopperShortsword)
            {
                item.damage = 10;
                item.rare = 1;
            }

            
        }





    }
    
}
