using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour {
    public PlayerControl player;
    bool canMove = false;
    Vector2 diff;
    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update () {
        Move();        
	}

    void Move()
    {
        if (!canMove)
        {
            diff = transform.position - player.transform.position;
        }else
        {
            transform.position = (Vector2)player.transform.position + diff;
        } 
        if (!Input.GetButton("Push"))
        {
            canMove = false;
        }       
    }

    public void setCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
    public bool getCanMove()
    {
        return canMove;
    }
}
