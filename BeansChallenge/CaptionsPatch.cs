using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using TMPro;

namespace BeansChallenge
{
    [HarmonyPatch(typeof(SubtitleController))]
    [HarmonyPatch("Initialize")]
    class CaptionPatch
    {
        static void Postfix(SubtitleController __instance)
        {
            __instance.text.font = BasePlugin.GetResourceFromName<TMP_FontAsset>("COMIC_Pro");
        }
    }
}
