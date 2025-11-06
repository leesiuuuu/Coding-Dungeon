using UnityEngine;

public class SkeletonSword : Enemy
{
	protected override void Initialize()
	{
		base.Initialize();
		var pathFind = new SkeletonSwordPathFind(this);
		var attack = new SkeletonSwordAttack(this);
		var prepareAttack = new SkeletonSwordPrepareAttack(this);
		_fsm.RegisterState("PathFind", pathFind);
		_fsm.RegisterState("Attack",attack);
		_fsm.RegisterState("PrepareAttack",prepareAttack);
	}

	protected override void Decision()
	{
		
	}
	
	private class SkeletonSwordPathFind:IState
	{
		private Enemy _enemy;
		public SkeletonSwordPathFind(Enemy enemy)
		{
			this._enemy = enemy;
		}

		public void Enter()
		{
			
		}

		public void Execute()
		{
			
		}
		public void FixedExecute()
		{
			
		}

		public void Exit()
		{
			
		}
	}
	private class SkeletonSwordPrepareAttack:IState
	{
		private Enemy _enemy;
		public SkeletonSwordPrepareAttack(Enemy enemy)
		{
			this._enemy = enemy;
		}

		public void Enter()
		{
			
		}

		public void Execute()
		{
			
		}
		public void FixedExecute()
		{
			
		}

		public void Exit()
		{
			
		}
	}
	private class SkeletonSwordAttack:IState
	{
		private Enemy _enemy;
		public SkeletonSwordAttack(Enemy enemy)
		{
			this._enemy = enemy;
		}

		public void Enter()
		{
			
		}

		public void Execute()
		{
			
		}
		public void FixedExecute()
		{
			
		}

		public void Exit()
		{
			
		}
	}
}