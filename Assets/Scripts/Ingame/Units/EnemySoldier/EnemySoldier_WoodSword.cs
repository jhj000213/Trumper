using UnityEngine;

public class EnemySoldier_WoodSword : EnemySoldier
{
    private void Start()
    {
        _MoveSpeed = -1.8f;
        _AttackRange = Random.Range(2.2f, 2.7f); ;
        _AttackDelay = 1.3f;
        _AttackDamageDelay = 0.5f;
        base.Start();
    }
}
