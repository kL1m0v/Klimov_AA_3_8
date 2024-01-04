using UnityEngine;
using UnityEngine.EventSystems;
using Ziggurat;

public class OpeningCanvas : MonoBehaviour, IPointerClickHandler
{
	
	[SerializeField] private Canvas _canvas;
	
	public void OnPointerClick(PointerEventData eventData)
	{
		foreach(Canvas canvas in Collections.canvases)
		{
			if(canvas != _canvas)
			{
				canvas.enabled = false;
			}
		}
		_canvas.enabled = _canvas.enabled ? false : true;
	}
	
	
}
