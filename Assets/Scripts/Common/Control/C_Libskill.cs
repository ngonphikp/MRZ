using MEC;
using System.Collections.Generic;
using UnityEngine;

public static class C_LibSkill
{
    public static void Shoot(C_Character actor, C_Bullet bullet, Transform parent, bool isRotate = false, float timeInit = 0.0f, float timeDlMove = 0.0f, params C_Character[] targets)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            Timing.RunCoroutine(_CreateBullet(actor, bullet, parent, targets[i], isRotate, timeInit, timeDlMove));
        }
    }

    private static IEnumerator<float> _CreateBullet(C_Character actor, C_Bullet bullet, Transform parent, C_Character target, bool isRotate = false, float timeInit = 0.0f, float timeDlMove = 0.0f)
    {
        // Delay tạo đạn
        yield return Timing.WaitForSeconds(timeInit / actor.speed);

        // Tạo viên đạn
        C_Bullet bl = MonoBehaviour.Instantiate(bullet, parent);

        if (isRotate) // Bullet có định hướng ban đầu
        {
            Vector3 A = bl.gameObject.transform.position;
            Vector3 B = target.transform.position;
            float angle = Mathf.Rad2Deg * Mathf.Atan((B.y - A.y) / (B.x - A.x));
            bl.gameObject.transform.Rotate(0, 0, angle);
        }

        Timing.RunCoroutine(bl._Move(actor, target, timeDlMove));
    }

    public static void Hit(C_Character actor, GameObject obj = null, float timeInit = 0.0f, float timeHit = 0.0f, params C_Character[] targets)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            Timing.RunCoroutine(_CreateEffect(actor, targets[i], obj, timeInit, timeHit));
        }
    }

    private static IEnumerator<float> _CreateEffect(C_Character actor, C_Character target, GameObject obj = null, float timeInit = 0.0f, float timeHit = 0.0f)
    {
        if (obj)
        {
            // Delay tạo hiệu ứng
            yield return Timing.WaitForSeconds(timeInit / actor.speed);

            // Tạo hiệu ứng
            MonoBehaviour.Instantiate(obj, target.posHit);
        }

        // Delay tạo hiệu ứng
        yield return Timing.WaitForSeconds(timeHit / actor.speed);
        if (target && target.isLive)
        {
            Timing.RunCoroutine(target._Beaten());
            target.ChangeHp(-actor.character.attack);
        }
    }
}
