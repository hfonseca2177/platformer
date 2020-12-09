using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField]private GameObject openenerKey;  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Player.Instance.gameObject == collision.gameObject)
        {
            GameManager gameManager = GameManager.Instance;
            if (gameManager.HasInventoryItem(openenerKey.name)){
                Collectable key = gameManager.RetrieveItem(openenerKey.name);
                gameManager.RemoveItem(key);
                Destroy(gameObject);
            }
        }
    }
}
