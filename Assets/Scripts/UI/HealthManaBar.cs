using UnityEngine;
using UnityEngine.UI;

public class HealthManaBar : MonoBehaviour
{
    public GameObject PlayerStatistics;
    public GameObject[] Mana;
    public GameObject[] Hearts;
    public GameObject MagicWheel;

    public 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerStatistics = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            PlayerStatistics.GetComponent<PlayerController>().FreezeAbilities();
            MagicWheel.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            MagicWheel.SetActive(false);
            PlayerStatistics.GetComponent<PlayerController>().UnfreezeAbilities();
        }
    }

    public void NextMagicSelection()
    {

    }


    public void EvaluateHealthState(int currentHealth)
    {
        if (currentHealth >= 120)
        {
            Hearts[0].GetComponent<SpriteRenderer>().enabled = true; Hearts[1].GetComponent<SpriteRenderer>().enabled = true; Hearts[2].GetComponent<SpriteRenderer>().enabled = true; Hearts[3].GetComponent<SpriteRenderer>().enabled = true; Hearts[4].GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (currentHealth >= 80 && currentHealth < 100)
        {
            Hearts[0].GetComponent<SpriteRenderer>().enabled = true; Hearts[1].GetComponent<SpriteRenderer>().enabled = true; Hearts[2].GetComponent<SpriteRenderer>().enabled = true; Hearts[3].GetComponent<SpriteRenderer>().enabled = true; Hearts[4].GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (currentHealth >= 60 && currentHealth < 80)
        {
            Hearts[0].GetComponent<SpriteRenderer>().enabled = true; Hearts[1].GetComponent<SpriteRenderer>().enabled = true; Hearts[2].GetComponent<SpriteRenderer>().enabled = true; Hearts[3].GetComponent<SpriteRenderer>().enabled = false; Hearts[4].GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (currentHealth >= 40 && currentHealth < 60)
        {
            Hearts[0].GetComponent<SpriteRenderer>().enabled = true; Hearts[1].GetComponent<SpriteRenderer>().enabled = true; Hearts[2].GetComponent<SpriteRenderer>().enabled = false; Hearts[3].GetComponent<SpriteRenderer>().enabled = false; Hearts[4].GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (currentHealth >= 20 && currentHealth < 40)
        {
            Hearts[0].GetComponent<SpriteRenderer>().enabled = true; Hearts[1].GetComponent<SpriteRenderer>().enabled = false; Hearts[2].GetComponent<SpriteRenderer>().enabled = false; Hearts[3].GetComponent<SpriteRenderer>().enabled = false; Hearts[4].GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (currentHealth >= 20 && currentHealth < 40)
        {
            Hearts[0].GetComponent<SpriteRenderer>().enabled = false; Hearts[1].GetComponent<SpriteRenderer>().enabled = false; Hearts[2].GetComponent<SpriteRenderer>().enabled = false; Hearts[3].GetComponent<SpriteRenderer>().enabled = false; Hearts[4].GetComponent<SpriteRenderer>().enabled = false;
        }
    }


    public void EvaluateManaState(int currentMana)
    {
        if (currentMana == 5)
        {
            Mana[0].GetComponent<SpriteRenderer>().enabled = true; Mana[1].GetComponent<SpriteRenderer>().enabled = true; Mana[2].GetComponent<SpriteRenderer>().enabled = true; Mana[3].GetComponent<SpriteRenderer>().enabled = true; Mana[4].GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (currentMana == 4)
        {
            Mana[0].GetComponent<SpriteRenderer>().enabled = true; Mana[1].GetComponent<SpriteRenderer>().enabled = true; Mana[2].GetComponent<SpriteRenderer>().enabled = true; Mana[3].GetComponent<SpriteRenderer>().enabled = true; Mana[4].GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (currentMana == 3)
        {
            Mana[0].GetComponent<SpriteRenderer>().enabled = true; Mana[1].GetComponent<SpriteRenderer>().enabled = true; Mana[2].GetComponent<SpriteRenderer>().enabled = true; Mana[3].GetComponent<SpriteRenderer>().enabled = false; Mana[4].GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (currentMana == 2)
        {
            Mana[0].GetComponent<SpriteRenderer>().enabled = true; Mana[1].GetComponent<SpriteRenderer>().enabled = true; Mana[2].GetComponent<SpriteRenderer>().enabled = false; Mana[3].GetComponent<SpriteRenderer>().enabled = false; Mana[4].GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (currentMana == 1)
        {
            Mana[0].GetComponent<SpriteRenderer>().enabled = true; Mana[1].GetComponent<SpriteRenderer>().enabled = false; Mana[2].GetComponent<SpriteRenderer>().enabled = false; Mana[3].GetComponent<SpriteRenderer>().enabled = false; Mana[4].GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (currentMana == 0)
        {
            Mana[0].GetComponent<SpriteRenderer>().enabled = false; Mana[1].GetComponent<SpriteRenderer>().enabled = false; Mana[2].GetComponent<SpriteRenderer>().enabled = false; Mana[3].GetComponent<SpriteRenderer>().enabled = false; Mana[4].GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    
}
