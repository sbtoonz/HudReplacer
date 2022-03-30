using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace HudReplacer
{
	public class HealthBar : MonoBehaviour
	{
		public static float CurHealthValue;

		public static float MaxHealthValue;

		[SerializeField]
		[CanBeNull]
		private Slider HealthSlider;

		[SerializeField]
		[CanBeNull]
		private Text HealthText;

		[SerializeField]
		private Material CoolMat;

		[SerializeField]
		private GameObject FillObj;

		internal static Material coolmatstatic;

		private void OnGUI()
		{
			SetSliders();
		}

		private void Awake()
		{
			coolmatstatic = CoolMat;
		}

		private void SetSliders()
		{
			HealthText.text = Math.Ceiling(CurHealthValue).ToString();
			float value = Map(Mathf.Ceil(CurHealthValue), 1f, Mathf.Ceil(MaxHealthValue), 0f, 1f);
			CoolMat.SetFloat("_FillLevel", value);
			FillObj.GetComponent<Image>().material.SetFloat("_FillLevel", value);
		}

		private static float Map(float value, float fromLow, float fromHigh, float toLow, float toHigh)
		{
			return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
		}
	}
}
