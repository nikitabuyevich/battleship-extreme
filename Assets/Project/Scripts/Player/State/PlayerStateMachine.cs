using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{

	private IState currentlyRunningState;
	private IState previouslyRunningState;

	public void ChangeState(IState newState)
	{
		if (this.currentlyRunningState != null)
		{
			this.currentlyRunningState.Exit();
		}
		this.previouslyRunningState = this.currentlyRunningState;
		this.currentlyRunningState = newState;
		this.currentlyRunningState.Enter();
	}

	public void ExecuteStateUpdate()
	{
		if (this.currentlyRunningState != null)
		{
			this.currentlyRunningState.Execute();
		}
	}

	public void SwitchToPreviouslyRunningState()
	{
		this.currentlyRunningState.Exit();
		this.currentlyRunningState = this.previouslyRunningState;
		this.currentlyRunningState.Enter();
	}
}