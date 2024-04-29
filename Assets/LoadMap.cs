using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMap : MonoBehaviour
{
    public GameObject VK1;
    public GameObject VK2;
    void Start()
    {
        if (PlayerPrefs.GetInt("VuKhi") == 0)
        {
            VK1.SetActive(true);
            VK2.SetActive(false);
        }
        else
        {
            VK1.SetActive(false);
            VK2.SetActive(true);
        }
    }
    // Start is called before the first frame update
    public void PlayGame()
    {
        PlayerPrefs.SetInt("LoadMap", 2);
        SceneManager.LoadScene(4);
    }

    public void MenuGame()
    {
        PlayerPrefs.SetInt("LoadMap", 1);
        SceneManager.LoadScene(4);
    }
    public void Item()
    {
        PlayerPrefs.SetInt("LoadMap", 3);
        SceneManager.LoadScene(4);
    }
    public void Shop()
    {
        PlayerPrefs.SetInt("LoadMap", 5);
        SceneManager.LoadScene(4);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void LoadMap2()
    {
        if (PlayerPrefs.GetInt("LoadMap") != 6)
            PlayerPrefs.SetInt("LoadMap", 2);
        SceneManager.LoadScene(4);
    }
    public void SetVK(int value)
    {
        PlayerPrefs.SetInt("VuKhi", value);
        if (PlayerPrefs.GetInt("VuKhi") == 0)
        {
            VK1.SetActive(true);
            VK2.SetActive(false);
        }
        else
        {
            VK1.SetActive(false);
            VK2.SetActive(true);
        }
    }
}
