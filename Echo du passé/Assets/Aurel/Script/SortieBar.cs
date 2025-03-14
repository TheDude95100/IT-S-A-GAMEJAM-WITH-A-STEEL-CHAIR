using UnityEngine;
using UnityEngine.SceneManagement;

public class SortieBar : InteractionSystem
{
    protected override void Interact()
    {
        Debug.Log(gameObject.name);
        SceneManager.LoadScene("Village");
    }
}
