using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTextBounce : MonoBehaviour
{
    RectTransform rectTransform;
    Vector3 smallText;
    public Vector3 bigText = new Vector3(1.07f, 1.07f, 0f);
    public float scaleSpeed = 1.5f;
    public float animDuration = 1f;
    public bool repeatable = true;
    

    IEnumerator Start ()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        smallText = rectTransform.localScale;

        while (repeatable)
        {
            // lerp up scale
            yield return RepeatLerp(smallText, bigText, animDuration);
            //lerp down scale
            yield return RepeatLerp(bigText, smallText, animDuration);
        }
    }

    public IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * scaleSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            rectTransform.localScale = Vector3.Lerp(a, b, i);
            yield return null;

        }
    }
}
