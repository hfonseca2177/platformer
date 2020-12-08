using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : PhysicsObject
{
    const string HorizontalKey = "Horizontal";
    const string JumpKey = "Jump";
    const int maxHealth = 100;
    private int currentHealth = 50;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    public int coins;
    [SerializeField] private Text coinsText;
    [SerializeField] private Image healthBar;
    private Vector2 healthBarOrigSize;
    public InventoryManager inventory;

    private static Player _instance;
    public static Player Instance 
    {
        get {
            if (_instance == null) _instance = FindObjectOfType<Player>();
            return _instance;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        healthBarOrigSize = healthBar.rectTransform.sizeDelta;
        UpdateHealthUI();
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

    public void AddCoin()
    {
        coins++;
        UpdateCoinsUI();
    }

    private void UpdateCoinsUI()
    {
        this.coinsText.text = coins.ToString();
    }

    public void AddHeart()
    {
        if (currentHealth < maxHealth)
        {
            this.currentHealth += 15;
            UpdateHealthUI();
        }        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        float healthPercentage = ((float)currentHealth / (float)maxHealth);
        this.healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * healthPercentage, healthBarOrigSize.y);
    }
}
