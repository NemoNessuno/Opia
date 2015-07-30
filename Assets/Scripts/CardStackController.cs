using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CardStackController : MonoBehaviour {

    public List<QuestionCardController> Cards;
    public Action<AnswerData> OnAnswerGiven;
    public Action OnStackEmpty;

    private const float CardSpace = 1 / 100f;

    public void InitializeCardStack(List<QuestionData> questions)
    {
        Cards = new List<QuestionCardController>();
        int i = 0;

        foreach (QuestionData questionData in questions)
        {
            var card = GameObjectManager.SpawnCard(
                new Vector3(i * CardSpace, i * CardSpace, i * CardSpace), 
                GameObjectManager.CardType.Question)
                    .GetComponent<QuestionCardController>();
            i++;

            var answers = RandomizationHelper<AnswerData>.ChooseRandomly(questionData.Answers, 4).ToArray();

            card.Texts = new string[5] { questionData.Text, answers[0].Text, answers[1].Text, answers[2].Text, answers[3].Text };
            card.Answers = answers;
            card.OnAnswerGiven += OnAnswerGiven;

            card.OnRemoveAnimationEnded += () => 
            {                
                Cards.Remove(card);
                GameObjectManager.SafeDestroy(card.gameObject);                

                if (Cards.Count < 1)
                {
                    OnStackEmpty();
                }
                else
                {
                    Cards.ForEach((scard) => scard.transform.position += new Vector3(-CardSpace, -CardSpace, -CardSpace));
                    //Cards[0].DisablingController.gameObject.SetActive(true);
                }
            };

            Cards.Add(card);
        }

        //Cards[0].DisablingController.gameObject.SetActive(true);
    }
}