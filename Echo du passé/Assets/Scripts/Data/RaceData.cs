using UnityEngine;

[CreateAssetMenu(fileName = "RaceData", menuName = "Scriptable Objects/RaceData")]
public class RaceData : ScriptableObject
{
    [SerializeField]
    private string _raceName = "...";
    [SerializeField]
    private string _raceDescription = "...";

    [SerializeField]
    [Range(1, 10)]
    private int _raceStrength = 1;
    [SerializeField]
    [Range(1, 10)]
    private int _raceDexterity = 1;
    [SerializeField]
    [Range(1, 10)]
    private int _raceIntelligence = 1;
    [SerializeField]
    [Range(1, 10)]
    private int _raceVitality = 1;
    [SerializeField]
    [Range(1, 10)]
    private int _raceLuck = 1;

    public string RaceName => _raceName;
    public string RaceDescription => _raceDescription;
    public int RaceStrength => _raceStrength;
    public int RaceDexterity => _raceDexterity;
    public int RaceIntelligence => _raceIntelligence;
    public int RaceVitality => _raceVitality;
    public int RaceLuck => _raceLuck;

}
