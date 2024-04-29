using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerController : MonoBehaviour
{
    public Slider _slider;
    public GameObject player;
    public GameObject[] Boss;
    public GameObject bootStart;
    public GameObject[] Items;
    public GameObject Win;
    public GameObject Win2;
    public GameObject Lose;
    List<GameObject> array;
    List<Vector3> que1;
    List<float> que2;
    public GameObject BOSS;
    public float time_spawm = 1;
    float m_time_spawm = 0;
    int lv;
    int slquai;
    public int map;
    Vector3 goc_trai_tren;
    int sizeM, sizeN;

    // Start is called before the first frame update
    void Start()
    {
        _slider.value = 0;
        _slider.maxValue = 100;
        lv = 0;
        BOSS.SetActive(false);
        Win.SetActive(false);
        Lose.SetActive(false);
        array = new List<GameObject>();
        que1 = new List<Vector3>();
        que2 = new List<float>();
        slquai = 0;
        if (map == 0)
        {
            goc_trai_tren = new Vector3(-18.82f, 16.5f, 0);
            sizeM = 20;
            sizeN = 22;

        }
        else if (map == 2)
        {
            goc_trai_tren = new Vector3(-33.11f, 24.5f, 0);
            sizeM = 17;
            sizeN = 35;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_slider.value >= _slider.maxValue)
        {
            if (Win.activeSelf == false)
                Win.SetActive(true);
        }
        if (Lose.activeSelf || Win2.activeSelf){
            player.SetActive(false);
            return;
        }

        m_time_spawm -= Time.deltaTime;
        //Debug.Log(array.Count);
        if (m_time_spawm <= 0 && array.Count < lv * 5 + 5)
        {
            que1.Add(new Vector3(Random.Range(goc_trai_tren[0] + 4, goc_trai_tren[0] + 2 * (sizeN - 2)), Random.Range(goc_trai_tren[1] - 4, goc_trai_tren[1] - 2 * (sizeM - 2)), 0));
            que2.Add(0.3f);
            m_time_spawm = time_spawm;
        }
        NewBoss();
        if ((_slider.value >= 0 && lv == 0) || (_slider.value >= 25 && lv == 1)
        || (_slider.value >= 50 && lv == 2) || (_slider.value >= 75 && lv == 3)
        || (_slider.value >= 90 && lv == 4))
        {
            lv++;
            int num = (int)Random.Range(lv * 2 + 1, lv * 3) * (map * 2);
            NewList(num);
        }
        if (_slider.value >= 100)
        {
            for (int i = 0; i < array.Count; i++)
                Destroy(array[i]);
            array.Clear();
        }
        if (_slider.value < 25)
            _slider.value += 0.1f;
        else if (_slider.value < 50)
            _slider.value += 0.05f;
        else if (_slider.value < 75)
            _slider.value += 0.025f;
        else
            _slider.value += 0.01f;
        _slider.value += 0.2f;
    }
    void NewBoss()
    {
        float delta = Time.deltaTime;
        if (que1.Count <= 0)
            return;
        for (int j = 0; j < que1.Count; j++)
        {
            if (que2[j] <= 0)
            {
                Vector3 index = que1[j];

                int ran = (int)Random.Range(0, 6), i;
                if (ran == 2)
                    i = 0;
                else if (ran == 5)
                    i = 2;
                else
                    i = 1;
                GameObject x = Instantiate(Boss[i], index, Quaternion.Euler(new Vector3(0, 0, 0))); // tạo địch
                SettingBoot y = x.GetComponent<SettingBoot>();
                y.SetPlayer(player);
                array.Add(x);
                que1.RemoveAt(j);
                que2.RemoveAt(j);
                slquai++;
                if (slquai % (lv * 5) == 1)
                {
                    PlayerController players = player.gameObject.GetComponent<PlayerController>();
                    players.LVUp();
                }
            }
            else
                que2[j] -= delta;
        }
    }
    void NewList(int n)
    {
        for (int i = 0; i < array.Count; i++)
            Destroy(array[i]);
        array.Clear();

        //Debug.Log(manager.Check(transform.position));
        for (int i = 0; i < n * (map + 1); i++)
        {
            Vector3 index;
            do
            {
                index = new Vector3(Random.Range(goc_trai_tren[0] + 4, goc_trai_tren[0] + 2 * (sizeN - 2)), Random.Range(goc_trai_tren[1] - 4, goc_trai_tren[1] - 2 * (sizeM - 2)), 0);
            } while (Check(index) == 1);
            Instantiate(bootStart, index, Quaternion.Euler(new Vector3(0, 0, 0)));
            que1.Add(index);
            que2.Add(0.3f);
        }
    }
    public void NewItem(Vector3 index)
    {
        int x = (int)Random.Range(0, 100);

        if (x % 25 == 1)
            Instantiate(Items[0], index, Quaternion.Euler(new Vector3(0, 0, 0))); // tạo địch
        else if (x % 25 == 2)
            Instantiate(Items[1], index, Quaternion.Euler(new Vector3(0, 0, 0))); // tạo địch
        else if (x % 45 == 1)
            Instantiate(Items[2], index, Quaternion.Euler(new Vector3(0, 0, 0))); // tạo địch
    }
    public void SetLose()
    {
        Lose.SetActive(true);
    }
    public void War()
    {
        if (map == 0)
        {
            map = 1;
            goc_trai_tren = new Vector3(39f, 16.5f, 0);
            sizeM = 10;
            sizeN = 17;
        }
        BOSS.SetActive(true);
    }
    public void WIN()
    {
        PlayerPrefs.SetInt("LoadMap", 6);
        Win2.SetActive(true);
    }
    public int Check(Vector3 index)
    {
        if (map == 0)
        {
            Vector2 index_map = new Vector2((index[0] - goc_trai_tren[0]) / 2 + 0.5f, (-index[1] + goc_trai_tren[1]) / 2 + 0.5f);
            //Debug.Log(index_map[0] + " " + index_map[1]);
            // 20 x 22
            int[,] tileMap = new int[,] { {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                            {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                            {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
                            {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
                            {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
                            {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
                            {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
                            {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
                            {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
                            {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
                            {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
                            {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
                            {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
                            {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0},
                            {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};
            return tileMap[(int)index_map[1], (int)index_map[0]];
        }
        else if (map == 1)
        {
            Vector2 index_map = new Vector2((index[0] - goc_trai_tren[0]) / 2 + 0.5f, (-index[1] + goc_trai_tren[1]) / 2 + 0.5f);
            //Debug.Log(index_map[0] + " " + index_map[1]);

            int[,] tileMap = new int[,] {
                                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};
            return tileMap[(int)index_map[1], (int)index_map[0]];
        }
        else
        {
            //Vector3 goc_trai_tren = new Vector3(-33.11f, 24.5f, 0);
            Vector2 index_map = new Vector2((index[0] - goc_trai_tren[0]) / 2 + 0.5f, (-index[1] + goc_trai_tren[1]) / 2 + 0.5f);
            //Debug.Log(index_map[0] + " " + index_map[1]);
            // 17x35
            int[,] tileMap = new int[17, 35] {
                                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                                {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
                                {0,1,0,0,0,0,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
                                {0,1,0,0,0,0,2,2,2,2,2,2,2,2,2,0,0,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,1,0},
                                {0,1,0,0,0,0,2,2,2,2,2,2,2,2,2,0,0,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,1,0},
                                {0,1,0,0,0,0,2,2,2,2,2,2,2,2,2,0,0,3,3,3,3,3,3,0,0,0,0,2,2,0,0,0,0,1,0},
                                {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,1,0},
                                {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,1,0},
                                {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
                                {0,1,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,1,0},
                                {0,1,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,1,0},
                                {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
                                {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
                                {0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
                                {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
                                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};
            return tileMap[(int)index_map[1], (int)index_map[0]];
        }
        return 0;

    }
}
