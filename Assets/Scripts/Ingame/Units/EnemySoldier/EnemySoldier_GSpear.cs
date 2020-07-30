using UnityEngine;

public class EnemySoldier_GSpear : EnemySoldier
{
    private void Start()
    {
        _MoveSpeed = -1.2f;
        _AttackRange = Random.Range(2.7f, 3.5f); ;
        _AttackDelay = 1.5f;
        _AttackDamageDelay = 0.5f;
        base.Start();
    }
}
