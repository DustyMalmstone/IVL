using UnityEngine;
using System.Collections;

public class BookScript : MonoBehaviour {

    public bool makeItOpen = false;
    public Material highlightMaterial = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void openItCotDammit() {
        Animator animator = GetComponent<Animator>();
        //animator.SetBool("BookOpen", true);
        animator.SetTrigger("BookOpen");
    }

    public void closeItCotDammit()
    {
        Animator animator = GetComponent<Animator>();
        //animator.SetBool("BookOpen", false);
        //animator.SetBool("PageFlip", false);

        animator.SetTrigger("BookClose");
    }

    public void flipPageCotDammit()
    {
        Animator animator = GetComponent<Animator>();
        //animator.SetBool("PageFlip", true);
        animator.SetTrigger("PageFlip");
    }

    public void stopFlipPageCotDammit()
    {
        Animator animator = GetComponent<Animator>();
        //animator.SetBool("FlipBack", true);

        animator.SetTrigger("FlipBack");
    }
    
}
