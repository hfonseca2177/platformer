using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : InventoryManager
{
    [Header("References")]
    public Text coinsText;
    public Image healthBar;
    private Vector2 healthBarOrigSize;
    public CameraEffects cameraEffects;

    [Header("SoundFX")]
    public AudioSource sfxAudio;

    //Singleton instantiation
    private static GameManager instance;
    private const string instanceName = "Game Manager";
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    private void Awake()
    {
        if (GameObject.Find(instanceName)) Destroy(gameObject);
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.name = instanceName;
    }

    public void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        if (healthBarOrigSize == Vector2.zero)
        {
            healthBarOrigSize = healthBar.rectTransform.sizeDelta;
        }
        float healthPercentage = ((float)currentHealth / (float)maxHealth);
        healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * healthPercentage, healthBarOrigSize.y);
    }

    public void AddCoinAndUpdateUI()
    {
        AddCoin();
        coinsText.text = coins.ToString();
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        instance.sfxAudio.PlayOneShot(clip, volume);
    }

    public IEnumerator FreezeEffect(float length, float timeScale)
    {
        Time.timeScale = timeScale;
        yield return new WaitForSeconds(length);
        Time.timeScale = 1;

    }

    public void ShakeCamera()
    {
        cameraEffects.Shake(5, 5f);
    }
    
}
