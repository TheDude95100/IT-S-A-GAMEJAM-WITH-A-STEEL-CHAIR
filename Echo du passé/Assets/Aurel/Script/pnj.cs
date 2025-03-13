using UnityEngine;

public class pnj : InteractionSystem
{
    protected override void Interact()
    {
        Debug.Log(gameObject.name);
    }
}
