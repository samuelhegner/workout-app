﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdjustToSliderValue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    
    public void AdjustTextToSliderValue(float value)
    {
        _text.text = value.ToString();
    }
}
