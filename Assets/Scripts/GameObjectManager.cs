using UnityEngine;
using System.Collections;

/// <summary>
/// Game object manager.
/// This class manages the creation and destruction of all our GameObjects
/// that we don't place via the editor.
/// </summary>
public class GameObjectManager{

	/// <summary>
	/// Safely destroy a game object.
	/// </summary>
	/// <param name="objectToDestroy">Object to destroy.</param>
	public static void SafeDestroy(GameObject objectToDestroy){		
		MonoBehaviour.Destroy (objectToDestroy);		
	}	
	
	/// <summary>
	/// Safely initializes a card.
	/// </summary>
	/// <returns>The initialized card.</returns>
	/// <param name="name">Name.</param>
	/// <param name="position">Position.</param>
    public static GameObject SpawnCard(Vector3 position, CardType cardType)
    {
        string typeString = null;

        switch (cardType)
        {
            case CardType.Question:
                typeString = "QuestionCard";
                break;
            case CardType.Menu:
                typeString = "MenuCard";
                break;
            case CardType.Options:
                typeString = "OptionsCard";
                break;
            case CardType.Eval:
                typeString = "EvaluationCard";
                break;
        }


        return (GameObject)MonoBehaviour.Instantiate(Resources.Load<GameObject>(typeString), position, Quaternion.identity);
	}

    public enum CardType
    {
        Question,
        Menu,
        Options,
        Eval
    }
}
