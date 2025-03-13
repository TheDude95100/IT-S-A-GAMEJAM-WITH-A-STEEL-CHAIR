using UnityEngine;
using TMPro;

public class ActionMenu : MonoBehaviour
{
    [SerializeField] GameObject _actionMenu;
    [SerializeField] TextMeshProUGUI _characterNameHolder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            transform.gameObject.SetActive(true);
        }
    }
}
