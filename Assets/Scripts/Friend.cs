using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour, IInteractable
{
    public string prompt = "Talk";
    public bool talked = false;
    public string interactionPrompt { get => prompt; }

    public Cutscene dialogueBackground;
    public GameObject backgroundGameObject;
    public TextAsset dialogueOne;

    bool IInteractable.Interact(Interactor interactor)
    {   
        Debug.Log("Talked to friend");
        if (!talked){
            talked = true;
            dialogueBackground.index = 0;
            dialogueBackground.textFile = dialogueOne;
            backgroundGameObject.SetActive(true);

        }

        return true;
    }
}
