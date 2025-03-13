using UnityEngine;
using UnityEngine.SceneManagement;

public class bar : InteractionSystem
{
    protected override void Interact()
    {
        Debug.Log(gameObject.name);
        //SceneManager.LoadScene("Bar");
    }
}
