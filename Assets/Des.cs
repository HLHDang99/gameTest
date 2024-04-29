using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Des : MonoBehaviour
{
    // Start is called before the first frame update
    public float time = 1f;
    void Start()
    {
        Invoke("Destroy", time);
    }

    // Update is called once per frame
    void Destroy()
    {
        Destroy(gameObject);
    }
}
