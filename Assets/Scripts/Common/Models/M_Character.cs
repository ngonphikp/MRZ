using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class M_Character
{
    public string id;

    public int maxHP;
    public int maxEP = 100;
    public int attack;
    public int lv = 1;

    public float speedRun = 1.0f;

    int curHP;
    int curEP = 0;

    public int position;

    public bool isEnemy;

    public C_Enum.Mode mode = C_Enum.Mode.Default;

    public int CurHP
    {
        get => curHP; set
        {
            if (value < 0) value = 0; if (value > maxHP) value = maxHP;
            curHP = value;
        }
    }

    public int CurEP
    {
        get => curEP; set
        {
            if (value < 0) value = 0; if (value > maxEP) value = maxEP;
            curEP = value;
        }
    }

    public void Update()
    {
        maxHP = ResourceManager.instance.dicMCT[id].maxHP;
        attack = ResourceManager.instance.dicMCT[id].attack;
        speedRun = ResourceManager.instance.dicMCT[id].speedRun;

        for (int i = 1; i < lv; i++)
        {
            maxHP = (int)(maxHP * 1.2f);

            attack = (int)(attack * 1.2f);
            speedRun = speedRun * 1.1f;
        }

        if (mode == C_Enum.Mode.Boss)
        {
            maxHP = (int)(maxHP * 1.5f);

            attack = (int)(attack * 1.5f);
        }

        curHP = maxHP;
    }
}
