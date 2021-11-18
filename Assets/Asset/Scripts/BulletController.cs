using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float _speed;
    private float horSpeed;
    private float verSpeed;
    private float diffX;
    private float diffY;
    private float angle;
    private Vector2 currPos;
    private Vector2 newPos;
    private Vector2 target;
    public bool isTargetting;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        newPos = new Vector2(_speed, 0);
    }
        // Update is called once per frame
        
    void Update()  
    {
        move();
    }    
    void move()     
    {     
        if(this.gameObject.tag == "PlayerFire")
        {
            currPos = transform.position;
            currPos += newPos;
            transform.position = currPos;
        }
        else
        {
            if(isTargetting)
            {
                if (player == null)
                {
                    player = GameObject.FindWithTag("Player");
                    target = player.transform.position;
                    diffX = Mathf.Abs(transform.position.x - target.x);
                    diffY = Mathf.Abs(transform.position.y - target.y);
                    horSpeed = diffX > diffY ? _speed : diffX / diffY * _speed;
                    verSpeed = diffX < diffY ? _speed : diffY / diffX * _speed;
                    horSpeed = transform.position.x < target.x ? horSpeed : -horSpeed;
                    verSpeed = transform.position.y < target.y ? verSpeed : -verSpeed;
                    newPos = new Vector2(horSpeed, verSpeed);
                    angle = Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg;
                    Debug.Log("Angle: " + angle);
                    if (target.y > transform.position.y)
                    {
                        transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                    }
                    else
                    {
                        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    }


                }
                currPos = transform.position;
                currPos += newPos;
                transform.position = currPos;
            }
            else
            {
                currPos = transform.position;
                currPos -= newPos;
                transform.position = currPos;
            }
        }
    }
    void setSpeed(float speed)
    {
        _speed = speed;
    }
    public void setTarget(bool target)
    {
        isTargetting = target;
    }
    private void OnTriggerEnter2D(Collider2D col)
        
    {      
        if (this.tag == "PlayerFire" && col.tag == "Enemy")       
        {        
            Destroy(this.gameObject);    
        }    
    }
}
