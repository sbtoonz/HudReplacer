using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GDPower : MonoBehaviour
{
	internal static Sprite GuardianSprite;

	internal static string CooldownTxt;

	internal static string GDName;

	internal static string GDKey;

	internal static float rawcooldown;

	internal static bool Gpowerindicator;

	public Image GuardianIcon;

	public Text CooldownText;

	public Text ActivationKey;

	public Text PowerName;

	public GameObject rootObj;

	private void LateUpdate()
	{
		SetInfo();
	}

	internal void SetInfo()
	{
		if (Gpowerindicator)
		{
			rootObj.SetActive(true);
			GuardianIcon.sprite = GuardianSprite;
			CooldownText.text = CooldownTxt;
			PowerName.text = GDName;
			ActivationKey.text = GDKey;
			if (rawcooldown <= 0f)
			{
				CooldownText.text = "READY";
			}
		}
		else
		{
			rootObj.SetActive(false);
		}
	}
}
