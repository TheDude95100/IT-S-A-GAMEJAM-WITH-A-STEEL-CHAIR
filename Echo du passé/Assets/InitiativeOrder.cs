using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InitiativeOrder : MonoBehaviour
{
    [SerializeField] private GameObject initiativeEntryPrefab; // Un prefab contenant un Text
    [SerializeField] private Transform initiativeListContainer; // Le parent contenant la liste des personnages

    private List<GameObject> initiativeEntries = new List<GameObject>();

    private void Start()
    {
        UpdateInitiativeUI();
    }

    /// <summary>
    /// Met à jour l'affichage de l'ordre d'initiative.
    /// </summary>
    public void UpdateInitiativeUI()
    {
        if (CombatManager.Instance == null) return;

        // Nettoie la liste existante
        foreach (GameObject entry in initiativeEntries)
        {
            Destroy(entry);
        }
        initiativeEntries.Clear();

        // Récupère la pile d'initiative depuis CombatManager
        Stack<CombatManager.Character> initiativeStack = CombatManager.Instance.GetInitiativeStack();

        // Affiche chaque personnage en UI
        foreach (CombatManager.Character character in initiativeStack)
        {
            GameObject entry = Instantiate(initiativeEntryPrefab, initiativeListContainer);
            entry.GetComponent<Text>().text = $"{character.Name} (Init: {character.Initiative})";
            initiativeEntries.Add(entry);
        }
    }
}
