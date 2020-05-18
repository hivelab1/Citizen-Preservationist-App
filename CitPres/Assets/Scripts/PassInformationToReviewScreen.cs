using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassInformationToReviewScreen : MonoBehaviour
{
    [Header("Data Entry")]
    public Text artifactTitleSend;
    public Text eraSend;
    public Text materialSend;
    public Text sizeSend;
    public Text artifactClassSend;
    public Text descriptionSend;

    [Header("Review")]
    public Text artifactTitleReceive;
    public Text eraReceive;
    public Text materialReceive;
    public Text sizeReceive;
    public Text artifactClassReceive;
    public Text descriptionReceive;

    public void PassInformation()
    {
        artifactTitleReceive.text = artifactTitleSend.text;
        eraReceive.text = eraSend.text;
        materialReceive.text = materialSend.text;
        sizeReceive.text = sizeSend.text;
        artifactClassReceive.text = artifactClassSend.text;
        descriptionReceive.text = descriptionSend.text;
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
