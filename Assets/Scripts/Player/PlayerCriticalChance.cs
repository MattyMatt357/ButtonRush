using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalculateDamage
{
    public class PlayerCriticalChance : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Calculates the chance of dealing critical or normal damage
        /// </summary>
        /// <param name="randomNum"></param>
        /// <param name="critChance"></param>
        /// <param name="baseDamage"></param>
        /// <param name="damageMultiplier"></param>
        /// <returns></returns>
        public static float WeaponDamageChance(int randomNum, int critChance, float baseDamage, float damageMultiplier)
        {
            if (randomNum >= critChance)
            {
                return baseDamage * damageMultiplier;
            }
            else
            {
                return baseDamage;
            }
        }
    }
}
