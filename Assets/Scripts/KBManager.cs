using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KBManager : MonoBehaviour {

    public Text searchText;
    public bool caps, lastCaps;
    public GameObject keysPanel;
    public GameObject[] keys;
    public QueryManager qm;

    /*
    private void Start()
    {
        keys = new GameObject[keysPanel.transform.childCount];
        int i = 0;
        foreach(Transform child in keysPanel.transform)
        {
            keys[i] = child.gameObject;
            Debug.Log(keys[i].GetComponent<Key>().label);
            i++;
        }
    }
    */

    private void LateUpdate()
    {
        if(caps != lastCaps)
        {
            if (caps)
            {
                foreach(GameObject key in keys)
                {
                    Key current = key.GetComponent<Key>();
                    current.label.text = current.altKey;
                }
            }
            else
            {
                foreach (GameObject key in keys)
                {
                    Key current = key.GetComponent<Key>();
                    current.label.text = current.key;
                }
            }
            lastCaps = caps;
        }
    }

    public void AddChar(Key key)
    {

        if (caps)
        {
            searchText.text += key.altKey;
        }
        else
        {
            searchText.text += key.key;
        }

    }

    public void ToggleCaps()
    {
        caps = !caps;
    }

    public void Backspace()
    {
        if(searchText.text != "")
        {
            searchText.text = searchText.text.Substring(0, searchText.text.Length - 1);
        }
    }

    public void Enter()
    {
        /*qc.runQuery(searchText.text);*/
        qm.Login(searchText.text);
    }

}
