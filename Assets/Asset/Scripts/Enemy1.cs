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
    private GameController gc;
    private GameObject gco;
    
    private void Start()
    {
        newPos = new Vector2(_speed, 0);
        gco = GameObject.FindWithTag("GameController");
        gc = gco.GetComponent<GameController>();
        
    }
    private void Update()
    {
        move();
    }
    protected override void attack()
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
        switch(col.tag)
        {
            case "Player":
                Debug.Log("Hit the player");
                break;
            case "PlayerFire":
                Debug.Log("Hit by PlayerFire");
                Destroy(this.gameObject);
                gc.Score += 100;
                Debug.Log("Score: " + gc.Score);
                break;
            default:
                break;
        }
    }
}
