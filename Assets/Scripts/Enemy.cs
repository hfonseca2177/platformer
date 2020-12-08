using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{

    [SerializeField] private float speed = 1;
    [SerializeField] private int raycastOffset = 1;
    [SerializeField] private int raycastLength = 1;
    [SerializeField] private LayerMask layerMaskIgnore;
    [SerializeField] private int attackPower = 10;
    [SerializeField] private bool projectRay =false;
    private int direction = 1;

    // Update is called once per frame
    void Update()
    {  
        if(CheckIfGroundNotExists())
        {
            ReverseDirection();
        }

        if (CheckIfWallExists())
        {
            ReverseDirection();
        }
        targetVelocity = new Vector2(speed * direction, 0);
    }

    private void ReverseDirection()
    {
        direction *= -1;
    }

    private bool CheckIfGroundNotExists()
    {
        RaycastHit2D raycastHit = CalcRaycast(direction * raycastOffset, Vector2.down);
        return raycastHit.collider == null;
    }

    private bool CheckIfWallExists()
    {
        RaycastHit2D raycastHit = CalcRaycast(0, new Vector2(direction,0));
        return raycastHit.collider != null;
    }

    private RaycastHit2D CalcRaycast(int xOffset, Vector2 rayDirection)
    {
        Vector2 origin = new Vector2(transform.position.x + xOffset, transform.position.y);
        if (projectRay)
        {
            Debug.DrawRay(origin , rayDirection * raycastLength, Color.red);
        }
        return Physics2D.Raycast(origin, rayDirection, raycastLength, layerMaskIgnore);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            Player.Instance.TakeDamage(attackPower);
        }
    }

}
