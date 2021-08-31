using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace HudReplacer
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class HudReplacer : BaseUnityPlugin
    {
        internal const string ModName = "HudReplacer";
        internal const string ModVersion = "2.0";
        private const string ModGUID = "com.zarboz.hudreplacer";

        private static GameObject NewHud;
        private static AssetBundle hudbundle;
        private static GameObject HudSwap;
        private static ConfigEntry<Vector3> HudLocation;
        private static ConfigEntry<Color> HealthBG;
        private static ConfigEntry<Color> HealthHotline;
        private static ConfigEntry<Color> UnfilledBG;
        private Vector3 barPostion;
        private static readonly int MainColor = Shader.PropertyToID("_MainColor");
        private static readonly int HotlineColor = Shader.PropertyToID("_HotLineColor");
        private static readonly int UnfilledColor = Shader.PropertyToID("_AlphaColor");

        public void Awake()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Harmony harmony = new(ModGUID);
            harmony.PatchAll(assembly);
            LoadAssets();
            HudLocation = Config.Bind("General", "SkillBar Location", barPostion,
                new ConfigDescription("Location of SkillBar"));
            HealthBG = Config.Bind("General", "SkillBar Health Background", new Color(0.8679f, 0f, 0.0281f, 1f),
                "The Color of the Healthbar filler");
            HealthHotline = Config.Bind("General", "SkillBar Health Hotline Color", new Color(0f, 0.7053f, 1f, 1f),
                "The Color of the Healthbar Hotline or the burn line at the top of the health when it is low or filling");
            UnfilledBG = Config.Bind("General", "SkillBar Health Unfilled Color", new Color(0f, 0f, 0f, 0f),
                "The Color of the unfilled portion of the Healthbar background");
        }

        public void Update()
        {
            if (Player.m_localPlayer == null)
                return;
            HealthBar.CurHealthValue = Player.m_localPlayer.GetHealth();
            HealthBar.MaxHealthValue = Player.m_localPlayer.GetMaxHealth(); 
            GuardianHUD.GetGuardianPower();
            if (Draggable.isMouseDown)
            {
                barPostion = HudSwap.transform.localPosition;
                HudLocation.Value = barPostion;
                Config.Save();
            }

        }
        

        public void LoadAssets()
        {
            hudbundle = GetAssetBundleFromResources("stockreplacer");
            NewHud = hudbundle.LoadAsset<GameObject>("StockReplacer");
        }
        private static AssetBundle GetAssetBundleFromResources(string filename)
        {
            var execAssembly = Assembly.GetExecutingAssembly();
            var resourceName = execAssembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(filename));

            using (var stream = execAssembly.GetManifestResourceStream(resourceName))
            {
                return AssetBundle.LoadFromStream(stream);
            }
        }
        
        [HarmonyPatch(typeof(Hud), nameof(Hud.Awake))]
        public static class HUDPatch
        {


            public static void Postfix(Hud __instance)
            {
                __instance.m_gpIcon.gameObject.SetActive(false);
                __instance.m_gpCooldown.gameObject.SetActive(false);
                __instance.m_gpName.gameObject.SetActive(false);
                __instance.m_foodBarRoot.gameObject.SetActive(false);
                __instance.m_healthPanel.gameObject.SetActive(false);
                HudSwap.transform.SetSiblingIndex(__instance.m_gpRoot.transform.GetSiblingIndex());
                HealthBar.coolmatstatic.SetColor(MainColor, HealthBG.Value);
                HealthBar.coolmatstatic.SetColor(HotlineColor, HealthHotline.Value);
                HealthBar.coolmatstatic.SetColor(UnfilledColor, UnfilledBG.Value);
              }

            public static void Prefix(Hud __instance)
            {
                HudSwap = Instantiate(NewHud, __instance.m_rootObject.transform, false); 
                __instance.m_gpRoot.transform.Find("Bkg").gameObject.SetActive(false);
                HudSwap.transform.localPosition = HudLocation.Value;
            }
        }
    }
}