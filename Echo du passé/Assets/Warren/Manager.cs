using UnityEngine;

namespace Combat
{
    public class Manager : MonoBehaviour
    {
        private Entity[] entityList;

        private void Awake()
        {
            entityList = FindObjectsByType<Entity>(FindObjectsSortMode.None);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            foreach(Entity entity in entityList)
            {
                Debug.Log(entity.gameObject.name);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnAction()
        {
            Debug.Log("Action taken");
            entityList[0].TakeDamage(1);
        }
    }
}
