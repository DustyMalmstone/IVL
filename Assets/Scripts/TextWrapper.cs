using UnityEngine;
using System.Collections;

public class TextWrapper : MonoBehaviour {


    public TextMesh textObject;
 
    int maxLineChars = 35; //maximum number of characters per line...experiment with different values to make it work
 
 
    string[] words;
    string result = "";
    int charCount;
 
    void Start()
    {
        updateText();   //call this function to format your string.
    }

    void updateText()
    {
        FormatString(textObject.text);
    }

    void FormatString(string text )
    {

        int currentLine = 1;
        charCount = 0;
        words = text.Split(" "[0]); //Split the string into seperate words
        result = "";

        for (var index = 0; index < words.Length; index++)
        {

            var word = words[index].Trim();

            if (index == 0)
            {
                result = words[0];
                textObject.text = result;
            }

            if (index > 0)
            {
                charCount += word.Length + 1; //+1, because we assume, that there will be a space after every word
                if (charCount <= maxLineChars)
                {
                    result += " " + word;
                }
                else
                {
                    charCount = 0;
                    result += "\n " + word;
                }


                textObject.text = result;
            }
        }
    }

}
