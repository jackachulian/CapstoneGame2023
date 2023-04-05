using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour, IInteractable
{
    public string prompt = "Talk";
    public string interactionPrompt { get => prompt; }

    bool IInteractable.Interact(Interactor interactor)
    {
        Debug.Log("Talked to friend");
        return true;
    }
}
