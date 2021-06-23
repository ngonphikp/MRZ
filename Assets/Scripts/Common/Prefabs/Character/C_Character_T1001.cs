using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Character_T1001 : A_Character
{
    [Header("Anim3")]
    [SerializeField] float time3d0 = 0.5f;
    [SerializeField] float time3db = 0.1f;
    [SerializeField] C_Bullet bl3d0 = null;
    [SerializeField] Transform posB3 = null;

    protected override IEnumerator<float> _Anim2()
    {
        yield break;
    }

    protected override IEnumerator<float> _Anim3()
    {
        C_LibSkill.Shoot(ctl, bl3d0, posB3, true, time3d0, time3db, ctl.target);
        SoundManager.instance.PlayOneShot(ResourceManager.instance.LoadAudioClip("Sounds/Hero_Sfx/GunShotSnglShotIn"));
        yield break;
    }

    protected override IEnumerator<float> _Anim4()
    {
        yield break;
    }

    protected override IEnumerator<float> _Anim5()
    {
        C_LibSkill.Shoot(ctl, bl3d0, posB3, true, time3d0, time3db, ctl.target);
        SoundManager.instance.PlayOneShot(ResourceManager.instance.LoadAudioClip("Sounds/Hero_Sfx/GunShotSnglShotIn"));
        yield break;

    }
}
