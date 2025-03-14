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

    private Stack<Entity> initiativeStack = new Stack<Entity>();
    public event EventHandler OnInitiativeRolled;

    public int NumberOfCharactersInBattle => _entityList.Count;
    public int NumberOfAllyInBattle => _allyIndexes.Count;

    public delegate void UpdateHealthUI(Entity target);
    public event UpdateHealthUI OnPlayerDamageTaken;

    public delegate void CombatMovementGo(Entity target, Transform posStart, Transform posFinish);
    public event CombatMovementGo OnEntityMovementGo;

    public delegate void CombatMovementReturn(Entity target, Transform posStart, Transform posFinish);
    public event CombatMovementReturn OnEntityMovementReturn;

    private void Awake()
    {
        Debug.Log("Awake");
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

        StartCoroutine(CombatLoop());
    }

    IEnumerator CombatLoop()
    {
        bool combatFinished = false;
        Entity activeEntity;

        while(!combatFinished)
        {
            activeEntity = NextTurn();

            Debug.Log(activeEntity);
            Transform activeEntityStartPosition = activeEntity.transform.parent;

            //entity avance a son ini
            if(activeEntity.CompareTag("Player"))
            {
                OnEntityMovementGo?.Invoke(activeEntity, activeEntityStartPosition, allyActionExecutionPosition);
                Debug.Log(activeEntity + " a bougé.");


                yield return new WaitForSeconds(2);
                OnEntityMovementReturn?.Invoke(activeEntity, allyActionExecutionPosition, activeEntityStartPosition);
                Debug.Log(activeEntity + " est revenu.");
            }
            else
            {
                OnEntityMovementGo?.Invoke(activeEntity, activeEntityStartPosition, enemyActionExecutionPosition);
                Debug.Log(activeEntity + " a bougé.");

                yield return new WaitForSeconds(2);
                OnEntityMovementReturn?.Invoke(activeEntity, enemyActionExecutionPosition, activeEntityStartPosition);
                Debug.Log(activeEntity + " est revenu.");
            }

            yield return new WaitForSeconds(3);
            combatFinished = IsCombatDone();
        }
        //si ally
        //  afficher bouton
        //sinon
        //  enemy attack
        //
        //si combat terminer
        //  écran victoire
        //sinon
        //  entity recule position initiale
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

    public void OnAction()
    {
        Debug.LogWarning("Hit");
        OnEntityMovementReturn?.Invoke(_entityList[0], enemyActionExecutionPosition, enemyPositions[0]);
    }
}
