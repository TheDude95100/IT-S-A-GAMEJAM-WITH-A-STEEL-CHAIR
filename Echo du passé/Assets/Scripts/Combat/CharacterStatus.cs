using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField] Entity character;
    [SerializeField] TextMeshProUGUI _hpValue, _characterName;
    [SerializeField] Slider _hpSlider;

    public TextMeshProUGUI HpValue { get => _hpValue; set => _hpValue = value; }
    public TextMeshProUGUI CharacterName { get => _characterName; set => _characterName = value; }
    public Slider HpSlider { get => _hpSlider; set => _hpSlider = value; }
    public Entity Character { get => character; set { character = value; UpdateCharacterName(); } }

    public void UpdateCharacterName(){
        CharacterName.text = Character.EntityName;
    }

    public void UpdateCharacterHP(Entity target)
    {
        if(target.gameObject.name.Equals(character.gameObject.name))
        {
            HpValue.text = target.CurrentHP + "/" + target.MaxHP;
            HpSlider.value = target.CurrentHP / target.MaxHP;
        }
    }
}
