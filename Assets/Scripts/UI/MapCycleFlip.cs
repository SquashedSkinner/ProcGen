using UnityEngine;

public class MapCycleFlip : MonoBehaviour
{

    public GameObject Map;
    public Animator mapAnim;
    public GameObject DayNight;
    public Animator DayNightAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    //public void Toggle()
    //{
    //    if(Map.active)
    //    {
    //        mapAnim.Toggle("Flip");
    //        DayNight.SetActive(true);
    //        DayNightAnim.Toggle("FlipBack");
           
    //    }
    //    else if (DayNight.active)
    //    {
    //        DayNightAnim.Toggle("Flip"); 
    //        Map.SetActive(true);
    //        MapAnim.Toggle("FlipBack");
    //    }
    //    //DayNight.SetActive(false);
    //    //Map.SetActive(true);
    //}


}
