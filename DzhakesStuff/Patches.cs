﻿using UnityEngine;
using RogueLibsCore;

namespace DzhakesStuff
{
    public static class Patches
    {
        [RLSetup]
        public static void Setup()
        {
            RoguePatcher patcher = new RoguePatcher(Core.Instance) { TypeWithPatches = typeof(Patches) };
            patcher.Prefix(typeof(AnalyticsFunctions),nameof(AnalyticsFunctions.SendData));
            patcher.Prefix(typeof(LoadLevel), nameof(LoadLevel.HomeBaseAgentSpawns));
            patcher.Postfix(typeof(LoadLevel), "SetupMore5_2");
        }

        //Don't send my data
        public static bool AnalyticsFunctions_SendData() => false;

        public static void LoadLevel_HomeBaseAgentSpawns()
        {
            for (float i = 88f; i < 95f; i++)
            {
                Demolish(76f, i); // Hacker
            }

            Demolish(64f, 54f); // Safe
            Demolish(84f, 66f); // Zombie
        }

        private static void Demolish(float x, float y)
        {
            GameController gc = GameController.gameController;
            Vector2 pos = new Vector2(x, y) * 0.64f;
            gc.tileInfo.DestroyWallTileAtPosition(pos.x, pos.y, true, gc.playerAgent);
            InvItem item = new InvItem { invItemName = "Wreckage" };
            item.SetupDetails(false);
            item.LoadItemSprite("WallBorderWreckage1");
            gc.spawnerMain.SpawnWreckage(pos, item, null, null, false).wallType = "WallBorder";
            item.LoadItemSprite("WallBorderWreckage2");
            gc.spawnerMain.SpawnWreckage(pos, item, null, null, false).wallType = "WallBorder";
            item.LoadItemSprite("WallBorderWreckage3");
            gc.spawnerMain.SpawnWreckage(pos, item, null, null, false).wallType = "WallBorder";
        }

        public static void LoadLevel_SetupMore5_2()
        {
            ChronomanticDilation.baseTimeScale = GameController.gameController.selectedTimeScale;
        }
    }
}
