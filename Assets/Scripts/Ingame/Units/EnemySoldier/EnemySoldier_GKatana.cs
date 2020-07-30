using UnityEngine;

public class EnemySoldier_GKatana : EnemySoldier
{
    private void Start()
    {
        _MoveSpeed = -1.8f;
        _AttackRange = Random.Range(2.4f, 3.0f);
        _AttackDelay = 1.2f;
        _AttackDamageDelay = 0.5f;
        base.Start();
    }
}
