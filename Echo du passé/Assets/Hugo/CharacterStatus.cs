using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField] CombatManager.Character character;
    [SerializeField] TextMeshProUGUI _hpValue, _mpValue, _characterName;
    [SerializeField] Slider _hpSlider, _mpSlider;

    public TextMeshProUGUI HpValue { get => _hpValue; set => _hpValue = value; }
    public TextMeshProUGUI MpValue { get => _mpValue; set => _mpValue = value; }
    public TextMeshProUGUI CharacterName { get => _characterName; set => _characterName = value; }
    public Slider HpSlider { get => _hpSlider; set => _hpSlider = value; }
    public Slider MpSlider { get => _mpSlider; set => _mpSlider = value; }
    public CombatManager.Character Character { get => character; set { character = value; UpdateCharacterName(); } }

    public void UpdateCharacterName(){
        CharacterName.text = Character.Name;
    }
}
