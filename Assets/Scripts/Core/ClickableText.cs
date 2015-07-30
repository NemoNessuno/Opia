using UnityEngine;
using System.Collections;
using System;

public class ClickableText : MonoBehaviour {

    [HideInInspector]
    public Action OnClick = () => { /*Do nothing*/ };

    private string _text;
    public string Text {
        set 
        {
            _text = value;
            TypeSettingHelper.SetTextOnMesh(value, GetComponent<TextMesh>(), .4f);
        }

        get
        {
            return _text;
        }
    }

    void OnMouseDown()
    {
        OnClick();
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
