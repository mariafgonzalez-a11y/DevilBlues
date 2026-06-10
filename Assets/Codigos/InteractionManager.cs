using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

interface IInteractable
{
    public void Interact();
}

public class InteractionManager : MonoBehaviour
{
    public Transform InteractionSource; //fonte de interação
    public float InteractionRange; //Alcance da interação
    public InputActionReference interactionInputAction;


    private void Onable()
    {
        interactionInputAction.action.performed += Interact;
    }

    private void Disable()
    {
        interactionInputAction.action.performed -= Interact;
    }


    private void Interact(InputAction.CallbackContext obj)
    {
        Ray playerAim = new Ray(InteractionSource.position, InteractionSource.forward);
        if(Physics.Raycast(playerAim, out RaycastHit hitInfo, InteractionRange))
        {
            if(hitInfo.collider.TryGetComponent(out IInteractable interactableObj))
            {
                interactableObj.Interact();
            }
        }
    }

    
}
