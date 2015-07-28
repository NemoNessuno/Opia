using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CardStackController : MonoBehaviour {

    public List<QuestionCardController> Cards;
    public Action<int> OnAnswerGiven;

    private const float CardSpace = 1 / 100f;

    public void InitializeCardStack(List<string[]> cardTexts)
    {
        Cards = new List<QuestionCardController>();
        int i = 0;

        foreach(string[] cardText in cardTexts)
        {
            var card = GameObjectManager.SpawnCard(new Vector3(i * CardSpace, i * CardSpace, i * CardSpace), GameObjectManager.CardType.Question);
            i++;

            var answers = RandomizationHelper<string>.ChooseRandomly(new List<string>() { cardText[1], cardText[2], cardText[3], cardText[4] }, 4);

            card.Texts = new string[5] { cardText[0], answers[0], answers[1], answers[2], answers[3] };
            card.OnAnswerGiven += OnAnswerGiven;

            card.OnRemoveAnimationEnded += () => 
            {                
                Cards.Remove(card);
                GameObjectManager.SafeDestroy(card.gameObject);
                Cards.ForEach((scard) => scard.transform.position += new Vector3(-CardSpace, -CardSpace, -CardSpace));
                Cards[0].DisablingController.gameObject.SetActive(true);
            };

            Cards.Add(card);
        }

        Cards[0].DisablingController.gameObject.SetActive(true);
    }
}