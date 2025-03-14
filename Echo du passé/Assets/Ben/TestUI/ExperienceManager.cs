using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUILevel;
    [SerializeField]
    private Slider expSlider;

    private int level = 1;

    private void Start()
    {
        expSlider.maxValue = player._nextLevel;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GainExperience(4);
        }
    }

    private void GainExperience(int amount)
    {
        player._currentXP+=amount;
        if (player._currentXP >= player._nextLevel)
        {
            LevelUp();
        }
        expSlider.value = player._currentXP;
    }

    private void LevelUp()
    {
        player._nextLevel += 20;
        expSlider.maxValue = player._nextLevel;
        expSlider.value = 0;
        player._currentXP = 0;
        level++;
        textMeshProUGUILevel.text = "Level "+level;
    }
}
