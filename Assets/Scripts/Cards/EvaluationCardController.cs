using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class EvaluationCardController : MonoBehaviour {

    public TextMesh PIValue;
    public TextMesh EIValue;
    public TextMesh PAValue;
    public TextMesh BuddyValue;
    public TextMesh IAValue;

    private float _physicalIntimacy = 0f;
    private float _emotionalInitimacy = 0f;
    private float _physicalAttraction = 0f;
    private float _buddy = 0f;
    private float _intellectualAttraction = 0f;

    public float PhysicalIntimacy {
        get 
        {
            return _physicalIntimacy;
        } 
        set 
        {
            _physicalIntimacy = value;
            PIValue.text = AsPercent(value);
        }
    }    
    public float EmotionalInitimacy
    {
        get 
        {
            return _emotionalInitimacy;
        } 
        set 
        {
            _emotionalInitimacy = value;
            EIValue.text = AsPercent(value);
        }
    }
    public float PhysicalAttraction
    {
        get
        {
            return _physicalAttraction;
        }
        set
        {
            _physicalAttraction = value;
            PAValue.text = AsPercent(value);
        }
    }
    public float Buddy
    {
        get
        {
            return _buddy;
        }
        set
        {
            _buddy = value;
            BuddyValue.text = AsPercent(value);
        }
    }
    public float IntellectualAttraction
    {
        get
        {
            return _intellectualAttraction;
        }
        set
        {
            _intellectualAttraction = value;
            IAValue.text = AsPercent(value);
        }
    }
 
    private string AsPercent(float value)
    {
        return (value * 100f).ToString("0") + "%";
    }
}
