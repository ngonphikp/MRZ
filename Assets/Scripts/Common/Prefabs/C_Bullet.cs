using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Bullet : MonoBehaviour
{
    [SerializeField] C_Enum.Find_Tag find_Tag = C_Enum.Find_Tag.Enemy;

    [SerializeField] float speed = 20.0f;
    [SerializeField] GameObject explosion = null;

    private C_Character actor;
    private Rigidbody2D mybody;

    private void Awake()
    {
        mybody = GetComponent<Rigidbody2D>();
    }

    public IEnumerator<float> _Move(C_Character actor, C_Character target, float timeDlMove = 0.0f)
    {
        this.actor = actor;

        // Delay di chuyển bullet
        yield return Timing.WaitForSeconds(timeDlMove / actor.speed);

        //while (true)
        //{
        //    float step = speed * Time.deltaTime;
        //    transform.position = Vector3.MoveTowards(transform.position, target.posHit.position, step);

        //    if (Vector3.Distance(transform.position, target.posHit.position) < 0.001f)
        //    {
        //        if (explosion)
        //        {
        //            GameObject fx = Instantiate(explosion, target.gameObject.transform);
        //            fx.transform.position = this.gameObject.transform.position;
        //        }

        //        if (target && target.isLive)
        //        {
        //            Timing.RunCoroutine(target._Beaten());
        //            target.ChangeHp(-actor.character.attack);
        //        }
        //        break;
        //    }            
        //    yield return Timing.WaitForOneFrame;
        //}

        //Destroy(gameObject);

        if (mybody) mybody.AddForce(transform.right * speed * 60);

        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(find_Tag.ToString()))
        {
            C_Character tg = collision.gameObject.GetComponent<C_Character>();
            if (tg && tg.isLive)
            {
                if (explosion)
                {
                    GameObject fx = Instantiate(explosion, tg.gameObject.transform);
                    fx.transform.position = this.gameObject.transform.position;

                    Timing.RunCoroutine(tg._Beaten());
                    tg.ChangeHp(-actor.character.attack);

                }

                Destroy(gameObject);
            }
        }

        if (collision.CompareTag("Bouncy"))
        {
            Destroy(gameObject);
        }
    }
}
