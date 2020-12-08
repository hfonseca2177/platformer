using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Almost same Original script from Curse
I want to have a cleaner code for this Raycast support
I did a refactor and it is on the Enemy.cs
 */
public class EnemyOriginal : PhysicsObject
{

    [SerializeField] private float speed = 1;
    private int direction = 1;

    private RaycastHit2D leftWallRay;
    private RaycastHit2D rightWallRay;

    private RaycastHit2D leftFloorRay;
    private RaycastHit2D rightFloorRay;

    private Vector2 raycastOffset = new Vector2(1, 0);
    [SerializeField] private int raycastLength = 1;
    [SerializeField] private LayerMask layerMaskIgnore;
    [SerializeField] private int attackPower = 10;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(speed * direction, 0);

        leftFloorRay = Physics2D.Raycast(new Vector2(transform.position.x - raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down, raycastLength);
        Debug.DrawRay(new Vector2(transform.position.x - raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down * raycastLength, Color.green);
        if (leftFloorRay.collider == null)
        {
            direction = 1;
        }
        else
        {
            Debug.Log("Left floor Colliding with " + leftFloorRay.collider.gameObject);
        }
        rightFloorRay = Physics2D.Raycast(new Vector2(transform.position.x + raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down, raycastLength);
        Debug.DrawRay(new Vector2(transform.position.x + raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down * raycastLength, Color.blue);
        if (rightFloorRay.collider == null)
        {
            direction = -1;
        }
        else
        {
            Debug.Log("Right floor Colliding with " + rightFloorRay.collider.gameObject);
        }
        leftWallRay = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, raycastLength, layerMaskIgnore);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.left * raycastLength, Color.red);
        if (leftWallRay.collider != null)
        {
            direction = 1;
            Debug.Log("Left wall Colliding with " + leftWallRay.collider.gameObject);
        }

        rightWallRay = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, raycastLength, layerMaskIgnore);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.right * raycastLength, Color.yellow);
        if (rightWallRay.collider != null)
        {
            direction = -1;
            Debug.Log("Left wall Colliding with " + rightWallRay.collider.gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            Player.Instance.TakeDamage(attackPower);
        }
    }

}
