using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class C_BtnSU : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        MainGame.instance.SpeedUp(true);
        MainGame.instance.isSta = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MainGame.instance.SpeedUp(false);
        MainGame.instance.isSta = false;
    }
}
