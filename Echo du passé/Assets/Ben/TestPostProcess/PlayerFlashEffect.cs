using UnityEngine;
using System.Collections;

public class PlayerFlashEffect : MonoBehaviour
{
    public Color flashColor = Color.red;  // Couleur du clignotement
    public float flashDuration = 0.2f;   // Durée du clignotement
    private SpriteRenderer[] spriteRenderers; // Liste des sprites

    private void Start()
    {
        // Récupère tous les SpriteRenderer de l'objet et de ses enfants
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void Flash()
    {
        Debug.Log("flash");
        StartCoroutine(FlashEffect());
    }

    private IEnumerator FlashEffect()
    {
        // Stocker les couleurs originales
        Color[] originalColors = new Color[spriteRenderers.Length];
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            originalColors[i] = spriteRenderers[i].color;
            spriteRenderers[i].color = flashColor; // Applique la couleur rouge
        }

        yield return new WaitForSeconds(flashDuration);

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = originalColors[i];
        }

        yield return new WaitForSeconds(flashDuration);

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = flashColor;
        }

        yield return new WaitForSeconds(flashDuration);

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = originalColors[i];
        }

        yield return new WaitForSeconds(flashDuration);

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = flashColor;
        }

        yield return new WaitForSeconds(flashDuration);

        // Restaure les couleurs originales
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = originalColors[i];
        }
    }
}
