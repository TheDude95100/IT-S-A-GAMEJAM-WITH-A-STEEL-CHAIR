using UnityEngine;

[CreateAssetMenu(fileName = "EntityData_", menuName = "Scriptable Objects/Entity")]
public class EntityData : ScriptableObject
{
    [SerializeField] 
    private string _entityName = "...";

    [Header("General stats")]

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

    [Header("Job stats")]

    [SerializeField][Range(1,10)]
    private int _level = 1;
    [SerializeField] 
    private JobData _job;

    [Header("Sprite")]

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

}
