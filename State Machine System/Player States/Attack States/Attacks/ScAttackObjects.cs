using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackNum", menuName = "Attacks")]
public class ScAttackObjects : ScriptableObject
{
    public AnimatorOverrideController _AttackAnimation;
    public float _AttackDamage;
}
