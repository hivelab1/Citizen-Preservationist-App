using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransparencyGradient : MonoBehaviour
{
    public RawImage focus;
    public Text focusText;

    bool decreasing = true;
    public float aTime;

    float minimum = 0.0F;
    float maximum = 1.0F;

    static float t = 0.0f;

    private void Update()
    {
        Color newColor = new Color(0, 0, 0, Mathf.Lerp(maximum, minimum, t));
        focus.color = newColor;
        focusText.color = newColor;

        t += 0.5f * Time.deltaTime / aTime;

        if (t > 1.0f)
        {
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
            t = 0.0f;
        }
    }
}
