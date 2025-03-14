using System.Collections;
using System.Net;
using UnityEngine;

public class CombatMovementManager : MonoBehaviour
{
    [SerializeField] private CombatManager _combatManager;
    private Animator _animator;
    [SerializeField] private float moveSpeed = 2f;

    void Awake()
    {
        _combatManager = FindFirstObjectByType<CombatManager>();
        _combatManager.OnEntityMovementGo += CombatMovementManager_MovementGo;
        _combatManager.OnEntityMovementReturn += CombatMovementManager_MovementReturn;
    }

    private void CombatMovementManager_MovementGo(Entity entity, Transform posStart, Transform posFinish)
    {
        StartCoroutine(MoveCoroutineGo(entity, posStart.position, posFinish.position));

    }

    private void CombatMovementManager_MovementReturn(Entity entity, Transform posStart, Transform posFinish)
    {
        StartCoroutine(MoveCoroutineReturn(entity, posStart.position, posFinish.position));

    }

    IEnumerator MoveCoroutineGo(Entity entity, Vector3 posStart, Vector3 posFinish)
    {

        _animator = entity.transform.GetChild(0).transform.Find("UnitRoot").GetComponent<Animator>();
        _animator.SetBool("1_Move", true);
        while (Vector3.Distance(entity.transform.position, posFinish) > 0.01f)
        {
            entity.transform.position = Vector3.MoveTowards(entity.transform.position, posFinish, moveSpeed * Time.deltaTime);
            yield return null;
        }
        entity.transform.position = posFinish;
        _animator.SetBool("1_Move", false);
    }

    IEnumerator MoveCoroutineReturn(Entity entity, Vector3 posStart, Vector3 posFinish)
    {
        while(entity.transform.position != posStart)
        {
            yield return null;
        }
        _animator.SetTrigger("2_Attack");
        yield return new WaitForSeconds(1);
        _animator.SetBool("1_Move", true);
        entity.transform.localScale = new Vector3(-1, 1, 1);
        while (Vector3.Distance(entity.transform.position, posFinish) > 0.01f)
        {
            entity.transform.position = Vector3.MoveTowards(entity.transform.position, posFinish, moveSpeed * Time.deltaTime);
            yield return null;
        }
        _animator.SetBool("1_Move", false);
        entity.transform.localScale = new Vector3(1, 1, 1);

    }
}
