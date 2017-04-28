using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public BookshelfSpawnController bsc;
    public GameObject keyboard, playerCam;
    public GameObject[] SpawnPoints;
    public int currentSpawn;
    public Transform hidePos;

    private void Start()
    {
        //ShelfFader.hidePos = hidePos;
    }

    private void Update()
    {
        if(playerCam.transform.position != SpawnPoints[currentSpawn].transform.position)
        {
            playerCam.transform.position = SpawnPoints[currentSpawn].transform.position;
        }
    }

    public void ReplaceBook()
    {
        if (BookshelfSpawnController.bookSelected)
        {
            BookshelfSpawnController.currentBook.GetComponent<BookDetector>().isReturn = true;
            SwipeController.foundBook = null;
        }
    }

    public void FadeKeyboard()
    {
        KBManager.fadePass = !KBManager.fadePass;
        Debug.Log("Fadepass set");
    }

    public void FadeShelves()
    {
        BookshelfSpawnController.fadePass = !BookshelfSpawnController.fadePass;
        //ShelfFader.fadePass = !ShelfFader.fadePass;
    }

    public void ResetShelves()
    {
        bsc.DestroyBooks();
    }

    public void NextLocation()
    {
        if(currentSpawn < 3)
        {
            currentSpawn++;
        }
        else
        {
            currentSpawn = 0;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

}
