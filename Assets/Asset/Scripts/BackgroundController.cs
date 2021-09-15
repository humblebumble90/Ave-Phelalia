using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject bgImg;
    public float resetPosX;
    private float speed;
    private Vector3 pos;
    private Vector3 newPos;
    private DataStorage dataStorage;
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
    void move()
    {
        if (transform.position.x > resetPosX)
        {
            transform.position -= newPos;
        }
        else
        {
            transform.position = pos;
            Debug.Log("Reset position");
        }
        Debug.Log("Is it moving?");
    }
    void initialize()
    {
        dataStorage = new DataStorage();
        pos = new Vector3(0, 0, 0);
        if(bgImg != null)
        {
            pos = transform.position;
        }
        switch(tag)
        {
            case "TitleScreenBg":
                speed = dataStorage.getTitleScreenSpeed();
                break;
            default:
                break;
        }
        newPos = new Vector3(speed, 0, 0);
        Debug.Log(tag);
        Debug.Log(speed);
    }
}
