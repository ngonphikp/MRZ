using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public int speed = 250;
    public float timeToDestroy = 3f;

    private void OnEnable()
    {
        StartCoroutine(AutoDestroy(timeToDestroy));
    }

    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    IEnumerator AutoDestroy(float _time)
    {
        yield return new WaitForSeconds(_time);
        Destroy(gameObject);
    }
}
