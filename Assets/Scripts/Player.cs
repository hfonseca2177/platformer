using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : PhysicsObject
{
    const string HorizontalKey = "Horizontal";
    const string JumpKey = "Jump";

    [Header("Attributes")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float attackDuration = 0.5f;
    const int maxHealth = 100;
    private int currentHealth = 100;
    public int attackPower = 25;


    [Header("References")]
    [SerializeField] private GameObject attackBox;

    //Singleton Instantiation
    private static Player instance;
    private const string instanceName = "Player Ref";
    public static Player Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<Player>();
            return instance;
        }
    }

    private void Awake()
    {
        //Control 1 instance between every scene
        if (GameObject.Find(instanceName)) Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Control 1 instance between every scene
        DontDestroyOnLoad(gameObject);
        gameObject.name = instanceName;

        UpdateHealthUI();
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis(HorizontalKey) * speed, 0);
        if (Input.GetButton(JumpKey) && grounded)
        {
            velocity.y = jumpForce;
        }
        //Flip player so attackbox flip as well in the same direction
        if (targetVelocity.x < -.01)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (targetVelocity.x > .01)
        {
            transform.localScale = new Vector2(1, 1);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ActivateAttack());
        }
    }

    public IEnumerator ActivateAttack()
    {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attackBox.SetActive(false);
    }

    public void AddCoin()
    {
        GameManager.Instance.AddCoinAndUpdateUI();
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
        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene("Level1");
    }

    private void UpdateHealthUI()
    {
        GameManager.Instance.UpdateHealthUI(currentHealth, maxHealth);
    }

    public void Spawn()
    {
        transform.position = GameObject.Find("SpawnLocation").transform.position;
    }
}
