using UnityEngine;
using System.Collections.Generic;
using Leap;
using Leap.Unity;
using System;

public class SwipeController : MonoBehaviour {

	public LeapProvider provider;
    public float sphereScale = .05f;
    public float str = .1f;
    public GameObject foundBook = null;

    public bool IsLeftSwiping { get; private set; }
    public bool IsRightSwiping { get; private set; }
    public bool IsSwipingLeft{ get; private set; }
    public bool IsSwipingRight { get; private set; }

    private LinkedList<Leap.Vector> palmPositionsL;
    private LinkedList<Leap.Vector> palmPositionsR;
    private LinkedList<GameObject> spheresR;
    private LinkedList<GameObject> spheresL;

    private int NumberOfFrames;

    private float minDistCoveredForSwipe = .25f;

    // Required for creating primatives
    private MeshFilter filter;
    private MeshRenderer render;
    private BoxCollider bCollider;
    private SphereCollider sCollider; 

	// Use this for initialization
	void Awake () {
        palmPositionsL = new LinkedList<Leap.Vector>();
        palmPositionsR = new LinkedList<Leap.Vector>();
        spheresR = new LinkedList<GameObject>();
        spheresL = new LinkedList<GameObject>();
	}

	void Update() {
		Frame frame = provider.CurrentFrame;

        List<Hand> hands = frame.Hands;
        
        // If there are no hands, clear everything so it resets
        if (hands.Count == 0)
        {

            DestroySpheres(spheresL);
            DestroySpheres(spheresR);
            palmPositionsL.Clear();
            palmPositionsR.Clear();
            spheresR.Clear();
            spheresL.Clear();
        }

        // If only the right hand is visible, clear all of the
        // left hand stuff so it resets
        else if (hands.Count == 1 && hands[0].IsRight)
        {
            DestroySpheres(spheresL);
            palmPositionsL.Clear();
            spheresL.Clear();
        }

        // If only the left hand is visible, clear all of the
        // right hand stuff so it resets
        else if (hands.Count == 1 && hands[0].IsLeft)
        {
            DestroySpheres(spheresR);
            palmPositionsR.Clear();
            spheresR.Clear();
        }

        float moveDirectionL = 0f;
        float moveDirectionR = 0f;

        foreach (Hand hand in hands)
        {

            // e^(-str * Mag + e) - 10
            // result will always be (+) because as MAG -> 0, eqn before subt -> 15
            // str := strength. How sensetive the detection is. Lower strength => less sensetive
            NumberOfFrames = Mathf.RoundToInt(Mathf.Exp(
                (float)((-str * hand.PalmVelocity.MagnitudeSquared) + Math.E))) - 10;

            if (hand.IsLeft)
            {
                if (palmPositionsL.Count >= NumberOfFrames)
                {
                    //GameObject lastSphere = spheresL.Last.Value;

                    palmPositionsL.RemoveLast();
//#if UNITY_EDITOR
//                    spheresL.RemoveLast();

//                    DestroyImmediate(lastSphere.gameObject);
//#endif
                }
                palmPositionsL.AddFirst(hand.PalmPosition);

#if UNITY_EDITOR
                //CreateSphere(spheresL, hand.PalmPosition);

                // Makes min distance a function of numFrames, but closer to actual
                // distance traveled
                minDistCoveredForSwipe = NumberOfFrames * .01f;
                // Calculates distance

                moveDirectionL = palmPositionsL.First.Value.ToVector3().x
                    - palmPositionsL.Last.Value.ToVector3().x;

                float distCovered = (float)Math.Round(Math.Abs(moveDirectionL), 2);

                if (distCovered > minDistCoveredForSwipe &&
                    hand.Fingers.Exists(x => x.IsExtended && x.Type == Finger.FingerType.TYPE_INDEX))
                {
                    IsLeftSwiping = true;

                }

                else IsLeftSwiping = false;
#endif
            }

            if (hand.IsRight)
            {
                if (palmPositionsR.Count >= NumberOfFrames)
                {
                    //GameObject lastSphere = spheresR.Last.Value;

                    palmPositionsR.RemoveLast();
//#if UNITY_EDITOR
//                    spheresR.RemoveLast();

//                    DestroyImmediate(lastSphere.gameObject);
//#endif
                }
                palmPositionsR.AddFirst(hand.PalmPosition);

//#if UNITY_EDITOR
//                CreateSphere(spheresR, hand.PalmPosition);
//#endif
                // Makes min distance a function of numFrames, but closer to actual
                // distance traveled
                minDistCoveredForSwipe = NumberOfFrames * .01f;
                // Calculates distance

                moveDirectionR = palmPositionsR.First.Value.ToVector3().x
                    - palmPositionsR.Last.Value.ToVector3().x;

                float distCovered = (float)Math.Round(Math.Abs(moveDirectionR), 2);

                if (distCovered > minDistCoveredForSwipe &&
                    hand.Fingers.Exists(x => x.IsExtended && x.Type == Finger.FingerType.TYPE_INDEX))
                {
                    IsRightSwiping = true;
                }
                else IsRightSwiping = false;
            }
        }

        // If right hand is swiping left and left hand is not swiping
        if (IsRightSwiping && !IsLeftSwiping && moveDirectionR < 0)
        {
            IsSwipingLeft = true;
            IsSwipingRight = false;
            //Debug.Log("Right hand swiping left");
        }

        // If right hand is swiping right and left hand is not swiping
        else if (IsRightSwiping && !IsLeftSwiping && moveDirectionR > 0)
        {
            IsSwipingLeft = false;
            IsSwipingRight = true;
            //Debug.Log("Right hand swiping right");
        }

        // If left hand is swiping left and right hand is not swiping
        else if (IsLeftSwiping && !IsRightSwiping && moveDirectionL < 0)
        {
            IsSwipingLeft = true;
            IsSwipingRight = false;
            //Debug.Log("Left hand swiping left");
        }

        // If left hand is wiping right and right hand is not swiping
        else if (IsLeftSwiping && !IsRightSwiping && moveDirectionL > 0)
        {
            IsSwipingLeft = false;
            IsSwipingRight = true;
            //Debug.Log("Left hand swiping right");
        }

        // If both hands are swiping, defer to right hand
        else if (IsRightSwiping && IsLeftSwiping)
        {
            if (moveDirectionR < 0)
            {
                IsSwipingLeft = true;
                IsSwipingRight = false;
                //Debug.Log("=hand swiping left");
            }
            else if (moveDirectionR > 0)
            {
                IsSwipingLeft = false;
                IsSwipingRight = true;
                //Debug.Log("hand swiping right");
            }

            // If not the above, nothing is swiping
            else
            {
                IsSwipingLeft = false;
                IsSwipingRight = false;
            }

        }

        // If none of the above, then nothing is swiping
        else
        {
            IsSwipingLeft = false;
            IsSwipingRight = false;
        }

        // If both hands are swiping 
	}

    void CreateSphere(LinkedList<GameObject> spheres, Leap.Vector pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.AddComponent<Sphere>();
        sphere.transform.position = pos.ToVector3();
        sphere.transform.localScale = new Vector3(sphereScale, sphereScale, sphereScale);

        spheres.AddFirst(new LinkedListNode<GameObject>(sphere));
    }

    void DestroySpheres(LinkedList<GameObject> spheres)
    {
        for (LinkedListNode<GameObject> it = spheres.First; it != null; )
        {
            LinkedListNode<GameObject> next = it.Next;

            DestroyImmediate(it.Value.gameObject);

            it = next;
        }
    }
}
