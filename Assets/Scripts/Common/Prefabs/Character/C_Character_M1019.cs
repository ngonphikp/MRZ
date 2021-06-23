using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Character_M1019 : A_Character
{
    [Header("Anim2")]
    [SerializeField] float time4d0 = 0.8f;
    //[SerializeField] GameObject fx4d0 = null;

    [Header("Anim3")]
    [SerializeField] float time5d0 = 0.8f;
    //[SerializeField] GameObject fx5d0 = null;

    protected override IEnumerator<float> _Anim2()
    {
        yield break;
    }

    protected override IEnumerator<float> _Anim3()
    {
        // C_LibSkill.Hit(ctl, fx4d0, time4d0, 0.0f, ctl.target);

        yield break;
    }

    protected override IEnumerator<float> _Anim4()
    {
        yield break;
    }

    protected override IEnumerator<float> _Anim5()
    {
        // C_LibSkill.Hit(ctl, fx5d0, time5d0, 0.0f, ctl.target);

        yield break;
    }
}
