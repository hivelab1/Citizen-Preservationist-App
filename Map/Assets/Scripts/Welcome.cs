using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Welcome : MonoBehaviour
{
    public static bool defaultMap = false;
    public Toggle defaultMaptoggle;
    public Text defaultText;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Pass()
    {
        defaultMap = defaultMaptoggle.isOn;
        SceneManager.LoadSceneAsync("Map");
    }

    private void Update()
    {
        if (defaultMaptoggle.isOn)
        {
            defaultText.enabled = false;
            defaultText.gameObject.GetComponentInParent<Image>().color = new Color(1, 1, 1, 0);
        }
        else
        {
            defaultText.enabled = true;
            defaultText.gameObject.GetComponentInParent<Image>().color = new Color(1, 1, 1, 100 / 255f);
        }
    }
}
