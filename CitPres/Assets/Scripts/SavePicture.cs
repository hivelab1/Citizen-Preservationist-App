using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class SavePicture : MonoBehaviour
{
    private readonly string saveDataJson;
    public enum Type {Collection, Dataset, Event, Image, InteractiveResource, MovingImage, Service, Software, Sound, StillImage, Text, };

    private readonly SaveData saveData;
    public Dropdown Era, Material, Size, ArtifactClass;
    public InputField Description, Name;
    public RawImage picture;
    float latitude, longitude;

    public GameObject Active;
    public GameObject Review;

    string fileName;
    string pictureName;

    public void Awake()
    {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }

        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
#endif
    }

    public void Start()
    {
        fileName = Application.persistentDataPath + "/Artifacts/" + System.DateTime.Now.ToString("MM-dd-yyyy");

        pictureName = Application.persistentDataPath + "/Pictures/" + System.DateTime.Now.ToString("MM-dd-yyyy");

#if PLATFORM_ANDROID
        pictureName = Application.persistentDataPath + "/DCIM/Bodie/" + System.DateTime.Now.ToString("MM-dd-yyyy");
#endif

#if UNITY_EDITOR
        pictureName = Application.persistentDataPath + "/Pictures/" + System.DateTime.Now.ToString("MM-dd-yyyy");
#endif

        try
        {
            if (!Directory.Exists(pictureName))
            {
                Directory.CreateDirectory(pictureName);
            }

            if (!Directory.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exeption: " + e);
        }
    }

    public void Update()
    {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }

        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
#endif
    }

    public void ResetFields()
    {
        StartCoroutine(ResetFieldsAsync());
     
    }

    IEnumerator ResetFieldsAsync()
    {
        yield return new WaitForEndOfFrame();

        //Era.text = "Select Era";
        Era.value = 0;
        //Material.text = "Select Material";
        Material.value = 0;
        //Size.text = "Select Size";
        Size.value = 0;
        //ArtifactClass.text = "Select Artifact Class";
        ArtifactClass.value = 0;
        Description.Select();
        Description.text = String.Empty;
        Name.Select();
        Name.text = String.Empty;

        if (!Input.location.isEnabledByUser)
        {
            Debug.LogError("Location Services not enabled!");
            yield break;
        }

        Input.location.Start();
        float maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            Debug.LogError("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location");
            yield break;
        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        Input.location.Stop();

       Name.Select();  
    }

    public void SavePictureButton()
    {
        string eraText = Era.captionText.text;
        string materialText = Material.captionText.text;
        string sizeText = Size.captionText.text;
        string artifactClassText = ArtifactClass.captionText.text;
        string descriptionText = Description.text;
        string nameText = Name.text;
        string currentTime = DateTime.UtcNow.ToString("G");
        // G :08/17/2000 16:32:32
        // Month/Day/Year Hour:Minute:Seconds

        fileName = Application.persistentDataPath + "/Artifacts/" + DateTime.Now.ToString("MM-dd-yyyy") + "/" + nameText + ".json";

        pictureName = Application.persistentDataPath + "/Pictures/" + System.DateTime.Now.ToString("MM-dd-yyyy") + "/" + nameText + ".png";

#if PLATFORM_ANDROID
        pictureName = Application.persistentDataPath + "/DCIM/Bodie/" + System.DateTime.Now.ToString("MM-dd-yyyy") + "/" + nameText + ".png";
#endif

#if UNITY_EDITOR
        pictureName = Application.persistentDataPath + "/Pictures/" + System.DateTime.Now.ToString("MM-dd-yyyy") + "/" + nameText + ".png";
#endif

        Texture2D pictureTexture = picture.texture as Texture2D;
        byte[] bytes;
        bytes = pictureTexture.EncodeToPNG();
        string pictureText = System.Convert.ToBase64String(bytes);

        PictureData tempArtifact = new PictureData(nameText, latitude, longitude, currentTime, eraText, materialText, sizeText, artifactClassText, descriptionText, pictureText);

        string jsonFile = JsonUtility.ToJson(tempArtifact);

        //saveData.Artifacts.Add(tempArtifact);

        //saveDataJson = JsonConvert.SerializeObject(saveData);

        try
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                using (StreamWriter write = new StreamWriter(fs))
                {
                    write.Write(jsonFile);
                }
            }
            //File.WriteAllText(fileName, saveDataJson);
        }
        catch (Exception e)
        {
            Debug.LogError("Exeption: " + e);
        }

        if (File.Exists(fileName))
        {
            Debug.Log("Saved to: " + fileName);
        }

        try
        {
            File.WriteAllBytes(pictureName, bytes);
        }
        catch (Exception e)
        {
            Debug.LogError("Exception: " + e);
        }

        if (File.Exists(pictureName))
        {
            Debug.Log("Saved to: " + pictureName);
        }
    }

    [Serializable]
    public class SaveData
    {
        public List<PictureData> Artifacts { get; set; }
    }

    [Serializable]
    public class PictureData
    {
        public string Name;
        public float Latitude;
        public float Longitude;
        public string CurrentTime;
        public string Era;
        public string Material;
        public string Size;
        public string ArtifactClass;
        public string Description;
        public string Picture;
        public PictureData(string Name, float Latitude, float Longitude, string CurrentTime, string Era, string Material, string Size, string ArtifactClass, string Description, string Picture)
        {
            this.Name = Name;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.CurrentTime = CurrentTime;
            this.Era = Era;
            this.Material = Material;
            this.Size = Size;
            this.ArtifactClass = ArtifactClass;
            this.Description = Description;
            this.Picture = Picture;
        }
    }

    [Serializable]
    public class DCMI
    {
        public string title;
        public Type type;
        public string rightsStatus;
        public string isShownAt;
        public string objectURL;


        public PictureData metadata;


        public DCMI()
        {

        }
    }
}
