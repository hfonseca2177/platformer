using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] private GameObject[] slots = new GameObject[3];
    
    [SerializeField] private Sprite emptySlotSprite;


    Dictionary<string, Collectable> inventory;

    private void Start()
    {
        inventory = new Dictionary<string, Collectable>();
    }

    public bool AddItem(Collectable item)
    {
        Image slot = GetNextSlotAvailable();
        if (slot == null)
        {
            Debug.Log("No slot available");
            return false;
        }
        else 
        {
            inventory.Add(item.name, item);
            SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
            slot.sprite = spriteRenderer.sprite;
            slot.color = spriteRenderer.color;
            return true;
        }
    }

    private Image GetNextSlotAvailable()
    {        
        foreach(GameObject slot in slots)
        {
            Image slotImage = slot.GetComponent<Image>();
            if (slotImage != null && slotImage.sprite.Equals(this.emptySlotSprite))
            {
                return slotImage;
            }        
        }
        return null;   
    }


}