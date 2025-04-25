using UnityEngine;

public abstract class InteractableAbstract : MonoBehaviour
{
    public KeyCode InteractionKey { get; }
    public bool IsInteracting { get; set; }

    public virtual void StartHover(Interactor interactor) { }
    public virtual void EndHover(Interactor interactor) { }

    public abstract void StartInteracting(Interactor interactor);
    public abstract void EndInteracting(Interactor interactor);
}
