using Sfs2X.Entities.Data;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager: MonoBehaviour
{
    public static ResourceManager instance = null;

    Dictionary<string, AudioClip> dicAS = new Dictionary<string, AudioClip>();
    Dictionary<string, C_Character> dicCCT = new Dictionary<string, C_Character>();
    public Dictionary<string, M_Character> dicMCT;

    private void Awake()
    {
        MakeSingleInstance();

        LoadJsonConfig();
    }

    private void LoadJsonConfig()
    {
        dicMCT = new Dictionary<string, M_Character>();
        TextAsset file = Resources.Load<TextAsset>("JsonConfig/Character");
        ISFSObject obj = SFSObject.NewFromJsonData(file.ToString());
        ISFSArray lst = obj.GetSFSArray("lst");
        for (int i = 0; i < lst.Count; i++)
        {
            ISFSObject item = lst.GetSFSObject(i);
            string key = item.GetUtfString("id");
            if (!dicMCT.ContainsKey(key))
            {
                dicMCT.Add(key, new M_Character()
                {
                    id = key,
                    maxHP = item.GetInt("maxHP"),
                    attack = item.GetInt("attack"),
                    speedRun = item.GetData("speedRun").Type == 7 ? item.GetFloat("speedRun") : item.GetInt("speedRun")
                });
            }
        }
    }

    private void MakeSingleInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public AudioClip LoadAudioClip(string path)
    {
        return Load(path, dicAS);
    }

    public C_Character LoadCharacter(string path)
    {
        return Load(path, dicCCT);
    }

    private T Load<T>(string path, Dictionary<string, T> dic) where T : Object
    {
        if (!dic.ContainsKey(path))
        {
            T t = Resources.Load<T>(path);

            if (t) dic.Add(path, t);
            else Debug.LogError("Resource Null: " + path);
        }

        if (dic.ContainsKey(path)) return dic[path];
        return default;
    }
}
