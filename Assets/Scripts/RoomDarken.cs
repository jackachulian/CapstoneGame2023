using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDarken : MonoBehaviour
{
    private MeshRenderer ms;
    void Start(){
        ms = GetComponent<MeshRenderer>();
        ms.enabled = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Collided with player");
            ms.enabled = false;
        }
        else{
            Debug.Log("Collided with non-player");
        }
    }

    void OnTriggerExit(Collider collider){
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Collision exit with player");
            ms.enabled = true;
        }
        else{
            Debug.Log("Collision exit with non-player");
        }
    }

}
