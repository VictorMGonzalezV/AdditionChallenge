using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberButton : MonoBehaviour
{
    [SerializeField]
    private int NumericValue;
    [SerializeField]
    private TMP_InputField InputField;
    private InputField NumberInputField;
    
    
    public void Start()
    {
        //Getting a reference to the InputField script so its functions can be called from this button
        NumberInputField = InputField.GetComponent<InputField>();
    }

    public void InputNumericValue()
    {
        NumberInputField.UpdateCurrentText(NumericValue);
    }
}
