﻿using Assets.Scripts.pathfinding;
using UnityEngine;

namespace Assets.Scripts.AI.Entity.Behaviours {

    // Created by:
    // Eelco Eikelboom
    // S1080542
    public class EntityPathFollowingBehaviour : AbstractEntityBehaviour {
        public static int A = 0;
        private readonly Animation _animation;

        private readonly float _speed;
        private bool _reached;

        public EntityPathFollowingBehaviour(LivingEntity entity) : this(entity, entity.Speed, Animation.Run) { }

        public EntityPathFollowingBehaviour(LivingEntity entity, float speed, Animation animation) : base(entity) {
            _speed = speed;
            _reached = false;
            CurrentRequest = null;
            _animation = animation;
        }

        public Vector3[] Path { get; private set; }
        public int CurrentIndex { get; set; }
        public Vector3? CurrentRequest { get; private set; }

        // Moves through the calculated path
        public override Vector3 Update() {
            if (Path == null || Path.Length <= 0) {
                Entity.Rotate(Vector3.forward);
                return Entity.transform.position;
            }
            Entity.PlayAnimation(_animation);
            Vector3 target;
            // Check whether we're at the end of the path
            if (CurrentIndex == Path.Length) {
                target = CurrentRequest.Value;
                _reached = true;
            }
            else {
                // Update the target
                target = Path[CurrentIndex];
                Path[CurrentIndex].y = 0.0f;
                // Check whether we've passed the target
                if (Vector3.Distance(Entity.transform.position, target) < 0.1f)
                    CurrentIndex += 1;
            }
            target.y = 0.0f;
            var destination = Vector3.MoveTowards(Entity.transform.position, target,
                Time.deltaTime * _speed);
            //Rotate towards the target
            Entity.Rotate(destination);
            return destination;
        }

        public void UpdateRequest(Vector3 target) {
            CurrentIndex = 0;
            _reached = false;
            CurrentRequest = target;
            PathRequestManager.RequestPath(Entity.transform.position, target, OnPathFound);
        }

        private void OnPathFound(Vector3[] newPath, bool pathFound) {
            if (pathFound)
                Path = newPath;
        }

        public bool Reached() {
            return _reached;
        }
    }
}