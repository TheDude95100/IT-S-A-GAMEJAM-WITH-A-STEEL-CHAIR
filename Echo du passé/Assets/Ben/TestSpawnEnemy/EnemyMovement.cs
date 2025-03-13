using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public EntityData entityData;
    public float moveSpeed = 2f;
    public float moveRadius = 5f;
    public float waitTime = 2f;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private bool _isMoving = false;

    private Animator _animator;
    private Transform _spriteTransform;
    private Camera _mainCamera;
    private bool isAlreadyGoingLeft = false;

    void Start()
    {
        _startPosition = transform.position;
        _animator = GetComponent<Animator>();

        _spriteTransform = transform.Find("Sprite");
        _mainCamera = Camera.main;

        StartCoroutine(MovementRoutine());
    }

    void Update()
    {
        FaceCamera();
    }

    IEnumerator MovementRoutine()
    {
        while (true)
        {
            if (!_isMoving)
            {
                _targetPosition = _startPosition + new Vector3(
                    Random.Range(-moveRadius, moveRadius),
                    0,
                    Random.Range(-moveRadius, moveRadius)
                );
                SwitchSide();

                _isMoving = true;
                _animator.SetBool("isMoving", true);
                yield return StartCoroutine(MoveToTarget(_targetPosition));
                _isMoving = false;
                _animator.SetBool("isMoving", false);

                yield return new WaitForSeconds(waitTime);
            }
            yield return null;
        }
    }

    private void SwitchSide()
    {
        if (_targetPosition.x < transform.position.x)
        {
            if (!isAlreadyGoingLeft)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                isAlreadyGoingLeft = true;
            }
        }
        else
        {
            if (isAlreadyGoingLeft)
            {
                transform.localScale = new Vector3(1, 1, 1);
                isAlreadyGoingLeft = false;
            }
        }
    }

    IEnumerator MoveToTarget(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    void FaceCamera()
    {
        if (_spriteTransform != null && _mainCamera != null)
        {
            _spriteTransform.forward = _mainCamera.transform.forward;
        }
    }
}
