public class AnswerData
{

    public string Text { get; private set; }
    public float PhysicalIntimacy { get; private set; }
    public float EmotionalInitimacy { get; private set; }
    public float PhysicalAttraction { get; private set; }
    public float IntellectualAttraction { get; private set; }
    public float Buddy { get; private set; }

    public AnswerData(string text, float pI, float eI, float pA, float buddy, float iA)
    {
        this.Text = text;
        this.PhysicalIntimacy = pI;
        this.EmotionalInitimacy = eI;
        this.PhysicalAttraction = pA;
        this.Buddy = buddy;
        this.IntellectualAttraction = iA;
    }
 
}