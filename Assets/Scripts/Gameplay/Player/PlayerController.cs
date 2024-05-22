 using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
     //Player
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject BattleHUD;
    public Animator anim;

     // Movement
    public Rigidbody2D rb;
    bool facingRight = true;
    [SerializeField] private Vector2 movement;


    // Combat
    public bool inCombat;
    public Transform attackPoint;
    public Transform attackPointThrust;
    public float attackRange;
    public LayerMask enemyLayers;
    public bool abilityFrozen;
    float knockbackForce = 0.25f;

    // magic
    public Transform summonOffset;
    public Transform magicOffset;
    public GameObject[] Spells;
    public int selectedSpell;
    public int manaCost;
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



    public void NextMagicSelection(int selectedSpell)
    {
        spellToCast = Spells[selectedSpell]; 
        Debug.Log(Spells[selectedSpell].name + " has been selected");
    }

    // Takes into account the selected spell and assigns it to be instantiated
    // Begin Magic Animation
    public void MagicSpell()
    {     
        anim.SetTrigger("Magic");      
    }


    // Mana is taken away
    // Checks tag for correct spawn position
    // Item is instantiated
    void CastSpell()
    {

        if (spellToCast.tag == "Totem")
        { 
            manaCost = 2;
            if (Player.GetComponent<PlayerStatistics>().GetMana() >= manaCost)
            {
                Instantiate(spellToCast, summonOffset.position, Quaternion.identity);
                Player.GetComponent<PlayerStatistics>().DecreaseMP(manaCost);
            }
        }
        else if (spellToCast.tag == "Magic")
        {       
            manaCost = 1;
            if (Player.GetComponent<PlayerStatistics>().GetMana() >= manaCost)
            {
                Instantiate(spellToCast, magicOffset.position, Quaternion.identity);
                Player.GetComponent<PlayerStatistics>().DecreaseMP(manaCost);
            }
        }

    }




    // Set Animation Trigger to Attack
    void Attack()
    {
        
            // Play animation
            
            if (Input.GetMouseButtonUp(0))
            {
                anim.SetBool("AttackHolding", false);
            }
            else
            {
                anim.SetBool("AttackHolding", true);
            }
        anim.SetTrigger("Attack");
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
            enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockbackForce, 0.0f));
            
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


    IEnumerator BeginRegen()
    {
            yield return new WaitForSeconds(5.0f);
            Player.GetComponent<PlayerStatistics>().IncreaseHealth(10);
            Player.GetComponent<PlayerStatistics>().IncreaseMana(1);

            
    }




    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (inCombat == false)
        {
            StartCoroutine(BeginRegen());
        }
        else if (inCombat == true)
        {
            StopCoroutine(BeginRegen());
        }
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
            if (abilityFrozen == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                anim.SetBool("AttackHolding", false);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                
                MagicSpell();         
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
        
    }








    // Flip Player
    public void flipPlayer()
    {
        Vector3 currentScale = Player.transform.localScale;
        // Set x to *= -1 to flip
        currentScale.x *= -1;
        Player.transform.localScale = currentScale;
        knockbackForce = knockbackForce * -1;
        facingRight = !facingRight;
    }
 
    public void FreezeAbilities()
    {
        abilityFrozen = true;
    }

    public void UnfreezeAbilities()
    {
        abilityFrozen = false;
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
