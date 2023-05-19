using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Addition : MonoBehaviour
{
    [SerializeField]
    private TMP_Text QuestionText;
    [SerializeField]
    //May not be needed, tweaking the gravity scale is easier than implementing movement logic in the script for such a simple movement pattern
    private float ScrollSpeed;


    private int Operand_1;
    private int Operand_2;
    private int AdditionValue;
    // Start is called before the first frame update
    void Start()
    {
        
        //Initialize the 2 random operands before displaying the text
        Operand_1 = Random.Range(1,10);
        Operand_2 = Random.Range(1,10);
        QuestionText.text = Operand_1 + "+" + Operand_2;
        //Storing the expected answer so the object only has to perform that operation once
        AdditionValue = Operand_1 + Operand_2;
        //The Addition enqueues itself into the queue used by the GameManager to keep track of them
        GameManager.Instance.AdditionQueue.Enqueue(this);
    }

    //Removed the Update method to ensure better performance

    //I'm not too comfortable yet with the getter/setter declarations inside property declarations, so for now I'm using the traditional method
    public int GetAdditionValue()
    {
        return AdditionValue;
    }



  

}
