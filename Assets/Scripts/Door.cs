using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string prompt;
    public string interactionPrompt { get => prompt; }

    public Transform hinge;

    private bool opened;


    bool IInteractable.Interact(Interactor interactor)
    {
        Debug.Log("Opening door!");
        opened = !opened;

        float angle;
        if (opened)
        {
            float dot = Vector3.Dot(transform.forward, Vector3.Normalize(interactor.transform.position - transform.position));
            bool inFront = dot > 0;
            angle = inFront ? 90 : -90;
        } else {
            angle = 0;
        }

        hinge.eulerAngles = Vector3.Slerp(hinge.eulerAngles, new Vector3(0, angle, 0), 90f);

        return true;
    }
}
