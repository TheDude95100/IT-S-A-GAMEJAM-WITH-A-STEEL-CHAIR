using UnityEngine;

namespace Combat
{
    public class Entity : MonoBehaviour
    {
        [SerializeField]
        private int hitPoints = 0;

        [SerializeField]
        private int damage = 0;

        [SerializeField]
        private Transform standbyPosition;

        [SerializeField]
        private Transform attackPosition;

        public int HitPoints => hitPoints;
        public bool IsAlive { get { return hitPoints > 0; } }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        }

        /// <summary>
        /// Fonction to add or remove damage from an Entity.
        /// </summary>
        /// <param name="amount">Represent the amount of damage taken. Amount will be negative if healing is given.</param>
        public void TakeDamage(int amount)
        {
            hitPoints -= amount;
            Debug.Log(gameObject.name + " has " + hitPoints + " left.");
        }
    }
}
