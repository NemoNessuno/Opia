using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YamlDotNet.RepresentationModel;

public class YAMLHelper
{
    public static string GetStringData(YamlMappingNode root, string key)
    {
        return root.Children[new YamlScalarNode(key)].ToString();
    }

    public static YamlNode GetYAMLNode(YamlMappingNode root, string key)
    {
        return root.Children[new YamlScalarNode(key)];
    }

    public static YamlMappingNode GetRootNode(YamlStream yaml)
    {
        return (YamlMappingNode)yaml.Documents[0].RootNode;
    }

    internal static AnswerData GetAnswer(YamlMappingNode question, string key)
    {
        var answerNode = (YamlMappingNode)GetYAMLNode(question, key);

        var aTest = GetStringData(answerNode, "text");
        var pI = float.Parse(GetStringData(answerNode, "physicalIntimacy"));
        var eI = float.Parse(GetStringData(answerNode, "emotionalInitimacy"));
        var pA = float.Parse(GetStringData(answerNode, "physicalAttraction"));
        var buddy = float.Parse(GetStringData(answerNode, "buddy"));
        var iA = float.Parse(GetStringData(answerNode, "intellectualAttraction"));

        return new AnswerData(aTest, pI, eI, pA, buddy, iA);
    }
}
