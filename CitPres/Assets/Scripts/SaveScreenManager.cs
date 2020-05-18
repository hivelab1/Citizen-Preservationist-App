using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SaveScreenManager : MonoBehaviour
{
    public GameObject Active;
    public GameObject Review;
    public RawImage reviewImage;

    string log = "Log";

    private void Start()
    {
        Active.SetActive(true);
        Review.SetActive(false);
    }

    public IEnumerator TakePicture(WebCamTexture backCam)
    {
        yield return new WaitForEndOfFrame();

        Texture2D tex = new Texture2D(backCam.width, backCam.height);
        tex.SetPixels(backCam.GetPixels());
        tex.Apply();

        reviewImage.texture = tex;
    }


}
