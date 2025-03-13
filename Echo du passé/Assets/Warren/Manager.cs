using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Combat
{
    public class Manager : MonoBehaviour
    {
        [SerializeField]
        private int nbAlly;
        [SerializeField]
        private int nbEnemy;

        [SerializeField]
        private GameObject allyPrefab;
        [SerializeField]
        private GameObject enemyPrefab;

        [SerializeField]
        private Transform[] allyPositions;
        [SerializeField]
        private Transform allyActionExecutionPosition;

        [SerializeField]
        private Transform[] enemyPositions;
        [SerializeField]
        private Transform enemyActionExecutionPosition;

        private List<Entity> _entityList;

        private List<int> _enemyIndexes,
                          _allyIndexes;

        private int _selected;

        private void Awake()
        {
            _enemyIndexes = new List<int>();
            _allyIndexes = new List<int>();
            _entityList = new List<Entity>();

            for(int i = 0; i < nbAlly; i++)
            {
                if(i >= allyPositions.Length)
                {
                    break;
                }
                GameObject entity = Instantiate(allyPrefab, allyPositions[i]);
                _entityList.Add(entity.GetComponent<Entity>());
            }

            for (int i = 0; i < nbEnemy; i++)
            {
                if (i >= enemyPositions.Length)
                {
                    break;
                }
                GameObject entity = Instantiate(enemyPrefab, enemyPositions[i]);
                _entityList.Add(entity.GetComponent<Entity>());
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            for(int counter = 0; counter < _entityList.Count; counter++)
            {
                Debug.Log(_entityList[counter].gameObject.name);

                if(_entityList[counter].CompareTag("Player"))
                {
                    _allyIndexes.Add(counter);
                }
                else
                {
                    _enemyIndexes.Add(counter);
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
            _entityList[_enemyIndexes[_selected]].TakeDamage(1);
        }

        public void OnSelectionRight()
        {
            _selected++;
            if(_selected >= _enemyIndexes.Count)
            {
                ResetSelection();
            }
            //Debug.Log(entityList[enemyIndexes[selected]].gameObject.name + " selected");
        }


        public void OnSelectionLeft()
        {
            _selected--;
            if (_selected < 0)
            {
                _selected = _enemyIndexes.Count - 1;
            }
            //Debug.Log(entityList[enemyIndexes[selected]].gameObject.name + " selected");
        }

        public void ResetSelection()
        {
            _selected = 0;
        }

        private bool VerifyWin()
        {
            int nbDeath = 0;
            foreach(int index in _enemyIndexes)
            {
                if (!_entityList[index].IsAlive)
                {
                    nbDeath++;
                }
            }

            if(nbDeath == _enemyIndexes.Count)
            {
                return true;
            }

            return false;
        }

        private bool VerifyLose()
        {
            int nbDeath = 0;
            foreach (int index in _allyIndexes)
            {
                if (!_entityList[index].IsAlive)
                {
                    nbDeath++;
                }
            }

            if (nbDeath == _allyIndexes.Count)
            {
                return true;
            }

            return false;
        }
    }
}
