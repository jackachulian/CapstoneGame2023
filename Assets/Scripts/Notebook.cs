using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notebook : MonoBehaviour
{
    private bool opened;

    // Start is called before the first frame update
    void Start()
    {
        opened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) {
            opened = !opened;
            gameObject.SetActive(opened);
        }
    }
}
