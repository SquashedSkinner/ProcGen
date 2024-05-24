using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    //Player
    [SerializeField] private GameObject Enemy;
    public Animator anim;

    // Movement
    public Transform Target;
    private bool pursuing;
    private bool patrolling;

    public Vector3 target;
    protected Vector3 velocity;
    protected Vector3 previousPos;

    private bool flipped;
    public Vector3[] waypoints;


    public Rigidbody2D rb;
    bool facingRight = true;
    public bool stopMove = false;

    public float moveTimer;
    public float pauseTimer;
    public float attackTimer;

    public float teleportRange;


    // Combat
    public Transform attackPoint_1;
    public Transform attackPoint_2;

    public float attackRange_1;
    public float attackRange_2;

    public LayerMask playerLayer;
    
    public bool attackEnabled;
    public int moveCount;



    [SerializeField] private float movementSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        patrolling = true;
        pursuing = false;

        target = waypoints[1];
        Target = GameObject.Find("Player").GetComponent<Transform>();
        Enemy = this.gameObject;

        movementSpeed = Enemy.GetComponent<EnemyStatistics>().GetSpeed();
        
        rb = Enemy.GetComponent<Rigidbody2D>();

        anim = Enemy.GetComponent<Animator>();

        int health = this.gameObject.GetComponent<EnemyStatistics>().GetHealth();
    }

    void FixedUpdate()
    {
        FaceTowards(Target.position);
    }

    // Update is called once per frame
    void Update()
    {
        int health = this.gameObject.GetComponent<EnemyStatistics>().GetHealth();
        
        if (pursuing)
        {
            Pursue();
        }
        else if (patrolling)
        {
            Patrol();
        }

        //Movement();

        // if (Enemy.transform.hasChanged == true)
        // {
        //     anim.SetBool("isMoving", true);
        // }

        // if (!stopMove)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, Target.transform.position - new Vector3(0f,0.3f,0f), movementSpeed * Time.deltaTime);
        // }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (attackEnabled == true)
        {
            if (col.tag == "Player")
            {

                stopMove = true;
                Attack();

                StopCoroutine(MovementPause());
                StopCoroutine(FollowTimer());

                StartCoroutine(AttackPause());
                StartCoroutine(MovementPause());

            }
            else if (col.tag == "Magic" || col.tag == "Ordnance")
            {
                // anim.SetTrigger("Teleport");
            }
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {

        if (col.tag == "Player")
        {
            stopMove = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint_1.position, attackRange_1);
        Gizmos.DrawWireSphere(attackPoint_2.position, attackRange_2);
       
    }
    
    public void Pursue()
    {
        patrolling = false;
        pursuing = true;

        velocity = ((transform.position - previousPos) / Time.deltaTime);
        previousPos = transform.position;

        if (transform.position != target)
        {
            Attack();
        }

    }

    public virtual IEnumerator SetTarget(Vector3 position)
    {
        yield return new WaitForSeconds(pauseTimer);
        target = position;
        FaceTowards(position - transform.position);
    }

    public virtual IEnumerator SetTargetDefault()
    {
        yield return new WaitForSeconds(pauseTimer);
        target = waypoints[0];
        FaceTowards(waypoints[0] - transform.position);
    }

    public virtual void Patrol()
    {
        pursuing = false;
        patrolling = true;

        velocity = ((transform.position - previousPos) / Time.deltaTime);
        previousPos = transform.position;

        if (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
        }
        else
        {
            if (target == waypoints[0])
            {
                if (flipped)
                {
                    flipped = !flipped;
                    StartCoroutine("SetTarget", waypoints[1]);
                }
            }
            else
            {
                if (!flipped)
                {
                    flipped = !flipped;
                    StartCoroutine("SetTarget", waypoints[0]);
                }
            }
        }
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

    public void Teleport()
    {
        float randX = Random.Range(-teleportRange, teleportRange);
        float randY = Random.Range(-teleportRange, teleportRange);

        this.gameObject.transform.position = new Vector3(transform.position.x + randX, transform.position.y + randY, 0);

    }

    
    public void FaceTowards(Vector3 dir)
    {
        Vector3 currentScale = this.gameObject.transform.localScale;

        if (dir.x < 0f)
        {
            if(facingRight)
            {
            Enemy.GetComponent<SpriteRenderer>().flipX = true;    
            facingRight = true;
            gameObject.transform.localScale = currentScale;
            }
        }
        else
        {
             if(!facingRight)
            {
            Enemy.GetComponent<SpriteRenderer>().flipX = false;    
            facingRight = false;
            gameObject.transform.localScale = currentScale;
            }        
        }
    }

    public void ToggleMove()
    {
        Debug.Log("inverting bool");
        stopMove = ! stopMove;
    }

    IEnumerator MovementPause()
    {
        stopMove = true;
        anim.SetBool("ismoving", false);

        yield return new WaitForSeconds(pauseTimer);

        stopMove = false;

        StartCoroutine(FollowTimer());
    }

    IEnumerator FollowTimer()
    {
        yield return new WaitForSeconds(moveTimer);
        StartCoroutine(MovementPause());
    }

    IEnumerator AttackPause()
    {
        attackEnabled = false;

        yield return new WaitForSeconds(attackTimer);

        attackEnabled = true;
    }
}
