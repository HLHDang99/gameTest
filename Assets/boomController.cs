using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomController : MonoBehaviour
{
    public float size = 1;
    public float time;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(size, size, 0);
        Invoke("Bum", time);
        ani = GetComponent<Animator>();
    }
    void Bum()
    {
        ani.SetBool("Bum", true);
        Invoke("Destroy", 2);
    }
    void Destroy()
    {
        Destroy(gameObject);
    }


}
