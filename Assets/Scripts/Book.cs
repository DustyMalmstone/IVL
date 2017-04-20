using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour {

    public float SpaceBetweenPages;
    public int NumberOfPages;
    public GameObject PagePrefab;
    public Transform Cover;
    public string BookName;
    public string Author;
    public string BookID;

    //private GameObject[] pages;
    private GameObject[] pageParts;
    private float BookWidth;
    private BoxCollider collider;

	// Use this for initialization
	void Start () {

        // Calculates the width of the book as double the number of pages times the space between each, with a tenth of that
        // then taken off to help keep the size close.
        BookWidth = ((SpaceBetweenPages * NumberOfPages) * 2) - ((SpaceBetweenPages * NumberOfPages) / 10); //

        //pages = new GameObject[NumberOfPages];
        // Since each page has two parts: a front and a back, the array containing all of the parts has to be twice
        // the number of pages
        pageParts = new GameObject[NumberOfPages * 2];

        Cover.localScale = new Vector3(Cover.localScale.x, BookWidth, Cover.localScale.z);
        // y position is half of bookwidth because scaling happens in both directions, so we only need to move it halfway.
        Cover.localPosition = new Vector3(Cover.localPosition.x, BookWidth / 2, Cover.localPosition.z);

        collider = Cover.gameObject.GetComponent<BoxCollider>();
        collider.size = new Vector3(2.5f, 1 + Cover.localScale.y, 2.25f);

        for (int i = 0; i < NumberOfPages /*pages.Length*/; i++)
        {
            GameObject page = Instantiate(PagePrefab);
            page.transform.parent = transform;
            page.transform.localPosition = new Vector3(0f, (i * SpaceBetweenPages), 0f);
            page.transform.localRotation = new Quaternion(180, 0, 0, 0);
            //pages[i] = Instantiate(PagePrefab);
            //pages[i].transform.parent = transform;
            //pages[i].transform.localPosition = new Vector3(0f, (i * SpaceBetweenPages), 0f); 
            //pages[i].transform.localRotation = new Quaternion(180, 0, 0, 0);

            // Page i front: 2i
            // Page i back: 2i+1
            pageParts[2*i] = page.transform.GetChild(0).gameObject;
            pageParts[2*i + 1] = page.transform.GetChild(1).gameObject;
            //pageParts[i] = pages[i].transform.GetChild(0).gameObject;
            //pageParts[i + 1] = pages[i].transform.GetChild(1).gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
