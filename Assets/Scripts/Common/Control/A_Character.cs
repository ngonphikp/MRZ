using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;

public abstract class A_Character : MonoBehaviour, I_Character
{
    [Header("Anim2")]
    [SerializeField]
    protected float timeAn2 = 0.0f;

    [Header("Anim3")]
    [SerializeField]
    protected float timeAn3 = 0.0f;

    [Header("Anim4")]
    [SerializeField]
    protected float timeAn4 = 0.0f;

    [Header("Anim5")]
    [SerializeField]
    protected float timeAn5 = 0.0f;

    protected bool isPlay = true;
    public bool IsPlay() { return isPlay; }

    protected C_Character ctl;

    private void Awake()
    {
        ctl = GetComponent<C_Character>();
    }

    public void Play(int anim)
    {
        switch (anim)
        {
            case 2:
                Anim2();
                break;
            case 3:
                Anim3();
                break;
            case 4:
                Anim4();
                break;
            case 5:
                Anim5();
                break;
            case 6:
                Anim6();
                break;
            case 7:
                Anim7();
                break;
        }
    }
    protected abstract IEnumerator<float> _Anim2();
    protected abstract IEnumerator<float> _Anim3();
    protected abstract IEnumerator<float> _Anim4();
    protected abstract IEnumerator<float> _Anim5();

    private void Anim2() { Timing.RunCoroutine(_Anim(2, timeAn2, _Anim2)); }
    private void Anim3() { Timing.RunCoroutine(_Anim(3, timeAn3, _Anim3)); }
    private void Anim4() { Timing.RunCoroutine(_Anim(4, timeAn4, _Anim4)); }
    private void Anim5() { Timing.RunCoroutine(_Anim(5, timeAn5, _Anim5)); }

    private void Anim6() { }
    private void Anim7() { }

    private IEnumerator<float> _Anim(int i, float time, Func<IEnumerator<float>> _action)
    {
        //Debug.Log("Anim: " + i);
        isPlay = false;

        if (_action != null) yield return Timing.WaitUntilDone(Timing.RunCoroutine(_action()));

        yield return Timing.WaitForSeconds(time / ctl.speed);
        isPlay = true;
    }
}
