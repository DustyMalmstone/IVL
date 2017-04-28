using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookDetector : MonoBehaviour {

    //public objects
    public int lerpTime;
    public bool isReturn = false;

    //private objects
    bool isMove = false;
    float currentLerpTime = 0;
    bool gotText = false;
    public Transform defReadSpot; //The default spot for reading from bsc
    Vector3 shelfSpot; //The position on the shelf of the book
    Quaternion shelfRot; //The rotation of the book on the shelf

    private void Start()
    {
        //defreadspot = bookshelfspawncontroller.defreadspot.transform;
        shelfSpot = gameObject.transform.position;
        shelfRot = gameObject.transform.rotation;
    }

    private void Update()
    {
        if (isMove)
        {
            gameObject.tag = "FadeExempt";
            BookshelfSpawnController.currentBook = gameObject;

            //BookshelfSpawnController.currentBookShelfSpot.transform.position = gameObject.transform.position;
            //shelfSpot = BookshelfSpawnController.currentBookShelfSpot.transform;

            //defReadSpot = BookshelfSpawnController.defReadSpot.transform;
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float distCovered = currentLerpTime / lerpTime;
            transform.position = Vector3.Lerp(shelfSpot, defReadSpot.position, distCovered);
            transform.rotation = Quaternion.Lerp(shelfRot, defReadSpot.rotation, distCovered);

            if (!gotText)
            {
                bool passed = gameObject.GetComponent<NewBook>().EstablishText();
            
                if (passed)
                {
                    gotText = true;
                    gameObject.GetComponent<NewBook>().EstablishPages();
                }
            }

            if (transform.position.Equals(defReadSpot.position))
            {
                isMove = false;
            }
        }

        if (isReturn)
        {
            
            BookshelfSpawnController.currentBook = null;
            BookshelfSpawnController.bookSelected = false;

            defReadSpot = BookshelfSpawnController.defReadSpot.transform;
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float distCovered = currentLerpTime / lerpTime;
            transform.position = Vector3.Lerp(defReadSpot.position, shelfSpot, distCovered);
            transform.rotation = Quaternion.Lerp(defReadSpot.rotation, shelfRot, distCovered);

            if (transform.position.Equals(shelfSpot))
            {
                isReturn = false;
                gameObject.tag = "Book";
            }
            Debug.Log("Moved book back");
        }
    }

    /*
     * This block of code was taken from a forum post by Weeba2933 on the Leap Motion forums and edited
     */
    private bool IsHand(Collider other)
    {
        if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>())
            return true;
        else
            return false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("SOMETHING TOUCHED ME");

        if (IsHand(other) && !BookshelfSpawnController.bookSelected)
        {
            Debug.Log("Yay! A hand collided!");
            isMove = true;
            BookshelfSpawnController.bookSelected = true;
        }
    }

    public void Move()
    {
        isMove = true;
    }
}
