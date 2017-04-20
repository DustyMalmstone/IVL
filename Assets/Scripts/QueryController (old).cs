using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System;
using UnityEngine.EventSystems;

public class QueryController : MonoBehaviour {

    public int queryNum;
    public BookshelfSpawnController spawner;
    public char[] splitters;

    string[] queries;
	
	//void Update ()
    //{
    //    if (Input.GetKeyDown(KeyCode.I))
    //    {
    //        string fullQuery = "1 John %";
    //        runQuery(fullQuery);
    //    }
    //}

    //Method written by StackOverflow user 'Master Morality'
    private void run_cmd(string cmd, string args)
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "C:/Program Files/Python34/python.exe";
        start.Arguments = string.Format("{0} {1}", cmd, args);
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                Console.Write(result);
            }
        }
    }

    //public void runQuery(string query)
    //{
    //    string cmd = "C:/Users/Sarah/Desktop/dbtransfer/ether_meta_script.py";
    //    run_cmd(cmd, query);
    //    spawner.SpawnBooks("C:/Users/Sarah/Desktop/outputfile.txt", splitters);
    //}
}
