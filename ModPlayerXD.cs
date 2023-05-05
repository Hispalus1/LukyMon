using log4net;
using log4net.Repository.Hierarchy;
using System;
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
            LogManager.GetLogger("LukyMon").Info("Mod is running!");
        }

        public override void OnEnterWorld(Player player)
        {
            if (player != null)
            {
                lock (playerLock)
                {
                    this.player = player;
                }
            }
        }

        public override void PostUpdate()
        {
            
            lock (playerLock)
            {
                if (player == null)
                {
                    return;
                }

                if (!player.dead)
                {
                    activeMinionIDs = GetActiveMinionIDs();
                }
                else
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
                if (player == null)
                {
                    return;
                }
                if (player != null) { 
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
            }   }
        }

        private List<int> GetActiveMinionIDs()
        {
            List<int> activeIDs = new List<int>();
            if (player != null && minionSlotsTakenField != null)
            {
                Projectile[] minions = (Projectile[])minionsField.GetValue(player);
                bool[] slotsTaken = (bool[])minionSlotsTakenField.GetValue(player);
                
                if (minions != null && slotsTaken != null)
                {
                    for (int i = 0; i < minions.Length; i++)
                    {
                        if (minions[i] != null && minions[i].active && minions[i].owner == player.whoAmI)
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
            }
            return activeIDs;
            
        }

        private void SetMinionAtSlot(int slot, Projectile minion)
        {
            if (player != null && minionsField != null && minionSlotsTakenField != null)
            {
                Projectile[] minions = (Projectile[])minionsField.GetValue(player);
                if (minions != null && slot < minions.Length)
                {
                    minions[slot] = minion;
                    minionsField.SetValue(player, minions);
                }
            }
        }

        private void SetMinionSlotTaken(int slot, bool taken)
        {
            if (player != null && minionsField != null && minionSlotsTakenField != null)
            {
                bool[] slotsTaken = (bool[])minionSlotsTakenField.GetValue(player);
                if (slotsTaken != null && slot < slotsTaken.Length)
                {
                    slotsTaken[slot] = taken;
                    minionSlotsTakenField.SetValue(player, slotsTaken);
                }
            }
        }

        private int GetEmptyMinionSlot()
        {
            if (player != null && minionsField != null && minionSlotsTakenField != null)
            {
                Projectile[] minions = (Projectile[])minionsField.GetValue(player);
                bool[] slotsTaken = (bool[])minionSlotsTakenField.GetValue(player);

                if (minions != null && slotsTaken != null)
                {
                    for (int i = 0; i < minions.Length; i++)
                    {
                        if (!slotsTaken[i])
                        {
                            LogManager.GetLogger("LukyMon").Info("Mod is running!");
                            return i;
                        }
                    }
                }
            }
            return -1;
        }
    }
}

