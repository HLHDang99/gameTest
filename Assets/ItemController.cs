using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float dame;
    public float hp;
    public float mp;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Invoke("Destroy", 100f);
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player.UseItem(hp, mp, dame);
            Destroy();
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.UseItem(hp, mp, dame);
            Destroy();
        }
    }
}
