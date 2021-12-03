using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Boss2Controller : Enemy
{
    public int _hp;
    public float _speed;
    public float _fireRate;
    private float fireCooltime1;
    private float fireCooltime2;
    private Vector2 currPos;
    private Vector2 newPos;
    private GameController gc;
    private GameObject gco;
    public Boundary boundary;
    private BulletController bc;
    private bool pattern1;
    private float pattern1Length;
    private bool pattern2;
    private float pattern2Length;
    public GameObject cannon1;
    public GameObject cannon2;
    public GameObject cannon3;
    public GameObject cannon4;
    public GameObject cannon5;
    public GameObject enemyFire;
    // Start is called before the first frame update
    void Start()
    {
        gco = GameObject.FindWithTag("GameController");
        gc = gco.GetComponent<GameController>();
        newPos = new Vector2(_speed, 0);
        pattern1 = true;
        fireCooltime1 = _fireRate;
        fireCooltime2 = _fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        attack();
        checkBounds();
    }
    protected override void attack()
    {
        if (fireCooltime1 < _fireRate)
        {
            fireCooltime1 += Time.deltaTime;
        }
        else
        {
            enemyFire.GetComponent<BulletController>().setTarget(true);
            Instantiate(enemyFire, cannon1.transform.position, Quaternion.identity);
            Instantiate(enemyFire, cannon3.transform.position, Quaternion.identity);
            Instantiate(enemyFire, cannon5.transform.position, Quaternion.identity);
            fireCooltime1 = 0;
            enemyFire.GetComponent<BulletController>().setTarget(false);
        }
        if (fireCooltime2 < _fireRate)
        {
            fireCooltime2 += Time.deltaTime;
        }
        else
        {
            Instantiate(enemyFire, cannon2.transform.position, Quaternion.identity);
            Instantiate(enemyFire, cannon4.transform.position, Quaternion.identity);
            fireCooltime2 = 0;
        }
    }

    protected override void move()
    {
        currPos = transform.position;
        currPos -= newPos;
        transform.position = currPos;
        if (pattern1)
        {
            if (transform.position.x <= boundary.Left/2 || transform.position.x >= boundary.Right)
            {
                _speed = -_speed;
                newPos = new Vector2(_speed, 0);
            }
            pattern1Length += Time.deltaTime;
            if (pattern1Length >= 10.0f)
            {
                pattern1 = false;
                pattern2 = true;
                pattern1Length = 0f;
                newPos = new Vector2(0, _speed * 0.5f);
            }
        }
        if (pattern2)
        {
            if (transform.position.y <= boundary.Bottom || transform.position.y >= boundary.Top)
            {
                _speed = -_speed;
                newPos = new Vector2(0, _speed * 0.5f);
            }
            pattern2Length += Time.deltaTime;
            if (pattern2Length >= 10.0f)
            {
                pattern2 = false;
                pattern1 = true;
                pattern2Length = 0f;
                newPos = new Vector2(_speed, 0);
            }
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
        if (col.tag == "PlayerFire")
        {
            Debug.Log("Shot by Player");
            _hp -= 1;
            if (_hp <= 0)
            {
                Destroy(this.gameObject);
                gc.Score += 2000;
                gc.clearStage();
            }
        }
    }
}
