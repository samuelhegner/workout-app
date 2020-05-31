using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveButtonEnabler : MonoBehaviour
{
    private bool named;

    private bool setLengthSet;

    
    [SerializeField] private TMP_InputField nameInputField;

    
    [SerializeField] private TMP_InputField[] setLengthInputFields;

    [SerializeField] private Button _saveButton;

    private void Update()
    {
        if (nameInputField.text != String.Empty)
        {
            named = true;
        }
        else
        {
            named = false;
        }


        if (setLengthInputFields[0].text != String.Empty || setLengthInputFields[1].text != String.Empty)
        {
            setLengthSet = true;
        }
        else
        {
            setLengthSet = false;
        }

        if (named && setLengthSet)
        {
            _saveButton.interactable = true;
        }
        else
        {
            _saveButton.interactable = false;
        }
    }
}
