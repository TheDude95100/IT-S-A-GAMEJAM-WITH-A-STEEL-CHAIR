using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InitiativeOrder : MonoBehaviour
{

    [SerializeField] private int _numberOfCharacters;
    [SerializeField] private GameObject _characterTextPrefab;

    private void Start()
    {
        CombatManager.Instance.OnInitiativeRolled += CombatManager_OnInitiativeRolled;
    }
    private void CombatManager_OnInitiativeRolled(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        _numberOfCharacters = CombatManager.Instance.CurrentNumberOfCharacters;
        for (int i = 0; i < _numberOfCharacters; i++)
        {
            // Instantie le prefab pour chaque personnage
            GameObject go = Instantiate(_characterTextPrefab, transform.position + Vector3.right * i * 50, Quaternion.identity, transform);
            go.name = "Character" + i;  // Donne un nom unique à chaque personnage

            go.GetComponent<TextMeshProUGUI>().text = CombatManager.Instance.GetCharacter(i).Name;
        }
    }
}
