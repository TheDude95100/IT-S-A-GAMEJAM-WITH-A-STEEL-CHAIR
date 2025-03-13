using UnityEngine;
using UnityEngine.SceneManagement;

public class Bar : InteractionSystem
{
    protected override void Interact()
    {
        Debug.Log(gameObject.name);
        //SceneManager.LoadScene("Bar");
    }
}
