using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public Slider sliderHP;
    public Slider sliderMP;
    public Slider[] VK;
    public GameObject bom;
    public struct ChiSo
    {
        public float max;
        public float min;
        public float value;
        public ChiSo(float max, float min, float value)
        {
            this.max = max;
            this.min = min;
            this.value = value;
        }
    }
    ChiSo speed;
    ChiSo hp;
    ChiSo mp;
    ChiSo dame;
    ChiSo time_spawm;
    ChiSo time_spawm2;
    ChiSo lv;
    int face; // -2: down, 2 top, -1 left, 1 right
    Weapon vk;
    Animator my_anim;
    int typeVK;
    // Start is called before the first frame update
    void Start()
    {
        lv = new ChiSo(99, 0, 1);
        hp = new ChiSo(2000, 0, 2000);
        mp = new ChiSo(1000, 0, 1000);
        dame = new ChiSo(2000, 100, 100);
        time_spawm = new ChiSo(2, 0, 0);
        time_spawm2 = new ChiSo(0.25f, 0, 0);
        speed = new ChiSo(4, 2, 4);
        face = -2;
        my_anim = GetComponent<Animator>();

        sliderHP.maxValue = hp.max;
        sliderMP.maxValue = mp.max;

        sliderHP.value = hp.value;
        sliderMP.value = mp.value;
        //PlayerPrefs.SetInt("VuKhi", value);
        typeVK = PlayerPrefs.GetInt("VuKhi");
        VK[typeVK].value = 0;
        VK[typeVK].maxValue = 1;
        if (typeVK == 0)
        {
            VK[1].gameObject.SetActive(false);
        }
        else
            VK[0].gameObject.SetActive(false);
        VK[2].value = 0;
        VK[2].maxValue = 1;
        vk = GameObject.FindGameObjectWithTag("sword").GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (mp.value < mp.max)
        {
            mp.value += 0.3f;
            sliderMP.value = mp.value;
        }
        this.Run();
        this.Attack();
    }
    void Run()
    {
        ManagerController manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerController>();
        //Debug.Log(manager.Check(transform.position));
        float move_x = Input.GetAxis("Horizontal"); // bam nut trái phải
        float move_y = Input.GetAxis("Vertical"); // bấm nút lên xuống
        if (speed.value < speed.max)
            speed.value += 0.5f;
        // if (aus && nhac[0] && time_nhac_chay <= 0)
        // { // bật nhạc với đk có nhạc đầu vào và thời gian nhạc chạy cũ đã hết tránh đè nhạc
        //     aus.PlayOneShot(nhac[0]); // phát nhạc
        //     time_nhac_chay = 0.5f; // gán lại thời gian nhạc
        // }

        // if ((transform.position.x < value_x.min && move_x < 0) || (transform.position.x > value_x.max && move_x > 0))
        //     move_x = 0;
        // if ((transform.position.y < value_y.min && move_y < 0) || (transform.position.y > value_y.max && move_y > 0))
        //     move_y = 0;
        // trai
        if (move_x < 0)
        {
            if (face == 1)
            {
                this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0);
            }
            face = -1;
            if (manager.Check(transform.position - new Vector3(0.15f, 0, 0)) == 1)
                move_x = 0;
        }
        else if (move_x > 0)
        {
            if (face != 1)
            {
                this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0);
            }
            face = 1;
            if (manager.Check(transform.position + new Vector3(0.15f, 0, 0)) == 1)
                move_x = 0;
        }

        if (move_y < 0)
        {
            if (face == 1)
            {
                this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0);
            }
            face = -2;
            if (manager.Check(transform.position - new Vector3(0, 0.15f, 0)) == 1)
                move_y = 0;
        }
        else if (move_y > 0)
        {
            if (face == 1)
            {
                this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0);
            }
            face = 2;
            if (manager.Check(transform.position + new Vector3(0, 0.15f, 0)) == 1)
                move_y = 0;
        }
        if (manager.Check(transform.position) == 2)
        {
            move_x /= 1.5f;
            move_y /= 1.5f;
        }
        else if (manager.Check(transform.position) == 3)
        {
            move_x *= 1.5f;
            move_y *= 1.5f;
        }
        my_anim.SetInteger("face", face);

        Vector3 move = new Vector3(speed.value * Time.deltaTime * move_x, speed.value * Time.deltaTime * move_y, 0); // tạo khoảng di chuyển để cho mượt ta nhân với chênh lệnh thời gian
        transform.position = transform.position + move;
    }
    void Attack()
    {
        time_spawm.value -= Time.deltaTime;
        time_spawm2.value -= Time.deltaTime;
        //Debug.Log(PlayerPrefs.GetInt("VuKhi"));
        VK[typeVK].value = time_spawm2.value / time_spawm2.max;
        VK[2].value = time_spawm.value / time_spawm.max;
        if (time_spawm.value <= 0 && mp.value >= 300 && Input.GetKeyDown(KeyCode.Space))
        {// thời gian hồi đã xong
            time_spawm.value = time_spawm.max; // gán lại thời gian hồi chiêu
            mp.value -= 300;
            Instantiate(bom, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))); // tạo viên đạn trùng với hướng hiện tại
        }
        if (time_spawm2.value <= 0 && Input.GetAxisRaw("Fire1") == 1)
        {
            ManagerController manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerController>();
            time_spawm2.value = time_spawm2.max;
            if (typeVK == 0)
            {
                GunController gunCTL = GameObject.FindGameObjectWithTag("Cung").GetComponent<GunController>();
                if (manager.map == 2)
                    gunCTL.Attack(0);
                else
                    gunCTL.Attack();
            }
            else
            {
                //GameObject.FindGameObjectWithTag("sword").gameObject.SetActive(true);
                vk.Reset();
            }
        }
    }
    public float Dame()
    {
        return dame.value;
    }
    public int Face()
    {
        return face;
    }
    public void AddDame(float value)
    {
        hp.value -= value;
        sliderHP.value = hp.value;
        if (hp.value <= 0)
            this.Die();
        speed.value = speed.value / 3;
    }
    void Die()
    {
        ManagerController menu = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerController>();
        menu.SetLose();
    }
    public void UseItem(float _hp, float _mp, float _dame)
    {
        //Debug.Log(hp.value);
        hp.value += _hp;
        if (hp.value > hp.max)
            hp.value = hp.max;
        sliderHP.value = hp.value;
        //Debug.Log(hp.value);

        mp.value += _mp;
        if (mp.value > mp.max)
            mp.value = mp.max;
        sliderMP.value = mp.value;

        dame.value += _dame;
        if (dame.value > dame.max)
            dame.value = dame.max;
    }
    public void LVUp()
    {
        hp.max += 200;
        hp.value += 100;
        mp.max += 100;
        dame.value += 50;
    }
    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     if (col.gameObject.CompareTag("Cong"))
    //     {
    //         transform.position = new Vector3(45, 7, 0);
    //         Debug.Log("cong");
    //         ManagerController menu = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerController>();
    //         menu.War();
    //     }
    //}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Cong"))
        {
            ManagerController menu = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerController>();
            menu.War();
            CameraFollow cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
            cam.max_x = 100;
            cam.max_y = 100;
            cam.min_y = -100;
            if (menu.map == 2)
                return;
            transform.position = new Vector3(45, 7, 0);
        }
        
    }
}
