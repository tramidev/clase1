using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{
    private static DebugText _instance;
    public TextMeshProUGUI textInstanceOk;
    public TextMeshProUGUI textInstanceError;
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ShowError(string errorNumero)
    {
        _instance.textInstanceError.SetText(errorNumero);
        _instance.textInstanceError.color = Color.red;
        _instance.textInstanceError.enabled = true;
    }
    
    public static void ShowGoal(string goalText)
    {
        _instance.textInstanceError.SetText(goalText);
        _instance.textInstanceError.color = Color.green;
        _instance.textInstanceError.enabled = true;
    }

    public static void ShowText(string instruction)
    {
        _instance.textInstanceOk.SetText(instruction);
        _instance.textInstanceOk.color = Color.green;
        _instance.textInstanceOk.enabled = true;
    }
}
