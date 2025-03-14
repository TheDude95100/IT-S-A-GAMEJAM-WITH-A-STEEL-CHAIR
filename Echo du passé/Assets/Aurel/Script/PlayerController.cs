using System;
using JetBrains.Annotations;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    InputAction moveAction;
    public float Speed = 5f;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }
    void FixedUpdate()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>(); //format float(X,Y)  ou X va g�rer notre d�placement 'lat�ral' (axe X) et Y le deplacement 'horizontal' (axe Z)
        //Debug.Log(moveValue);
        
        Vector3 moveDirection = new Vector3(moveValue.x, 0, moveValue.y);
        transform.Translate(moveDirection * Speed * Time.deltaTime);


    }

    private void OnTriggerEnter(Collider collider)
    {

        string tag = collider.gameObject.tag;
        /*switch (tag)
        {
            case "Ennemy": //SceneManager.LoadScene("placeholder"); 
                Debug.Log("enter" + tag);           
                break;
            case "Gate": //SceneManager.LoadScene("placeholder");
                Debug.Log("enter" + tag);
                break;
            case "Interactable_PNJ": //SceneManager.LoadScene("placeholder");
                Debug.Log("enter" + tag);
                break;
            case "Boss_Gate": //SceneManager.LoadScene("placeholder");
                Debug.Log("Boss Gate");
                break;
        }*/
    }
}