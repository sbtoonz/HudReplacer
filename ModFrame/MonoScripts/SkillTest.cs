using System.Runtime.CompilerServices;
using UnityEngine;

namespace SkillBarz
{
	public class SkillTest : MonoBehaviour
	{
		public Sprite[] TestSprites = new Sprite[0];

		private void Start()
		{
			for (int i = 0; i < TestSprites.Length; i++)
			{
				Skill skill = new Skill($"Skill No{i}", TestSprites[i], delegate(Skill activated)
				{
					Debug.Log("Activated " + activated.Name);
				});
				SkillBar.Instance.AddSkill(skill, i);
			}
		}
	}
}
