using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class boss1_controller : Enemy
{
    public int _hp;
    public float _speed;
    public float _fireRate;
    private Vector2 currPos;
    private Vector2 newPos;
    private GameController gc;
    private GameObject gco;
    public Boundary boundary;
    private bool pattern1;
    private float pattern1Length;
    private bool pattern2;
    private float pattern2Length;
    // Start is called before the first frame update
    void Start()
    {
        gco = GameObject.FindWithTag("GameController");
        gc = gco.GetComponent<GameController>();
        newPos = new Vector2(_speed, 0);
        pattern1 = true;
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
    }

    protected override void move()
    {
        currPos = transform.position;
        currPos -= newPos;
        transform.position = currPos;
        if (pattern1)
        {
            if(transform.position.x <= boundary.Left || transform.position.x >= boundary.Right)
            {
                _speed = -_speed;
                newPos = new Vector2(_speed, 0);
            }
            pattern1Length += Time.deltaTime;
            if(pattern1Length >= 5.0f)
            {
                pattern1 = false;
                pattern2 = true;
                pattern1Length = 0f;
                newPos = new Vector2(0, _speed);
            }
        }
        if(pattern2)
        {
            if (transform.position.y <= boundary.Bottom || transform.position.y >= boundary.Top)
            {
                _speed = -_speed;
                newPos = new Vector2(0, _speed);
            }
            pattern2Length += Time.deltaTime;
            if (pattern2Length >= 5.0f)
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
}
