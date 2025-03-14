using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    private int nbAlly;
    [SerializeField]
    private int nbEnemy;

    [SerializeField]
    private GameObject allyPrefab;
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private Transform[] allyPositions;
    [SerializeField]
    private Transform allyActionExecutionPosition;

    [SerializeField]
    private Transform[] enemyPositions;
    [SerializeField]
    private Transform enemyActionExecutionPosition;

    [SerializeField]
    private GameObject actionChoiceHUD;

    private List<Entity> _entityList;

    private List<int> _enemyIndexes,
                      _allyIndexes;

    private int _selected, 
                _xpGained;

    private bool _isActionSelected;

    private Stack<Entity> initiativeStack = new Stack<Entity>();
    public event EventHandler OnInitiativeRolled;

    public int NumberOfCharactersInBattle => _entityList.Count;
    public int NumberOfAllyInBattle => _allyIndexes.Count;

    public delegate void UpdatePlayerUI(Entity target);
    public event UpdatePlayerUI OnUpdatePlayerHP;

    public delegate void CombatMovementGo(Entity target, Transform posStart, Transform posFinish);
    public event CombatMovementGo OnEntityMovementGo;

    public delegate void CombatMovementReturn(Entity target, Transform posStart, Transform posFinish);
    public event CombatMovementReturn OnEntityMovementReturn;

    private void Awake()
    {
        _entityList = new List<Entity>();

        SpawnEntities(nbAlly, allyPrefab, allyPositions);
        SpawnEntities(nbEnemy, enemyPrefab, enemyPositions);
    }

    private void Start()
    {
        InitializeInitiative(_entityList);
        StartCombat();
    }

    //retourne de la liste complete
    public Entity GetCharacter(int initiativeOrder)
    {
        return initiativeStack.ToArray()[initiativeOrder];
    }

    //retourne des allié uniquement
    public Entity GetAlly(int listIndex)
    {
        return _entityList[_allyIndexes[listIndex]];
    }

    private bool IsCombatDone()
    {
        if (VerifyWin())
        {
            Debug.Log("You win");
            return true;
        }

        if (VerifyLose())
        {
            Debug.Log("You lost");
            return true;
        }

        return false;
    }

    private void SpawnEntities(int numberToSpawn, GameObject prefab, Transform[] positionList)
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            if (i >= positionList.Length)
            {
                break;
            }
            GameObject entity = Instantiate(prefab, positionList[i]);
            entity.name = entity.tag + i;
            _entityList.Add(entity.GetComponent<Entity>());
        }
    }

    /// <summary>
    /// Initialise l'ordre d'initiative et remplit la pile.
    /// </summary>
    public void InitializeInitiative(List<Entity> characters)
    {
        // Attribuer une initiative aléatoire à chaque personnage
        foreach (var character in characters)
        {
            character.RollInitiative();
        }

        // Trier du plus haut au plus bas
        characters.Sort((a, b) => a.Initiative.CompareTo(b.Initiative));

        // Remplir la pile avec l'ordre des initiatives
        initiativeStack.Clear();
        foreach (var character in characters)
        {
            initiativeStack.Push(character);
        }
        UpdateEntitiesIndexes();
    }

    /// <summary>
    /// Update the indexes to know where the allies and the ennemies are in the list.
    /// </summary>
    private void UpdateEntitiesIndexes()
    {
        _enemyIndexes = new List<int>();
        _allyIndexes = new List<int>();
        for (int counter = 0; counter < _entityList.Count; counter++)
        {
            Debug.Log(_entityList[counter].gameObject.name);

            if (_entityList[counter].CompareTag("Player"))
            {
                _allyIndexes.Add(counter);
            }
            else
            {
                _enemyIndexes.Add(counter);
            }
        }
    }

    /// <summary>
    /// Update the indexes to know where the allies and the ennemies are in the list.
    /// </summary>
    private void UpdateHealthUI()
    {
        foreach(int index in _allyIndexes)
        {
            Debug.Log(_entityList[index].gameObject.name);

            OnUpdatePlayerHP?.Invoke(_entityList[index]);
        }
    }

    /// <summary>
    /// Lance le combat en affichant l'ordre d'initiative.
    /// </summary>
    public void StartCombat()
    {
        Debug.Log("Début du combat ! Ordre d'initiative :");

        Stack<Entity> tempStack = new Stack<Entity>(initiativeStack);
        while (tempStack.Count > 0)
        {
            Entity chara = tempStack.Pop();
            Debug.Log($"{chara.EntityName} (Initiative: {chara.Initiative})");
        }
        OnInitiativeRolled?.Invoke(this, EventArgs.Empty);
        UpdateHealthUI();

        StartCoroutine(CombatLoop());
    }

    IEnumerator CombatLoop()
    {
        bool combatFinished = false;
        Entity activeEntity;

        while(!combatFinished)
        {
            activeEntity = NextTurn();

            Transform activeEntityStartPosition = activeEntity.transform.parent;

            if(activeEntity.CompareTag("Player"))
            {
                _isActionSelected = false;

                OnEntityMovementGo?.Invoke(activeEntity, activeEntityStartPosition, allyActionExecutionPosition);
                yield return new WaitForSeconds(2);

                actionChoiceHUD.SetActive(true);
                while(!_isActionSelected)
                {
                    yield return null;
                }

                _entityList[_enemyIndexes[_selected]].TakeDamage(activeEntity.DamageGeneration(0));

                OnEntityMovementReturn?.Invoke(activeEntity, allyActionExecutionPosition, activeEntityStartPosition);
            }
            else
            {
                OnEntityMovementGo?.Invoke(activeEntity, activeEntityStartPosition, enemyActionExecutionPosition);

                yield return new WaitForSeconds(2);
                int randomTarget = UnityEngine.Random.Range(0, _allyIndexes.Count);

                _entityList[_allyIndexes[randomTarget]].TakeDamage(activeEntity.DamageGeneration(0));

                OnEntityMovementReturn?.Invoke(activeEntity, enemyActionExecutionPosition, activeEntityStartPosition);
            }
            UpdateHealthUI();

            yield return new WaitForSeconds(3);
            combatFinished = IsCombatDone();
        }
    }

    public void OnAction()
    {
        _entityList[_enemyIndexes[_selected]].Shadow.SetActive(false);
        _isActionSelected = true;
    }

    public void OnSelectionRight()
    {
        _entityList[_enemyIndexes[_selected]].Shadow.SetActive(false);
        _selected++;
        if (_selected >= _enemyIndexes.Count)
        {
            ResetSelection();
        }
        _entityList[_enemyIndexes[_selected]].Shadow.SetActive(true);
        //Debug.Log(entityList[enemyIndexes[selected]].gameObject.name + " selected");
    }

    public void OnSelectionLeft()
    {
        _entityList[_enemyIndexes[_selected]].Shadow.SetActive(false);
        _selected--;
        if (_selected < 0)
        {
            _selected = _enemyIndexes.Count - 1;
        }
        _entityList[_enemyIndexes[_selected]].Shadow.SetActive(true);
        //Debug.Log(entityList[enemyIndexes[selected]].gameObject.name + " selected");
    }

    public void AttackSelected()
    {
        _entityList[_enemyIndexes[_selected]].Shadow.SetActive(true);
        actionChoiceHUD.SetActive(false);
    }

    /// <summary>
    /// Passe au prochain combattant.
    /// </summary>
    public Entity NextTurn()
    {
        if (initiativeStack.Count == 0)
        {
            Debug.Log("Fin du round, réinitialisation de l'ordre d'initiative.");
            ResetInitiative();
        }

        Entity currentCharacter = initiativeStack.Pop();
        Debug.Log($"{currentCharacter.EntityName} joue son tour !");
        return currentCharacter;
    }

    /// <summary>
    /// Réinitialise la pile pour un nouveau round.
    /// </summary>
    private void ResetInitiative()
    {
        // Transformer la pile en liste temporaire et réinitialiser
        List<Entity> tempList = new List<Entity>(_entityList);
        InitializeInitiative(tempList);
    }

    /// <summary>
    /// Ajoute un nouveau combattant au combat.
    /// </summary>
    public void AddCharacterToInitiative(Entity newCharacter)
    {
        newCharacter.RollInitiative();

        List<Entity> tempList = new List<Entity>(initiativeStack) { newCharacter };
        InitializeInitiative(tempList);
    }

    public Stack<Entity> GetInitiativeStack()
    {
        return initiativeStack;
    }

    public void ResetSelection()
    {
        _selected = 0;
    }

    private bool VerifyWin()
    {
        int nbDeath = 0;
        foreach (int index in _enemyIndexes)
        {
            Debug.Log(_entityList[index] + " has " + _entityList[index].CurrentHP );
            if (!_entityList[index].IsAlive)
            {
                nbDeath++;
            }
        }

        if (nbDeath == _enemyIndexes.Count)
        {
            return true;
        }

        return false;
    }

    private bool VerifyLose()
    {
        int nbDeath = 0;
        foreach (int index in _allyIndexes)
        {
            Debug.Log(_entityList[index] + " has " + _entityList[index].CurrentHP);
            if (!_entityList[index].IsAlive)
            {
                nbDeath++;
            }
        }

        if (nbDeath == _allyIndexes.Count)
        {
            return true;
        }

        return false;
    }
}
