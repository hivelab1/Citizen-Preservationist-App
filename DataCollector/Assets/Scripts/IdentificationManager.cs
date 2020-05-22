using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdentificationManager : MonoBehaviour
{
    public static IdentificationManager instance;

    public InputField UserIDText, LocationIDText;

    public static string UserID;
    public static string LocationID;
    private string pool = "0123456789abcdef";

    public void Awake()
    {
        instance = this;
    }

    public void SetUserID()
    {
        UserID = UserIDText.text;
        LocationID = LocationIDText.text;
    }

    public string GenerateID()
    {
        string result = "";
        for (int i = 0; i < 32; i++)
        {
            result = result + string.Format("{0}", pool[Random.Range(0, 16)]);
        }
        return string.Format("v{0}_{1}", UserID, result);
    }
}
