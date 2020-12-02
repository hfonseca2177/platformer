using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {  Coin, Ammo, Health }

public class Collectable : MonoBehaviour
{

    [SerializeField] private ItemType itemType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colliding something...");

        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Collision with a " + itemType);
            collision.gameObject.GetComponent<NewPlayer>().coins += 1;
            Destroy(gameObject);
        }
    }
    
}
