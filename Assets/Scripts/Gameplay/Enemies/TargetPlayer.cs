using UnityEngine;

public class TargetPlayer : MonoBehaviour
{
    public GameObject Enemy;

    void Awake()
    {
        Enemy = this.gameObject.transform.parent.gameObject;
    }

   void OnTriggerEnter2D(Collider2D col)
   {
        Enemy.GetComponent<EnemyController>().StartCoroutine("SetTarget", col.gameObject.transform.position);
   }

   void OnTriggerExit2D(Collider2D col)
   {
        Enemy.GetComponent<EnemyController>().StartCoroutine("SetTargetDefault");
   }
}
