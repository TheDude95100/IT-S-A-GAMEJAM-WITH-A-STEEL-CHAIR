using UnityEngine;

public class BossDoor : InteractionSystem
{
    protected override void Interact()
    {
        Debug.Log(gameObject.name);
    }
}
