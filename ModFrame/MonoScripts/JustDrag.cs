using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class JustDrag : MonoBehaviour
{
	[SerializeField]
	[CanBeNull]
	private RectTransform dragRect;

	public void OnDrag(PointerEventData eventData)
	{
		dragRect.anchoredPosition += eventData.delta * 2f;
	}
}
