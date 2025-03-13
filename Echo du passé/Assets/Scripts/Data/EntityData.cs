using UnityEngine;

[CreateAssetMenu(fileName = "EntityData_", menuName = "Dataset/Entity")]
public class EntityData : ScriptableObject
{
    // Editor param
    [SerializeField]
    private bool _showDesignerDisplay = false;

    [SerializeField] 
    private string _entityName = "...";

    [SerializeField]
    [Range(1, 10)]
    private int _strength = 1;
    [SerializeField]
    [Range(1, 10)]
    private int _dexterity = 1;
    [SerializeField]
    [Range(1, 10)]
    private int _intelligence = 1;
    [SerializeField]
    [Range(1, 10)]
    private int _vitality = 1;
    [SerializeField]
    [Range(1, 10)]
    private int _luck = 1;

    [SerializeField][Range(1,10)]
    private int _level = 1;
    [SerializeField] 
    private JobData _job;
    [SerializeField]
    private RaceData _raceData;

    [SerializeField]
    private Sprite _sprite;


    public string Name => _entityName;
    public int Level => _level;
    public int Strenth => _strength;
    public int Dexterity => _dexterity;
    public int Intelligence => _intelligence;
    public int Vitality => _vitality;
    public int Luck => _luck;
    public JobData Job => _job;
    public RaceData RaceData => _raceData;
    public Sprite Sprite => _sprite;

    public bool ShowDesignerDisplay => _showDesignerDisplay;

}
