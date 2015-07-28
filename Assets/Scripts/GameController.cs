using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.RepresentationModel;
using System.Linq;

public class GameController : MonoBehaviour {

    public int AmountOfCards;
    public CardController OptionsCardController;
    public MenuCardController MenuCardController;
    public CardStackController CardStackController;

    private List<int> _answers = new List<int>();
    private bool _initialized = false;
    private List<string[]> _questions = new List<string[]>();
    private List<string[]> Questions
    {
        get
        {
            if (!_initialized)
            {
                var cardTexts = new List<string[]>();
                var reader = new StreamReader(Application.dataPath + "/questions.yaml", Encoding.Default);
                var yaml = new YamlStream();
                yaml.Load(reader);

                var questions = ((YamlMappingNode)yaml.Documents[0].RootNode).Children[new YamlScalarNode("questions")];

                foreach (var child in ((YamlSequenceNode)questions).Children)
                {
                    var question = (YamlMappingNode)child;
                    var questionstrings = new string[5];
                    questionstrings[0] = question.Children[new YamlScalarNode("text")].ToString();
                    questionstrings[1] = question.Children[new YamlScalarNode("answer1")].ToString();
                    questionstrings[2] = question.Children[new YamlScalarNode("answer2")].ToString();
                    questionstrings[3] = question.Children[new YamlScalarNode("answer3")].ToString();
                    questionstrings[4] = question.Children[new YamlScalarNode("answer4")].ToString();

                    cardTexts.Add(questionstrings);
                }

                _questions = RandomizationHelper<string[]>.ChooseRandomly(cardTexts, AmountOfCards);
                _initialized = true;
            }
            return _questions;
        }
    }

    void Start()
    {
        CardStackController.OnAnswerGiven += (answer) => _answers.Add(answer);
        CardStackController.InitializeCardStack(Questions);
        //CreateMenuCard();
    }

    private void CreateMenuCard()
    {

        if (MenuCardController == null)
        {
            var menuCard = GameObjectManager.SpawnCard(Vector3.zero, GameObjectManager.CardType.Menu);
            MenuCardController = menuCard.GetComponent<MenuCardController>();
        }

        MenuCardController.OnStartClicked += () =>
        {
            MenuCardController.OnRemoveAnimationEnded += () =>
            {
                CardStackController.InitializeCardStack(Questions);
                GameObjectManager.SafeDestroy(MenuCardController.gameObject);
            };

            MenuCardController.PlayRemoveAnimation();
        };
    }
}
