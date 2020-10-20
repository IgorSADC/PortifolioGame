using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Gameplay.MovementSystem
{

    // IMPLEMENTAR REFLECTION PARA INJETAR VARIAVEIS IMPORTANTES DIRETAMENTE NESTA CLASSE

    /// <summary>
    /// This callback represents a modular piece of the movement system.
    /// </summary>
    public delegate void MovementBehaviour(ref Rigidbody referenceVariable);

    /// <summary>
    /// This class composes a series of behaviours (callbacks) responsable to make the character move.
    /// </summary>
    public class MovementPipeline<C>
    {
        private event MovementBehaviour movementPipeline;
        private Rigidbody target;
        private C caller;
        private IEnumerable<FieldInfo> observableFields;
        public Dictionary<string, FieldInfo> AttributesTranslation;

        public MovementPipeline(Rigidbody target, C caller)
        {
            this.target = target;
            this.caller = caller;

            //var flags = BindingFlags.GetField | BindingFlags.NonPublic;
            var fields = caller.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance );
            observableFields = fields.Where(f => CheckForAttribute(f));

            AttributesTranslation = new Dictionary<string, FieldInfo>();
            foreach (var field in observableFields)
            {
                
                AttributesTranslation.Add(field.Name, field);
            }

            movementPipeline += ChangeVelocity;
            movementPipeline += ClampVelocity;
                                                      
        }

        private bool CheckForAttribute(FieldInfo f)
        {
            return Attribute.GetCustomAttribute(f, typeof(ObservableOnPipelineAttribute)) != null;
        }

        public void Execute(){
            movementPipeline(ref target);
        }

        private object GetValueOfAttribute(string attr)
        {
            return AttributesTranslation[attr].GetValue(caller);
        }

#region DefaultBahaviours
        private void ChangeVelocity(ref Rigidbody target)
        {
            var dir = (Vector3) GetValueOfAttribute(DefaultKeys.DirectionKey);
            var maxSpeedChange = (float) GetValueOfAttribute(DefaultKeys.maxSpeedChange);
            var maxSpeed = (float) GetValueOfAttribute(DefaultKeys.maxSpeedKey);

            var desiredVelocity = dir * maxSpeed;
            var speed = target.velocity;
            speed.x = Mathf.MoveTowards(speed.x, desiredVelocity.x, maxSpeedChange);
            speed.z = Mathf.MoveTowards(speed.z, desiredVelocity.z, maxSpeedChange);
            target.velocity = speed;
        }

        private void ClampVelocity(ref Rigidbody target)
        {
            var maxSpeed = (float) GetValueOfAttribute(DefaultKeys.maxSpeedKey);
            target.velocity = Vector3.ClampMagnitude(target.velocity, maxSpeed);
        }
#endregion

    }

}