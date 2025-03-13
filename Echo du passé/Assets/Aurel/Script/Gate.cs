using UnityEngine;

public class Gate : InteractionSystem
{
    protected override void Interact()
    {
        Debug.Log(gameObject.name);
        
    }
}
