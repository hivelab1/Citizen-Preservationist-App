using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScreenManager : MonoBehaviour
{
    public Transform Canvas;
    public GameObject Welcome;
    public RawImage reviewImage;

    private void Start()
    {
        foreach (Transform child in Canvas)
        {
           child.gameObject.SetActive(false);
        }

        Welcome.SetActive(true);
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
