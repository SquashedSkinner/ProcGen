 using UnityEngine;

public class PlayerController : MonoBehaviour
{
     //Player
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject BattleHUD;
    public Animator anim;

     // Movement
    public Rigidbody2D rb;
    bool facingRight = true;
    bool stopMove = false;
    [SerializeField] private Vector2 movement;
 

    // Combat
    public Transform attackPoint;
    public Transform attackPointThrust;
    public float attackRange;
    public LayerMask enemyLayers;

    // magic
    public Transform summonOffset;
    public Transform magicOffset;
    public GameObject[] Spells;
    public int selectedSpell;
    public GameObject spellToCast;
   

    [SerializeField] private float movementSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementSpeed = Player.GetComponent<PlayerStatistics>().GetSpeed();
        Player = this.gameObject;
        rb = Player.GetComponent<Rigidbody2D>();

        anim = Player.GetComponent<Animator>();
    }

    // Takes into account the selected spell and assigns it to be instantiated
    // Begin Magic Animation
    public void MagicSpell()
    {
        spellToCast = Spells[selectedSpell];
        anim.SetTrigger("Magic");
        
    }


    // Mana is taken away
    // Checks tag for correct spawn position
    // Item is instantiated
    void CastSpell()
    {
        int manaCost;

        if (spellToCast.tag == "Totem")
        {
            Instantiate(spellToCast, summonOffset.position, Quaternion.identity);
            manaCost = 3;
            Player.GetComponent<PlayerStatistics>().DecreaseMP(manaCost);
        }
        else if (spellToCast.tag == "Magic")
        {
            Instantiate(spellToCast, magicOffset.position, Quaternion.identity);
            manaCost = 1;
            Player.GetComponent<PlayerStatistics>().DecreaseMP(manaCost);
        }

    }




    // Set Animation Trigger to Attack
    void Attack()
    {
        // Play animation
        anim.SetTrigger("Attack");
        if (Input.GetMouseButton(0))
        {
            anim.SetBool("AttackHolding", true);
        }  
    }

    public void AttackCollider()
    {
        // Detect Enemies within range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            int damage = Player.GetComponent<PlayerStatistics>().GetDamage();
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    // Identical code: different collider position
    public void AttackCollider2()
    {
        // Detect Enemies within range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointThrust.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            int damage = (Player.GetComponent<PlayerStatistics>().GetDamage()) * 2;
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    

    




    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Push")
        {
            rb.mass = 1.0f;
            anim.SetBool("isPushing", true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Push")
        {
            anim.SetBool("isPushing", false);
            rb.mass = 0.0001f;
        }
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
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (Player.GetComponent<PlayerStatistics>().GetMana() > 0)
                {
                    MagicSpell();
                }
            }
            else if (Input.GetKeyDown("q"))
            {
                anim.SetTrigger("Shoot");
            }
            else if (Input.GetKeyDown("r"))
            {
                anim.SetTrigger("Dive");
                Player.GetComponent<BoxCollider2D>().enabled = false;
                
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
 
 
 
    public void SpawnArrow()
    {
        
    }

    public void ResetAttackTrigger()
    {
        anim.ResetTrigger("Attack");
    }

    public void ReenableCollider()
    {
        Player.GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(attackPointThrust.position, attackRange);
    }

}
