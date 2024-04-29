using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Gun;
    public GameObject player;
    Vector3 offset;
    public float delay;
    Vector3 mousePos;
    Vector3 distance;
    float time_spawn = 0.5f;
    float m_time_spawn = 0;
    float angle = 90;
    ManagerController manager;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (manager.Win.activeSelf)
        //     return;
        mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.z;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 cameraPlayer = player.transform.position + offset; // lấy tọa độ mới của cam khi theo nhân vật
        transform.position = Vector3.Lerp(transform.position, cameraPlayer, delay * Time.deltaTime);  // caajo nhật vị trí
        // Tính toán hướng của súng
        Vector3 gunDirection = worldMousePos - transform.position;
        gunDirection.z = 0;
        gunDirection.Normalize();

        // Xoay súng
        angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //Attack();
    }
    public void Attack(int value = 1)
    {
        // m_time_spawn -= Time.deltaTime;
        // if (m_time_spawn <= 0)// && Input.GetAxis("Fire1") != 0)
        // {
        ani.SetBool("Attack", true);
        Invoke("SetAttack", 0.5f);
        m_time_spawn = time_spawn;
        if (value == 0)
            Instantiate(Bullet, Gun.transform.position, Quaternion.Euler(new Vector3(0, 0, angle + 180)));
        else
            Instantiate(Bullet, Gun.transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        //}
    }
    void SetAttack()
    {
        ani.SetBool("Attack", false);
    }
}
