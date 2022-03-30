using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace SkillBarz
{
	public class SkillBar : MonoBehaviour
	{
		public static SkillBar Instance;

		public GameObject[] SkillSlots = new GameObject[0];

		private readonly Skill[] Skills = new Skill[8];

		private void Awake()
		{
			Instance = this;
		}

		public void AddSkill(Skill skill, int slot)
		{
			Skills[slot] = skill;
			SkillSlots[slot].GetComponent<Image>().sprite = skill.Icon.sprite;
			skill.Prefab.transform.SetParent(SkillSlots[slot].transform, false);
		}
	}
}
