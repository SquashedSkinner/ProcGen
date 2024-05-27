using UnityEngine;

public class attackScript : MonoBehaviour
{
    public GameObject Enemy;

    void Start()
    {
        Enemy = this.gameObject.transform.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Enemy.GetComponent<EnemyController>().Attack();
        }
    }
}
