using UnityEngine;
using TMPro;

public class WeatherGenerator : MonoBehaviour
{
    public GameObject Weather;
    public Animator WeatherStates;
    public string[] WeatherType;
    public TMP_Text Text;
    public int counter;
    public float Time;

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
        Time = (float)counter * 300.0f;
        Debug.Log(Time);
        Text.text = WeatherType[counter];

        LockButton();
        }

    }

    public float SetTime()
    {
        return Time;
    }

    public void LockButton()
    {
        locked = true;
    }

    public void UnlockButton()
    {
        locked = false;
    }
}
