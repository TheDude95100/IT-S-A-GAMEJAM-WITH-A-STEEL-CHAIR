using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour
{
    private Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = transform.GetChild(0).transform.Find("UnitRoot").GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.anyKeyDown) // Vérifie si une touche est pressée
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                PlayAnimation(1);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                PlayAnimation(2);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                PlayAnimation(3);
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                PlayAnimation(4);
        }
    }
    void PlayAnimation(int index)
    {
        switch (index)
        {
            case 1:
                _animator.Play("ATTACK");
                break;
            case 2:
                _animator.Play("DAMAGED");
                break;
            case 3:
                _animator.Play("DEATH");
                break;
            case 4:
                _animator.Play("OTHER");
                break;
            default:
                _animator.Play("IDLE");
                break;
        }
    }

}
