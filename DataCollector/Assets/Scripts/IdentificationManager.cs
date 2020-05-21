using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdentificationManager : MonoBehaviour
{
    public static IdentificationManager instance;

    public InputField UserIDText, LocationIDText;

    public string UserID;
    public string LocationID;
    private string pool = "0123456789abcdef";

    public void Awake()
    {
        instance = this;
    }

    public void SetUserID()
    {
        instance.UserID = UserIDText.text;
        instance.LocationID = LocationIDText.text;
    }

    public string GenerateID()
    {
        string result = "";
        for (int i = 0; i < 32; i++)
        {
            result = result + string.Format("{0}", pool[Random.Range(0, 16)]);
        }
        return string.Format("v{0}_{1}", instance.UserID, result);
    }
}
