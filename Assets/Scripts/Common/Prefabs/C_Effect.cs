using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Effect : MonoBehaviour
{
    [SerializeField] float time = 0.0f;
    [SerializeField] Finish action = Finish.Destroy;

    private void OnEnable()
    {
        Timing.RunCoroutine(_AutoDestroy());
    }

    private IEnumerator<float> _AutoDestroy()
    {
        yield return Timing.WaitForSeconds(time);
        if (this != null && gameObject != null && action == Finish.Destroy) Destroy(gameObject);
        if (this != null && gameObject != null && action == Finish.Hide && gameObject.activeSelf) gameObject.SetActive(false);
    }

    private enum Finish
    {
        Destroy,
        Hide
    }
}
