using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KBFader : MonoBehaviour {
    Color startColor;
    Color currentColor;
    Color endColor;
    public bool shouldFadeIn = false, shouldFadeOut = false, canFade = true, faded = false;
    public static bool fadePass = false;
    float startTime;
    float endTime;
    public float seconds = 5.0f;
    float t;

    void Start()
    {
        //if(gameObject.tag != "FadeExempt")
        //{
            //if (gameObject.GetComponent<Image>() != null)
            //{
            //    startColor = gameObject.GetComponent<Image>().color;
            //    endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
            //}

            ApplyToChildren();
        //}
    }

    void Update()
    {
        if (canFade && !faded && fadePass)
        {
            shouldFadeOut = true;

            canFade = false;

            startTime = Time.time;

            endTime = startTime + seconds;
            Debug.Log("Inside the Update() if statement to fade");
        }
        else if(canFade && faded && fadePass)
        {
            shouldFadeIn = true;

            canFade = false;

            startTime = Time.time;

            endTime = startTime + seconds;
        }

        if (shouldFadeOut)
        {
            Debug.Log("Inside the Update() if statement to run FadeOut()");
            FadeOut();
            Debug.Log("Inside the Update() if statement to run FadeOut() and went past FadeOut()");
        }

        if (shouldFadeIn)
        {
            FadeIn();
        }
        
    }

    public void ApplyToChildren()
    {
        for (int childIndex = 0; childIndex < gameObject.transform.childCount; childIndex++)
        {
            Transform child = gameObject.transform.GetChild(childIndex);

            child.gameObject.AddComponent<KBFader>();
        }
    }

    void FadeOut()
    {
        if(shouldFadeOut && !gameObject.tag.Equals("FadeExempt"))
        {
            gameObject.SetActive(false);
        }

        //Debug.Log("Inside FadeOut()");
        //if (shouldFadeOut)
        //{
        //    Debug.Log("shouldFadeOut is true inside FadeOut()");
        //    t = Time.deltaTime;

        //    currentColor = Color.Lerp(startColor, endColor, t);

        //    gameObject.GetComponent<Image>().color = currentColor;
        //    Debug.Log("Color should be set");

        //    if (currentColor == endColor)
        //    {
        //        Debug.Log("At the end of fading");
        //        shouldFadeOut = false;
        //        canFade = true;
        //        fadePass = false;
        //        startTime = 0.0f;
        //        endTime = 0.0f;
        //        t = 0.0f;
        //    }
        //}
    }

    void FadeIn()
    {
        if(shouldFadeIn && !gameObject.tag.Equals("FadeExempt"))
        {
            gameObject.SetActive(true);
        }

    //    if (shouldFadeIn)
    //    {
    //        t = Time.time / endTime;

    //        currentColor = Color.Lerp(endColor, startColor, t);

    //        gameObject.GetComponent<Image>().color = currentColor;

    //        if (currentColor == startColor)
    //        {
    //            shouldFadeIn = false;
    //            canFade = true;
    //            fadePass = false;
    //            startTime = 0.0f;
    //            endTime = 0.0f;
    //            t = 0.0f;
    //        }
    //    }
    }
}
