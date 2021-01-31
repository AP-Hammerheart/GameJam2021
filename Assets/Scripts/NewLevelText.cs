using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewLevelText : MonoBehaviour
{
    Text text;
    public void LevelText()
    {
        text.enabled = true;
        var t = FindObjectOfType<GameManager1>();
        string level = t.roundNumber.ToString(); // get level
        text = GetComponent<Text>();
        text.text = "level " + level;
        Invoke("DisableText", 4);
        
        // update text
        // show canvas few seconds
    }

    public void DisableText()
    {
        text.enabled = false;
    }
}
