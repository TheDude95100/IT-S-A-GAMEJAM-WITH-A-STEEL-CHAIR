using UnityEngine;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }

    private Stack<Character> initiativeStack = new Stack<Character>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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
        characters.Sort((a, b) => b.Initiative.CompareTo(a.Initiative));

        // Remplir la pile avec l'ordre des initiatives
        initiativeStack.Clear();
        foreach (var character in characters)
        {
            initiativeStack.Push(character);
        }
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
            return Random.Range(1, 21); // Dé 20
        }
    }
}
