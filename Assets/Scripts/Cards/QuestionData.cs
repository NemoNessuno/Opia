using System;
using System.Collections.Generic;
using System.Linq;

public class QuestionData
{
    public string Text { get; private set; }
    public List<AnswerData> Answers { get; private set; }

    public QuestionData(string text, List<AnswerData> answers)
    {
        if (answers.Count < 4) throw new Exception("Too few answers! Expected at least 4 but got only " + answers.Count);

        Text = text;
        Answers = answers;
    }

    public QuestionData(
        string qtext, 
        AnswerData answer1, 
        AnswerData answer2, 
        AnswerData answer3, 
        AnswerData answer4)
        : this(qtext, new List<AnswerData>()
        { answer1, answer2, answer3, answer4 }
        )
    {
        //Empty constructor
    }
}
