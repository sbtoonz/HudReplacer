using System.Runtime.CompilerServices;
using UnityEngine;

[ExecuteInEditMode]
public class ContentSizeAggregator : MonoBehaviour
{
	public float Padding = 20f;

	public RectTransform ContainerRect;

	private float calculatedHeight = 0f;

	public void ApplyUpdate()
	{
		if (ContainerRect == null)
		{
			ContainerRect = base.transform as RectTransform;
		}
		calculatedHeight = 0f;
		foreach (RectTransform item in base.transform)
		{
			if (item.gameObject.activeSelf)
			{
				if (item.anchoredPosition.y != 0f - calculatedHeight)
				{
					item.anchoredPosition = new Vector2(0f, 0f - calculatedHeight);
				}
				calculatedHeight += item.rect.height;
			}
		}
		if (ContainerRect.rect.height != calculatedHeight)
		{
			ContainerRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, calculatedHeight + Padding);
		}
	}
}
