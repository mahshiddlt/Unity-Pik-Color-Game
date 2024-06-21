using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Game_Maneger : MonoBehaviour
{

    public List<Color> Color_List;

    public List<string> Color_Names;

    public List<Image> Doc_List;

    public int The_Index;

    public int Score;


    public TextMeshProUGUI Color_NAme;
    public TextMeshProUGUI Time_Text;
    public TextMeshProUGUI Score_Text;


    public float Time_For_Round = 5f;

    public bool Timer_Is_On = false;

    public GameObject Win_Panel;
    public GameObject Lost_Panel;


    public AudioSource Win_Sound;

    public AudioSource Lost_Sound;

    void Start()
    {
        Make_Doc_Colory();
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }



    public void Make_Doc_Colory()
    {


        int i = UnityEngine.Random.Range(0, Color_List.Count);
        int i1 = UnityEngine.Random.Range(0, Color_List.Count);
        int i2 = UnityEngine.Random.Range(0, Color_List.Count);
        int i3 = UnityEngine.Random.Range(0, Color_List.Count);


        if (i != i1
            && i != i2
            && i != i3
            && i1 != i2
            && i1 != i3
            && i2 != i3)
        {
            Doc_List[0].color = Color_List[i];
            Doc_List[1].color = Color_List[i1];
            Doc_List[2].color = Color_List[i2];
            Doc_List[3].color = Color_List[i3];
        }
        else
        {
            Make_Doc_Colory();
        }

        int[] indexs = { i, i1, i2, i3 };

        int chosenColorIndex = UnityEngine.Random.Range(0, indexs.Length);


        The_Index = indexs[chosenColorIndex];

        Color_NAme.text = Color_Names[The_Index];

        Invoke("StartGame", 1f);
    }



    public void StartGame()
    {
        Timer_Is_On = true;  
    }


    private void Timer()
    {
        if (Timer_Is_On)
        {
            Time_For_Round -= Time.deltaTime;

            if (Time_For_Round <= 0)
            {
                Controller_Score(-1);

           

                if (Score > 0)
                {
                    Time_For_Round = 5;
                    Make_Doc_Colory();
                }


            }

            Time_Text.text = "Timer : " + Time_For_Round.ToString("N3");
        }
    }




    private void Controller_Score(int New_Geted_Score)
    {
        Score += New_Geted_Score;

        if (Score >= 20)
        {
           
            Timer_Is_On = false;

            Win_Panel.SetActive(true);
            Score_Text.text = "Score : "+  Score.ToString();
         
        }
        else
        {

            if (Score <= 0)
            {

                Lost_Panel.SetActive(true);
                Score = 0;
                Score_Text.text = "Score : " + Score.ToString();
                Timer_Is_On = false;
              
            }
            else
            {
               
                Score_Text.text = "Score : " + Score.ToString();
                Timer_Is_On = false;

                Time_Text.text = "";

        
            }

            CancelInvoke();
        }
    }


    public void Check_Currect_Color( Image image)
    {
        if (image.color == Color_List[The_Index])
        {

            Controller_Score(1);

            Win_Sound.Play();

            if (Score > 0)
            {
                Time_For_Round = 5;
                Make_Doc_Colory();
            }


        }
        else
        {
            Controller_Score(-1);

            Lost_Sound.Play();
            if (Score > 0)
            {
                Time_For_Round = 5;
                Make_Doc_Colory();
            }
        }
    }



    public void Reset_Game()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
