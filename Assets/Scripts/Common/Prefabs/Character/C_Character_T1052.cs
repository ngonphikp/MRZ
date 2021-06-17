using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Character_T1052 : A_Character
{
    [Header("Anim3")]
    [SerializeField] float time3d0 = 0.5f;
    [SerializeField] float time3db = 0.1f;
    [SerializeField] C_Bullet bl3d0 = null;
    [SerializeField] Transform posB3 = null;

    [Header("Anim5")]
    [SerializeField] float[] time5ds = null;
    [SerializeField] float time5db = 0.1f;
    [SerializeField] C_Bullet bl5d0 = null;
    [SerializeField] Transform posB5 = null;
    [SerializeField] float time5d1 = 1.0f;
    [SerializeField] C_Bullet bl5d1 = null;

    protected override IEnumerator<float> _Anim2()
    {
        yield break;
    }

    protected override IEnumerator<float> _Anim3()
    {
        C_LibSkill.Shoot(ctl, bl3d0, posB3, true, time3d0, time3db, ctl.target);
        yield break;
    }

    protected override IEnumerator<float> _Anim4()
    {
        yield break;
    }

    protected override IEnumerator<float> _Anim5()
    {
        for (int i = 0; i < time5ds.Length; i++)
        {
            C_LibSkill.Shoot(ctl, bl5d0, posB5, true, time5ds[i], time5db, ctl.target);
        }
        C_LibSkill.Shoot(ctl, bl5d1, posB5, true, time5d1, time5db, ctl.target);

        yield break;
    }
}
