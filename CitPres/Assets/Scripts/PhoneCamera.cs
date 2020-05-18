using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour
{

    //public Image artifactPreview;

    //private int numImages = 1;

    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;

    public float cooldown = 0.5f;

    void Start()
    {
        CheckCameras();
    }

    void Update()
    {
        if (!camAvailable)
        {
            CheckCameras();
            return;
        }
        float ratio = backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orientation = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);

        cooldown -= Time.deltaTime;
    }

    public void TakePicture()
    {
        StartCoroutine(GetComponent<SaveScreenManager>().TakePicture(backCam));
    }

    private void CheckCameras()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            //Debug.Log("No cameras detected");
            camAvailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
#if !UNITY_EDITOR
            if (!devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
#endif

#if UNITY_EDITOR
            if (i == 0)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
#endif
        }

        if (backCam == null)
        {
            //Debug.Log("Unable to find a back camera");
            return;
        }

        backCam.Play();
        background.texture = backCam;

        camAvailable = true;
    }
}
