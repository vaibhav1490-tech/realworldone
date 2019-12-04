using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamedata : MonoBehaviour
{
    public static float gametotalTime=180;
   public static List<veggie> vegetable;

    public static float customermaxwaittime=90;


    public static int player1score=0, player2score = 0;

    public static Text pl1scoretext,pl2scoretext;

    public Transform Gameovercanvas;

    public Transform pl1, pl2;

    [System.Serializable]
    public class veggie
    {
        public string vegieName;
        public Sprite vegiImage;
    }

    public List<veggie> vegs;
    private void Start()
    {
        
        pl1scoretext = GameObject.Find("pl1score").gameObject.GetComponent<Text>();
        pl2scoretext = GameObject.Find("pl2score").gameObject.GetComponent<Text>();
        pl1scoretext.text = "00";
        pl2scoretext.text = "00";
        vegetable = new List<veggie>();

        vegetable = vegs;
        /*   vegetable.Add("tomato");
         vegetable.Add("capsicum");
         vegetable.Add("brinjal");
         vegetable.Add("carrot");
         vegetable.Add("broccoli");
         vegetable.Add("cucumber");*/


    }

    public static void addscore(GameObject player, GameObject customer)
    {
        int score = 0;
        customer cr = customer.GetComponent<customer>();
        float timereward = (cr.ordertime / customermaxwaittime) * 100;
        Debug.Log("===========" + timereward);

        if (timereward > 75)
        {
            score = 50;
        }
        else if (timereward < 75 && timereward > 20)
        {
            score = 30;
        }
        else if(timereward<20)
        {
            score = -10;
        }


        if (player.GetComponent<Player>().player2 == true)
        {
            addplayer2score(score);
        }
        else
        {
            addplayer1score(score);
        }
        cr.GenrateRandomOrder();
    }

   public static void addplayer1score(int score)
    {
        player1score += score;
        pl1scoretext.text = player1score.ToString();
    }

   public static void addplayer2score(int score)
    {
        player2score += score;
        pl2scoretext.text = player2score.ToString();
    }


    private void Update()
    {
        if (GetComponent<timer>().seconds <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Gameovercanvas.gameObject.SetActive(true);
        Text des = Gameovercanvas.Find("des").gameObject.GetComponent<Text>();
        pl1.gameObject.GetComponent<Player>().enabled = false;
        pl2.gameObject.GetComponent<Player>().enabled = false;
        if (player1score == player2score)
        {
            des.text = "Match Tied";
        }
        else if (player1score > player2score)
        {
            des.text = "Player - 1 Win";
        }
        else
        {
            des.text = "Player - 2 Win";
        }
    }


    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
