using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy:MonoBehaviour
{
    private float hp;
    private float speed;
    private float fireRate;
    private bool ifBoss;
    private bool ifFire;

    public abstract void move();
    public abstract void attack();
    public abstract void destroy();
}
