using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Stamina : MonoBehaviour
{
    [SerializeField] Image image = null;
    [SerializeField] float anim = 0.5f;

    public float Sta
    {
        get => sta; set
        {
            if (value < 0.0f) value = 0.0f; if (value > 1.0f) value = 1.0f;
            sta = value;
        }
    }

    float sta = 0.0f;

    float curSta = 0.01f;

    private void OnEnable()
    {
        if (curSta != sta) ChangeSta();
    }

    private void Update()
    {
        if (curSta != sta) ChangeSta();
    }

    private void ChangeSta()
    {
        float dis = sta - curSta;
        if (dis < 0) dis *= -1;
        curSta = Mathf.Lerp(curSta, sta, Time.deltaTime * anim / dis);
        image.fillAmount = curSta;
    }
}
