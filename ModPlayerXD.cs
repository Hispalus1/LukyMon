using log4net;
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
        private int maxMinions;
        private bool shouldRestoreMinions = false;

        public override void Load()
        {
            player = Main.LocalPlayer;
            minionsField = typeof(Player).GetField("_minions", BindingFlags.Instance | BindingFlags.NonPublic);
            minionSlotsTakenField = typeof(Player).GetField("_minionSlotsTaken", BindingFlags.Instance | BindingFlags.NonPublic);
            maxMinions = player.maxMinions;
            LogManager.GetLogger("LukyMon").Info("Mod is running!");
        }

        public override void OnEnterWorld(Player player)
        {
            if (player != null)
            {
                lock (playerLock)
                {
                    this.player = player;
                    maxMinions = player.maxMinions;
                    shouldRestoreMinions = true;
                }
            }
        }

        public override void PostUpdate()
        {
            lock (playerLock)
            {
                if (player != null)
                {
                    if (!player.dead)
                    {
                        activeMinionIDs = GetActiveMinionIDs();
                    }
                    else
                    {
                        activeMinionIDs.Clear();
                    }

                    if (shouldRestoreMinions)
                    {
                        RestoreActiveMinions();
                        shouldRestoreMinions = false;
                    }
                }
            }
        }

        public override void OnRespawn(Player player)
        {
            LogManager.GetLogger("LukyMon").Info("Spawn!");
            lock (playerLock)
            {
                this.player = player;
                maxMinions = player.maxMinions;
                shouldRestoreMinions = true;
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
                LogManager.GetLogger("LukyMon").Info("Restore!");
            }
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
            LogManager.GetLogger("LukyMon").Info("SetSlot!");
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
            LogManager.GetLogger("LukyMon").Info("SetSlotTaken!");
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
            LogManager.GetLogger("LukyMon").Info("Get!");
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
                            
                            return i;
                        }
                    }
                }
            }
            return -1;
        }
    }
}

