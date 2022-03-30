using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class FoodList : MonoBehaviour
{
	internal static List<Player.Food> foods = new List<Player.Food>();

	public Image[] FoodIcon = new Image[3];

	public Text[] FoodBurn = new Text[3];

	public Image[] Cooldown = new Image[3];

	private void LateUpdate()
	{
		if (!(Player.m_localPlayer == null))
		{
			FoodGrabber();
		}
	}

	public static string TimeFomat(int secs)
	{
		int num = secs % 3600 / 60;
		secs %= 60;
		return $"{num:00}:{secs:00}";
	}

	internal void FoodGrabber()
	{
		foods = Player.m_localPlayer.GetFoods();
		for (int i = 0; i < Hud.instance.m_foodBars.Length; i++)
		{
			Image image = FoodIcon[i];
			Text text = FoodBurn[i];
			Image image2 = Cooldown[i];
			if (i < foods.Count)
			{
				Player.Food food = foods[i];
				image.gameObject.SetActive(true);
				image.sprite = food.m_item.GetIcon();
				float num = food.m_health / food.m_item.m_shared.m_food;
				text.text = TimeFomat(Mathf.CeilToInt(num * food.m_item.m_shared.m_foodBurnTime));
				image2.fillAmount = num;
				image.color = (food.CanEatAgain() ? new Color(1f, 1f, 1f, 0.6f + Mathf.Sin(Time.time * 10f) * 0.4f) : Color.white);
				text.gameObject.SetActive(true);
			}
			else
			{
				image.gameObject.SetActive(false);
			}
		}
	}
}
