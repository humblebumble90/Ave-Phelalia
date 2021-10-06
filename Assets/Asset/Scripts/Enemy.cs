using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy
{
    private float hp;
    private float speed;
    private float fireRate;
    private bool ifFire;
    private bool ifBoss;
    public Enemy(float hp, float speed, float fireRate, bool ifFire, bool ifBoss)
    {
        this.hp = hp;
        this.speed = speed;
        this.fireRate = fireRate;
        this.ifFire = ifFire;
        this.ifBoss = ifBoss;
    }

    public abstract void move();
    public abstract void attack();
    public abstract void destroy();
}
