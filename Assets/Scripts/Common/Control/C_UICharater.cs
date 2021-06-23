using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_UICharater : MonoBehaviour
{
    [SerializeField] GameObject topUI = null;
    [SerializeField] Sprite enemyHp = null;
    [SerializeField] Image Hp = null;
    [SerializeField] Image Ep = null;

    [SerializeField] float anim = 0.5f;
    public float hp = 0.0f;
    public float ep = 0.0f;

    float curHp = 0.345f;
    float curEp = 0.345f;

    [SerializeField] GameObject fullMana = null;
    [SerializeField] GameObject fullManaUi = null;
    [SerializeField] GameObject txtHPPref;
    private GameObject hpUI = null;

    private void OnEnable()
    {
        if (curHp != hp) ChangeHp();
        if (curEp != ep) ChangeEp();
    }

    private void Update()
    {
        if (curHp != hp) ChangeHp();
        if (curEp != ep) ChangeEp();
    }

    public void ChangeHp()
    {
        float dis = hp - curHp;
        if (dis < 0) dis *= -1;
        curHp = Mathf.Lerp(curHp, hp, Time.deltaTime * anim / dis);
        Hp.fillAmount = curHp;


    }

    public void ChangeEp()
    {
        float dis = ep - curEp;
        if (dis < 0) dis *= -1;
        curEp = Mathf.Lerp(curEp, ep, Time.deltaTime * anim / dis);
        Ep.fillAmount = curEp;

        fullMana.SetActive(curEp >= 1);
        fullManaUi.SetActive(curEp >= 1);

        if (av) av.GetComponent<Button>().interactable = curEp >= 1;
    }

    bool isEnemy = false;
    C_Avatar av;

    public void Set(bool isEnemy, C_Avatar av)
    {
        this.av = av;
        if (isEnemy == this.isEnemy) return;

        this.isEnemy = isEnemy;

        if (isEnemy)
        {
            Vector3 scl = gameObject.transform.localScale;
            scl.x *= -1;
            gameObject.transform.localScale = scl;

            Hp.sprite = enemyHp;
        }
    }

    public void Show(bool isShow)
    {
        topUI.SetActive(isShow);
        fullMana.SetActive(isShow);

        if (av && !isShow) av.GetComponent<Button>().interactable = false;
    }

    public void ShowAnimHp(int vlHP)
    {
        if (hpUI != null)
        {
            Destroy(hpUI);
        }

        hpUI = Instantiate(txtHPPref, this.gameObject.transform);
        hpUI.GetComponent<Text>().text = vlHP.ToString();
    }
}
