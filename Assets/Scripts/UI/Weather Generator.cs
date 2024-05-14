using UnityEngine;
using TMPro;

public class WeatherGenerator : MonoBehaviour
{
    public GameObject Weather;
    public Animator WeatherStates;
    public string[] WeatherType;
    public TMP_Text Text;
    public int counter;

    public bool locked;

    void Start()
    {
        counter = 1;
        Text.text = WeatherType[counter];
    }

    public void NextConditions()
    {
        if (locked == false)
        {
        WeatherStates.SetTrigger("NextState");

        counter++;
        if(counter > 4)
        {
            counter = 1;
        }

        Text.text = WeatherType[counter];

        LockButton();
        }

    }

    public void LockButton()
    {
        Debug.Log("Locking");
        locked = true;
    }

    public void UnlockButton()
    {
        Debug.Log("Unlocking");
        locked = false;
    }
}
