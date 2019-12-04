using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{

    public float seconds;

    public Text Player1_Timer, Player2_Timer;
    // Start is called before the first frame update
    void Start()
    {
        seconds = Gamedata.gametotalTime;
    }


    private void Update()
    {
        if (seconds > 0)
        {
            seconds -= Time.deltaTime;
            string timestring = GetTimeFromSec((int)seconds);
            Player1_Timer.text = timestring;
            Player2_Timer.text = timestring;
        }
    }

    static string GetTimeFromSec(int secs)
    {
        int s = secs % 60;
        secs /= 60;
        int mins = secs % 60;
        return string.Format("{0:D2}:{1:D2}", mins, s);
    }
}
