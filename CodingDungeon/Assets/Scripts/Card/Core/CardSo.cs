public abstract class CardSo<TParam> : AbstractCardSo
{
	public override void StartAction(object param)
	{
		StartActionInternal((TParam)param);
	}

	protected abstract void StartActionInternal(TParam param);
}
