using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    public Slider thanhTiLe;
    public GameObject player;
    Animator playerAni;
    float timeDelta;
    float mTimeDelta;
    // Start is called before the first frame update
    void Start()
    {
        playerAni = player.gameObject.GetComponent<Animator>();
        thanhTiLe.value = 0;
        thanhTiLe.maxValue = 100;
        timeDelta = 0.2f;
        mTimeDelta = 0;
        playerAni.SetInteger("face", -2);
    }

    // Update is called once per frame
    void Update()
    {
        mTimeDelta -= Time.deltaTime;
        if (mTimeDelta <= 0)
        {
            mTimeDelta = timeDelta + (thanhTiLe.value * 0.5f / thanhTiLe.maxValue);
            int x = (int)Random.Range(1, 10);
            thanhTiLe.value += x;
            if (thanhTiLe.value >= thanhTiLe.maxValue)
                SceneManager.LoadScene(PlayerPrefs.GetInt("LoadMap"));
            player.transform.position = player.transform.position + new Vector3(x * 2.5f * 9.7f / 100, 0, 0);
        }
    }
}
