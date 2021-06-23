using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame2 : MonoBehaviour
{
    public static MainGame2 instance = null;

    [SerializeField] Transform[] posCharacter = new Transform[5];

    [SerializeField] C_Factory factory = null;

    [SerializeField] Text txtRound = null;
    [SerializeField] Text txtTime = null;

    [SerializeField] int timeShow = 3;
    [SerializeField] int timeCount = 10;
    [SerializeField] float timeSum = 5;

    int round = 0;

    List<M_Character> datas = new List<M_Character>();

    List<C_Character> teams = new List<C_Character>();

    private void Awake()
    {
        if (instance == null) instance = this;

        if (Application.platform != RuntimePlatform.Android) Application.runInBackground = true;
    }

    CoroutineHandle CountDownTime;
    CoroutineHandle Stamina;

    public void StartGame()
    {
        oldTimeSum = timeSum;
        Timing.RunCoroutine(_Start());
    }

    float oldTimeSum;
    private IEnumerator<float> _Start()
    {
        popUp.SetActive(false);
        countTeamDT = 0;
        countEnemyDt = 0;

        isEndGame = false;
        timeSum = oldTimeSum;

        DemoData();
        LoadData();

        CountDownTime = Timing.RunCoroutine(_CountDownTime());
        yield break;
    }

    private void DemoData()
    {
        round = 0;
        datas.Clear();

        for (int i = 0; i < 1; i++)
        {
            M_Character ct = new M_Character()
            {
                id = "T1001",// Random.Range(0, 2) % 2 == 0 ? "T1052" : "T1001",
                position = i,
                isEnemy = false
            };
            ct.Update();
            datas.Add(ct);
        }
    }

    private void LoadData()
    {

        for (int i = 0; i < posCharacter.Length; i++)
        {
            foreach (Transform child in posCharacter[i])
            {
                Destroy(child.gameObject);
            }
        }

        teams.Clear();
        for (int i = 0; i < datas.Count; i++)
        {
            C_Character ctAs = ResourceManager.instance.LoadCharacter("Prefabs/Character/" + datas[i].id);
            C_Character ct = Instantiate(ctAs, posCharacter[datas[i].position]);

            ct.Set(datas[i], null);
            teams.Add(ct);
        }
    }

    private void Spawn()
    {
        round++;
        txtRound.text = "Round " + round;
        txtRound.gameObject.SetActive(false);
        txtRound.gameObject.SetActive(true);
        txtRound.color = round % 3 == 0 ? Color.red : Color.white;

        int size = Mathf.Clamp(Random.Range(2, 4) + round / 3, 1, 7);

        string[] arrId = { "M2001" }; //"M1010", "M1018", 

        List<M_Character> enemys = new List<M_Character>();
        for (int i = 0; i < size; i++)
        {
            M_Character enemy = new M_Character()
            {
                id = arrId[Random.Range(0, arrId.Length)],
                position = Random.Range(0, 5),
                isEnemy = true,
                lv = 1 + round / 5,
            };

            enemy.Update();
            enemy.maxHP += 4 * round;
            enemy.CurHP = enemy.maxHP;

            enemys.Add(enemy);
        }

        if (round % 3 == 0)
        {
            M_Character enemy = new M_Character()
            {
                id = arrId[Random.Range(0, arrId.Length)],
                position = Random.Range(0, 5),
                isEnemy = true,
                lv = 1 + round / 5,
                mode = C_Enum.Mode.Boss
            };

            enemy.Update();
            enemy.maxHP += 4 * round;
            enemy.CurHP = enemy.maxHP;

            enemys.Add(enemy);
        }

        Timing.RunCoroutine(factory._Spawn(enemys.ToArray()));
    }

    private IEnumerator<float> _CountDownTime()
    {
        while (true)
        {
            yield return Timing.WaitForSeconds(1f);
            if (isEndGame) continue;

            timeSum -= 1f;

            if (timeSum <= timeShow)
            {
                txtTime.text = timeSum + "";
                txtTime.gameObject.SetActive(true);

                if (timeSum == 0)
                {
                    timeSum = timeCount;
                    Spawn();
                    txtTime.gameObject.SetActive(false);
                }
            }
        }
    }

    int countTeamDT = 0;
    int countEnemyDt = 0;

    public bool isEndGame = false;

    public void DestroyCharacter(bool isEnemy)
    {
        if (isEnemy) countEnemyDt++;
        else
        {
            countTeamDT++;
            if (countTeamDT == teams.Count) Timing.RunCoroutine(_EndGame());
        }
    }

    [SerializeField] GameObject popUp = null;

    private IEnumerator<float> _EndGame()
    {
        isEndGame = true;
        Timing.KillCoroutines(CountDownTime);
        Timing.KillCoroutines(Stamina);

        txtRound.text = "End Game";
        txtRound.gameObject.SetActive(false);
        txtRound.gameObject.SetActive(true);

        txtTime.gameObject.SetActive(false);

        yield return Timing.WaitForSeconds(1.0f);
        popUp.SetActive(true);
        factory.Realese();
    }
}
