using UnityEngine;
using System.Collections;

public class Archangel : MonoBehaviour
{
    public Transform player;
    //public Vector3 lastPosition;
    public bool facingRight;
    public Rigidbody2D rb;

    public Animator anim;
    public float speed;
    public int phase = 1;
    public bool stopMove;
    [SerializeField] private Vector2 movement;

    // Combat
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public bool attackEnabled;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FollowTimer());
        attackEnabled = true;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        //lastPosition = this.gameObject.transform.position;
        anim = this.gameObject.GetComponent<Animator>();
        anim.SetFloat("Health", (float)this.gameObject.GetComponent<EnemyStats>().GetHealth());
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        int health = this.gameObject.GetComponent<EnemyStats>().GetHealth();
        if(health < 150)
        {
            Phase2();
        }
        if (player.position.x - this.gameObject.transform.position.x < 0.0f)
        {
            if (facingRight == false)
            {
                flip();
            }
        }
        else if (player.position.x - this.gameObject.transform.position.x > 0.0f)
        {
           if (facingRight == true)
            {
                flip();
            }  
        }

        if (phase == 1)
        {
            speed = 1.0f;
            if (stopMove == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }

            // Move slowly towards player, swing within distance.
        }
        else if (phase == 2)        
        {   speed = 3.0f;
            if (stopMove == false)
            {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            int tpCheck = Random.Range(1, 250);
            if (tpCheck == 2)
            {
                anim.SetTrigger("Teleport");
            }

            }
            // Increase speed, ocassional teleport
        }
        
    }

    IEnumerator MovementPause()
    {
        stopMove = true;

        yield return new WaitForSeconds(2.5f);

        stopMove = false;

        StartCoroutine(FollowTimer());
    }

    IEnumerator FollowTimer()
    {
        yield return new WaitForSeconds(4.5f);
        StartCoroutine(MovementPause());
    }

    IEnumerator AttackPause()
    {
        attackEnabled = false;

        yield return new WaitForSeconds(1.5f);

        attackEnabled = true;
    }

    public void flip()
    {
        Debug.Log("Flipping");
        Vector3 currentScale = this.gameObject.transform.localScale;
        // Set x to *= -1 to flip
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    public void Phase2()
    {
        phase = 2;
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
                anim.SetTrigger("Teleport");
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
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
        // Detect Player within range
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D player in hitPlayer)
        {
            int damage = this.gameObject.GetComponent<EnemyStats>().GetDamage();
            player.GetComponent<PlayerStatistics>().ReduceHealth(damage);
        }
    }

    public void Teleport()
    {
        float randX = Random.Range(-2.5f, 2.5f);
        float randY = Random.Range(-2.5f, 2.5f);

        this.gameObject.transform.position = new Vector3(transform.position.x + randX, transform.position.y + randY, 0);
        
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public void Move()
    {
        stopMove = false;
    }
}
