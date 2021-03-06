using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Enemy2 : Enemy
{
    public int _hp;
    public float _speed;
    public float _fireRate;
    private Vector2 currPos;
    private GameController gc;
    private GameObject gco;
    private GameObject player;
    private Vector2 target;
    private float horSpeed;
    private float verSpeed;
    private float diffX;
    private float diffY;
    private float angle;
    private Vector2 thisSpeed;
    // Start is called before the first frame update
    void Start()
    {
        gco = GameObject.FindWithTag("GameController");
        gc = gco.GetComponent<GameController>();
        calculateTarget();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    protected override void attack()
    {
    }

    protected override void move()
    {
        currPos = transform.position;
        currPos += thisSpeed;
        transform.position = currPos;
    }
    private void calculateTarget()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if(player != null)
            {
                target = player.transform.position;
                diffX = Mathf.Abs(transform.position.x - target.x);
                diffY = Mathf.Abs(transform.position.y - target.y);
                horSpeed = diffX > diffY ? _speed : diffX / diffY * _speed;
                verSpeed = diffX < diffY ? _speed : diffY / diffX * _speed;
                horSpeed = transform.position.x < target.x ? horSpeed : -horSpeed;
                verSpeed = transform.position.y < target.y ? verSpeed : -verSpeed;
                thisSpeed = new Vector2(horSpeed, verSpeed);
                angle = Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg;
                if (target.y > transform.position.y)
                {
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                }
                else
                {
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
            else
            {
                target = new Vector2(0,0);
                diffX = Mathf.Abs(transform.position.x - target.x);
                diffY = Mathf.Abs(transform.position.y - target.y);
                horSpeed = diffX > diffY ? _speed : diffX / diffY * _speed;
                verSpeed = diffX < diffY ? _speed : diffY / diffX * _speed;
                horSpeed = transform.position.x < target.x ? horSpeed : -horSpeed;
                verSpeed = transform.position.y < target.y ? verSpeed : -verSpeed;
                thisSpeed = new Vector2(horSpeed, verSpeed);
                angle = Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "PlayerFire")
        {
            gc.audioSources[(int)SoundClip.EXPLOSION_SOUND].Play();
            _hp -= 1;
            if(_hp <= 0)
            {
                Destroy(this.gameObject);
                gc.Score += 200;
            }
        }
    }
}
