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

        private Stack<Character> initiativeStack = new Stack<Character>();
        public event EventHandler OnInitiativeRolled;

        public int CurrentNumberOfCharacters => initiativeStack.Count;

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
                _entityList.Add(entity.GetComponent<Entity>());
                _allyIndexes.Add(i);
            }

            for (int i = 0; i < nbEnemy; i++)
            {
                if (i >= enemyPositions.Length)
                {
                    break;
                }
                GameObject entity = Instantiate(enemyPrefab, enemyPositions[i]);
                _entityList.Add(entity.GetComponent<Entity>());
                _enemyIndexes.Add(i + nbAlly);
            }
        }

        private void Start()
        {
            List<Character> combattants = new List<Character>
        {
            new Character("Guerrier"),
            new Character("Mage"),
            new Character("Voleur"),
            new Character("Orc")
        };

            InitializeInitiative(combattants);
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
        /// Initialise l'ordre d'initiative et remplit la pile.
        /// </summary>
        public void InitializeInitiative(List<Character> characters)
        {
            // Attribuer une initiative aléatoire à chaque personnage
            foreach (var character in characters)
            {
                character.Initiative = character.RollInitiative();
            }

            // Trier du plus haut au plus bas
            characters.Sort((a, b) => a.Initiative.CompareTo(b.Initiative));

            // Remplir la pile avec l'ordre des initiatives
            initiativeStack.Clear();
            foreach (var character in characters)
            {
                initiativeStack.Push(character);
            }
        }

        public Character GetCharacter(int initiativeOrder)
        {
            return initiativeStack.ToArray()[initiativeOrder];
        }

        /// <summary>
        /// Lance le combat en affichant l'ordre d'initiative.
        /// </summary>
        public void StartCombat()
        {
            Debug.Log("Début du combat ! Ordre d'initiative :");

            Stack<Character> tempStack = new Stack<Character>(initiativeStack);
            while (tempStack.Count > 0)
            {
                Character chara = tempStack.Pop();
                Debug.Log($"{chara.Name} (Initiative: {chara.Initiative})");
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

            Character currentCharacter = initiativeStack.Pop();
            Debug.Log($"{currentCharacter.Name} joue son tour !");
        }

        /// <summary>
        /// Réinitialise la pile pour un nouveau round.
        /// </summary>
        private void ResetInitiative()
        {
            // Transformer la pile en liste temporaire et réinitialiser
            List<Character> tempList = new List<Character>(initiativeStack);
            InitializeInitiative(tempList);
        }

        /// <summary>
        /// Ajoute un nouveau combattant au combat.
        /// </summary>
        public void AddCharacterToInitiative(Character newCharacter)
        {
            newCharacter.Initiative = newCharacter.RollInitiative();

            List<Character> tempList = new List<Character>(initiativeStack) { newCharacter };
            InitializeInitiative(tempList);
        }

        public Stack<Character> GetInitiativeStack()
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

        /// <summary>
        /// Classe des combattants.
        /// </summary>
        public class Character
        {
            public string Name { get; private set; }
            public int Initiative { get; set; }

            public Character(string name)
            {
                Name = name;
            }

            /// <summary>
            /// Jette un dé pour déterminer l'initiative.
            /// </summary>
            public int RollInitiative()
            {
                return UnityEngine.Random.Range(1, 21); // Dé 20
            }
        }
    }
}
