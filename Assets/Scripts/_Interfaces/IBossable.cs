using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossable
{
    public int GetHealth();
    public int GetHitCount();

    public void BossAttack();

    public float GetAttackDuration();

    public bool isActive();
}
