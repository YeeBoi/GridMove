using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision2D : MonoBehaviour
{    
    public LayerMask collisionMask;
    
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;


    const float skinWidth = .015f;
    float horizontalRaySpacing;
    float verticalRaySpacing;
    PlayerControl player;

    BoxCollider2D collider;
    RaycastOrigins raycastOrigins;
    Vector2 velocity;

    void Start()
    {
        player = GetComponent<PlayerControl>();
        collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    void Update()
    {
        UpdateRaycastOrigins();
    }

    public bool HorizontalCollisions(Vector3 velocity, float rayLength)
    {
        bool canMove = true;
        float directionX = Mathf.Sign(velocity.x);

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            if (hit)
            {
                canMove = false;
            }

        }
        return canMove;
    }

    public bool VerticalCollisions(Vector3 velocity, float rayLength)
    {
        bool canMove = true;
        float directionY = Mathf.Sign(velocity.y); 

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
            if (hit)
            {
                canMove = false;
            }
        }
        return canMove;
    }

    public void UpdateRaycastOrigins()
    {

        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);


        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;        
    }
}
