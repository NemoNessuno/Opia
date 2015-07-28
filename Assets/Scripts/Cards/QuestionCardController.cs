using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

public class QuestionCardController : CardController
{
    public TextMesh CardText;
    public OnClickBehaviour Answer1;
    public OnClickBehaviour Answer2;
    public OnClickBehaviour Answer3;
    public OnClickBehaviour Answer4;
    public DisablingController DisablingController;
    public Action<int> OnAnswerGiven;
   
    private string[] _texts;
    public string[] Texts
    {
        get { return _texts; }
        set
        {
            if (value == null || value.Length < 5)
            {
                _texts = new string[5];
            }
            else
            {
                _texts = value;
                TypeSettingHelper.SetTextOnMesh(value[0], CardText, .94f);
                Answer1.Text = value[1];
                Answer2.Text = value[2];
                Answer3.Text = value[3];
                Answer4.Text = value[4];
            }
        }
    }

    void Start()
    {
        OnAnswerGiven += (answer) => HideOtherAnswersAndActivateCollider(answer);

        DisablingController.OnDisabledEnd += () =>
        {
            Answer1.OnClick += () => OnAnswerGiven(1);
            Answer2.OnClick += () => OnAnswerGiven(2);
            Answer3.OnClick += () => OnAnswerGiven(3);
            Answer4.OnClick += () => OnAnswerGiven(4);
        };
    }

    private void HideOtherAnswersAndActivateCollider(int answer)
    {        
        var answers = new List<OnClickBehaviour> { Answer1, Answer2, Answer3, Answer4 };
        answers.ForEach((answerItem) =>
        {
            if (answers.IndexOf(answerItem) != answer-1) answerItem.gameObject.SetActive(false);
        });

        gameObject.AddComponent<BoxCollider2D>();
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
}