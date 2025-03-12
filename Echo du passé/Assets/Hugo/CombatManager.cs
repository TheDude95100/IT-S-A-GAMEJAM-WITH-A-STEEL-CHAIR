using UnityEngine;
using System;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{

    private Stack<Character> initiativeStack = new Stack<Character>();
    public event EventHandler OnInitiativeRolled;

    public int CurrentNumberOfCharacters => initiativeStack.Count;

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
        // Attribuer une initiative al�atoire � chaque personnage
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
        Debug.Log("D�but du combat ! Ordre d'initiative :");

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
            Debug.Log("Fin du round, r�initialisation de l'ordre d'initiative.");
            ResetInitiative();
        }

        Character currentCharacter = initiativeStack.Pop();
        Debug.Log($"{currentCharacter.Name} joue son tour !");
    }

    /// <summary>
    /// R�initialise la pile pour un nouveau round.
    /// </summary>
    private void ResetInitiative()
    {
        // Transformer la pile en liste temporaire et r�initialiser
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
        /// Jette un d� pour d�terminer l'initiative.
        /// </summary>
        public int RollInitiative()
        {
            return UnityEngine.Random.Range(1, 21); // D� 20
        }
    }
}
