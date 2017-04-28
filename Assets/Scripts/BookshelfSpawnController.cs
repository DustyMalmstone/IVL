#define ERROR

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BookshelfSpawnController : MonoBehaviour {

    //public objects
    public GameObject bookshelf, bookModel, //prefab references for bookshelf and book
        spawnCore; //references to a central bookshelf spawn reference and the top left of a bookshelf
    public Transform[] spawnPoints; //array of spawnpoints
    public Text[] Textures;
    public int WordsPerPage;
    public string BookText;
    public float shelfY, shelfX, shelfZ; //distance between shelves and distances between books on the same shelf, respectively
    public int maxLength = 10;
    public static bool bookSelected; //boolean to determine whether a book is currently off the shelf
    public Transform defaultReadSpot; //transform of the default location a book will lerp to after being selected
    public static Transform defReadSpot; //static transform of the default location a book will lerp to after being selected (kill me please)
    public Transform hideSpot;
    public static bool fadePass = false;
    public static GameObject currentBook, currentBookShelfSpot;
    public GameObject shelfSpot;

    //private objects
    GameObject[] bookshelves; //array of bookshelves
    GameObject[,,] bookModels; //first index is bookshelf, second index is shelf on bookshelf from top, third index is order from left on shelf
    Quaternion bookshelfRotation; //Quaternion to keep the bookshelf rotation consistent
    float bookWidth = 2f; //maximum increase in Z scale to make books appear larger
    int shelfNum = 4, bookNum = 10; //number of shelves on each bookshelf and number of books on each shelf
    Transform bookSpawnPos; //position of the book reference point
    Color[] colors = { Color.red, Color.blue, Color.green, Color.black, Color.cyan, Color.grey };


    string[] bookInfo = new string[5]; //Index 0 == Book ID, 1 == Title, 2 == Author, 3 == Genre, 4 == Length

    void Start()
    {
        currentBookShelfSpot = shelfSpot;
        bookshelfRotation = spawnCore.transform.rotation;
        bookshelves = new GameObject[spawnPoints.Length];
        bookModels = new GameObject[bookshelves.Length, shelfNum, bookNum];
        SpawnShelves();
        //SpawnBooks();
        bookSelected = false;
        defReadSpot = defaultReadSpot;
    }

    private void Update()
    {
        if (fadePass)
        {
            for(int i = 0; i < bookshelves.Length; i++)
            {
                bookshelves[i].GetComponent<ShelfFader>().faded = !bookshelves[i].GetComponent<ShelfFader>().faded;
            }
            for(int i = 0; i < bookshelves.Length; i++)
            {
                if (bookshelves[i].GetComponent<ShelfFader>().faded)
                {
                    bookshelves[i].transform.position = new Vector3(bookshelves[i].transform.position.x, bookshelves[i].transform.position.y - 1000, bookshelves[i].transform.position.z);
                    if (currentBook != null)
                    {
                        currentBook.transform.position = new Vector3(currentBook.transform.position.x, currentBook.transform.position.y + 250, currentBook.transform.position.z);
                    }
                }
                else
                {
                    bookshelves[i].transform.position = new Vector3(bookshelves[i].transform.position.x, bookshelves[i].transform.position.y + 1000, bookshelves[i].transform.position.z);
                    if (currentBook != null)
                    {
                        currentBook.transform.position = new Vector3(currentBook.transform.position.x, currentBook.transform.position.y - 250, currentBook.transform.position.z);
                    }
                }
            }
            fadePass = false;
        }
    }

    void LateUpdate()
    {
        if (spawnCore.transform.rotation != bookshelfRotation)
        {
            spawnCore.transform.rotation = bookshelfRotation;
        }
    }

    void SpawnShelves()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            bookshelves[i] = (GameObject)Instantiate(bookshelf, spawnCore.transform.position, spawnCore.transform.rotation);
            bookshelves[i].transform.parent = spawnCore.transform;

            bookSpawnPos = bookshelves[i].transform.GetChild(0);

            bookshelves[i].transform.position = spawnPoints[i].transform.position;
            bookshelves[i].transform.rotation = spawnPoints[i].transform.rotation;
        }
    }

    public void SpawnBooks(string[] bookSet, char[] splitters)
    {
        //String used to store each book, represented by each line of the text file
        string nextBook;

        //If there are books on the shelves, get rid of them
        DestroyBooks();

        //Create file object & skip first line
        //System.IO.StreamReader qResult = new System.IO.StreamReader(filePath);
        //qResult.ReadLine();

        int bookIndex = 0;
        int posSwitcher = 1;

        //Loop for each bookshelf
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            bookSpawnPos = bookshelves[i].transform.GetChild(0);
            if (i % 3 != 0)
            {
                posSwitcher = -1;
            }
            else
            {
                posSwitcher = 1;
            }
            //Loop for each shelf
            for (int j = 0; j < shelfNum; j++)
            {
                //Loop for each position on the shelf
                for (int k = 0; k < bookNum; k++)
                {
                    //If the end of the array has not been reached
                    if(bookIndex < bookSet.Length)
                    {
                        Debug.Log(bookIndex + " SET LENGTH: " + bookSet.Length);
                        //Split the current line to represent book metadata and retrieve its title
                        bookInfo = bookSet[bookIndex].Split(splitters);

                        //ERROR TEST
#if ERROR
                        if (bookInfo[0] == "" || bookInfo[0] == null)
                        {
                            continue;
                        }
                        string bookTitle = null;
                        string bookId = "";
                        try
                        {
                            bookTitle = bookInfo[1];
                            bookId = bookInfo[0];
                            Debug.Log(bookTitle);
                        }
                        catch (System.IndexOutOfRangeException) {
                            foreach(string SHUTUP in bookInfo)
                            {
                                Debug.Log(SHUTUP);
                            }
                        }
                        //END ERROR TEST
#endif

                        //Build the book
                        bookModels[i, j, k] = (GameObject)Instantiate(bookModel, bookSpawnPos.position, bookSpawnPos.rotation);
                        bookModels[i, j, k].GetComponent<NewBook>().bookId = bookId;
                        bookModels[i, j, k].GetComponent<BookDetector>().defReadSpot = defReadSpot;
                        bookModels[i, j, k].GetComponent<NewBook>().WordsPerPage = WordsPerPage;
                        bookModels[i, j, k].GetComponent<NewBook>().EstablishTextures(Textures);
                        bookModels[i, j, k].transform.parent = bookshelves[i].transform;
                        //bookModels[i, j, k].GetComponent<BookDetector>().bsc = this;
                        //bookModels[i, j, k].GetComponent<Book>().NumberOfPages = (int)System.Math.Ceiling(double.Parse(bookInfo[4]) / 1000);

                        //Moving in the Y direction will always be the same, but the book will move differently in the X and Z directions depending on which bookshelf it is on
                        float ypos = bookModels[i, j, k].transform.position.y - (shelfY * (j + 1)) / 4;
                        float xpos = 0f;
                        float zpos = 0f;

                        //If we are on an even-numbered shelf, move the books in the X direction
                        if(i % 2 == 0)
                        {
                            xpos = bookModels[i, j, k].transform.position.x + (shelfX * k * posSwitcher) / 4;
                            zpos = bookModels[i, j, k].transform.position.z + (shelfZ * k) / 4;
                        }
                        //Otherwise, we are on an odd-numbered shelf, so we move the books in the Z direction
                        else
                        {
                            xpos = bookModels[i, j, k].transform.position.x + (shelfX * k) / 4;
                            zpos = bookModels[i, j, k].transform.position.z + (shelfZ * k * posSwitcher) / 4;
                        }

                        //Set the final position and scale of the book and give it a random and color
                        bookModels[i, j, k].transform.position = new Vector3(xpos, ypos, zpos);
                        bookModels[i, j, k].transform.localScale = new Vector3(.2f, .2f, .2f);
                        bookModels[i, j, k].transform.GetChild(1).GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];

                        //bookModels[i, j, k].transform.localScale = new Vector3(1f, 1f, Mathf.Ceil(int.Parse(bookInfo[4]) / 1000));

                        //Replace the default cover and spine text with the retrieved title, cut to manageable size
                        if (bookTitle.Length > maxLength)
                        {
                            string smallTitle = bookTitle.Substring(0, maxLength);
                            //bookModels[i, j, k].transform.GetChild(0).GetComponent<TextMesh>().text = smallTitle;
                            //bookModels[i, j, k].transform.GetChild(1).GetComponent<TextMesh>().text = smallTitle;
                        }
                        else
                        {
                            //bookModels[i, j, k].transform.GetChild(0).GetComponent<TextMesh>().text = bookTitle;
                            //bookModels[i, j, k].transform.GetChild(1).GetComponent<TextMesh>().text = bookTitle;
                        }
                        bookIndex++;
                    }
                }
            }
            //Switch the Z and X movement values for the next shelf
            float zTemp = shelfZ;
            shelfZ = shelfX;
            shelfX = zTemp;

            bookshelves[i].GetComponent<ShelfFader>().ApplyToChildren();
        }
        bookIndex = 0;
    }

    private string getBookText() { return ""; }

    public void DestroyBooks()
    {
        //If there are already books on the shelves, get rid of them
        if (bookModels[0, 0, 0] != null)
        {
            //Loop for each bookshelf
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                //Loop for each shelf
                for (int j = 0; j < shelfNum; j++)
                {
                    //Loop for each position on the shelf
                    for (int k = 0; k < bookNum; k++)
                    {
                        Destroy(bookModels[i, j, k]);
                    }
                }
            }
        }
    }

    //public void SpawnBooksFull()
    //{
    //    for (int i = 0; i < spawnPoints.Length; i++)
    //    {
    //        for (int j = 0; j < shelfNum; j++)
    //        {
    //            //float offsetY = (shelfY * j) + 1;
    //            for (int k = 0; k < bookNum; k++)
    //            {
    //                //float offsetX = shelfX * k;
    //                //float offsetZ = shelfZ * k;
    //                //Vector3 finalPos = new Vector3(offsetX, offsetY, offsetZ);
    //                bookModels[i, j, k] = (GameObject)Instantiate(bookModel, bookSpawnPos.position, bookSpawnPos.rotation);
    //                bookModels[i, j, k].transform.parent = bookshelves[i].transform;
    //                float ypos = bookModels[i, j, k].transform.position.y - (shelfY * (j + 1)) / 4;
    //                float xpos = bookModels[i, j, k].transform.position.x + (shelfX * k) / 4;
    //                float zpos = bookModels[i, j, k].transform.position.z + (shelfZ * k) / 4;
    //                bookModels[i, j, k].transform.position = new Vector3(xpos, ypos, zpos);
    //                bookModels[i, j, k].transform.localScale = new Vector3(1f, 1f, Random.Range(1f, 2f));
    //                bookModels[i, j, k].transform.GetChild(0).GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];
    //            }
    //        }
    //    }
    //}

}