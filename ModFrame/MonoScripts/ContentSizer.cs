using System.Runtime.CompilerServices;
using UnityEngine;

[ExecuteInEditMode]
public class ContentSizer : MonoBehaviour
{
	public float Padding = 20f;

	public RectTransform ContainerRect;

	public RectTransform ContentRect;

	public ContentSizeAggregator ContentAggregator;

	private float calculatedHeight = 0f;

	public void FixedUpdate()
	{
		if (ContainerRect == null)
		{
			ContainerRect = base.transform as RectTransform;
		}
		if (ContentRect == null)
		{
			Transform transform = base.transform.Find("Content");
			if (transform != null)
			{
				ContentRect = base.transform.Find("Content") as RectTransform;
			}
		}
		if (ContentAggregator == null)
		{
			ContentAggregator = base.transform.parent.GetComponent<ContentSizeAggregator>();
		}
		if (ContentAggregator == null)
		{
			Debug.Log("Did not find content size aggregator!! " + base.transform.name);
			return;
		}
		calculatedHeight = ContentRect.rect.height + Padding;
		if (ContainerRect.rect.height != calculatedHeight)
		{
			ContainerRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, calculatedHeight);
			if (ContentAggregator != null)
			{
				ContentAggregator.ApplyUpdate();
			}
		}
	}

	public void OnDestroy()
	{
		if (ContentAggregator != null)
		{
			ContentAggregator.ApplyUpdate();
		}
	}
}
