public struct PossibleMove
{
    public int MoveIndex;
    public Modifier Modifier;
    public bool ShootEnemy;
    public bool UseModifier;

    public PossibleMove(int moveIndex, Modifier modifier, bool shootEnemy, bool useModifier)
    {
        this.MoveIndex = moveIndex;
        this.Modifier = modifier;
        this.ShootEnemy = shootEnemy;
        this.UseModifier = useModifier;
    }
}
