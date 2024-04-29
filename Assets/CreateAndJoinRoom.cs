using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public InputField roomNameCreateInputField;
    public InputField roomNameJoinInputField;

    public void CreateRoom()
    {
        Debug.Log("Creating room " + roomNameCreateInputField.text);
        PhotonNetwork.CreateRoom(roomNameCreateInputField.text);
    }

    public void JoinRoom()
    {
        Debug.Log("Joining room " + roomNameJoinInputField.text);
        PhotonNetwork.JoinRoom(roomNameJoinInputField.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Test");
    }
}
