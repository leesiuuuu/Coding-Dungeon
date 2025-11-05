using UnityEngine;

[CreateAssetMenu(menuName = "Coding Dungeon/Output Card/Sweep")]
public class SweepOutputCard : AbstractOuputCardSo<OutputCardActionParams>
{
	protected override void StartActionInternal(OutputCardActionParams param)
	{
		param.User.ExamplePerform();
		param.Target[0].Attacked();
	}
}