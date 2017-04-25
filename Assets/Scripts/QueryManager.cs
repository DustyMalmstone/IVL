using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QueryManager : MonoBehaviour {

    //public InputField Username;
    //public InputField Password;
    //public InputField ReturnMessage;
    public BookshelfSpawnController bsc;

    private string URL = "http://localhost:8080/ServerManager.php";

    public void Login(string titleSearch, string authorSearch, string keywordSearch)
    {
        Debug.Log("Started Login");
        StartCoroutine(loginRoutine(titleSearch, authorSearch, keywordSearch));
    }

    IEnumerator loginRoutine(string titleSearch, string authorSearch, string keywordSearch)
    {
        //Debug.Log("Started Coroutine: " + text);
        //string uname = Username.text;
        //string pass = Password.text;

        WWWForm form = new WWWForm();
        //Debug.Log("created form");
        form.AddField("title", titleSearch);
        form.AddField("author", authorSearch);
        form.AddField("keyword", keywordSearch);

        WWW w = new WWW(URL, form);
        //Debug.Log("Ran search");

        yield return w;


        if (w.error != null)
        {
            Debug.Log(w.error);
        }
        else
        {
            string[] splits = w.text.Split(new string[] {"\r\n"}, StringSplitOptions.None);

            //ReturnMessage.text = w.text;
            bsc.SpawnBooks(splits, new char[] { '|' });
            foreach (string thing in splits)
            {
                Debug.Log(thing);
                //ReturnMessage.text += thing + "\r\n";
            }
        }

        //Username.text = "";
        //Password.text = "";
    }
}
