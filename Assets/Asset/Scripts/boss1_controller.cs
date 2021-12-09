using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class boss1_controller : Enemy
{
    public int _hp;
    public float _speed;
    public float _fireRate;
    private float fireCooltime;
    private Vector2 currPos;
    private Vector2 newPos;
    private GameController gc;
    private GameObject gco;
    public Boundary boundary;
    private BulletController bc;
    private float pattern1Length;
    private float pattern2Length;
    public GameObject cannon1;
    public GameObject cannon2;
    public GameObject cannon3;
    public GameObject enemyFire;
    // Start is called before the first frame update
    void Start()
    {
        gco = GameObject.FindWithTag("GameController");
        gc = gco.GetComponent<GameController>();
        newPos = new Vector2(0, _speed);
        fireCooltime = _fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        attack();
        checkBounds();
    }
    protected override void move()
    {
        currPos = transform.position;
        currPos -= newPos;
        transform.position = currPos;
        if (transform.position.y <= boundary.Bottom || transform.position.y >= boundary.Top)
            {
                _speed = -_speed;
                newPos = new Vector2(0, _speed);
            }
    }
    protected override void attack()
    {
        if(fireCooltime < _fireRate)
        {
            fireCooltime += Time.deltaTime;
        } 
        else
        {
            enemyFire.GetComponent<BulletController>().setTarget(true);
            Instantiate(enemyFire, cannon1.transform.position, Quaternion.identity);
            Instantiate(enemyFire, cannon2.transform.position, Quaternion.identity);
            Instantiate(enemyFire, cannon3.transform.position, Quaternion.identity);
            fireCooltime = 0;
            enemyFire.GetComponent<BulletController>().setTarget(false);
        }
    }
    void checkBounds()
    {
        if (transform.position.x <= boundary.Left)
        {
            transform.position = new Vector2(boundary.Left, transform.position.y);
        }
        //Check right boundary.
        if (transform.position.x >= boundary.Right)
        {
            transform.position = new Vector2(boundary.Right, transform.position.y);
        }
        if (transform.position.y >= boundary.Top)
        {
            transform.position = new Vector3(transform.position.x, boundary.Top);
        }
        if (transform.position.y <= boundary.Bottom)
        {
            transform.position = new Vector3(transform.position.x, boundary.Bottom);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "PlayerFire")
        {
            Debug.Log("Shot by Player");
            gc.audioSources[(int)SoundClip.EXPLOSION_SOUND].Play();
            _hp -= 1;
            if(_hp <= 0)
            {
                Destroy(this.gameObject);
                gc.Score += 1000;
                gc.clearStage();
            }
        }
    }
}
