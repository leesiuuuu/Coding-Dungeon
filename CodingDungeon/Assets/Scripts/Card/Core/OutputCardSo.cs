public abstract class OutputCardSo : AbstractCardSo
{
	public void EnqueueBattleAction(CardActionContext context, AbstractBattleAction battleAction) 
	{
		var repeatTimesProvider = context.ProviderGroup.RepeatTimesProvider;
		for (int i = 0; i < repeatTimesProvider.RepeatTimes; i++)
		{
			BattleManager.Instance.BattleActions.Enqueue(battleAction);
		}
		repeatTimesProvider.RepeatTimes = 1;
	}
}