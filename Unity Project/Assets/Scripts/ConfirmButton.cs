using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConfirmButton : MonoBehaviour
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

    // Update is called once per frame
    public void ConfirmAnswer()
    {
        //Since the logic to check answers is inside the GameManager, this button hands everything over to the GameManager
        GameManager.Instance.CheckAnswer();
    }
}
