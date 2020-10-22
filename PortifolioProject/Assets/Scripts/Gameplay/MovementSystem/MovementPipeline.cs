using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Gameplay.MovementSystem
{
    /// <summary>
    /// This callback represents a modular piece of the movement system.
    /// </summary>
    public delegate void MovementBehaviour(ref Rigidbody referenceVariable);

    /// <summary>
    /// This class composes a series of behaviours (callbacks) responsable to make the character move. The system is organized by events instead of nodes.
    /// The limitation is that functions must have the same signature.
    /// I am going to build a node based one on the future.
    /// </summary>
    public class MovementPipeline<C>
    {
        public event MovementBehaviour movementPipeline;
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
        }

        private bool CheckForAttribute(FieldInfo f)
        {
            return Attribute.GetCustomAttribute(f, typeof(ObservableOnPipelineAttribute)) != null;
        }

        public void Execute(){
            movementPipeline(ref target);
        }

        public object GetValueOfAttributeByName(string attr)
        {
            return AttributesTranslation[attr].GetValue(caller);
        }

    }

}