﻿using FickleFrames.ActionSystem;
using UnityEngine;

public class Testing_03 : MonoBehaviour
{
    private void Awake()
    {
        ActionManager.RegisterAction(action, "Hemanth");
    }

    private void action(IActionParameters parameters)
    {
        string data = (string)parameters.data;
        Debug.Log(data + $" in {gameObject.name}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ActionManager.ExecuteAction("Appu", $"Data from {gameObject.name}");
    }
}
