using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float speed = 15;
    Vector2 destination;
    float time = 3f;
    float Angle;
    Rigidbody2D myBody;
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
            Destroy(gameObject);
        Vector2 directionPlus90 = Quaternion.Euler(0, 0, 180) * transform.right;
        myBody.AddForce(directionPlus90 * speed);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("boot"))
        {
            Destroy(gameObject);
        }
    }
}
