using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject openenerKey;

    [Header("SoundFX")]    
    [SerializeField] private AudioClip openingGateAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Player.Instance.gameObject == collision.gameObject)
        {
            GameManager gameManager = GameManager.Instance;
            if (gameManager.HasInventoryItem(openenerKey.name)){
                Collectable key = gameManager.RetrieveItem(openenerKey.name);
                gameManager.RemoveItem(key);
                PlayOpeningGate();
                Destroy(gameObject);
            }
        }
    }

    private void PlayOpeningGate()
    {
        GameManager.Instance.PlaySFX(openingGateAudio, 0.3f);
    }
}
