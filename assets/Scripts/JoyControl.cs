using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OSX;
using Unity.VisualScripting.FullSerializer;

public class JoyControl : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Image _bgImg;
    [SerializeField]
    private Image _joyImg;

    public Vector3 InputMove;

    private void Start()
    {
        _bgImg = GetComponent<Image>();
        _joyImg = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(_bgImg.rectTransform,ped.position,ped.pressEventCamera,out pos)) // Identificar a entrada do toque na area
        {
            pos.x = (pos.x / _bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / _bgImg.rectTransform.sizeDelta.y);

            InputMove = new Vector3(pos.x * 2, pos.y *2);

            InputMove = (InputMove.magnitude > 1) ? InputMove.normalized : InputMove; // Limitar a image de movimento

            _joyImg.rectTransform.anchoredPosition = new Vector3(InputMove.x * (_bgImg.rectTransform.sizeDelta.x / 2.5f), InputMove.y * (_bgImg.rectTransform.sizeDelta.y / 2.5f)); // Movimento da Image
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        InputMove = Vector3.zero;
        _joyImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float MoveHorizontal()
    {
        if(InputMove.x != 0)
        {
            return InputMove.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public float MoveVertical()
    {
        if(InputMove.y != 0)
        {
            return InputMove.y;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }
}
