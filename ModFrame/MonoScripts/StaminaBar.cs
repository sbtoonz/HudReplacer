using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{
	public static float CurrStamina;

	public static float MaxStamina;

	[SerializeField]
	private Slider m_StaminaSlider;

	[SerializeField]
	private Text m_StaminaText;

	private void OnGUI()
	{
		GetStamina();
	}

	private void GetStamina()
	{
		m_StaminaSlider.maxValue = (int)Math.Round(MaxStamina);
		m_StaminaSlider.value = (int)Math.Round(CurrStamina);
		m_StaminaText.text = m_StaminaSlider.value.ToString();
	}
}
