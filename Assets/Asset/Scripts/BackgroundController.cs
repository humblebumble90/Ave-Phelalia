using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject bgImg1;
    public GameObject bgImg2;
    public float speed;
    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 newPos;
    public float resetPosX1;
    public float resetPosX2;

    // Start is called before the first frame update
    void Start()
    {
        initialize();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    void initialize()
    {
        if (bgImg1 != null && bgImg2 != null)
        {
            //pos = transform.position;
            pos1 = bgImg1.transform.position;
            pos2 = bgImg2.transform.position;
            Debug.Log("ResetPosx1: " + resetPosX1);
            Debug.Log("ResestPosx2: " + resetPosX2);
        }
        newPos = new Vector2(speed, 0);
        Debug.Log(speed);
    }
    void move()
    {
        if(bgImg1.transform.position.x > resetPosX1)
        {
            bgImg1.transform.position -= newPos;
        }
        else
        {
            bgImg1.transform.position = pos2;
        }

        if(bgImg2.transform.position.x > resetPosX2)
        {
            bgImg2.transform.position -= newPos;
        }
        else
        {
            bgImg2.transform.position = pos2;
        }
    }
    
}
