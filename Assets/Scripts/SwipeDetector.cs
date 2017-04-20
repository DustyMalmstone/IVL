using UnityEngine;
using System.Collections;
using Leap;
using Leap.Unity;
using Leap.Unity.Attributes;

public class SwipeDetector : Detector {


    [Tooltip("The interval in seconds at which to check this detector's conditions.")]
    public float Period = .1f; //seconds

    [AutoFind(AutoFindLocations.Parents)]
    [Tooltip("The hand model to watch. Set automatically if detector is on a hand.")]
    public IHandModel HandModel = null;

    [Tooltip("Reference to a SwipeController to get if hand is swiping or not")]
    public SwipeController controller = null;

    public NewBook book = null;

    private Animator bookAnimator = null;

    private IEnumerator watcherCoroutine;


    private void Awake()
    {
        //bookAnimator = book.gameObject.GetComponent<Animator>();
        watcherCoroutine = swipeWatcher();
        Debug.Log(watcherCoroutine);
    }

    private void OnEnable()
    {
        Debug.Log("Enabled");
        StartCoroutine(watcherCoroutine);
    }

    private void OnDisable()
    {
        StopCoroutine(watcherCoroutine);
        Deactivate();
    }

    private void swipeForward()
    {
        if (controller.foundBook != null)
        {
            Debug.Log("WHAAAAAAAA");
            controller.foundBook.GetComponent<NewBook>().turnForward();
        }
    }

    private void swipeBackward()
    {
        if (controller.foundBook != null)
        {
            Debug.Log("BLARGEN FLOOF");
            controller.foundBook.GetComponent<NewBook>().turnBackward();
        }
    }

    private IEnumerator swipeWatcher()
    {
        while (true)
        {
            if (controller.IsSwipingLeft)
            {
                swipeForward();
            }
            else if (controller.IsSwipingRight) {
                swipeBackward();
            }

            yield return new WaitForSeconds(Period);
        }
    }
}
