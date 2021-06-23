using MEC;
using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;
using NPS;
using System;
using UnityEngine.UI;

[System.Serializable]
public class Pooling_C_Character : Pooling<C_Character> { }

public class C_Character : MonoBehaviour
{
    [SerializeField] Animator anim = null;
    [SerializeField] C_Radar radar = null;
    [SerializeField] C_UICharater UICharater = null;

    public M_Character character = new M_Character();
    public bool isLive = true;
    public float speed { get; set; } = 1.0f;

    [SerializeField] bool isAttack = false;
    [SerializeField] bool isFind = false;
    [SerializeField] bool isRun = false;
    [SerializeField] Status status = Status.Find;
    public bool isAutoUlti = true;
    public bool isUlti = false;

    [SerializeField] float radius = 1.0f;
    [SerializeField] Vector3 direction = Vector3.left;

    public C_Character target = null;
    public Transform posHit = null;

    CoroutineHandle AutoAttack;
    CoroutineHandle AutoRun;
    CoroutineHandle AutoFind;

    I_Character ctl = null;

    SkeletonAnimation sa = null;
    Image img = null;

    private void Awake()
    {
        ctl = this.GetComponent<I_Character>();
        sa = anim.gameObject.GetComponent<SkeletonAnimation>();
        img = anim.gameObject.GetComponent<Image>();
    }

    Status oldStatus;
    private void Start()
    {
        oldStatus = status;

        AutoAttack = Timing.RunCoroutine(_AutoAttack());
        AutoRun = Timing.RunCoroutine(_AutoRun());
        AutoFind = Timing.RunCoroutine(_AutoFind());
    }

    Action callback;
    float oldSpeedRun;
    public void Set(M_Character character, Action callback = null, C_Avatar av = null)
    {
        this.character = character;
        this.callback = callback;
        oldSpeedRun = character.speedRun;

        status = oldStatus;
        UICharater.Show(true);

        CircleCollider2D cc = GetComponent<CircleCollider2D>();
        if (cc != null) cc.enabled = true;

        CapsuleCollider2D ca = GetComponent<CapsuleCollider2D>();
        if (ca != null) ca.enabled = true;

        UICharater.hp = character.CurHP * 1.0f / character.maxHP;
        UICharater.ep = character.CurEP * 1.0f / character.maxEP;

        UICharater.ChangeHp();
        UICharater.ChangeEp();

        isLive = character.CurHP != 0;

        UICharater.Set(character.isEnemy, av);

        float size = character.mode == C_Enum.Mode.Boss ? 1.35f : 1.0f;
        this.gameObject.transform.localScale = new Vector3(character.isEnemy ? -size : size, size, 1.0f);
    }

    private IEnumerator<float> _AutoFind()
    {
        while (true)
        {
            if (ctl == null) break;

            if (ctl.IsPlay() && status == Status.Find)
            {
                Target tg = radar.FindMinTarget();
                if (tg == null)
                {
                    Vector3 finish = transform.position + direction;
                    float step = character.speedRun * Time.deltaTime * speed;
                    transform.position = Vector3.MoveTowards(transform.position, finish, step);
                }
                else if (isRun) status = Status.Run;
            }
            yield return Timing.WaitForOneFrame;
        }
    }

    private IEnumerator<float> _AutoRun()
    {
        while (true)
        {
            if (ctl == null) break;

            if (ctl.IsPlay() && status == Status.Run)
            {
                Target tg = radar.FindMinTarget();
                if (tg != null)
                {
                    if (tg.dis > radius)
                    {
                        float step = character.speedRun * Time.deltaTime * speed;
                        transform.position = Vector3.MoveTowards(transform.position, tg.ctl.gameObject.transform.position, step);
                    }
                    else if (isAttack) status = Status.Attack;
                }
                else if (isFind) status = Status.Find;
            }
            yield return Timing.WaitForOneFrame;
        }
    }

