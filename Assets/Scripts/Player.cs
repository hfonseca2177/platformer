using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : PhysicsObject
{
    const string HorizontalKey = "Horizontal";
    const string JumpKey = "Jump";

    [Header("Attributes")]
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float attackDuration = 0.5f;
    const int maxHealth = 100;
    private int currentHealth = 100;
    public int attackPower = 25;


    [Header("References")]
    [SerializeField] private GameObject attackBox;
    [SerializeField] private Animator animator;

    [Header("SoundFX")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;



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
        targetVelocity = new Vector2(Input.GetAxis(HorizontalKey) * maxSpeed, 0);
        if (Input.GetButton(JumpKey) && grounded)
        {
            velocity.y = jumpForce;
            PlayJumpSound();
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
            animator.SetTrigger("attack");
            StartCoroutine(ActivateAttack());
        }

        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        animator.SetFloat("velocityY", velocity.y);
        animator.SetBool("grounded", grounded);

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
        animator.SetTrigger("hurt");
        PlayHurtSound();
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
        PlayDeathSound();
        StartCoroutine(WaitCoroutine());
        SceneManager.LoadSceneAsync("Level1");
    }

    private void UpdateHealthUI()
    {
        GameManager.Instance.UpdateHealthUI(currentHealth, maxHealth);
    }

    public void Spawn()
    {
        transform.position = GameObject.Find("SpawnLocation").transform.position;
    }

    private void PlayJumpSound()
    {
        GameManager.Instance.PlaySFX(jumpSound, 0.5f);
    }

    public void PlayHurtSound()
    {
        GameManager.Instance.PlaySFX(hurtSound, 0.3f);
    }

    public void PlayDeathSound()
    {
        GameManager.Instance.PlaySFX(deathSound, 0.3f);
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(10);
    }   

}
