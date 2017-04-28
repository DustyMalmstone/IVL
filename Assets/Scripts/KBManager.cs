using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KBManager : MonoBehaviour {

    public InputField titleSearch, authorSearch, keywordSearch;
    public bool caps, lastCaps;
    public GameObject keysPanel;
    public GameObject[] keys;
    public QueryManager qm;
    public Toggle capsKey;
    public GameObject[] fadeVictims;
    public Transform hideSpot;
    public static bool fadePass = false;

    char selectedSearch;

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

    private void Update()
    {
        if (titleSearch.isFocused && selectedSearch != 't')
        {
            selectedSearch = 't';
        }
        if (authorSearch.isFocused && selectedSearch != 'a')
        {
            selectedSearch = 'a';
        }
        if (keywordSearch.isFocused && selectedSearch != 'k')
        {
            selectedSearch = 'k';
        }

        if (fadePass)
        {
            for (int i = 0; i < fadeVictims.Length; i++)
            {
                fadeVictims[i].GetComponent<KBFader>().faded = !fadeVictims[i].GetComponent<KBFader>().faded;
            }
            for (int i = 0; i < fadeVictims.Length; i++)
            {
                if (fadeVictims[i].GetComponent<KBFader>().faded)
                {
                    fadeVictims[i].transform.position = new Vector3(fadeVictims[i].transform.position.x, fadeVictims[i].transform.position.y - 1000, fadeVictims[i].transform.position.z);
                }
                else
                {
                    fadeVictims[i].transform.position = new Vector3(fadeVictims[i].transform.position.x, fadeVictims[i].transform.position.y + 1000, fadeVictims[i].transform.position.z);
                }
            }
            fadePass = false;
        }
    }

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
                    capsKey.transform.GetChild(0).GetComponentInChildren<Text>().text = "Caps Lock";
                }
            }
            else
            {
                foreach (GameObject key in keys)
                {
                    Key current = key.GetComponent<Key>();
                    current.label.text = current.key;
                    capsKey.transform.GetChild(0).GetComponentInChildren<Text>().text = "Caps Lock";
                }
            }
            lastCaps = caps;
        }
    }

    public void AddChar(Key key)
    {
        if (selectedSearch == 't')
        {
            if (caps)
            {
                titleSearch.text += key.altKey;
            }
            else
            {
                titleSearch.text += key.key;
            }
        }
        else if (selectedSearch == 'a')
        {
            if (caps)
            {
                authorSearch.text += key.altKey;
            }
            else
            {
                authorSearch.text += key.key;
            }
        }
        else if (selectedSearch == 'k')
        {
            if (caps)
            {
                keywordSearch.text += key.altKey;
            }
            else
            {
                keywordSearch.text += key.key;
            }
        }


    }

    public void ToggleCaps()
    {
        caps = !caps;
    }

    public void Backspace()
    {
        if (selectedSearch == 't')
        {
            if (titleSearch.text != "")
            {
                titleSearch.text = titleSearch.text.Substring(0, titleSearch.text.Length - 1);
            }
        }
        if (selectedSearch == 'a')
        {
            if (authorSearch.text != "")
            {
                authorSearch.text = authorSearch.text.Substring(0, authorSearch.text.Length - 1);
            }
        }
        if (selectedSearch == 'k')
        {
            if (keywordSearch.text != "")
            {
                keywordSearch.text = keywordSearch.text.Substring(0, keywordSearch.text.Length - 1);
            }
        }

    }

    public void Enter()
    {
        /*qc.runQuery(searchText.text);*/
        Debug.Log("Pressed Enter");
        qm.Login(titleSearch.text, authorSearch.text, keywordSearch.text);
    }

}
