using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{
    [Header("Attributes")]
    [SerializeField] private float speed = 1;
    [SerializeField] private int attackPower = 10;
    public int health = 100;

    //Raycast help to check if the enemy is about to hit a wall or fall
    [Header("Raycast Sensor")]
    //offset relative to center of object therefore we can predict a hit
    [SerializeField] private int raycastOffset = 1;
    [SerializeField] private int raycastLength = 1;
    //layers that raycast will interact with
    [SerializeField] private LayerMask layerMaskIgnore;
    //Case necessary, projects a visual ray to check the reach
    [SerializeField] private bool projectRay = false;
    //Current X direction Right=1 Left=-1
    private int direction = 1;

    // Update is called once per frame
    void Update()
    {
        if (CheckIfGroundNotExists())
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
        //offset=0 because the ray is pointing directly to sides
        RaycastHit2D raycastHit = CalcRaycast(0, new Vector2(direction, 0));
        return raycastHit.collider != null;
    }

    private RaycastHit2D CalcRaycast(int xOffset, Vector2 rayDirection)
    {
        Vector2 origin = new Vector2(transform.position.x + xOffset, transform.position.y);
        if (projectRay)
        {
            Debug.DrawRay(origin, rayDirection * raycastLength, Color.red);
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

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
