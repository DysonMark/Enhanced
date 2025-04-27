using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Interactable currentInteractable;

    public Transform holdPos;
    
    void Update()
    {
        if (!(currentInteractable is { IsInteracting: true }))
        {
            GetInteractableObject();
        }
        //GetInteractableObject();
        HandleInteraction();
    }
    
    private void HandleInteraction()
    {
        if(currentInteractable == null)return;
        if (Input.GetKeyDown(currentInteractable.InteractionKey))
        {
            if (currentInteractable.IsInteracting)
            {
                currentInteractable.EndInteracting(this);
            }
            else
            {
                currentInteractable.StartInteracting(this);
            }
        }
    }
    
    private void GetInteractableObject()
    {
        Ray ray=new Ray(holdPos.position, holdPos.forward);
        if (Physics.Raycast(ray,out RaycastHit hit,1.3f))
        {
            var interactable = hit.collider.GetComponent<Interactable>();
            currentInteractable?.EndHover(this);
            currentInteractable=interactable;
            currentInteractable.StartHover(this);
        }
        else
        {
            currentInteractable?.EndHover(this);
            currentInteractable = null;
        }
    }
}

public interface Interactable
{
    void StartInteracting(Interactor interactor);
    void EndInteracting(Interactor interactor);
    void StartHover(Interactor interactor);
    void EndHover(Interactor interactor);
    public KeyCode InteractionKey { get; }
    public bool IsInteracting { get; set; }
}
