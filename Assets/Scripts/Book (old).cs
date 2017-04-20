//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Book : MonoBehaviour {

//    public float SpaceBetweenPages;
//    public int NumberOfPages;
//    public GameObject PagePrefab;

//    private GameObject[] pages;
//    private float BookWidth;
//    private GameObject spine;
//    private GameObject frontCoverRotator;
//    private GameObject backCoverRotator;
//    private GameObject spineRotator;
//    private GameObject frontCover;
//    private GameObject backCover;

//	// Use this for initialization
//	void Start () {
//        BookWidth = (SpaceBetweenPages * NumberOfPages) + .02f;

//        spine = transform.FindChild("Cover").FindChild("SpineRotator").FindChild("Spine").gameObject;
//        frontCoverRotator = transform.FindChild("Cover").FindChild("FrontCoverRotator").gameObject;
//        backCoverRotator = transform.FindChild("Cover").FindChild("BackCoverRotator").gameObject;
//        spineRotator = transform.FindChild("Cover").FindChild("SpineRotator").gameObject;
//        frontCover = transform.FindChild("Cover").FindChild("FrontCoverRotator").FindChild("Front Cover").gameObject;
//        backCover = transform.FindChild("Cover").FindChild("BackCoverRotator").FindChild("Back Cover").gameObject;

//        spine.transform.localScale = new Vector3(1.4f, BookWidth, .05f);
//        spine.transform.localPosition = new Vector3(0f, (-BookWidth / 2f), 0f);
//        frontCoverRotator.transform.localPosition = new Vector3(0f, (spine.transform.localPosition.y+(-(BookWidth/2) + .005f)), -1f + (spine.transform.localScale.z / 2));
//        backCoverRotator.transform.localPosition = new Vector3(0f, (spine.transform.localPosition.y + ((BookWidth / 2) + .005f)), -1f + (spine.transform.localScale.z / 2));
//        //frontCover.transform.localPosition = new Vector3(0f, 0f, .5f);
//        //backCover.transform.localPosition = new Vector3(0f, 0f, .5f);

//        pages = new GameObject[NumberOfPages];
//        //transform.FindChild("Cover").FindChild("SpineRotator").FindChild("Spine").transform.localScale.y
//        SpaceBetweenPages = (float)(BookWidth - .01) / pages.Length;

//        for (int i = 0; i < pages.Length; i++)
//        {
//            pages[i] = Instantiate(PagePrefab);
//            pages[i].transform.parent = transform;
//            pages[i].transform.localPosition = new Vector3(0f, (-.005f + - i * SpaceBetweenPages), 0f);
//            pages[i].transform.localRotation = new Quaternion(180, 0, 0, 0);
//        }
//	}
	
//	// Update is called once per frame
//	void Update () {
		
//	}
//}
