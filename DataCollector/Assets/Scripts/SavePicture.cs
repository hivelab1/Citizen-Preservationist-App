using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class SavePicture : MonoBehaviour
{
    public Dropdown Era, Material, ArtifactClass, ConditionText, ArtifactGroup, ArtifactCategory;
    public InputField Description, Name, LengthText, WidthText, HeightText;
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

        Description.characterLimit = 1800;

    }

    public void Start()
    {
        fileName = Application.persistentDataPath + "/Artifacts/" + System.DateTime.Now.ToString("yyyy-MM-dd");

        pictureName = Application.persistentDataPath + "/Pictures/" + System.DateTime.Now.ToString("yyyy-MM-dd");

#if PLATFORM_ANDROID
        pictureName = Application.persistentDataPath + "/DCIM/Bodie/" + System.DateTime.Now.ToString("yyyy-MM-dd");
#endif

#if UNITY_EDITOR
        pictureName = Application.persistentDataPath + "/Pictures/" + System.DateTime.Now.ToString("yyyy-MM-dd");
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

        foreach (Dropdown content in FindObjectsOfType<Dropdown>())
        {
            content.value = 0;
            Debug.Log(content.name);
        }

        foreach (InputField content in FindObjectsOfType<InputField>())
        {
            content.Select();
            content.text = String.Empty;
        }

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
        string nameText = Name.text;
        string materialText = Material.captionText.text;
        string eraText = Era.captionText.text;
        string artifactClassText = ArtifactClass.captionText.text;
        string group = ArtifactGroup.captionText.text;
        string category = ArtifactCategory.captionText.text;
        string condition = ConditionText.captionText.text;

        double length = Convert.ToDouble(LengthText.text);
        double width = Convert.ToDouble(WidthText.text);
        double height = Convert.ToDouble(HeightText.text);

        string descriptionText = Description.text;

        string currentTime = DateTime.UtcNow.ToString("yyyy-MM-dd");
        // G :08/17/2000 16:32:32
        // Month/Day/Year Hour:Minute:Seconds

        //yyyy-MM-dd
        // year-Month-Day



        Texture2D pictureTexture = picture.texture as Texture2D;
        byte[] bytes;
        bytes = pictureTexture.EncodeToPNG();
        string pictureText = System.Convert.ToBase64String(bytes);

        PictureData tempArtifact = new PictureData(nameText, latitude, longitude, currentTime, eraText, materialText, length, width, height, artifactClassText, category, group,condition, descriptionText, pictureText, IdentificationManager.instance.GenerateID());

        string jsonFile = JsonUtility.ToJson(tempArtifact);

        fileName = Application.persistentDataPath + "/Artifacts/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + tempArtifact.Identifer + ".json";

        pictureName = Application.persistentDataPath + "/Pictures/" + System.DateTime.Now.ToString("yyyy-MM-dd") + "/" + tempArtifact.Identifer + ".png";

#if PLATFORM_ANDROID
        pictureName = Application.persistentDataPath + "/DCIM/Bodie/" + System.DateTime.Now.ToString("yyyy-MM-dd") + "/" + tempArtifact.Identifer + ".png";
#endif

#if UNITY_EDITOR
        pictureName = Application.persistentDataPath + "/Pictures/" + System.DateTime.Now.ToString("yyyy-MM-dd") + "/" + tempArtifact.Identifer + ".png";
#endif

        //saveData.Artifacts.Add(tempArtifact);

        //saveDataJson = JsonConvert.SerializeObject(saveData);

        #region Writing Files
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
        #endregion
    }



    //[Serializable]
    //public class SaveData
    //{
    //    public List<PictureData> Artifacts { get; set; }
    //}

    [Serializable]
    public class PictureData
    {
        #region Fields
        public string AccessRestrictions = "UC Merced only";
        public string CampusUnit = "UC Merced Library and Special Collections";
        public string Rights = "Material in the public domain: this work has been created by citizen preservationists/volunteers under a joint project developed by the University of California, Merced and the California Department of Parks and Recreation. No modifications should be made. Users of this work should not remove any public domain mark or this rights statement. The trademarks, name, or logos of the University of California, Merced and/or the California Department of Parks and Recreation should not be used to endorse (or imply origin of) the public domain work without written consent. Suggested appropriate tagging, annotation, or commenting on this work is welcome";
        public string CopyrightStatus = "public domain";
        public string Creator = "Anonymous";
        public string Date;                     // Application Defined
        public string Description1;             // User Defined
        public string Description1Type = "scopecontent";
        public string Description2 = "This artifact was recorded by citizen preservationists via the CitPres app with the authorization of California State Parks";
        public string Description2Type = "Acquisitions";
        public string Extent;                   // User Defined
        public string Identifer;                // Application Defined
        public string Language = "English";
        public string LanguageCode = "eng";
        public string Place1AuthorityID = "LCNAF";
        public string Place1Coordinates;        // Application Defined
        public string Place1Name;               // Application Defined
        public string Subject = "Property of California. Department of Parks and Recreation";
        public string Subject1NameType = "local";
        public string Coverage;                 // User Defined
        public string Title;                    // User Defined
        public string Type = "image";
        public string ArtifactGroup;            // User Defined
        public string ArtifactType;             // User Defined
        public string Condition;                // User Defined
        public string ArtifactCategory;         // User Defined
        public string Material;                 // User Defined

        public string Picture;                  // Application Defined

        #endregion
        public PictureData(string Title, float Latitude, float Longitude, string CurrentTime, string Coverage, string Material, double Length, double Width, double height, string ArtifactClass, string ArtifactCategory, string ArtifactGroup, string Condition, string Description, string Picture, string Identifier)
        {
            this.Title = Title;
            this.Place1Coordinates = String.Format("{0} {1}", Latitude, Longitude);
            this.Date = CurrentTime;
            this.Coverage = Coverage;
            this.Material = Material;
            this.Extent = String.Format("{0} x {1} x {2} x cm", ConvertInToCm(Length), ConvertInToCm(Width), ConvertInToCm(height));
            this.Description1 = Description;
            this.ArtifactType = ArtifactClass;
            this.ArtifactCategory = ArtifactCategory;
            this.ArtifactGroup = ArtifactGroup;
            this.Condition = Condition;
            this.Picture = Picture;
            this.Identifer = Identifier;
            this.Place1Name = IdentificationManager.LocationID;
        }

        private double ConvertInToCm(double measurement)
        {
            return measurement * 2.54;
        }
    }
}
