using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
public class Enemy3 : Enemy
{
    public GameObject spawnPoint;
    public GameObject enemyFire;
    public float _hp;
    public float _speed;
    public float _fireRate;
    private float _fireCooltime;
    private Vector2 newPos;
    private Vector2 currPos;
    private GameController gc;
    private GameObject gco;
    // Start is called before the first frame update
    void Start()
    {
        newPos = new Vector2(_speed, 0);
        gco = GameObject.FindWithTag("GameController");
        gc = gco.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        attack();
    }

    protected override void attack()
    {
        if (_fireRate >= _fireCooltime)
        {
            _fireCooltime += Time.deltaTime;
        }
        else
        {
            enemyFire.GetComponent<BulletController>().setTarget(true);
            Instantiate(enemyFire, spawnPoint.transform.position, spawnPoint.transform.rotation);
            enemyFire.GetComponent<BulletController>().setTarget(false);
            _fireCooltime = 0;
        }
    }

    protected override void move()
    {
        currPos = transform.position;
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
                gc.audioSources[(int)SoundClip.EXPLOSION_SOUND].Play();
                _hp -= 1;
                if(_hp <= 0)
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
