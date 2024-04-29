using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float speed;
    public GameObject player;
    Vector3 offset;
    public float delay;
    float huong_hien_tai = 0; // hướng nhân vật
    float rotateVelocity = 0f; // biện tạm lưu trữ hướng nhân vật 
    void Update()
    {
        RotateGun();
    }
    void RotateGun()
    {
        //huong_hien_tai %= 360;
        huong_hien_tai = Mathf.SmoothDamp(huong_hien_tai, huong_hien_tai + 360, ref rotateVelocity, speed);// * Mathf.Rad2Deg; // xoay nhân vật
        transform.rotation = Quaternion.Euler(0f, 0f, huong_hien_tai); // cập nhật hướng mới
        Vector3 cameraPlayer = player.transform.position + offset; // lấy tọa độ mới của cam khi theo nhân vật
        transform.position = Vector3.Lerp(transform.position, cameraPlayer, delay * Time.deltaTime);  // caajo nhật vị trí
        if (huong_hien_tai >= 360)
        {
            huong_hien_tai = 0;
            gameObject.SetActive(false);
        }
    }
    public void Reset()
    {
        gameObject.SetActive(true);
    }
}
