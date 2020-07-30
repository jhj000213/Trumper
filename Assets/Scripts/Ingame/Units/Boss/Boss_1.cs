public class Boss_1 : Boss_Walk
{
    new private void Start()
    {
        _MoveSpeed = -3.0f;
        _AttackRange = 3.0f;
        _AttackDelay = 0.9f;
        _AttackDamageDelay = 0.2f;
        base.Start();
    }
}
