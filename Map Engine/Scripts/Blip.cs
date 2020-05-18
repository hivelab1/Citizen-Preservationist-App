using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/***
 * Blip retains all the data for a map blip. This includes the local and global data, the image data, and the text data.
 ***/
public class Blip : MonoBehaviour {

    //What is shown on the map
    [Header("Blip object on map")]
    public Image blipIcon;
    public Text artifactTitle;
    string blipTitle;
    string blipDate;

    //What is shown when tapped
    [Header("Blip open panel")]
    public GameObject openedBlipPanel;
    public GameObject coverScrollview;
    public GameObject cornerButton;
    public Text blipTitleDisplay;
    string description;
    string material;
    string era;
    string size;
    string artifactClass;
    Texture2D artifact;
    List<Blip> blipData;
    Text blipDescription;
    RawImage blipPicture;
    Text GPSCoords;
    Text date;
    Text materialText;
    Text eraText;
    Text sizeText;
    Text classText;
    

    RectTransform rect;

    Vector2Int pos;

    public GameObject slider;

    float latitude = 0;
    float longitude = 0;

    public void CreateBlip(string artifactName, Vector3 position, string description, string jsonPicture, float latitude, float longitude, string date, string material, string era,string size, string artifactClass, GameObject manager)
    {
        this.blipTitle = artifactName;
        this.blipDate = date;
        this.artifactTitle.text = blipTitle;
        this.material = material;
        this.era = era;
        this.size = size;
        this.artifactClass = artifactClass;

        this.description = description;

        this.artifact = new Texture2D(1, 1);
        this.artifact.LoadImage(System.Convert.FromBase64String(jsonPicture));
        Debug.Log(artifact);

        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = position;
        rect.localPosition = new Vector3(rect.localPosition.x + (blipIcon.transform.GetComponent<RectTransform>().sizeDelta.x), rect.localPosition.y, 0.0f);

        pos.x = Mathf.Abs((int)position.x);
        pos.y = Mathf.Abs((int)position.y);

        this.latitude = latitude;
        this.longitude = longitude;

        this.blipData = manager.GetComponent<SettingsManager>().blipData;
        this.openedBlipPanel = manager.GetComponent<SettingsManager>().openedBlipPanel;
        this.coverScrollview = manager.GetComponent<SettingsManager>().blockScrollview;
        this.blipTitleDisplay = manager.GetComponent<SettingsManager>().blipTitle;
        this.blipDescription = manager.GetComponent<SettingsManager>().blipDescription;
        this.blipPicture = manager.GetComponent<SettingsManager>().blipPicture;
        this.GPSCoords = manager.GetComponent<SettingsManager>().GPSCoords;
        this.date = manager.GetComponent<SettingsManager>().date;
        this.slider = manager.GetComponent<SettingsManager>().slider.gameObject;
        this.materialText = manager.GetComponent<SettingsManager>().blipMaterial;
        this.eraText = manager.GetComponent<SettingsManager>().blipEra;
        this.sizeText = manager.GetComponent<SettingsManager>().blipSize;
        this.classText = manager.GetComponent<SettingsManager>().blipClass;
        this.cornerButton = manager.GetComponent<SettingsManager>().buttonCorner;
        
        
        //Bodie upper left corner roughly 38.218427, -119.017472
        //Bodie lower right corner roughly 38.206502, -119.004842
    }

    public void OpenBlip()
    {
        foreach (Blip blip in blipData)
        {
            if (this.name == blip.name)
            {
                blipTitleDisplay.text = blipTitle;
                blipDescription.text = description;
                materialText.text = material;
                GPSCoords.text = "GPS Coordinates: (" + latitude + ", " + longitude + ")";
                eraText.text = era;
                sizeText.text = size;
                classText.text = artifactClass;
                openedBlipPanel.SetActive(true);
                coverScrollview.SetActive(true);
                cornerButton.SetActive(false);
                date.text = blipDate;
                slider.SetActive(false);
                blipPicture.texture = artifact;
                break;
            }
        }
    }
}
