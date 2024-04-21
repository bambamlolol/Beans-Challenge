using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BeansChallenge
{
    [HarmonyPatch(typeof(MainMenu))]
    [HarmonyPatch("Start")]
    class TitleScreen
    {
        public static void Postfix(MainMenu __instance)
        {
            foreach (RectTransform asset in __instance.GetComponentsInChildren<RectTransform>())
            {
                switch (asset.name)
                {
                    case "Image":
                        asset.GetComponent<Image>().sprite = BasePlugin.assetMan.Get<Sprite>("titleGraphic");
                        break;
                    case "Version":
                        asset.GetComponent<TextMeshProUGUI>().text = "V2.0.0";
                        break;
                    case "Copyright":
                        asset.GetComponent<TextMeshProUGUI>().text = "Created by BamBamLol";
                        break;
                }
            }
        }
    }
}
