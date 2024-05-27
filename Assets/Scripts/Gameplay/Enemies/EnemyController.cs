using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    // Enemy
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Pathfinder;
    public Animator anim;

    // Movement
    public Transform Player;
    public bool hasLineOfSight = false;
    public bool inRange = false;

    // public Rigidbody2D rb;
    bool facingRight = true;
    public bool stopMove = false;


    // Combat
    public Transform attackPoint_1;
    public Transform attackPoint_2;

    public float attackRange_1;
    public float attackRange_2;

    public LayerMask playerLayer;
    public int moveCount;



    [SerializeField] private float movementSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Transform>();
        Enemy = this.gameObject;

        movementSpeed = Enemy.GetComponent<EnemyStatistics>().GetSpeed();

        //rb = Enemy.GetComponent<Rigidbody2D>();

        anim = Enemy.GetComponent<Animator>();

        int health = this.gameObject.GetComponent<EnemyStatistics>().GetHealth();
    }

    void FixedUpdate()
    {
        if (inRange)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, Player.transform.position - transform.position);
            if (ray.collider != null)

            {
                hasLineOfSight = ray.collider.CompareTag("Player");
                if (hasLineOfSight)
                {
                    Debug.DrawRay(transform.position, Player.transform.position - transform.position, Color.green);
                }
                else
                {
                    Debug.DrawRay(transform.position, Player.transform.position - transform.position, Color.red);
                    Debug.Log(ray.collider.name);
                }
            }
        } 
        FaceTowards(Player.position);
    }

    // Update is called once per frame
    void Update()
    {

        if (inRange)
        {
            if (hasLineOfSight)
            {
                Enemy.GetComponent<Pathfinding.AIPath>().enabled = true;
            }
            else
            {
                Enemy.GetComponent<Pathfinding.AIPath>().enabled = false;
            }
        }
        else
        {
            Enemy.GetComponent<Pathfinding.AIPath>().enabled = false;
        }

        int health = this.gameObject.GetComponent<EnemyStatistics>().GetHealth();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            inRange = true;
        }


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            inRange = false;
            Attack();
        }


    }

    void OnTriggerExit2D(Collider2D col)
    {

        if (col.tag == "Player")
        {
            inRange = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint_1.position, attackRange_1);
        Gizmos.DrawWireSphere(attackPoint_2.position, attackRange_2);

    }


    public void Attack()
    {
        int moveChoice = Random.Range(1, moveCount);
        if (moveChoice == 1)
        {
            anim.SetTrigger("Attack");
            // Detect Player within range
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint_1.position, attackRange_1, playerLayer);

            foreach (Collider2D player in hitPlayer)
            {
                int damage = this.gameObject.GetComponent<EnemyStatistics>().GetDamage();
                Debug.Log(damage);
                player.GetComponent<PlayerStatistics>().ReduceHealth(damage);
            }
        }
        else if (moveChoice == 2)
        {
            anim.SetTrigger("Attack_2");
            // Detect Player within range
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint_1.position, attackRange_2, playerLayer);

            foreach (Collider2D player in hitPlayer)
            {
                int damage = this.gameObject.GetComponent<EnemyStatistics>().GetDamage();
                player.GetComponent<PlayerStatistics>().ReduceHealth(damage);
            }
        }
    }


    public void FaceTowards(Vector3 dir)
    {
        Vector3 currentScale = this.gameObject.transform.localScale;

        if (dir.x < 0f)
        {
            if (facingRight)
            {
                Enemy.GetComponent<SpriteRenderer>().flipX = true;
                facingRight = true;
                gameObject.transform.localScale = currentScale;
            }
        }
        else
        {
            if (!facingRight)
            {
                Enemy.GetComponent<SpriteRenderer>().flipX = false;
                facingRight = false;
                gameObject.transform.localScale = currentScale;
            }
        }
    }

    public void EnableMove()
    {
        Enemy.GetComponent<Pathfinding.AIPath>().enabled =true;
    }

    public void DisableMove()
    {
        Enemy.GetComponent<Pathfinding.AIPath>().enabled = false;
    }

}