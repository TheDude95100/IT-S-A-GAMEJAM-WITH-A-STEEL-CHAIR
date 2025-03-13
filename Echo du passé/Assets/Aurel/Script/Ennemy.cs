using UnityEngine;

public class Ennemy : InteractionSystem
{
    protected override void Interact()
    {
        Debug.Log(gameObject.name);
    }
}
