public struct PossibleMove
{
    public Modifier Modifier;
    public bool ShootEnemy;
    public bool UseModifier;

    public PossibleMove(Modifier modifier, bool shootEnemy, bool useModifier)
    {
        this.Modifier = modifier;
        this.ShootEnemy = shootEnemy;
        this.UseModifier = useModifier;
    }
}
