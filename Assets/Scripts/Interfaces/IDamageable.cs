using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void ReceiveDamage(float damage);
    void ReceiveDamage(float damage, ButtonDamageType buttonDamageTypes, bool isCritHit);
}
