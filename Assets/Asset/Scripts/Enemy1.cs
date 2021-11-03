using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    public float _hp;
    public float _speed;
    public float _fireRate;
    private Vector2 newPos;
    private Vector2 currPos;
    
    private void Start()
    {
        newPos = new Vector2(_speed, 0);
        
    }
    private void Update()
    {
        move();
    }
    protected override void attack()
    {
    }

    protected override void destroy()
    {
    }

    protected override void move()
    {
        currPos = transform.position;
        currPos -= newPos;
        transform.position = currPos;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
    }
}
