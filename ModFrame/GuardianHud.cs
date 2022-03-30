using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace HudReplacer
{
    public class GuardianHUD
    {
        private static StatusEffect _guardianPower;
        private static float _cooldown;
        [CanBeNull] internal static Localize _localize;
        internal string GP_text;
        

        internal static void GetGuardianPower()
        {
            Player.m_localPlayer.GetGuardianPowerHUD(out _guardianPower, out _cooldown);
            if (_guardianPower == null)
            {
                GDPower.Gpowerindicator = false;
            }
            else if (_guardianPower != null)
            {
                GDPower.Gpowerindicator = true;
                GDPower.GuardianSprite = _guardianPower.m_icon;
                GDPower.CooldownTxt = TimeFomat(Mathf.CeilToInt(_cooldown));
                GDPower.GDName = Localization.instance.Localize(_guardianPower.m_name);
                GDPower.GDKey = Localization.instance.Localize("$KEY_GP");
                GDPower.rawcooldown = _cooldown;
            }
        }
        
        public static string TimeFomat(int secs)
        {
            int mins = (secs % 3600) / 60;
            secs = secs % 60;
            return $"{mins:00}:{secs:00}";
        }
    }
}