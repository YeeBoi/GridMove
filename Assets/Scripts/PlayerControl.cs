using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    Vector3 pos;
    Vector2 input;
    Vector2 velocity;
    float speed = 5f;
    public Vector2 gridSpace;
    Collision2D collision;
    bool smoothing = true;


    void Start () {
        collision = GetComponent<Collision2D>();
        transform.position = Vector2.zero;
        pos = transform.position;
    }

    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("x"), Input.GetAxisRaw("y"));
        smoothing = (Mathf.Abs(input.x) < Mathf.Abs(input.y)) ? false : true;
        if (pos == transform.position)
        {
            if (input.x != 0 && smoothing)
            {
                velocity = new Vector2(input.x * gridSpace.x, 0);
                if (collision.HorizontalCollisions(velocity, gridSpace.x))
                {
                    pos += new Vector3(input.x * gridSpace.x, 0, 0);
                }
            }else if (input.y != 0)
            {
                velocity = new Vector2(0, input.y * gridSpace.y);
                if (collision.VerticalCollisions(velocity,gridSpace.y))
                {
                    pos += new Vector3(0, input.y * gridSpace.y, 0);
                }
                
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }
}
