using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace blarg
{
    [RequireComponent(typeof(Animator))]
    public class NewBook : MonoBehaviour
    {

        private Animator bookAnimator;

        // { Open, Flipped 1, Flipped 2, Flipped 3, Flipped 4 }
        private List<bool> bookStates = new List<bool>()
    {
        false,
        false,
        false,
        false,
        false
    };

        // Use this for initialization
        void Start()
        {
            bookAnimator = gameObject.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("f"))
            {
                turnForward();
            }

            else if (Input.GetKeyDown("b"))
            {
                turnBackward();
            }
        }

        public void turnForward()
        {
            // If there is no true, then the book is closed and we need to open it.
            if (!bookStates.Contains(true))
            {
                bookStates[0] = true;
                bookAnimator.SetTrigger("Open");
            }
            else
            {
                int spotIndex = bookStates.FindIndex(x => x == true);
                // This is ugly, but it will work for now
                switch (spotIndex)
                {
                    case 0:
                        bookStates[0] = false;
                        bookStates[1] = true;
                        bookAnimator.SetTrigger("Flip 1");
                        break;
                    case 1:
                        bookStates[1] = false;
                        bookStates[2] = true;
                        bookAnimator.SetTrigger("Flip 2");
                        break;
                    case 2:
                        bookStates[2] = false;
                        bookStates[3] = true;
                        bookAnimator.SetTrigger("Flip 3");
                        break;
                    case 3:
                        bookStates[3] = false;
                        bookStates[4] = true;
                        bookAnimator.SetTrigger("Flip 4");
                        break;
                    default:
                        break;
                }
            }
        }

        public void turnBackward()
        {
            // if there is a true anywhere, that means the book is open and able to be turned back
            if (bookStates.Contains(true))
            {
                int spotIndex = bookStates.FindIndex(x => x == true);

                switch (spotIndex)
                {
                    case 0:
                        bookStates[0] = false;
                        bookAnimator.SetTrigger("Close");
                        break;
                    case 1:
                        bookStates[1] = false;
                        bookStates[0] = true;
                        bookAnimator.SetTrigger("Back 1");
                        break;
                    case 2:
                        bookStates[2] = false;
                        bookStates[1] = true;
                        bookAnimator.SetTrigger("Back 2");
                        break;
                    case 3:
                        bookStates[3] = false;
                        bookStates[2] = true;
                        bookAnimator.SetTrigger("Back 3");
                        break;
                    case 4:
                        bookStates[4] = false;
                        bookStates[4] = true;
                        bookAnimator.SetTrigger("Back 4");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

