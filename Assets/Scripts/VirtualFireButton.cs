using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VirtualFireButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public Sprite StartIm;
    public Sprite AltIm;
    // private SpriteRenderer spriteRenderer;
    private Image spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<Image>();
    }


    public bool ButtonPress { set; get;}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		ButtonPress = true;
        spriteRenderer.sprite = AltIm;
    }
	
	public virtual void OnPointerUp(PointerEventData ped)
	{
		ButtonPress = false;
        spriteRenderer.sprite = StartIm;
    }

}
