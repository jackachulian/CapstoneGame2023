using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform interactionPoint;
    public float interactionPointRadius;
    public LayerMask interactableMask;

    public readonly Collider[] colliders = new Collider[3];
    public int numFound;

    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);

        if (numFound > 0){
            var interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null && Input.GetKeyDown(KeyCode.Space))
            {
                interactable.Interact(this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}