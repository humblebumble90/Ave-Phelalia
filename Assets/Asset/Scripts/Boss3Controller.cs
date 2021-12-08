using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Boss3Controller : Enemy
{
    public int _hp;
    public float _speed;
    private float horSpeed;
    private float verSpeed;
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
    public GameObject spawningPoint1;
    public GameObject spawningPoint2;
    public GameObject spawningPoint3;
    public GameObject spawningPoint4;
    public GameObject spawningPoint5;
    public GameObject enemyFire;
    // Start is called before the first frame update
    void Start()
    {
        gco = GameObject.FindWithTag("GameController");
        gc = gco.GetComponent<GameController>();
        horSpeed = _speed;
        verSpeed = _speed * 0.5f;
        newPos = new Vector2(horSpeed, 0);
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
            Instantiate(enemyFire, spawningPoint1.transform.position, Quaternion.identity);
            Instantiate(enemyFire, spawningPoint3.transform.position, Quaternion.identity);
            Instantiate(enemyFire, spawningPoint5.transform.position, Quaternion.identity);
            fireCooltime1 = 0;
            enemyFire.GetComponent<BulletController>().setTarget(false);
        }
        if (fireCooltime2 < _fireRate)
        {
            fireCooltime2 += Time.deltaTime;
        }
        else
        {
            Instantiate(enemyFire, spawningPoint2.transform.position, Quaternion.identity);
            Instantiate(enemyFire, spawningPoint4.transform.position, Quaternion.identity);
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
            if (transform.position.x <= boundary.Left)
            {
                horSpeed = -horSpeed;
                newPos = new Vector2(horSpeed * 0.25f, 0);
            }
            if (transform.position.x >= boundary.Right)
            {
                horSpeed = -horSpeed;
                newPos = new Vector2(horSpeed, 0);
            }
            pattern1Length += Time.deltaTime;
            if (pattern1Length >= 10.0f)
            {
                pattern1 = false;
                pattern2 = true;
                pattern1Length = 0f;
                newPos = new Vector2(horSpeed*0.5f, verSpeed);
            }
        }
            
        if(pattern2)
        {    
            if (transform.position.x <= boundary.Left)    
            {  
                horSpeed = -horSpeed;    
                newPos = new Vector2(horSpeed *0.25f, verSpeed);
                
            }      
            if (transform.position.x >= boundary.Right)  
            { 
                horSpeed = -horSpeed; 
                newPos = new Vector2(horSpeed*0.5f, verSpeed);
                
            }
                
            if(transform.position.y <= boundary.Bottom)    
            {
                verSpeed = -verSpeed;     
                newPos = new Vector2(horSpeed*0.5f, verSpeed);  
            }
            
            if (transform.position.y >= boundary.Top)   
            {   
                verSpeed = -verSpeed;   
                newPos = new Vector2(horSpeed, verSpeed * 0.5f);    
            }    
            pattern2Length += Time.deltaTime;     
            if (pattern2Length >= 10.0f)    
            {    
                pattern2 = false;  
                pattern1 = true;   
                pattern2Length = 0f;   
                newPos = new Vector2(horSpeed, 0); 
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
            gc.audioSources[(int)SoundClip.EXPLOSION_SOUND].Play();
            _hp -= 1;
            if (_hp <= 0)
            {
                Destroy(this.gameObject);
                gc.Score += 5000;
                gc.clearStage();
            }
        }
    }
}
