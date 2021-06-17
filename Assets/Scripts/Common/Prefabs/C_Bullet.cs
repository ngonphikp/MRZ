using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Bullet : MonoBehaviour
{
    [SerializeField] float speed = 20.0f;
    [SerializeField] GameObject explosion = null;

    public IEnumerator<float> _Move(C_Character actor, C_Character target, float timeDlMove = 0.0f)
    {
        // Delay di chuyển bullet
        yield return Timing.WaitForSeconds(timeDlMove /actor.speed);

        while (true)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.posHit.position, step);

            if (Vector3.Distance(transform.position, target.posHit.position) < 0.001f)
            {
                if (explosion)
                {
                    GameObject fx = Instantiate(explosion, target.gameObject.transform);
                    fx.transform.position = this.gameObject.transform.position;
                }

                if (target && target.isLive)
                {
                    Timing.RunCoroutine(target._Beaten());
                    target.ChangeHp(-actor.character.attack);
                }
                break;
            }            
            yield return Timing.WaitForOneFrame;
        }

        Destroy(gameObject);
    }
}
