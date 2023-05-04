using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace LukyMon
{
    public class MyPlayerXD : ModPlayer
    {
        private List<int> activeMinionIDs = new List<int>();
        private FieldInfo minionsField;
        private FieldInfo minionSlotsTakenField;
        private readonly object playerLock = new object();
        private Player player; // new field to hold the player instance

        public override void Load()
        {
            player = Main.LocalPlayer;
            minionsField = typeof(Player).GetField("_minions", BindingFlags.Instance | BindingFlags.NonPublic);
            minionSlotsTakenField = typeof(Player).GetField("_minionSlotsTaken", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        
        

        public override void OnEnterWorld(Player player)
        {
            lock (playerLock)
            {
                this.player = player;
            }
        }

        public override void PostUpdate()
        {
            lock (playerLock)
            {
                if (player != null && !player.dead)
                {
                    activeMinionIDs = GetActiveMinionIDs();
                }
                else if (player != null)
                {
                    RestoreActiveMinions();
                    activeMinionIDs.Clear();
                }
            }
        }

        private void RestoreActiveMinions()
        {
            lock (playerLock)
            {
                if (player != null)
                {
                    for (int i = 0; i < activeMinionIDs.Count; i++)
                    {
                        int minionID = activeMinionIDs[i];
                        int minionSlot = GetEmptyMinionSlot();
                        if (minionSlot != -1)
                        {
                            SetMinionSlotTaken(minionSlot, true);
                            Projectile minion = new Projectile();
                            minion.SetDefaults(minionID);
                            minion.owner = player.whoAmI;
                            SetMinionAtSlot(minionSlot, minion);
                        }
                    }
                }
            }
        }

        private List<int> GetActiveMinionIDs()
        {
            List<int> activeIDs = new List<int>();
            if (player != null)
            {
                Projectile[] minions = (Projectile[])minionsField.GetValue(player);
                bool[] slotsTaken = (bool[])minionSlotsTakenField.GetValue(player);

                for (int i = 0; i < minions.Length; i++)
                {
                    if (minions[i].active && minions[i].owner == player.whoAmI)
                    {
                        activeIDs.Add(minions[i].type);
                        slotsTaken[i] = true;
                    }
                    else
                    {
                        slotsTaken[i] = false;
                    }
                }

                minionSlotsTakenField.SetValue(player, slotsTaken);
            }
            return activeIDs;
        }

        private void SetMinionAtSlot(int slot, Projectile minion)
        {
            Projectile[] minions = (Projectile[])minionsField.GetValue(player);
            if (minions != null && slot < minions.Length && player != null)
            {
                minions[slot] = minion;
                minionsField.SetValue(player, minions);
            }
        }

        private void SetMinionSlotTaken(int slot, bool taken)
        {
            bool[] slotsTaken = (bool[])minionSlotsTakenField.GetValue(player);
            if (slotsTaken != null && slot < slotsTaken.Length && player != null)
            {
                slotsTaken[slot] = taken;
                minionSlotsTakenField.SetValue(player, slotsTaken);
            }
        }

        private int GetEmptyMinionSlot()
        {
            if (player != null)
            {
                Projectile[] minions = (Projectile[])minionsField.GetValue(player);
                bool[] slotsTaken = (bool[])minionSlotsTakenField.GetValue(player);

                for (int i = 0; i < minions.Length; i++)
                {
                    if (!slotsTaken[i])
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
    }
}

