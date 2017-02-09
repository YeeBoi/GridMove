using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    Vector3 pos,input,velocity;
    public LayerMask interectionMaks;
    float speed = 10f;
    public Vector2 gridSpace;
    Collision2D collision;
    bool OnlyHorizontal, OnlyVertical = false;
    float seeThroughtCollision = 1;

    void Start () {
        collision = GetComponent<Collision2D>();
        transform.position = Vector2.zero;
        pos = transform.position;
    }

    void Update()
    {
        if (!Input.GetButton("Push"))
        {
            OnlyHorizontal = false;
            OnlyVertical = false;
        }
        input = new Vector2(Input.GetAxisRaw("x"), Input.GetAxisRaw("y"));
        detectObject();
        if (pos == transform.position)
        {
            if (input.x != 0 && !OnlyVertical)
            {
                velocity = new Vector2(input.x * gridSpace.x, 0);
                if (collision.HorizontalCollisions(velocity, gridSpace.x * seeThroughtCollision))
                {                    
                    pos += new Vector3(input.x * gridSpace.x, 0, 0);
                }
            }else if (input.y != 0 && !OnlyHorizontal)
            {
                velocity = new Vector2(0, input.y * gridSpace.y);
                if (collision.VerticalCollisions(velocity,gridSpace.y * seeThroughtCollision))
                {
                    pos += new Vector3(0, input.y * gridSpace.y, 0);
                }
                
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }

    void detectObject()
    {
        BoxMovement box;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, velocity, 0.75f, interectionMaks);
        if (hit && Input.GetButton("Push"))
        {
            longCollision();
            if (velocity.x != 0)
            {
                OnlyHorizontal = true;
            }else if (velocity.y != 0)
            {
                OnlyVertical = true;
            }
            box = hit.collider.GetComponent<BoxMovement>();
            box.setCanMove(true);
            hit.collider.gameObject.layer = 0;
        } else
        {
            seeThroughtCollision = 1;
        }
    }

    void longCollision()
    {
        
        if (velocity.x != 0)
        {
            seeThroughtCollision = 4f;            
        }
        else if (velocity.y > 0)
        {
            seeThroughtCollision = 2.5f;
        } else if (velocity.y < 0)
        {
            seeThroughtCollision = 3f;
        }
    }
}
