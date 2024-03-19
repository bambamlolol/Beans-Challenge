using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using System.Text;

namespace BeansChallenge
{
    [HarmonyPatch(typeof(EnvironmentController))]
    [HarmonyPatch("SpawnNPCs")]
    class Fuck
    {
        static bool Prefix(EnvironmentController __instance)
        {
            for (int i = 0; i < BasePlugin.QuarterSizeLimit; i++)
            {
                __instance.SpawnNPC(BasePlugin.GetResourceFromName<Beans>("Beans"), new IntVector2(0, 0));
            }
            __instance.SpawnNPC(BasePlugin.GetResourceFromName<Baldi>("Baldi"), new IntVector2(0, 0));
            return false;
        }
    }
}
