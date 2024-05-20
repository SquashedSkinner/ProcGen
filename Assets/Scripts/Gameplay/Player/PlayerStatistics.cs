using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
     // Statistics
    public int currentHealth;
    public int maxHealth;

    public int regenRate;

    public int currentMana;
    public int maxMana;

    public int damage = 25;

    [SerializeField] private float movementSpeed = 3.0f;
    [SerializeField] private float defaultSpeed = 3.0f;

    public int GetDamage()
    {
        return damage;
    }

    public void ReduceHealth(int damage)
    {
        currentHealth = currentHealth - damage;
    }

    public void IncreaseHealth(int cure)
    {
        currentHealth = currentHealth + cure;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void IncreaseMP(int mana)
    {
        currentMana = currentMana - mana;
    }

    public void DecreaseMP(int mana)
    {
        currentMana = currentMana + mana;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }
    
    public float GetSpeed()
    {
        return movementSpeed;
    }

    public void ReduceSpeed(float debuff)
    {
        movementSpeed = movementSpeed * debuff;
    }

    public void IncreaseSpeed(float buff)
    {
        movementSpeed = movementSpeed * buff;
    }

    public void DefaultSpeedValues()
    {
        movementSpeed = defaultSpeed;
    }

    public void UpdateUI()
    {
        // more setter functions
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
