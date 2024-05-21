using UnityEngine;

public class Explode : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollision2D(Collider2D col)
    {
        this.gameObject.GetComponent<Rigidbody2D>().mass = 40.0f;
    }
}
