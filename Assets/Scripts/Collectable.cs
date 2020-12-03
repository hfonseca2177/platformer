using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {  Coin, Ammo, Health, Inventory }

public class Collectable : MonoBehaviour
{

    [SerializeField] private ItemType itemType;
    [SerializeField] private string itemName;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            bool destroyCollectable = true;
            NewPlayer player = collision.gameObject.GetComponent<NewPlayer>();
            if (ItemType.Coin.Equals(this.itemType))
            {
                player.AddCoin();
            }
            else if (ItemType.Health.Equals(this.itemType))
            {
                player.AddHeart();
            }
            else if (ItemType.Inventory.Equals(this.itemType)){
                destroyCollectable = player.inventory.AddItem(this);
            }
            if (destroyCollectable)
            {
                Destroy(gameObject);
            }
        }
    }
    
}
