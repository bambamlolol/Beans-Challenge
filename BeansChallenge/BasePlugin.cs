using System;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using MTM101BaldAPI.Registers;
using MTM101BaldAPI.OptionsAPI;
using MTM101BaldAPI.AssetTools;
using MTM101BaldAPI;

namespace BeansChallenge
{
    [BepInPlugin("bam.bam.baldiplus.beans", "The Beans Challenge", "1.0.0.0")]
    public class BasePlugin : BaseUnityPlugin
    {
		public WeightedNPC beans;
		public static AdjustmentBars QSLBar;
		public static MenuToggle CaptionsToggle;

		public static int QuarterSizeLimit = 5;
		public static BaseUnityPlugin instance;
		public static AssetManager assetMan = new AssetManager();
		void Awake()
		{
			Harmony harmony = new Harmony("bam.bam.baldiplus.beans");
			harmony.PatchAll();
			CustomOptionsCore.OnMenuInitialize += OnMen;
			GeneratorManagement.Register(this, GenerationModType.Finalizer, levelGenStuff);

			LoadingEvents.RegisterOnAssetsLoaded(optionsStuff, false);
			instance = this;
			assetMan.Add<Sprite>("titleGraphic", AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(this, new string[]
			{
				"BeanMenu_Low.png"
			}), 1f));
		}

		void levelGenStuff(string name, int id, LevelObject levelObject)
		{
			WeightedNPC beansW = new WeightedNPC();
			beansW.selection = NPCMetaStorage.Instance.Get(Character.Beans).value;
			levelObject.potentialNPCs.Clear();
			levelObject.additionalNPCs = 0;
			levelObject.potentialNPCs.Add(beansW);
			
			
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

			QSLBar = CustomOptionsCore.CreateAdjustmentBar(__instance, new Vector2(-92f, 0f), "Beans Amount", 10, "The Amount Of Beans", QuarterSizeLimit, () =>
			{
				QuarterSizeLimit = QSLBar.GetRaw();
			});
			// = CustomOptionsCore.CreateToggleButton(__instance, new Vector2(20, 10), "Comic Sans Caption", true, "Decides if captions show comic sans");
			//CaptionsToggle.Set(true);
			// attach everything to the options menu
			//CaptionsToggle.transform.SetParent(ob.transform, false);
			QSLBar.transform.SetParent(ob.transform, false);
			//QSLBar.transform.localScale = new Vector3(0.3f, 0.3f, 1);
		}
	}
}
