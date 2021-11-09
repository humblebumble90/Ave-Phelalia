using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    private Vector2 currPos;
    private Vector2 newPos;
    // Start is called before the first frame update
    void Start()
    {
        newPos = new Vector2(speed, 0);
    }

        // Update is called once per frame
        void Update()
        {
            move();
        }

        void move()
        {
        currPos = transform.position;
        currPos += newPos;
        transform.position = currPos;
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Enemy")
            {
                Debug.Log("Bullet hits enemy");
                Destroy(this.gameObject);
            }    
        }
}
