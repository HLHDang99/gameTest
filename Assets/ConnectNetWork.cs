using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectNetWork : MonoBehaviourPunCallbacks
{
    void Start()
    {
        //connect to the photon server
        Debug.Log("Connecting to the photon server");
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        SceneManager.LoadScene("Lobby");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
