using UnityEngine;

public class PlayerController : MonoBehaviour
{
     //Player
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject BattleHUD;
    private Animator anim;

     // Movement
    public Rigidbody2D rb;
    bool facingRight = true;
    bool stopMove = false;
    [SerializeField] private Vector2 movement;
 

    // Statistics
    public int currentHealth;
    public int maxHealth;

    public int regenRate;

    public int currentMana;
    public int maxMana;

    [SerializeField] private float movementSpeed = 3.0f;
    [SerializeField] private float defaultSpeed = 3.0f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = this.gameObject;
        rb = Player.GetComponent<Rigidbody2D>();

        anim = this.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Speed", movement.sqrMagnitude);

            if ((movement.x != 0) || (movement.y != 0))
            {
                anim.SetBool("isMoving", true);

                // Flip Object if travelling in other direction
                if (Input.GetAxisRaw("Horizontal") > 0 && !facingRight)
                {
                    flipPlayer();
                }

                if (Input.GetAxisRaw("Horizontal") < 0 && facingRight)
                {
                    flipPlayer();
                }
            }
            else if ((movement.x == 0) || (movement.y == 0))
            {
                anim.SetBool("isMoving", false);
            }

            // Combat controls
            if (Input.GetKeyDown("e"))
            {
                anim.SetTrigger("Attack");
                if (Input.GetKey("e"))
                {
                    anim.SetBool("AttackHolding", true);
                }
            }
            else if (Input.GetKeyDown("q"))
            {
                anim.SetTrigger("Shoot");
            }
            else if (Input.GetKeyDown("r"))
            {
                anim.SetTrigger("Dive");
                
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("Jump");
            }
        if(Input.GetKeyUp("e"))
        {
            anim.SetBool("AttackHolding", false);
        }
        
    }

    // Flip Player
    public void flipPlayer()
    {
        Vector3 currentScale = Player.transform.localScale;
        // Set x to *= -1 to flip
        currentScale.x *= -1;
        Player.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    public void ResetAttackTrigger()
    {
        anim.ResetTrigger("Attack");
    }

    public void SpawnArrow()
    {

    }

    public void CalculateDamage()
    {

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
    }

    public void ReduceSpeed(float debuff)
    {
        movementSpeed = movementSpeed * debuff;
    }

    public void IncreaseSpeed(float buff)
    {
        movementSpeed = movementSpeed * buff
    }

    public void DefaultSpeedValues()
    {
        movementSpeed = defaultSpeed;
    }

    public void UpdateUI()
    {
        // more setter functions
    }

    public int SetCurrentHealth()
    {

    }

    public int SetCurrentMana()
    {

    }

}
