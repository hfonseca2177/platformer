using UnityEngine;

public enum ItemType {  Coin, Ammo, Health, Inventory }

public class Collectable : MonoBehaviour
{

    [SerializeField] private ItemType itemType;
    [SerializeField] private string itemName;


    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (Player.Instance.gameObject == collision.gameObject)
        {
            Player player = Player.Instance;
            if (ItemType.Coin.Equals(this.itemType))
            {
                player.AddCoin();
                Destroy(gameObject);
            }
            else if (ItemType.Health.Equals(this.itemType))
            {
                player.AddHeart();
                Destroy(gameObject);
            }
            else if (ItemType.Inventory.Equals(this.itemType)){
                bool collected = player.inventory.AddItem(this);
                if (collected)
                {
                    gameObject.SetActive(false);
                }
            }
            
        }
    }
    
}
