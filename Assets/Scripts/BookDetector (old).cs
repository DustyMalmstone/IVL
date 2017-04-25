//using Leap.Unity;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BookDetector : MonoBehaviour {

//    //public objects
//    public int lerpTime;

//    //private objects
//    bool isMove = false;
//    float currentLerpTime = 0;
//    Transform defReadSpot; //The default spot for reading from bsc

//    private void Start()
//    {
//        defReadSpot = BookshelfSpawnController.defReadSpot.transform;
//    }

//    private void Update()
//    {
//        if (isMove)
//        {
//            defReadSpot = BookshelfSpawnController.defReadSpot.transform;
//            currentLerpTime += Time.deltaTime;
//            if (currentLerpTime >= lerpTime)
//            {
//                currentLerpTime = lerpTime;
//            }
//            float distCovered = currentLerpTime / lerpTime;
//            transform.position = Vector3.Lerp(transform.position, defReadSpot.position, distCovered);

//            if (transform.position.Equals(defReadSpot.position))
//            {
//                isMove = false;
//            }
//        }
//    }

//    /*
//     * This block of code was taken from a forum post by Weeba2933 on the Leap Motion forums and edited
//     */
//    private bool IsHand(Collider other)
//    {
//        if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>())
//            return true;
//        else
//            return false;
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        Debug.Log("SOMETHING TOUCHED ME");

//        if (IsHand(other) && !BookshelfSpawnController.bookSelected)
//        {
//            Debug.Log("Yay! A hand collided!");
//            isMove = true;
//            BookshelfSpawnController.bookSelected = true;
//        }
//    }

//    public void Move()
//    {
//        isMove = true;
//    }
//}
