using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

// [System.Serializable]
// public class AccountInfo
// {
//     public string username;
//     public string password;
// }

// [System.Serializable]
// public class AccountList
// {
//     public AccountInfo[] accounts;
// }

public class Register : MonoBehaviour
{
    public InputField inputTK;
    public InputField inputMK;
    public InputField inputMK2;
    public GameObject fail;
    public GameObject notFail;
    List<AccountInfo> accounts;

    // Start is called before the first frame update
    public void Start()
    {
        accounts = new List<AccountInfo>();
        string path = Application.dataPath + "/dataPass.json";
        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd();
            reader.Close();
            AccountList accountList = JsonUtility.FromJson<AccountList>(json);
            foreach (AccountInfo account in accountList.accounts)
            {
                accounts.Add(account);
                //Debug.Log("Username: " + account.username + ", Password: " + account.password);
            }
        }
        fail.SetActive(false);
        notFail.SetActive(false);
    }

    // Update is called once per frame
    public void Reset()
    {
        inputMK.text = "";
        inputMK2.text = "";
        inputTK.text = "";
        fail.SetActive(false);
    }

    public void RegisterPass()
    {
        foreach (AccountInfo account in accounts)
        {
            if (inputTK.text == account.username)
            {
                fail.SetActive(true);
                return;
            }
        }
        if (inputMK.text == inputMK2.text)
        {
            accounts.Add(new AccountInfo { username = inputTK.text, password = inputMK.text });
            notFail.SetActive(true);

            this.Out();
        }
    }
    void Out()
    {
        string path = Application.dataPath + "/dataPass.json";
        AccountList accountList = new AccountList { accounts = accounts.ToArray() };
        string json = JsonUtility.ToJson(accountList);
        StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
        writer.Close();
    }
}