    private IEnumerator<float> _AutoAttack()
    {
        while (true)
        {
            if (ctl == null) break;

            if (ctl.IsPlay() && status == Status.Attack)
            {
                Target tg = radar.FindMinTarget();
                if (tg != null)
                {
                    if (tg.dis <= radius)
                    {
                        target = tg.ctl;

                        if (character.CurEP == character.maxEP && (isAutoUlti || isUlti))
                        {
                            anim.SetTrigger("anim5");
                            ctl.Play(5);

                            ChangeEp(-character.maxEP);
                            isUlti = false;
                        }
                        else
                        {
                            anim.SetTrigger("anim3");
                            ctl.Play(3);

                            ChangeEp(20);
                        }
                    }
                    else if (isRun) status = Status.Run;
                }
                else if (isFind) status = Status.Find;
            }
            yield return Timing.WaitForOneFrame;
        }
    }

    public IEnumerator<float> _Beaten()
    {
        character.speedRun = oldSpeedRun / 3;
        if (ctl != null)
        {
            if (character.CurHP > 0)
            {
                if (isAnim1())
                {
                    anim.SetTrigger("anim7");
                    ctl.Play(7);
                }
            }
        }

        yield return Timing.WaitForSeconds(0.05f);
        character.speedRun = oldSpeedRun;

        yield break;
    }

    public void ChangeHp(int value)
    {
        character.CurHP += value;
        UICharater.hp = character.CurHP * 1.0f / character.maxHP;

        isLive = character.CurHP != 0;

        if (!isLive) Timing.RunCoroutine(_Die());
    }

    private IEnumerator<float> _Die()
    {
        CircleCollider2D cc = GetComponent<CircleCollider2D>();
        if (cc != null) cc.enabled = false;

        CapsuleCollider2D ca = GetComponent<CapsuleCollider2D>();
        if (ca != null) ca.enabled = false;

        status = Status.None;
        MainGame2.instance.DestroyCharacter(character.isEnemy);

        while (true)
        {
            if (isAnim1())
            {
                anim.SetTrigger("anim6");
                ctl.Play(6);
                break;
            }
            yield return Timing.WaitForSeconds(0.01f);
        }

        UICharater.Show(false);

        float timeOp = 0.5f;
        float op = 0.5f;
        while (true)
        {
            if (op <= 0.0f || timeOp <= 0.0f) break;

            float delta = Time.deltaTime / speed;

            timeOp -= delta;
            op = timeOp / 0.5f;

            yield return Timing.WaitForSeconds(delta);

            if (sa) sa.skeleton.SetColor(new Color(1.0f, 1.0f, 1.0f, op));
            if (img) img.color = new Color(1.0f, 1.0f, 1.0f, op);
        }

        if (sa) sa.skeleton.SetColor(new Color(1.0f, 1.0f, 1.0f, 1.0f));
        if (img) img.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        this.gameObject.SetActive(false);
    }

    private bool isAnim1()
    {
        AnimatorClipInfo[] m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        if (m_CurrentClipInfo.Length < 1) return false;
        string m_ClipName = m_CurrentClipInfo[0].clip.name;
        //Debug.Log(m_ClipName);
        return m_ClipName == "animation1";
    }

    public void ChangeEp(int value)
    {
        character.CurEP += value;
        UICharater.ep = character.CurEP * 1.0f / character.maxEP;
    }

    public void ScaleSpeed(float value)
    {
        speed = value;

        anim.speed = speed;
        if (sa) sa.timeScale = speed;
    }

    private void OnDestroy()
    {
        Timing.KillCoroutines(AutoAttack);
        Timing.KillCoroutines(AutoRun);
        Timing.KillCoroutines(AutoFind);
    }

    private void OnEnable()
    {
        if (AutoAttack.IsValid) Timing.ResumeCoroutines(AutoAttack);
        if (AutoRun.IsValid) Timing.ResumeCoroutines(AutoRun);
        if (AutoFind.IsValid) Timing.ResumeCoroutines(AutoFind);
    }

    private void OnDisable()
    {
        Timing.PauseCoroutines(AutoAttack);
        Timing.PauseCoroutines(AutoRun);
        Timing.PauseCoroutines(AutoFind);

        if (callback != null) callback();
    }

    private enum Status
    {
        Attack,
        Run,
        Find,
        None
    }
}
