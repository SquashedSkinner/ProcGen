using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    [SerializeField]
    private int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        Debug.Log("Hit");
        currentHealth -= damage;

        // Play damaged

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }

    void Die()
    {
        // Dead innit
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
