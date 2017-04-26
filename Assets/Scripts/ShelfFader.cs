using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfFader : MonoBehaviour {
    Color startColor;
    Color currentColor;
    Color endColor;
    bool shouldFadeOut = false, shouldFadeIn = false, canFade = true, faded = false;
    public static bool fadePass = false;
    float startTime;
    float endTime;
    public float seconds = 5.0f;
    float t;

    // Use this for initialization
    void Start()
    {
        if(gameObject.GetComponent<Renderer>() != null)
        {
            startColor = gameObject.GetComponent<Renderer>().material.color;
            endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
        }

        ApplyToChildren();
    }

    // Update is called once per frame
    void Update()
    {
        if (canFade && !faded && fadePass)
        {
            shouldFadeOut = true;

            canFade = false;

            startTime = Time.time;

            endTime = startTime + seconds;
        }
        else if (canFade && faded && fadePass)
        {
            shouldFadeIn = true;

            canFade = false;

            startTime = Time.time;

            endTime = startTime + seconds;
        }

        if (shouldFadeOut)
        {
            FadeOut();
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
        if (shouldFadeOut)
        {
            t = Time.time / endTime;

            currentColor = Color.Lerp(startColor, endColor, t);

            gameObject.GetComponent<Renderer>().material.SetColor("_Color", currentColor);

            if (currentColor == endColor)
            {
                shouldFadeOut = false;
                canFade = true;
                fadePass = false;
                startTime = 0.0f;
                endTime = 0.0f;
                t = 0.0f;
            }
        }
    }

    void FadeIn()
    {
        if (shouldFadeIn)
        {
            t = Time.time / endTime;

            currentColor = Color.Lerp(endColor, startColor, t);

            gameObject.GetComponent<Renderer>().material.SetColor("_Color", currentColor);

            if (currentColor == startColor)
            {
                shouldFadeIn = false;
                canFade = true;
                fadePass = false;
                startTime = 0.0f;
                endTime = 0.0f;
                t = 0.0f;
            }
        }
    }
}
