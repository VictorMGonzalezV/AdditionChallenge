using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackspaceButton : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField InputField;
    private InputField NumberInputField;
    // Start is called before the first frame update
    void Start()
    {
        //Getting a reference to the InputField script so its functions can be called from this button
        NumberInputField = InputField.GetComponent<InputField>();
    }

    public void DeleteNumber()
    {
        NumberInputField.DeleteLastNumber();
    }


}
