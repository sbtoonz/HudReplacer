using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	public Transform target;

	public bool shouldReturn;

	internal static bool isMouseDown;

	private Vector3 startMousePosition;

	private Vector3 startPosition;

	internal static bool donedrag;

	private void Update()
	{
		if (isMouseDown)
		{
			Vector3 mousePosition = Input.mousePosition;
			Vector3 vector = mousePosition - startMousePosition;
			Vector3 position = startPosition + vector;
			target.position = position;
		}
	}

	public void OnPointerDown(PointerEventData dt)
	{
		isMouseDown = true;
		donedrag = isMouseDown;
		Debug.Log("Draggable Mouse Down");
		startPosition = target.position;
		startMousePosition = Input.mousePosition;
	}

	public void OnPointerUp(PointerEventData dt)
	{
		Debug.Log("Draggable mouse up");
		isMouseDown = false;
		donedrag = isMouseDown;
		if (shouldReturn)
		{
			target.position = startPosition;
		}
	}
}
