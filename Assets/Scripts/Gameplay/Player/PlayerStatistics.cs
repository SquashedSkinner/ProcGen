using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    public GameObject HealthManaBar;

     // Statistics
    public int currentHealth;
    public int maxHealth;
    public Animator anim;

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
        anim.SetTrigger("Hurt");
        // Play damaged
        HealthManaBar.GetComponent<HealthManaBar>().EvaluateHealthState(currentHealth);
            if (currentHealth <= 0)
            {
                anim.SetTrigger("Die");
            }
        
    }

    public void IncreaseHealth(int cure)
    {
        currentHealth = currentHealth + cure;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

    }

    public int GetMana()
    {
        return currentMana;
    }

    public void IncreaseMP(int mana)
    {
        currentMana = currentMana + mana;
        HealthManaBar.GetComponent<HealthManaBar>().EvaluateManaState(currentMana);
    }

    public void DecreaseMP(int mana)
    {
        currentMana = currentMana - mana;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        HealthManaBar.GetComponent<HealthManaBar>().EvaluateManaState(currentMana);
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
        currentHealth = maxHealth;
        currentMana = maxMana;
        anim = this.gameObject.GetComponent<Animator>();
        HealthManaBar = GameObject.Find("HealthFrame");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
