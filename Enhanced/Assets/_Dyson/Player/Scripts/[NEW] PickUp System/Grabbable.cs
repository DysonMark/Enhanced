using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject interactionPanel;
    [SerializeField] private KeyCode interactionKey;
    public KeyCode InteractionKey => interactionKey;
    public bool IsInteracting { get; set; }
    private Rigidbody _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
        interactionPanel = Resources.FindObjectsOfTypeAll<GameObject>()
            .FirstOrDefault(go => go.name == "Pickup_TXT");
    }
    
    public void StartInteracting(Interactor interactor)
    {
        this.transform.SetParent(interactor.holdPos);
        _body.isKinematic = true;
        IsInteracting = true;
        this.transform.localPosition=Vector3.zero;
        EndHover(interactor);
    }

    public void EndInteracting(Interactor interactor)
    {
        this.transform.SetParent(null);
        _body.isKinematic = false;
        IsInteracting = false;
    }
    
    public void StartHover(Interactor interactor)
    {
        interactionPanel?.SetActive(true);
    }

    public void EndHover(Interactor interactor)
    {
        interactionPanel?.SetActive(false);
    }
}
