using System.IO;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using MTM101BaldAPI.Registers;
using MTM101BaldAPI.OptionsAPI;
using MTM101BaldAPI;

namespace BeansChallenge
{
    [BepInPlugin("bam.bam.baldiplus.beans", "The Beans Challenge", "1.0.0.0")]
    public class BasePlugin : BaseUnityPlugin
    {
		public WeightedNPC beans;
		public static AdjustmentBars QSLBar;

		public static int QuarterSizeLimit = 5;
		void Awake()
		{
			Harmony harmony = new Harmony("bam.bam.baldiplus.beans");
			harmony.PatchAll();
			CustomOptionsCore.OnMenuInitialize += OnMen;
			GeneratorManagement.Register(this, GenerationModType.Finalizer, levelGenStuff);

			LoadingEvents.RegisterOnAssetsLoaded(optionsStuff, false);
		}

		void levelGenStuff(string name, int id, LevelObject levelObject)
		{
			levelObject.potentialNPCs.RemoveAll(x => x.selection.Character == Character.Baldi);
			levelObject.potentialNPCs.RemoveAll(a => a.selection.Character == Character.Playtime);
			levelObject.potentialNPCs.RemoveAll(b => b.selection.Character == Character.Crafters);
			levelObject.potentialNPCs.RemoveAll(c => c.selection.Character == Character.Bully);
			levelObject.potentialNPCs.RemoveAll(d => d.selection.Character == Character.Prize);
			levelObject.potentialNPCs.RemoveAll(e => e.selection.Character == Character.Cumulo);
			levelObject.potentialNPCs.RemoveAll(f => f.selection.Character == Character.Sweep);
			levelObject.potentialNPCs.RemoveAll(h => h.selection.Character == Character.Chalkles);
			levelObject.potentialNPCs.RemoveAll(i => i.selection.Character == Character.Pomp);
			levelObject.potentialNPCs.RemoveAll(j => j.selection.Character == Character.LookAt);
			levelObject.potentialNPCs.RemoveAll(k => k.selection.Character == Character.DrReflex);
		}

		public static T GetResourceFromName<T>(string name) where T : UnityEngine.Object
		{
			// Does a simple linear search based on the resource name
			foreach (T asset in Resources.FindObjectsOfTypeAll<T>())
			{
				// Stops the loop and returns the asset given
				if (asset.name == name)
				{
					return asset;
				}
			}

			// Returns null if the resource with that name does not exist.
			return null;
		}

		void optionsStuff()
        {
		}

		void OnMen(OptionsMenu __instance)
		{
			if (Singleton<CoreGameManager>.Instance != null) return; // these settings can only be changed when an active game is NOT going
			GameObject ob = CustomOptionsCore.CreateNewCategory(__instance, "Beans Challenge");

			QSLBar = CustomOptionsCore.CreateAdjustmentBar(__instance, new Vector2(-92f, 0f), "Beans Amount", 5, "The Amount Of Beans", QuarterSizeLimit, () =>
			{
				QuarterSizeLimit = QSLBar.GetRaw() * 10;
			});
			// attach everything to the options menu
			QSLBar.transform.SetParent(ob.transform, false);
		}
	}
}
