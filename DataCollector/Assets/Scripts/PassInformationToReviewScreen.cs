using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassInformationToReviewScreen : MonoBehaviour
{
    //[Header("Data Entry")]
    //public Text artifactTitleSend;
    //public Text materialSend;
    //public Text eraSend;
    //public Text classSend;
    //public Text groupSend;
    //public Text categorySend;
    //public Text lengthSend, widthSend, heightSend;
    //public Text descriptionSend;

    //[Header("Review")]
    //public Text artifactTitleReceive;
    //public Text materialReceive;
    //public Text eraReceive;
    //public Text classReceive;
    //public Text groupReceive;
    //public Text categoryReceive;
    //public Text lengthReceive, widthReceive, heightReceive;
    //public Text descriptionReceive;

    public GameObject[] pagesSend;
    public GameObject[] pagesReceive;

    public void Awake()
    {
        
    }

    public void PassInformation()
    {
        for (int i = 0; i < pagesSend.Length; i++)
        {
            pagesReceive[i].GetComponentInChildren<Text>().text = pagesSend[i].GetComponentInChildren<Text>().text;
        }

        //for (int i = pagesSend.Length - 4; i < pagesSend.Length; i++)
        //{
        //    pagesReceive[i].GetComponentInChildren<InputField>().text = pagesSend[i].GetComponentInChildren<Text>().text;
        //}
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
