using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public Animator anim;

    public int maxHealth;
    [SerializeField]
    private int currentHealth;

    public int damage;
    public int movementSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        currentHealth = currentHealth - damage;
        anim.SetTrigger("Hurt");

        // Play damaged

        if (currentHealth <= 0)
        {
            anim.SetTrigger("Die");
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
