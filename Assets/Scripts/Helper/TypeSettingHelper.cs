using System.Text;
using UnityEngine;

class TypeSettingHelper
{
    public static void SetTextOnMesh(string text, TextMesh mesh, float boundary)
    {
        //TODO: This is rather naive. Hyphenation?
        text = text.Replace(System.Environment.NewLine, "");
        string[] words = text.Split(' ');

        var sb = new StringBuilder();
        var renderer = mesh.GetComponent<Renderer>();

        for (int i = 0; i < words.Length; i++)
        {
            sb.Append(words[i] + " ");
            mesh.text = sb.ToString();

            if (renderer.bounds.size.x >= boundary && i!=0)
            {
                //TODO: This crashes on first word. why +2 anyway?
                sb.Remove(sb.Length - (words[i].Length + 1), words[i].Length + 1);
                sb.Append(System.Environment.NewLine);
                sb.Append(words[i] + " ");
            }
        }
        sb.Remove(sb.Length - 1, 1);

        mesh.text = sb.ToString();
    }
}
