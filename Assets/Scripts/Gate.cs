using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField]private GameObject openenerKey;  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Player.Instance.gameObject == collision.gameObject)
        {
            Player player = Player.Instance;
            if (player.inventory.HasInventoryItem(openenerKey.name)){
                Collectable key = player.inventory.RetrieveItem(openenerKey.name);
                player.inventory.RemoveItem(key);
                Destroy(gameObject);
            }
        }
    }
}
