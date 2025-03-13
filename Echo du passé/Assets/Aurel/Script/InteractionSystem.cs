using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Add TextMeshPro namespace

public abstract class InteractionSystem : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject interactionPrompt;
    [SerializeField] private TMP_Text promptText; 

    [Header("Interaction Settings")]
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private string promptMessage = "E";

    protected bool canInteract = false;

    protected void Start()
    {
        // Make sure the prompt is hidden at start
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }

        // Set the prompt text
        if (promptText != null)
        {
            promptText.text = promptMessage;
        }
    }

    protected void Update()
    {
        // Check if player can interact and is pressing the interaction key
        if (canInteract && Input.GetKeyDown(interactionKey))
        {
            Interact();
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Show interaction prompt
            canInteract = true;
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(true);
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Hide interaction prompt
            canInteract = false;
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(false);
            }
        }
    }

    protected abstract void Interact();

}