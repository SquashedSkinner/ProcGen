using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField]
    private GameObject Item;
    private bool inRange;
    private GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Player");
        inRange = false;
        Item = this.gameObject;
    }

    void Update()
    {
        if(inRange)
        {
            if(Input.GetKeyDown("e"))
            {
                PickupItem(Player, Item);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            inRange = false;
        }
    }

    public void PickupItem(GameObject Player, GameObject Item)
    {
        Debug.Log("Collected " + Item.name);
        Destroy(this.gameObject);
        // Player.GetComponent<InventoryOrWhatever>().AddItem()
    }
}
