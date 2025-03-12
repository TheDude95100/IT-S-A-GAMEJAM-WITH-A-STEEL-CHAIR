using System;
using JetBrains.Annotations;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    InputAction moveAction;
    public float Speed = 5f;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }
    void FixedUpdate()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>(); //format float(X,Y)  ou X va gérer notre déplacement 'latéral' (axe X) et Y le deplacement 'horizontal' (axe Z)
        //Debug.Log(moveValue);
        
        Vector3 moveDirection = new Vector3(moveValue.x, 0, moveValue.y);
        transform.Translate(moveDirection * Speed * Time.deltaTime);


    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        Debug.Log("Collided with " + tag);
        /*switch (tag)
        {
            case "Ennemy": SceneManager.LoadScene("placeholder"); break;
            case "Gate" : SceneManager.LoadScene("placeholder"); break;
            case "Interactable_PNJ": SceneManager.LoadScene("placeholder");break;
        }*/
    }
}