using System;

/// <summary>
/// Class made to replicate random dice rolls.
/// </summary>
public class Dice
{
    //Variables
    private static Random rand = new Random();

    //Number of faces on the dice
    private byte _nbOfSide;

    //Value of the face the dice is on
    private byte _value;

    /// <summary>
    /// Enum to constrict the selection of sides the dices can have.
    /// </summary>
    public enum DiceType : byte
    {
        D3 = 3,
        D4 = 4,
        D6 = 6,
        D8 = 8,
        D10 = 10,
        D12 = 12,
        D20 = 20,
        D100 = 100
    }

    //Constructors

    /// <summary>
    /// Default constructor the dice, making a 6-sided dice.
    /// </summary>
    public Dice()
    {
        this._nbOfSide = (byte)DiceType.D6;
        this._value = 1;
    }

    /// <summary>
    /// Constructor for a dice other than a D6.
    /// </summary>
    /// <param name="nbOfSide">Number of side the dice will have.</param>
    public Dice(DiceType nbOfSide)
    {
        this._nbOfSide = (byte)nbOfSide;
        this._value = 1;
    }

    //Getter

    /// <summary>
    /// Get the value of the dice.
    /// </summary>
    /// <returns>Return the value of the dice.</returns>
    public int GetValue()
    {
        return this._value;
    }

    //Methods

    /// <summary>
    /// Randomise the value of that would be displayed on the top side of the dice.
    /// </summary>
    /// <returns>Return the result from roll of the dice.</returns>
    public int ThrowDice()
    {
        this._value = (byte)rand.Next(1, this._nbOfSide + 1);

        return this._value;
    }
}
