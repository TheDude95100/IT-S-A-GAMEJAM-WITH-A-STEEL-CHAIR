using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Combat
{
    public class Manager : MonoBehaviour
    {
        private Entity[] entityList;

        private List<int> enemyIndexes,
                          allyIndexes;

        private int selected;

        private void Awake()
        {
            entityList = FindObjectsByType<Entity>(FindObjectsSortMode.None);
            enemyIndexes = new List<int>();
            allyIndexes = new List<int>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            for(int counter = 0; counter < entityList.Length; counter++)
            {
                Debug.Log(entityList[counter].gameObject.name);

                if(entityList[counter].CompareTag("Player"))
                {
                    allyIndexes.Add(counter);
                }
                else
                {
                    enemyIndexes.Add(counter);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            if(VerifyWin())
            {
                Debug.Log("You win");
            }
            if (VerifyLose())
            {
                Debug.Log("You lost");
            }
        }

        public void OnAction()
        {
            Debug.Log("Action taken");
            entityList[enemyIndexes[selected]].TakeDamage(1);
        }

        public void OnSelectionRight()
        {
            selected++;
            if(selected >= enemyIndexes.Count)
            {
                ResetSelection();
            }
            //Debug.Log(entityList[enemyIndexes[selected]].gameObject.name + " selected");
        }

        public void OnSelectionLeft()
        {
            selected--;
            if (selected < 0)
            {
                selected = enemyIndexes.Count - 1;
            }
            //Debug.Log(entityList[enemyIndexes[selected]].gameObject.name + " selected");
        }

        public void ResetSelection()
        {
            selected = 0;
        }

        private bool VerifyWin()
        {
            int nbDeath = 0;
            foreach(int index in enemyIndexes)
            {
                if (!entityList[index].IsAlive)
                {
                    nbDeath++;
                }
            }

            if(nbDeath == enemyIndexes.Count)
            {
                return true;
            }

            return false;
        }

        private bool VerifyLose()
        {
            int nbDeath = 0;
            foreach (int index in allyIndexes)
            {
                if (!entityList[index].IsAlive)
                {
                    nbDeath++;
                }
            }

            if (nbDeath == allyIndexes.Count)
            {
                return true;
            }

            return false;
        }
    }
}
