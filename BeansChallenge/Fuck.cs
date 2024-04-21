using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using System.Text;
using MTM101BaldAPI.Registers;

namespace BeansChallenge
{
    [HarmonyPatch(typeof(BaseGameManager))]
    [HarmonyPatch("Start")]
    class Fuck
    {
        static void Prefix(BaseGameManager __instance)
        {
            for (int i = 0; i < (BasePlugin.QuarterSizeLimit * 10); i++)
            {
                __instance.levelObject.forcedNpcs = __instance.levelObject.forcedNpcs.AddToArray(NPCMetaStorage.Instance.Get(Character.Beans).value);
            }
        }
    }
}
