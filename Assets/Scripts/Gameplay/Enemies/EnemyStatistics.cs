using UnityEngine;
using UnityEngine.Events;

public class EnemyStatistics : MonoBehaviour
{
    public Animator anim;

    public int maxHealth;
    [SerializeField]
    private int currentHealth;

    public int damage;
    public int movementSpeed;

    public UnityEvent OnHit;

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    
    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void TakeDamage(int damage)
    {
        OnHit?.Invoke();
        currentHealth = currentHealth - damage;
        this.gameObject.GetComponent<FloatingHealthbar>().UpdateHealthBar(currentHealth, maxHealth);
        anim.SetTrigger("Hurt");

        // Play damaged

        if (currentHealth <= 0)
        {
            anim.SetTrigger("Die");
            this.gameObject.GetComponent<EnemyController>().enabled = false;
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public void Hit()
    {
        anim.SetTrigger("Hurt");
    }

    public float GetSpeed()
    {
        return movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
