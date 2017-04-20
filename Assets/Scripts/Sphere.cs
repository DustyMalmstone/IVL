using UnityEngine;
using System.Collections;

public class Sphere : MonoBehaviour {

    public float targetScale = 0.001f;
    public float shrinkSpeed = 0.75f;

	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = Vector3.Lerp(transform.localScale,
            new Vector3(targetScale, targetScale, targetScale), Time.deltaTime * shrinkSpeed);
	}
}
