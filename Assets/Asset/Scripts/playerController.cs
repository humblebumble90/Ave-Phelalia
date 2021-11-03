using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class playerController : MonoBehaviour
{
    public float freRate;
    public Speed speed;
    public Boundary boundary;
    private int _hp;
    private int _score;
    private Vector2 newPos;
    public int hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }
    public int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        checkBounds();
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
    private void OnTriggerEnter(Collider other)
    {
        
    }

}
