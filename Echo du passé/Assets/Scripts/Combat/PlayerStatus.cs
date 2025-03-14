using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private GameObject _characterStatusPrefab;
    [SerializeField] private CombatManager _combatManager;
    private int _numberOfCharacters;
    private int _Y_spacing = -100;
    private int _X_position = -120;
    private int _Y_position = -50;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _combatManager = FindFirstObjectByType<CombatManager>();
        _combatManager.OnInitiativeRolled += CombatManager_OnInitiativeRolled;
    }
    
    private void CombatManager_OnInitiativeRolled(object sender, System.EventArgs e)
    {
        CreateVisual();
    }

    private void CreateVisual()
    {
        Debug.Log("Bite");
        _numberOfCharacters = _combatManager.NumberOfAllyInBattle;
        for (int i = 0; i < _numberOfCharacters; i++)
        {
            Debug.Log("CONNAAAAARD");
            // Instantie le prefab pour chaque personnage
            GameObject go = Instantiate(_characterStatusPrefab, transform.localPosition, Quaternion.identity, transform);
            go.transform.localPosition = new Vector3(_X_position, _Y_position + i * _Y_spacing, 0);
            go.name = "Character" + i;  // Donne un nom unique à chaque personnage
            CharacterStatus displayedStatsHolder = go.GetComponent<CharacterStatus>();
            _combatManager.onPlayerDamageTaken += displayedStatsHolder.UpdateCharacterHP;
            displayedStatsHolder.Character = _combatManager.GetAlly(i);
        }
    }
}
