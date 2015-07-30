using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

public class MenuCardController : CardController
{
    public ClickableText StartMenuPoint;
    public ClickableText OptionsMenuPoint;

    public Action OnStartClicked;
    public Action OnOptionsClicked;
    
    void Start()
    {
        StartMenuPoint.OnClick += OnStartClicked;
        OptionsMenuPoint.OnClick += OnOptionsClicked;
    }
}