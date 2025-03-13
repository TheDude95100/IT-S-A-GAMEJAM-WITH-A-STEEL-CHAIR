using UnityEngine;
using System;
using System.Collections.Generic;

namespace Combat
{
    public class Manager : MonoBehaviour
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

        private List<Entity> _entityList;

        private List<int> _enemyIndexes,
                          _allyIndexes;

        private int _selected;

        private Stack<Entity> initiativeStack = new Stack<Entity>();
        public event EventHandler OnInitiativeRolled;

        public int NumberOfCharactersInBattle => _entityList.Count;
        public int NumberOfAllyInBattle => _allyIndexes.Count;

        private void Awake()
        {
            Debug.Log("Awake");
            _enemyIndexes = new List<int>();
            _allyIndexes = new List<int>();
            _entityList = new List<Entity>();

            for (int i = 0; i < nbAlly; i++)
            {
                if (i >= allyPositions.Length)
                {
                    break;
                }
                GameObject entity = Instantiate(allyPrefab, allyPositions[i]);
                entity.name = "Player" + i;
                _entityList.Add(entity.GetComponent<Entity>());
            }

            for (int i = 0; i < nbEnemy; i++)
            {
                if (i >= enemyPositions.Length)
                {
                    break;
                }
                GameObject entity = Instantiate(enemyPrefab, enemyPositions[i]);
                entity.name = "Enemy" + i;
                _entityList.Add(entity.GetComponent<Entity>());
            }
        }

        private void Start()
        {
            InitializeInitiative(_entityList);
            StartCombat();
        }

        private void FixedUpdate()
        {
            /*if (VerifyWin())
            {
                Debug.Log("You win");
            }
            if (VerifyLose())
            {
                Debug.Log("You lost");
            }*/
        }

        /// <summary>
        /// Update the indexes to know where the allies and the ennemies are in the list.
        /// </summary>
        private void UpdateEntitiesIndexes()
        {
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

            NextTurn();
        }

        /// <summary>
        /// Passe au prochain combattant.
        /// </summary>
        public void NextTurn()
        {
            if (initiativeStack.Count == 0)
            {
                Debug.Log("Fin du round, réinitialisation de l'ordre d'initiative.");
                ResetInitiative();
            }

            Entity currentCharacter = initiativeStack.Pop();
            Debug.Log($"{currentCharacter.EntityName} joue son tour !");
        }

        /// <summary>
        /// Réinitialise la pile pour un nouveau round.
        /// </summary>
        private void ResetInitiative()
        {
            // Transformer la pile en liste temporaire et réinitialiser
            List<Entity> tempList = new List<Entity>(initiativeStack);
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
                Debug.Log(index);
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
}
