public class EnemyCastle : Castle
{
    private void Start()
    {
        _UnitName = "enemycastle";
        _MyCastle = false;
        _MaxHP = 20.0f;
        base.Start();
    }


}
