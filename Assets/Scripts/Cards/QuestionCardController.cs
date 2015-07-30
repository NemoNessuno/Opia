using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Text;

public class QuestionCardController : CardController
{
    public TextMesh CardText;
    public ClickableText Answer1;
    public ClickableText Answer2;
    public ClickableText Answer3;
    public ClickableText Answer4;
    public DisablingController DisablingController;
    private Action<int> _onAnswerClicked;

    public Action<AnswerData> OnAnswerGiven;

    [HideInInspector]
    public AnswerData[] Answers;

    private string[] _texts;

    /// <summary>
    /// When set this property automatically updates
    /// the Text property on each Answer.
    /// </summary>
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

    public QuestionCardController()
    {
        OnClick = () =>
        {
            GetComponent<BoxCollider2D>().enabled = false;
            OnClick = PlayRemoveAnimation;
            DisablingController.gameObject.SetActive(true);
        };
    }

    /// <summary>
    /// Activates the onClick events for the Answers
    /// </summary>
    void Start()
    {
        _onAnswerClicked += (answer) => HideOtherAnswersAndActivateCollider(answer);

        DisablingController.OnDisabledEnd += () =>
        {
            GetComponentsInChildren<BoxCollider2D>().ToList().ForEach(collider => collider.enabled = true);

            Answer1.OnClick += () => _onAnswerClicked(1);
            Answer2.OnClick += () => _onAnswerClicked(2);
            Answer3.OnClick += () => _onAnswerClicked(3);
            Answer4.OnClick += () => _onAnswerClicked(4);
        };
    }

    /// <summary>
    /// Does three things:
    /// 1. Hides the other Answers
    /// 2. Activates the Collider for the Card onClick event
    /// 3. Firest OnAnswerGiven to return the answer
    /// </summary>
    /// <param name="answer"></param>
    private void HideOtherAnswersAndActivateCollider(int answer)
    {        
        var answers = new List<ClickableText> { Answer1, Answer2, Answer3, Answer4 };
        answers.ForEach((answerItem) =>
        {
            if (answers.IndexOf(answerItem) != answer-1) answerItem.gameObject.SetActive(false);
        });

        //Fire AnswerGiven event to return the answer Data
         OnAnswerGiven(Answers[answer - 1]);

        //gameObject.AddComponent<BoxCollider2D>();
        //GetComponent<BoxCollider2D>().isTrigger = true;
         GetComponent<BoxCollider2D>().enabled = true;
    }
}