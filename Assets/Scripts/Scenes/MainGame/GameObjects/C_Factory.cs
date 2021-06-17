using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Factory : MonoBehaviour
{
    [SerializeField] Transform content = null;

    [SerializeField] Transform[] pos = new Transform[5];

    Dictionary<string, Pooling_C_Character> dic = new Dictionary<string, Pooling_C_Character>();

    public IEnumerator<float> _Spawn(params M_Character[] datas)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            if (MainGame.instance.isEndGame) break;

            string key = datas[i].id;
            if (!dic.ContainsKey(key))
            {
                Pooling_C_Character PO = new Pooling_C_Character();
                C_Character ctAs = ResourceManager.instance.LoadCharacter("Prefabs/Character/" + key);
                PO.Set(ctAs, content);

                dic.Add(key, PO);
            }

            C_Character ct = dic[key].Get();
            ct.gameObject.transform.position = pos[datas[i].position].position;
            ct.Set(datas[i], () => dic[key].Release(ct));

            yield return Timing.WaitForSeconds(Random.Range(0.3f, 0.7f));
        }

        yield break;
    }

    public void Realese()
    {
        foreach (Transform item in content)
        {
            item.gameObject.SetActive(false);
        }
    }
}
