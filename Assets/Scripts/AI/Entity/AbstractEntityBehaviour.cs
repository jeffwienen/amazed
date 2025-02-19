﻿using UnityEngine;

namespace Assets.Scripts.AI.Entity.Behaviours {
    public abstract class AbstractEntityBehaviour {
        // Created by:
        // Eelco Eikelboom
        // S1080542
        protected LivingEntity Entity;

        protected AbstractEntityBehaviour(LivingEntity entity) {
            Entity = entity;
            RotationSpeed = 8f;
        }

        public float RotationSpeed { get; protected set; }

        public abstract Vector3 Update();
    }
}