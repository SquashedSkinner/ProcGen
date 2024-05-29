using UnityEngine;
using UnityEngine.Events;

public class Objects : MonoBehaviour
{
    public UnityEvent OnHit;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Sending");
        OnHit?.Invoke();
    }
}
