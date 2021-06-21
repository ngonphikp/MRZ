using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Radar : MonoBehaviour
{
    [SerializeField] C_Enum.Find_Tag find_Tag = C_Enum.Find_Tag.Enemy;

    Dictionary<int, Target> targets = new Dictionary<int, Target>();

    public Target FindMinTarget()
    {
        float min = float.MaxValue;
        int key = -1;
        foreach (KeyValuePair<int, Target> item in targets)
        {
            if (min > item.Value.dis)
            {
                min = item.Value.dis;
                key = item.Key;
            }
        }

        if (key != -1)
        {
            //Debug.Log("Min: " + key + " => " + targets[key].dis);
            return targets[key];
        }

        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(find_Tag.ToString()))
        {
            int key = collision.GetHashCode();
            float dis = Vector2.Distance(this.gameObject.transform.position, collision.gameObject.transform.position);
            //Debug.Log("Enter: " + key);
            if (!targets.ContainsKey(key))
            {
                C_Character tg = collision.gameObject.GetComponent<C_Character>();
                if (tg)
                {
                    //Debug.Log("Add: " + key);
                    targets.Add(key, new Target(tg, dis));
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(find_Tag.ToString()))
        {
            int key = collision.GetHashCode();
            float dis = Vector2.Distance(this.gameObject.transform.position, collision.gameObject.transform.position);
            if (targets.ContainsKey(key))
            {
                //Debug.Log("Update: " + key + " => " + dis);
                targets[key].dis = dis;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        int key = collision.GetHashCode();
        //Debug.Log("Exit: " + key);
        if (targets.ContainsKey(key))
        {
            //Debug.Log("Remove: " + key);
            targets.Remove(key);
        }
    }
}

public class Target
{
    public C_Character ctl;
    public float dis;

    public Target()
    {

    }

    public Target(C_Character ctl, float dis)
    {
        this.ctl = ctl;
        this.dis = dis;
    }
}
