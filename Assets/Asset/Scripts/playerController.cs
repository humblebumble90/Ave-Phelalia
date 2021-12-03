using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class playerController : MonoBehaviour
{
    public float fireRate;
    public Speed speed;
    public Boundary boundary;
    private bool collidable = true;
    private Vector2 newPos;
    private float fireTime;
    public GameObject spawningPoint;
    public GameObject fire;
    private GameController gc;
    private HpBarController hc;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gco = GameObject.FindGameObjectWithTag("GameController");
        GameObject hpo = GameObject.FindGameObjectWithTag("HpStatus");
        gc = gco.GetComponent<GameController>();
        hc = hpo.GetComponent<HpBarController>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        checkBounds();
        shoot();
    }
    void move()
    {
        newPos = transform.position;
        if (Input.GetAxis("Vertical") > 0.0f)
        {
            newPos += new Vector2(0.0f, speed.max);
        }
        if (Input.GetAxis("Vertical") < 0.0f)
        {
            newPos += new Vector2(0.0f, speed.min);
        }
        if (Input.GetAxis("Horizontal") > 0.0f)
        {
            newPos += new Vector2(speed.max, 0.0f);
        }
        if (Input.GetAxis("Horizontal") < 0.0f)
        {
            newPos += new Vector2(speed.min, 0.0f);
        }
        transform.position = newPos;
    }
    void checkBounds()
    {
        if (transform.position.x < boundary.Left)
        {
            transform.position = new Vector2(boundary.Left, transform.position.y);
        }
        //Check right boundary.
        if (transform.position.x > boundary.Right)
        {
            transform.position = new Vector2(boundary.Right, transform.position.y);
        }
        if (transform.position.y > boundary.Top)
        {
            transform.position = new Vector3(transform.position.x, boundary.Top);
        }
        if (transform.position.y < boundary.Bottom)
        {
            transform.position = new Vector3(transform.position.x, boundary.Bottom);
        }
    }
    void shoot()
    {
        if (fireRate >= fireTime)
        {
            fireTime += Time.deltaTime;
        }
        else
        {
            if (Input.GetButton("Fire1"))
            {
                Instantiate(fire, spawningPoint.transform.position, spawningPoint.transform.rotation);
                fireTime = 0;
            }
        }
    }
    private IEnumerator getHit()
    {
        GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
        collidable = false;
        gc.setHp(-20);
        hc.SetDamage(20);
        yield return new WaitForSeconds(1.0f);
        GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
        collidable = true;
    }
    public void setFireRate(float rate)
    {
        fireRate = rate;
    }
    public void destroyPlayer()
    {
        Destroy(this.gameObject);
    }
    public bool getCollidable() => collidable;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy" && collidable == true
            || col.tag == "EnemyFire" && collidable == true)
        {
            Debug.Log("Hit by enemy");   
            StartCoroutine(getHit());
        }
    }

}
