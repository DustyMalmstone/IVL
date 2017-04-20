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

    public void Login(string text)
    {
        StartCoroutine(loginRoutine(text));
    }

    IEnumerator loginRoutine(string text)
    {
        //string uname = Username.text;
        //string pass = Password.text;

        WWWForm form = new WWWForm();
        //form.AddField("uname", uname);
        //form.AddField("pass", pass);
        form.AddField("author", text);
        //form.AddField("min_char_count", 0);
        //form.AddField("max_char_count", 60000);

        WWW w = new WWW(URL, form);

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
