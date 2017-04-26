using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public BookshelfSpawnController bsc;
    public GameObject keyboard, playerCam;
    public GameObject[] SpawnPoints;
    public int currentSpawn;

    private void Update()
    {
        if(playerCam.transform.position != SpawnPoints[currentSpawn].transform.position)
        {
            playerCam.transform.position = SpawnPoints[currentSpawn].transform.position;
        }
    }

    public void replaceBook()
    {
        if (BookshelfSpawnController.bookSelected)
        {
            BookDetector.isReturn = true;
        }
    }

    public void FadeKeyboard()
    {
        KBFader.fadePass = true;
        Debug.Log("Fadepass set");
    }

    public void FadeShelves()
    {
        ShelfFader.fadePass = true;
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
