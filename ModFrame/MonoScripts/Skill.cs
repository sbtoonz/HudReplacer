using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkillBarz
{
	public class Skill
	{
		internal Image Icon;

		public string Name;

		private readonly Action<Skill> OnSkillActivated;

		internal GameObject Prefab;

		public Skill(string name, Sprite icon, Action<Skill> activated)
		{
			Prefab = new GameObject("Skill", typeof(RectTransform));
			Name = name;
			Icon = Prefab.AddComponent<Image>();
			Icon.sprite = icon;
			float num = 0.01f;
			Icon.transform.localScale = new Vector3(num, num, num);
			EventTrigger eventTrigger = Prefab.AddComponent<EventTrigger>();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate
			{
				Activate();
			});
			eventTrigger.triggers.Add(entry);
			OnSkillActivated = activated;
		}

		public void Activate()
		{
			OnSkillActivated?.Invoke(this);
		}
	}
}
