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

    private List<AnswerData> _answers = new List<AnswerData>();
    private bool _initialized = false;

    private List<QuestionData> _questions = new List<QuestionData>();
    private List<QuestionData> Questions
    {
        get
        {
            if (!_initialized)
            {
                var questionList = new List<QuestionData>();
                var reader = new StreamReader(Application.dataPath + "/questions.yaml", Encoding.Default);
                var yaml = new YamlStream();                
                yaml.Load(reader);
                var root = YAMLHelper.GetRootNode(yaml);

                var questions = YAMLHelper.GetYAMLNode(root, "questions");

                foreach (var child in ((YamlSequenceNode)questions).Children)
                {
                    var question = (YamlMappingNode)child;

                    var qtext = YAMLHelper.GetStringData(question, "text");
                    var answer1 = YAMLHelper.GetAnswer(question, "answer1");
                    var answer2 = YAMLHelper.GetAnswer(question, "answer2");
                    var answer3 = YAMLHelper.GetAnswer(question, "answer3");
                    var answer4 = YAMLHelper.GetAnswer(question, "answer4");

                    questionList.Add(new QuestionData(qtext, answer1, answer2, answer3, answer4));
                }

                _questions = RandomizationHelper<QuestionData>.ChooseRandomly(questionList, AmountOfCards);
                _initialized = true;
            }

            return _questions;
        }
    }

    void Start()
    {
        CardStackController.OnAnswerGiven += (answer) => {

            //This event gets fired more than once sometimes
            if (!_answers.Contains(answer)) _answers.Add(answer);
        };

        CardStackController.OnStackEmpty = Evaluate;

        CreateMenuCard();
    }

    private void Evaluate()
    {
        var physicalIntimacy = 0f;
        var emotionalInitimacy = 0f;
        var physicalAttraction = 0f;
        var buddy = 0f;
        var intellectualAttraction = 0f;

        _answers.ForEach(fanswer =>
        {
            physicalIntimacy += fanswer.PhysicalIntimacy;
            emotionalInitimacy += fanswer.EmotionalInitimacy;
            physicalAttraction += fanswer.PhysicalAttraction;
            buddy += fanswer.Buddy;
            intellectualAttraction += fanswer.IntellectualAttraction;
        });

        var evalCard = GameObjectManager.SpawnCard(Vector3.zero, GameObjectManager.CardType.Eval).GetComponent<EvaluationCardController>();

        evalCard.PhysicalIntimacy = physicalIntimacy / AmountOfCards;
        evalCard.EmotionalInitimacy = emotionalInitimacy / AmountOfCards;
        evalCard.PhysicalAttraction = physicalAttraction / AmountOfCards;
        evalCard.Buddy = buddy / AmountOfCards;
        evalCard.IntellectualAttraction = intellectualAttraction / AmountOfCards;
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
