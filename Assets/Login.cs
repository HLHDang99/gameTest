using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;


[System.Serializable]
public class AccountInfo
{
    public string username;
    public string password;
}
[System.Serializable]
public class AccountList
{
    public AccountInfo[] accounts;
}
public class Login : MonoBehaviour
{
    //public GameObject register;
    public InputField inputTK;
    public InputField inputMK;
    public GameObject faile;
    List<AccountInfo> account;
    // Start is called before the first frame update
    public void Start()
    {
        account = new List<AccountInfo>();
        string path = Application.dataPath + "/dataPass.json";
        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd();
            reader.Close();
            AccountList accountList = JsonUtility.FromJson<AccountList>(json);
            foreach (AccountInfo accounts in accountList.accounts)
            {
                account.Add(accounts);
                //Debug.Log("Username: " + account.username + ", Password: " + account.password);
            }
        }
        faile.SetActive(false);
    }

    // Update is called once per frame
    public void StartGame()
    {
        string tk = inputTK.text;
        string mk = inputMK.text;
        for (int i = 0; i < account.Count; i++)
            if (tk == account[i].username && mk == account[i].password)
            {
                PlayerPrefs.SetInt("LoadMap", 1);
                SceneManager.LoadScene(4);
                return;
            }
        faile.SetActive(true);
    }
    public void Out()
    {
        string json = JsonUtility.ToJson(new { account = account });
        string path = Application.dataPath + "/dataPass.json";
        File.WriteAllText(path, json);
        Application.Quit();
    }
    public void Reset()
    {
        inputMK.text = "";
        inputTK.text = "";
        faile.SetActive(false);
    }
}
