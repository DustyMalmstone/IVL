#define DEBUG

using UnityEngine;
using System.Collections;
using System.Linq;
using Leap;
using Leap.Unity.Attributes;
using Leap.Unity;

public class CustomDirectionDetector : Detector {

    [Tooltip("The interval in seconds at which to check this detector's conditions.")]
    public float Period = .1f; //seconds

    [AutoFind(AutoFindLocations.Parents)]
    [Tooltip("The hand model to watch. Set automatically if detector is on a hand.")]
    public IHandModel HandModel = null;

    [Tooltip("The finger to observe.")]
    public Finger.FingerType FingerName = Finger.FingerType.TYPE_INDEX;

    [Tooltip("The target direction.")]
    public Vector3 PointingDirection = Vector3.forward;

    [Tooltip("How to treat the target direction.")]
    public PointingType PointingType = PointingType.RelativeToHorizon;

    [Tooltip("The angle in degrees from the target direction at which to turn on.")]
    [Range(0, 360)]
    public float OnAngle = 15f; //degrees

    [Tooltip("The angle in degrees from the target direction at which to turn off.")]
    [Range(0, 360)]
    public float OffAngle = 25f; //degrees

    public SwipeController controller;

    private IEnumerator watcherCoroutine;
    //private Transform TargetObject;

    private void OnValidate()
    {
        if (OffAngle < OnAngle)
        {
            OffAngle = OnAngle;
        }
    }

    private void Awake()
    {
        watcherCoroutine = fingerPointingWatcher();
    }

    private void OnEnable()
    {
        StartCoroutine(watcherCoroutine);
    }

    private void OnDisable()
    {
        StopCoroutine(watcherCoroutine);
        Deactivate();
    }

    private IEnumerator fingerPointingWatcher()
    {
        Hand hand;
        Vector3 fingerStart;
        Vector3 fingerDirection;
        Vector3 forward;
        int selectedFinger = selectedFingerOrdinal();
        while (true)
        {
            if (HandModel != null)
            {
                hand = HandModel.GetLeapHand();
                if (hand != null)
                {
                    fingerStart = hand.Fingers[selectedFinger].Bone(Bone.BoneType.TYPE_DISTAL).Center.ToVector3();
                    fingerDirection = hand.Fingers[selectedFinger].Bone(Bone.BoneType.TYPE_DISTAL).Direction.ToVector3();

                    //Finds delta direction, adds that to the start direction, and multiplies by 1/4
                    // So that it doesn't go out to far. Use deltas to change as the finger direction changes
                    forward = new Vector3(.5F * (fingerStart.x + (fingerDirection.x - fingerStart.x)),
                        .5F * (fingerStart.y + (fingerDirection.y - fingerStart.y)),
                        .5F * (fingerStart.z + (fingerDirection.z - fingerStart.z)));

#if DEBUG
                    Debug.DrawRay(fingerStart, forward, Color.blue);
#endif
                    RaycastHit hit;
                    Transform hitObject = null;
                    if (hand.Fingers[selectedFinger].IsExtended && HandModel.IsTracked && Physics.Raycast(fingerStart, forward, out hit))
                    {
                        if (hit.transform.gameObject.tag == "Book" )
                        {
#if DEBUG
                            //Debug.Log("Found book");
#endif
                            hitObject = hit.transform;
                            Activate();

                            controller.foundBook = hitObject.gameObject;
                            hitObject.gameObject.GetComponent<BookDetector>().Move();
                        }
                    }

                    else
                    {
#if DEBUG
                        //Debug.Log("Stopped finding book");
#endif
                        Deactivate();

                        if (hitObject != null)
                         hitObject.GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
                    }
                }
            }

            yield return new WaitForSeconds(Period);
        }
    }

    /*private Vector3 selectedDirection(Vector3 tipPosition)
    {
        switch (PointingType)
        {
            case PointingType.RelativeToHorizon:
                Quaternion cameraRot = Camera.main.transform.rotation;
                float cameraYaw = cameraRot.eulerAngles.y;
                Quaternion rotator = Quaternion.AngleAxis(cameraYaw, Vector3.up);
                return rotator * PointingDirection;
            case PointingType.RelativeToCamera:
                return Camera.main.transform.TransformDirection(PointingDirection);
            case PointingType.RelativeToWorld:
                return PointingDirection;
            case PointingType.AtTarget:
                return TargetObject.position - tipPosition;
            default:
                return PointingDirection;
        }
    }*/

        private int selectedFingerOrdinal(){
      switch(FingerName){
        case Finger.FingerType.TYPE_INDEX:
          return 1;
        case Finger.FingerType.TYPE_MIDDLE:
          return 2;
        case Finger.FingerType.TYPE_PINKY:
          return 4;
        case Finger.FingerType.TYPE_RING:
          return 3;
        case Finger.FingerType.TYPE_THUMB:
          return 0;
        default:
          return 1;
      }
    }

  #if UNITY_EDITOR
    /*private void OnDrawGizmos () {
      if (ShowGizmos && HandModel != null) {
        Color innerColor;
        if (IsActive) {
          innerColor = Color.green;
        } else {
          innerColor = Color.blue;
        }
        Finger finger = HandModel.GetLeapHand().Fingers[selectedFingerOrdinal()];
        Vector3 fingerDirection = finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.ToVector3();
        Utils.DrawCone(finger.TipPosition.ToVector3(), fingerDirection, OnAngle, finger.Length, innerColor);
        Utils.DrawCone(finger.TipPosition.ToVector3(), fingerDirection, OffAngle, finger.Length, Color.red);
        Debug.DrawRay(finger.TipPosition.ToVector3(), selectedDirection(finger.TipPosition.ToVector3()), Color.grey);
      }
    }*/
  #endif
}
