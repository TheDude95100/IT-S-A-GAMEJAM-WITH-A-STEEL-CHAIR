using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator animator;
    InputAction moveAction;
    private Transform _spriteTransform;
    private Camera _mainCamera;
    Vector3 moveDirection;
    int side = 1;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        _spriteTransform = transform.GetChild(0).transform.Find("UnitRoot");
        animator = transform.GetChild(0).transform.Find("UnitRoot").GetComponent<Animator>();
    }

    private void Update()
    {
        FaceCamera();
    }

    void FixedUpdate()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        moveDirection = new Vector3(moveValue.x, 0, moveValue.y);
        SwitchSide();
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        if (animator != null)
        {
            animator.SetBool("1_Move", moveDirection.magnitude > 0);
        }

    }

    void FaceCamera()
    {
        if (_spriteTransform != null && _mainCamera != null)
        {
            _spriteTransform.forward = _mainCamera.transform.forward;
        }
    }

    private void SwitchSide()
    {
        if (moveDirection.x < 0)
        {
            side = 1;
        }
        else if (moveDirection.x > 0)
        {
            side= -1;
        }
        _spriteTransform.transform.localScale = new Vector3(side, 1, 1);
    }
}
