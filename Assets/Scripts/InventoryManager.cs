using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Sprite emptySlotSprite;
    [SerializeField] protected GameObject[] slots = new GameObject[3];

    protected int coins;
    protected Dictionary<string, Collectable> inventory = new Dictionary<string, Collectable>();

    public bool AddItem(Collectable item)
    {
        Image slot = GetNextSlotAvailable();
        if (slot == null)
        {
            Debug.Log("No slot available (TODO: show as Message in Game)");
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
        foreach (GameObject slot in slots)
        {
            Image slotImage = slot.GetComponent<Image>();
            if (slotImage != null && slotImage.sprite.Equals(this.emptySlotSprite))
            {
                return slotImage;
            }
        }
        return null;
    }

    public bool HasInventoryItem(string keyName)
    {
        return inventory.ContainsKey(keyName);
    }

    public Collectable RetrieveItem(string keyName)
    {
        return inventory[keyName];
    }

    public void RemoveItem(Collectable item)
    {
        inventory.Remove(item.name);
        CleanSlot(item);
        GameObject.Destroy(item);
    }

    private void CleanSlot(Collectable item)
    {
        SpriteRenderer itemSpriteRenderer = item.GetComponent<SpriteRenderer>();
        foreach (GameObject slot in slots)
        {
            Image slotImage = slot.GetComponent<Image>();
            if (slotImage.sprite.Equals(itemSpriteRenderer.sprite) && slotImage.color.Equals(itemSpriteRenderer.color))
            {
                slotImage.sprite = emptySlotSprite;
                slotImage.color = Color.white;
                break;
            }
        }
    }

    protected void AddCoin()
    {
        coins++;
    }


}