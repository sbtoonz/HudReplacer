using System.Runtime.CompilerServices;
using UnityEngine;

public static class UnityHelpers
{
	public static int DepthChecked;

	public static Transform RecursiveFind(this Transform transform, string search, bool caseInsensitive = true)
	{
		return transform.CheckChildren(search, caseInsensitive);
	}

	private static Transform CheckChildren(this Transform transform, string search, bool caseInsensitive = true)
	{
		Transform transform2 = null;
		if (IsEqual(transform.name, search, caseInsensitive))
		{
			transform2 = transform;
		}
		if (transform.childCount > 0 && transform2 == null)
		{
			DepthChecked++;
			foreach (Transform item in transform)
			{
				if (!(transform2 != null))
				{
					transform2 = item.CheckChildren(search, caseInsensitive);
				}
			}
		}
		return transform2;
	}

	public static bool IsEqual(string name, string otherName, bool caseInsensitive = true)
	{
		return caseInsensitive ? name.ToLower().Equals(otherName.ToLower()) : name.Equals(otherName);
	}
}
