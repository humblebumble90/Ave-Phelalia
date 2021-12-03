using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Enemy4 : Enemy
{
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject enemyFire;
    public float _hp;
    public float _speed;
    private float horSpeed;
    private float verSpeed;
    public float _fireRate;
    private float fireCooltime;
    private Vector2 newPos;
    private Vector2 currPos;
    private float startPosY;
    private GameController gc;
    private GameObject gco;
    // Start is called before the first frame update
    void Start()
    {
        horSpeed = _speed;
        verSpeed = _speed * 0.5f;
        newPos = new Vector2(horSpeed, verSpeed);
        startPosY = transform.position.y;
        gco = GameObject.FindWithTag("GameController");
        gc = gco.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        attack();
        move();
    }

    protected override void attack()
    {
        if (fireCooltime < _fireRate)
        {
            fireCooltime += Time.deltaTime;
        }
        else
        {
            enemyFire.GetComponent<BulletController>().setTarget(true);
            Instantiate(enemyFire, spawnPoint1.transform.position, Quaternion.identity);
            Instantiate(enemyFire, spawnPoint2.transform.position, Quaternion.identity);
            fireCooltime = 0;
            enemyFire.GetComponent<BulletController>().setTarget(false);
        }
    }

    protected override void move()
    {
        currPos = transform.position;
        if(transform.position.y < startPosY - 0.25f || transform.position.y > startPosY + 0.25f)
        {
            verSpeed = -verSpeed;
            newPos = new Vector2(horSpeed, verSpeed);
        }
        currPos -= newPos;
        transform.position = currPos;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Player":
                Debug.Log("Hit the player");
                break;
            case "PlayerFire":
                Debug.Log("Hit by PlayerFire");
                _hp -= 1;
                if (_hp <= 0)
                {
                    Destroy(this.gameObject);
                    gc.Score += 300;
                    Debug.Log("Score: " + gc.Score);
                }
                break;
            default:
                break;
        }
    }
}
