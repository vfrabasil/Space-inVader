using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class VirtualJoystick : MonoBehaviour , IDragHandler, IPointerUpHandler, IPointerDownHandler
{
	private Image bgImg;
	private Image joystickImg;

	public Vector2 InputDirection { set; get;}

  //  private Vector2 input = Vector2.zero;
    public float Horizontal { get { return InputDirection.x; } }
    public float Vertical { get { return InputDirection.y; } }

    private void Start()
	{
		bgImg = GetComponent<Image>();
		joystickImg = transform.GetChild(0).GetComponent<Image> ();

		InputDirection = Vector2.zero;
			
	}

	public virtual void OnDrag(PointerEventData ped)
	{
		Vector2 pos = Vector2.zero;

		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (
			bgImg.rectTransform,
			ped.position,
			ped.pressEventCamera,
			out pos)) 
		{
			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

			float x = (bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1 ;
			float y = (bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1 ;

			InputDirection = new Vector2(x, y);

			InputDirection = (InputDirection.magnitude > 1 ) ? InputDirection.normalized : InputDirection;

			joystickImg.rectTransform.anchoredPosition = 
				new Vector2(InputDirection.x * (bgImg.rectTransform.sizeDelta.x / 3),
				            InputDirection.y * (bgImg.rectTransform.sizeDelta.y / 3) );

		}


	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		OnDrag (ped);		
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		InputDirection = Vector2.zero;
		joystickImg.rectTransform.anchoredPosition = Vector2.zero;
		
	}





}
