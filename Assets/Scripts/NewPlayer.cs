using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : PhysicsObject
{
    const string HorizontalKey = "Horizontal";
    const string JumpKey = "Jump";
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    public int coins;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        targetVelocity = new Vector2(Input.GetAxis(HorizontalKey) * speed ,0);
        if(Input.GetButton(JumpKey) && grounded)
        {
            velocity.y = jumpForce;
        }        
    }
}
