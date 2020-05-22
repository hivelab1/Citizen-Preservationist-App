using SFB;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Settings Panel Handler")]
    public GameObject blockScrollview;

    public string folderPath;

    [Header("Blip Engine")]
    MapConfig mapConfig;
    public TextAsset mapCfg;
    Vector2 mapSize;
    public RawImage mapImage;
    public List<Blip> blipData;
    public GameObject blipPrefab;
    public Transform blipParent;
    public GameObject buttonDirect;

    [Header("Blip Usage Controller")]
    public GameObject openedBlipPanel;
    public GameObject manager;
    public GameObject buttonCorner;
    public Text blipTitle;
    public Text blipDescription;
    public RawImage blipPicture;
    public Text GPSCoords;
    public Text date;
    public Text blipMaterial;
    public Text blipEra;
    public Text blipSize;
    public Text blipClass;

    public GameObject browse;

    public GameObject template;
    public Slider slider;
    public List<GameObject> pages;
    public Text label;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        mapSize = blipParent.GetComponent<RectTransform>().sizeDelta;
        if (PlayerPrefs.HasKey("Path"))
        {
            folderPath = PlayerPrefs.GetString("Path");
            PopulateMap(Welcome.defaultMap);
        }
        else
        {
            folderPath = Application.persistentDataPath;
            ChooseFolder();
        }

        openedBlipPanel.SetActive(false);
        blockScrollview.SetActive(false);
    }

    public void ChooseFolder()
    {
        try
        {
            folderPath = StandaloneFileBrowser.OpenFolderPanel("", folderPath, false)[0];
            PlayerPrefs.SetString("Path", folderPath);
            PopulateMap(Welcome.defaultMap);

        }
        catch (Exception)
        {
            // Add popup here (modal dialog) to indicate the error.
        }
    }

    public void SliderChanged()
    {
        if (slider.value >= pages.Count)
        {
            foreach (GameObject page in pages)
            {
                page.SetActive(true);
                label.text = "Showing All";
            }
        }
        else
        {
            for (int i = 0; i < pages.Count; i++)
            {
                if (i == slider.value)
                {
                    pages[i].SetActive(true);
                    label.text = pages[i].name;
                }
                else
                {
                    pages[i].SetActive(false);
                }
            }
        }
    }

    public void PopulateMap(bool defaultMap)
    {
        if (!defaultMap)
        {
            mapConfig = new MapConfig();
            mapImage = Resources.Load<RawImage>("Map/Map");
            mapSize = blipParent.GetComponent<RectTransform>().sizeDelta;
        }
        else
        {
            mapConfig = JsonUtility.FromJson<MapConfig>(mapCfg.text);
        }

        if (pages != null && pages.Count > 0)
        {
            foreach (GameObject page in pages)
            {
                GameObject.Destroy(page);
            }
        }
        blipData = new List<Blip>();
        pages = new List<GameObject>();
        slider.gameObject.SetActive(true);
        slider.maxValue = Directory.GetDirectories(folderPath).Length;
        if (Directory.GetDirectories(folderPath).Length > 0)
        {
            Debug.Log("Directory Mode");
            foreach (string file in Directory.GetDirectories(folderPath))
            {
                Debug.Log("DIR: " + file);
                GameObject entry = GameObject.Instantiate(template, Vector3.zero, Quaternion.identity, template.transform.parent);
                entry.GetComponent<RectTransform>().localPosition = Vector3.zero;
                string[] parts = file.Replace("\\", "/").Split("/"[0]);
                entry.name = parts[parts.Length - 1];
                pages.Add(entry);
                foreach (string subfile in Directory.GetFiles(file))
                {
                    Debug.Log("FILE: " + file);
                    Load(subfile, entry.transform);
                }
            }
        }
        else
        {
            Debug.Log("File Mode");
            slider.gameObject.SetActive(false);
            GameObject entry = GameObject.Instantiate(template);
            entry.GetComponent<RectTransform>().localPosition = Vector3.zero;
            entry.transform.parent = template.transform.parent;
            string[] parts = folderPath.Replace("\\", "/").Split("/"[0]);
            entry.name = parts[parts.Length - 1];
            pages.Add(entry);
            foreach (string file in Directory.GetFiles(folderPath))
            {
                Debug.Log("FILE: " + file);
                Load(file, entry.transform);
            }
        }
        SliderChanged();
        buttonDirect.SetActive(true);
    }

    public void Load(string file, Transform parent)
    {
        if (file.EndsWith(".json"))
        {
            string dataAsJson = File.ReadAllText(file);

            try
            {
                Debug.Log("Loading!");
                buttonDirect.SetActive(false);
                PictureData loadedData = JsonUtility.FromJson<PictureData>(dataAsJson);
                GameObject blipObject = GameObject.Instantiate(blipPrefab, Vector3.zero, Quaternion.identity, parent);
                Vector3 pos = ComputeBlipPos(loadedData.Latitude, loadedData.Longitude);

                Debug.Log("[MAP] Created " + loadedData.Name + " at " + pos);

                blipObject.name = loadedData.Name;
                blipObject.GetComponent<Blip>().CreateBlip(loadedData.Name, pos, loadedData.Description, loadedData.Picture, loadedData.Latitude, loadedData.Longitude, loadedData.CurrentTime, loadedData.Material, loadedData.Era, loadedData.Size, loadedData.ArtifactClass, manager);
                blipData.Add(blipObject.GetComponent<Blip>());
            }
            catch (Exception e)
            {
                Debug.Log("Exception: " + e);
            }

        }
    }

    Vector3 ComputeBlipPos(double lat, double lon)
    {
        float xPos = -mapSize.x + (float)((lon - mapConfig.leftWGS84Corner) / mapConfig.pixelRatioX);
        float yPos = -(float)((lat - mapConfig.topWGS84Corner) / mapConfig.pixelRatioY);
        return new Vector3(xPos, yPos, 0.0f);
    }

    [Serializable]
    public class PictureData
    {
        public string Name;
        public float Latitude;
        public float Longitude;
        public string Era;
        public string Material;
        public string Size;
        public string ArtifactClass;
        public string Description;
        public string Picture;
        public string CurrentTime;

        public PictureData(string Name, float Latitude, float Longitude, string Era, string Material, string Size, string ArtifactClass, string Description, string Picture, string CurrentTime)
        {
            this.Name = Name;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.Era = Era;
            this.Material = Material;
            this.Size = Size;
            this.ArtifactClass = ArtifactClass;
            this.Description = Description;
            this.Picture = Picture;
            this.CurrentTime = CurrentTime;
        }
    }

    [Serializable]
    public class MapConfig
    {
        public double leftWGS84Corner;
        public double topWGS84Corner;

        public double pixelRatioX;
        public double pixelRatioY;

        public MapConfig()
        {
            Location location1 = JsonUtility.FromJson<Location>(Resources.Load<TextAsset>("Map/location1").text);
            Location location2 = JsonUtility.FromJson<Location>(Resources.Load<TextAsset>("Map/location2").text);

            double dYGlobal = location1.WSGLat - location2.WSGLat;
            double dXGlobal = location1.WSGLong - location2.WSGLong;

            int dXLocal = location1.pxX - location2.pxX;
            int dYLocal = location1.pxY - location2.pxY;

            double xScalar = dXGlobal / dXLocal;
            double yScalar = dYGlobal / dYLocal;

            double leftOffsetA = location1.WSGLong - (xScalar * location1.pxX);
            double topOffsetA = location1.WSGLat - (yScalar * location1.pxY);

            double leftOffsetB = location2.WSGLong - (xScalar * location2.pxX);
            double topOffsetB = location2.WSGLat - (yScalar * location2.pxY);

            double leftCornerGlobal = (leftOffsetA + leftOffsetB) / 2;
            double topCornerGlobal = (topOffsetA + topOffsetB) / 2;

            double pxRatioAX = (location1.WSGLong - leftCornerGlobal) / location1.pxX;
            double pxRatioAY = (location1.WSGLat - topCornerGlobal) / location1.pxY;

            double pxRatioBX = (location2.WSGLong - leftCornerGlobal) / location2.pxX;
            double pxRatioBY = (location2.WSGLat - topCornerGlobal) / location2.pxY;

            double pxRatioX = (pxRatioAX + pxRatioBX) / 2;
            double pxRatioY = (pxRatioAY + pxRatioBY) / 2;

            leftWGS84Corner = leftCornerGlobal;
            topWGS84Corner = topCornerGlobal;
            pixelRatioX = pxRatioX;
            pixelRatioY = pxRatioY;
        }

    }

    [Serializable]
    public class Location
    {
        public int pxX;
        public int pxY;

        public double WSGLat;
        public double WSGLong;
    }
}
