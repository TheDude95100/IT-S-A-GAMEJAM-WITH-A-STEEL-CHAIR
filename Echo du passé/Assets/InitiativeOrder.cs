using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InitiativeOrder : MonoBehaviour
{

    [SerializeField] private int _numberOfCharacters;
    [SerializeField] private GameObject _characterTextPrefab;
    [SerializeField] private CombatManager _combatManager;

    private void Start()
    {
        _combatManager = FindFirstObjectByType<CombatManager>();
        _combatManager.OnInitiativeRolled += CombatManager_OnInitiativeRolled;
    }
    private void CombatManager_OnInitiativeRolled(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        _numberOfCharacters = _combatManager.CurrentNumberOfCharacters;
        for (int i = 0; i < _numberOfCharacters; i++)
        {
            // Instantie le prefab pour chaque personnage
            GameObject go = Instantiate(_characterTextPrefab, transform.position + Vector3.right * i * 25, Quaternion.identity, transform);
            go.name = "Character" + i;  // Donne un nom unique à chaque personnage

            go.GetComponent<TextMeshProUGUI>().text = _combatManager.GetCharacter(i).Name;
        }
    }
}
