using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour {
    public PlayerControl player;
    bool canMove = false;
    Vector2 diff;
    public static bool pushingOnebox;
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
        if (!Input.GetButton("Push") && transform.position.x % player.gridSpace.x == 0 && 
            transform.position.y % player.gridSpace.y == 0)
        {
            canMove = false;
            if (this.gameObject.layer != 10)
            {
                this.gameObject.layer = 10;
            }
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
