using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    public float _hp;
    public float _speed;
    public float _fireRate;
    private Vector2 newPos;
    private Vector2 currPos;
    private GameController gc;
    private GameObject gco;
    private playerController player;


    // Start is called before the first frame update
    void Start()
    {
        newPos = new Vector2(_speed, 0);
        gco = GameObject.FindWithTag("GameController");
        gc = gco.GetComponent<GameController>();
        player = GameObject.FindWithTag("Player").GetComponent<playerController>();
        transform.rotation.SetEulerRotation(0, 0, 45);
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
        currPos -= newPos;
        transform.position = currPos;
    }
}
