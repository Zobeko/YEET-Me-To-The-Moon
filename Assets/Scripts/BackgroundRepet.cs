using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepet : MonoBehaviour
{

    private BoxCollider2D bc;

    private Rigidbody2D rb;

    private float length;
    public float speed = 3f;

    
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        length = bc.size.y;
        rb.velocity = new Vector2(0, -speed);

    }

   
    void Update()
    {
        if(transform.position.y < -length)
        {
            Reposition();
        }
    }

    void Reposition()
    {
        Vector2 vector = new Vector2(0, 2f*length);
        transform.position += vector;
    }
}
