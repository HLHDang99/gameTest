using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Spawner : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(player.name, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //PhotonNetwork.Instantiate(player.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
    }
}
