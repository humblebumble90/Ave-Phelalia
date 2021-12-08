using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Enemy1 : Enemy
{
    public int _hp;
    public float _speed;
    private Vector2 newPos;
    private Vector2 currPos;
    private GameController gc;
    private GameObject gco;
    public GameObject spawnPoint;
    public GameObject enemyFire;
    public float _fireRate;
    private float _fireCooltime;

    private void Start()
    {
        newPos = new Vector2(_speed, 0);
        gco = GameObject.FindWithTag("GameController");
        gc = gco.GetComponent<GameController>();
        _fireCooltime = _fireRate;


    }
    private void Update()
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
            Instantiate(enemyFire, spawnPoint.transform.position, spawnPoint.transform.rotation);
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
        switch(col.tag)
        {
            case "Player":
                Debug.Log("Hit the player");
                break;
            case "PlayerFire":
                Debug.Log("Hit by PlayerFire");
                _hp -= 1;
                if(_hp <= 0)
                {
                    gc.audioSources[(int)SoundClip.EXPLOSION_SOUND].Play();
                    Destroy(this.gameObject);
                    gc.Score += 100;
                    Debug.Log("Score: " + gc.Score);
                }
                break;
            default:
                break;
        }
    }
}
