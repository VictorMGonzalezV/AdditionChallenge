using System.Collections;
using System.Collections.Generic;
//using the System namespace to have access to the Convert methods
using System;
using TMPro;
using UnityEngine;

public class InputField : MonoBehaviour
{
    //I'm storing the current text as a string so the input values from the buttons  are converted to characters and appended
    private string CurrentText;
    [SerializeField]
    private TMP_InputField TextInputField;

    //I turned this into a function so the text is updated both when adding a number and when deleting one
    public void UpdateDisplayText()
    {
        TextInputField.text = CurrentText;
    }

    //This function is called when a numeric button is clicked on
    public void UpdateCurrentText(int number)
    {
        //In case a stack is used to store the numbers the player enters, this function would call Push(number) on the int stack
        CurrentText += number;
        UpdateDisplayText();
    }
    //This function is called when the backspace button is clicked on
    public void DeleteLastNumber()
    {
        if(CurrentText.Length>=1)
        {
            //Using a stack to store the numeric values is another possible approach,in which case this function would call Pop() on the stack
            //Considering the small size of the strings involved, using the relatively inefficient Remove() function should be acceptable
            CurrentText = CurrentText.Remove(CurrentText.Length - 1, 1);
            UpdateDisplayText();
        }
    }

    public void ClearInputField()
    {
        //In case a stack is used to store the numbers the player inputs, this function would call Clear() on the stack instead of manually
        //setting the text to an empty string
        CurrentText = "";
        UpdateDisplayText();
    }

    public int GetInputValue()
    {
        //convert the current text on the input field into an int that can be compared
        return Convert.ToInt32(CurrentText);
    }
}
