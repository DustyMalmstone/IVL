using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class NewBook : MonoBehaviour {

    public Text FrontText1;
    public Text BackText1;
    public Text FrontText2;
    public Text BackText2;
    public Text FrontText3;
    public Text BackText3;
    public int WordsPerPage;
    public string bookId;
    //public Text[] textures;

    public string bookText = "";

    private Animator bookAnimator;
    private string[] bookWords;
    private List<string[]> pageTexts;
    private int front1Pos;
    private int front2Pos;
    private int front3Pos;
    private int back1Pos;
    private int back2Pos;
    private int back3Pos;

    // { Open, Flipped 1, Flipped 2, Flipped 3, Flipped 4 }
    private List<bool> bookStates = new List<bool>() 
    {
        false, 
        false, 
        false, 
        false
    };

    // { Open, Flipped 1 }
    public List<bool> newBookStates;

	// Use this for initialization
	void Start () {
        bookAnimator = gameObject.GetComponent<Animator>();
        
        front1Pos = 0;
        back1Pos = 1;
        front2Pos = 2;
        back2Pos = 3;
        front3Pos = 4;
        back3Pos = 5;

        newBookStates = new List<bool>() { false, false };



    }

    public void EstablishPages()
    {
        pageTexts = createPageTexts();
        Debug.Log(FrontText1.text);
        FrontText1.text = string.Join(" ", pageTexts[front1Pos]);
        BackText1.text = string.Join(" ", pageTexts[back1Pos]);
        FrontText2.text = string.Join(" ", pageTexts[front2Pos]);
        BackText2.text = string.Join(" ", pageTexts[back2Pos]);
        FrontText3.text = string.Join(" ", pageTexts[front3Pos]);
        //BackText3.text = string.Join(" ", pageTexts[back3Pos]);
    }

    public void EstablishTextures(Text[] textures)
    {
        FrontText1 = textures[0];
        BackText1 = textures[1];
        FrontText2 = textures[2];
        BackText2 = textures[3];
        FrontText3 = textures[4];
        BackText3 = textures[5];
    }

    public bool EstablishText()
    {
        string bookFolder = @"C:\Users\Sarah\Desktop\newDBBooks\";

        using (StreamReader reader = File.OpenText(bookFolder + bookId + ".txt"))
        {
            bookText = reader.ReadToEnd().Trim();
        }
        Debug.Log("Book Text: " + bookText);
        bookWords = bookText.Split(' ');
        if (bookText != "") { return true; }
        else { return false; }
    }

    List<string[]> createPageTexts()
    {
        int numberOfPages = (int)Math.Ceiling((0f + bookWords.Length) / WordsPerPage);

        Debug.Log("Number of Pages: " + numberOfPages);
        List<string[]> pages = new List<string[]>();
        for (int i = 0; i < numberOfPages; i++)
        {
            pages.Add(bookWords.Skip(i * WordsPerPage).Take(WordsPerPage).ToArray());
        }

        return pages;
    }

    IEnumerator GoForward()
    {
        bookAnimator.SetTrigger("Flip 2");
        // Page flip animations last 2 seconds
        yield return new WaitForSeconds(2.0f);

        establishPagesForward();
        //yield return null;
    }

    IEnumerator GoBackward()
    {
        bookAnimator.SetTrigger("ScrollBack");
        yield return new WaitForSeconds(2f);
        establishPageBackward();
        //yield return null;

    }
	
	// Update is called once per frame
	void Update () {
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
        if (!newBookStates.Contains(true))
        {
            newBookStates[0] = true;
            bookAnimator.SetTrigger("Open");
        }
        else
        {
            int spotIndex = newBookStates.FindIndex(x => x == true);
            // This is ugly, but it will work for now
            switch (spotIndex)
            {
                case 0:
                    newBookStates[0] = false;
                    newBookStates[1] = true;
                    bookAnimator.SetTrigger("Flip 1");
                    break;
                case 1:
                    //bookStates[1] = false;
                    //bookStates[2] = true;
                    //bookAnimator.SetTrigger("Flip 2");
                    StartCoroutine("GoForward");
                    break;
                //case 2:
                //case 3:
                //    bookStates[2] = false;
                //    bookStates[3] = true;
                //    StartCoroutine("GoForward");
                //    break;
                default:
                    break;
            }
        }
    }

    public void turnBackward()
    {
        // if there is a true anywhere, that means the book is open and able to be turned back
        if (newBookStates.Contains(true))
        {

            int spotIndex = newBookStates.FindIndex(x => x == true);

            switch(spotIndex)
            {
                case 0:
                    newBookStates[0] = false;
                    bookAnimator.SetTrigger("Close");
                    break;
                case 1:
                    if (front1Pos == 0)
                    {
                        Debug.Log("Front 1 Pos is 0. Flipping to front");
                        newBookStates[1] = false;
                        newBookStates[0] = true;
                        bookAnimator.SetTrigger("Back 1");
                    }
                    else
                    {
                        Debug.Log("Front 1 Pos not yet 0. Reestablishing Backward");
                        StartCoroutine("GoBackward");
                    }
                    break;
                //case 2:
                //    bookStates[2] = false;
                //    bookStates[1] = true;
                //    bookAnimator.SetTrigger("Back 2");
                //    break;
                //case 3:
                //    bookStates[3] = false;
                //    bookStates[1] = true;
                //    bookAnimator.SetTrigger("Back 2");
                //    break;
                default:
                    break;
            }
        }
    }

    void establishPagesForward()
    {
        Debug.Log("Page Count: " + pageTexts.Count);
        if (front3Pos < pageTexts.Count)
        {
            Debug.Log("Front 1 Position: " + front1Pos);
            Debug.Log("Back 1 Position: " + back1Pos);
            Debug.Log("Front 2 Position: " + front2Pos);
            Debug.Log("Back 2 Position: " + back2Pos);
            Debug.Log("Front 3 Position: " + front3Pos);
            front1Pos = front2Pos;
            back1Pos = back2Pos;
            front2Pos = front3Pos;
            FrontText1.text = string.Join(" ", pageTexts[front1Pos]);
            BackText1.text = string.Join(" ", pageTexts[back1Pos]);
            FrontText2.text = string.Join(" ", pageTexts[front2Pos]);
            
            if (back2Pos + 2 >= pageTexts.Count) { BackText2.text = ""; }
            else {
                back2Pos += 2;
                BackText2.text = string.Join(" ", pageTexts[back2Pos]);
            }

            if (front3Pos + 2 >= pageTexts.Count) { FrontText3.text = ""; }
            else {
                front3Pos += 2;
                FrontText3.text = string.Join(" ", pageTexts[front3Pos]);
            }

            Debug.Log("New Front 1 Position: " + front1Pos);
            Debug.Log("New Back 1 Position: " + back1Pos);
            Debug.Log("New Front 2 Position: " + front2Pos);
            Debug.Log("New Back 2 Position: " + back2Pos);
            Debug.Log("New Front 3 Position: " + front3Pos);
        }
    }

    void establishPageBackward()
    {

        if (front1Pos - 2 >= 0)
        {
            front3Pos = front2Pos;
            front2Pos = front1Pos;
            back2Pos = back1Pos;
            front1Pos -= 2;
            back1Pos -= 2;
            FrontText1.text = string.Join(" ", pageTexts[front1Pos]);
            BackText1.text = string.Join(" ", pageTexts[back1Pos]);
            FrontText2.text = string.Join(" ", pageTexts[front2Pos]);
            BackText2.text = string.Join(" ", pageTexts[back2Pos]);
            FrontText3.text = string.Join(" ", pageTexts[front3Pos]);

        }
        
    }
}
