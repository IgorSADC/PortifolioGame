using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

namespace Gameplay.MovementSystem.Behaviours
{
    //Adicionar usando reflection sem usar enum depois.
    public enum RigibodyBehaviours 
    {
        ChangeVelocity, 
        ClampVelocity
    }
    public class DefaultBehavioursRigidbody<C>
    {
        public MovementPipeline<C> TargetPipeline { get; set; }
        private static Dictionary<RigibodyBehaviours, MovementBehaviour> _mappingDict;
        public DefaultBehavioursRigidbody(MovementPipeline<C> pipeline)
        {
            TargetPipeline = pipeline;
            _mappingDict = new Dictionary<RigibodyBehaviours, MovementBehaviour>();
            _mappingDict.Add(RigibodyBehaviours.ChangeVelocity, ChangeVelocity);
            _mappingDict.Add(RigibodyBehaviours.ClampVelocity, ClampVelocity);
            
        }
        public void ComposeBehaviours(params RigibodyBehaviours[] behaviours)
        {
            foreach (var behaviour in behaviours)
            {
                TargetPipeline.movementPipeline += _mappingDict[behaviour];
            }
        }
        private void ChangeVelocity(ref Rigidbody target)
        {
            var dir = (Vector3) TargetPipeline.GetValueOfAttributeByName(DefaultKeys.DirectionKey);
            var maxSpeedChange = (float) TargetPipeline.GetValueOfAttributeByName(DefaultKeys.maxSpeedChange);
            var maxSpeed = (float) TargetPipeline.GetValueOfAttributeByName(DefaultKeys.maxSpeedKey);

            var desiredVelocity = dir * maxSpeed;
            var speed = target.velocity;
            speed.x = Mathf.MoveTowards(speed.x, desiredVelocity.x, maxSpeedChange);
            speed.z = Mathf.MoveTowards(speed.z, desiredVelocity.z, maxSpeedChange);
            target.velocity = speed;
        }

        private void ClampVelocity(ref Rigidbody target)
        {
            var maxSpeed = (float) TargetPipeline.GetValueOfAttributeByName(DefaultKeys.maxSpeedKey);
            target.velocity = Vector3.ClampMagnitude(target.velocity, maxSpeed);
        }
    }

}