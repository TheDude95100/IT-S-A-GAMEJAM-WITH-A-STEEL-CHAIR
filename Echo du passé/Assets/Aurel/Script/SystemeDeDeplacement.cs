using System;
using JetBrains.Annotations;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class SystemeDeDeplacement : MonoBehaviour
{
    InputAction moveAction;
    public float Speed = 5f;

    private void Start()
    {
        
        moveAction = InputSystem.actions.FindAction("Move");
    }
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>(); //format float(X,Y)  ou X va gérer notre déplacement 'latéral' (axe X) et Y le deplacement 'horizontal' (axe Z)
        Debug.Log(moveValue);
        
        Vector3 moveDirection = new Vector3(moveValue.x, 0, moveValue.y);
        transform.Translate(moveDirection * Speed * Time.deltaTime);


    }
}
