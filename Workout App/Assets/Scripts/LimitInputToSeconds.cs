using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LimitInputToSeconds : MonoBehaviour
{
    [SerializeField] private TMP_InputField _textToAdjust;

    public void AdjustValueToSeconds(string value)
    {
        if (value == String.Empty)
            return;

        //convert string to int
        print(value);

        bool parsable = int.TryParse(value, out int intValue);

        string changedValue = "";

        if (parsable)
        {
            if (intValue > 59)
            {
                intValue = 59;
                changedValue = intValue.ToString();
            }
            else if (intValue < 10)
            {
                changedValue = "0" + intValue;
            }
            else
            {
                changedValue = intValue.ToString();
            }
        }


        _textToAdjust.text = changedValue;
        print(_textToAdjust.text);
    }
}