﻿using Assets.Scripts.AI.Entity;
using Assets.Scripts.AI.Entity.Behaviours;
using Assets.Scripts.World;

namespace Assets.Scripts.AI.GOAP.States {
    // Created by:
    // Eelco Eikelboom
    // S1080542
    public class GoapIdleState : AbstractState {
        private readonly Character.Character _character;
        private readonly EntityWanderBehaviour _wander;

        public GoapIdleState(GoapAgent agent) : base(agent) {
            _wander = new EntityWanderBehaviour(agent.Entity);
            _character = GameManager.Instance.Character;
        }

        public override void Enter() {
            _wander.Reset();
        }


        public override void Execute() {
            Agent.Entity.PlayAnimation(Animation.Walk);
            //Check whether there is a requested plan active
            if (Agent.ActionQueue.Count > 0)
                Agent.StateMachine.ChangeState(GoapStateMachine.StateType.Moving);
            //Check if the character is visible, if so, activate the plan
            else if (_character != null && Agent.Entity.Perspective.Visible(_character.gameObject))
                Agent.Planner.Plan(new GoapPlan(GoapCondition.InAttackRange, true));
            //No plan, just wander 
            else Agent.Entity.SetBehaviour(_wander);
        }
    }
}