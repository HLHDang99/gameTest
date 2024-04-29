using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBoot : MonoBehaviour
{
    PlayerController player;
    ManagerController manager;
    public float HP;
    public float DAME;
    GameObject _player;
    Animator ani;
    struct ChiSo
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
    ChiSo hp;
    ChiSo dame;

    bool face_right = true;
    public float speed;
    public float _distance;
    ChiSo time_spawm;
    void Start()
    {
        hp = new ChiSo(HP, 0, HP);
        dame = new ChiSo(DAME, 50, DAME);
        time_spawm = new ChiSo(1, 0, 0);
        _player = GameObject.FindGameObjectWithTag("Player");
        player = _player.GetComponent<PlayerController>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerController>();
        ani = GetComponent<Animator>();
        ani.SetBool("Attack", false);
    }
    public void SetPlayer(GameObject value)
    {
        _player = value;
    }
    void Update()
    {
        FollowPlayer();
    }
    void FollowPlayer()
    {
        if ((gameObject.transform.position.x > _player.transform.position.x && face_right == false)
        || (gameObject.transform.position.x < _player.transform.position.x && face_right == true))
        {
            face_right = !face_right;
            gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, 0);
        }
        Vector3 distance = this._player.transform.position - transform.position;
        if (distance.magnitude > _distance)
        {
            Vector3 targetPoint = this._player.transform.position - distance.normalized * _distance;
            ManagerController manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ManagerController>();
            if (!((gameObject.transform.position.x > _player.transform.position.x && manager.Check(transform.position + new Vector3(0.25f, 0, 0)) == 1)
            || (gameObject.transform.position.x > _player.transform.position.x && manager.Check(transform.position - new Vector3(0.25f, 0, 0)) == 1)))
                gameObject.transform.position =
                    Vector3.MoveTowards(gameObject.transform.position, targetPoint, speed * Time.deltaTime);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player.AddDame(dame.value);
            ani.SetBool("Attack", true);
            Invoke("SetAttack", 1f);
        }
    }
    void SetAttack()
    {
        ani.SetBool("Attack", false);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("hitPlayer"))
        {
            hp.value -= player.Dame();
            Debug.Log(player.Dame());
            Debug.Log(hp.value);
            ani.SetBool("Hit", true);
            Invoke("SetAni", 0.5f);
            if (hp.value < 0)
            {
                if (manager.BOSS.activeSelf)
                {
                    manager.WIN();
                }
                Invoke("Destroy", 1);
            }
        }
        if(col.tag == "Player" && !ani.GetBool("Attack")){
            player.AddDame(dame.value);
            ani.SetBool("Attack", true);
            Invoke("SetAttack", 1f);
        }
    }
    void SetAni()
    {
        ani.SetBool("Hit", false);
    }
    void Destroy()
    {
        manager.NewItem(transform.position);
        gameObject.SetActive(false);
    }
}
