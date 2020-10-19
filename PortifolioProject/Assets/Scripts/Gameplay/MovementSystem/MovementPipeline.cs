using System;
using System.Collections;
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
    public delegate void MovementBehaviour<T>(ref T referenceVariable);

    /// <summary>
    /// This class composes a series of behaviours (callbacks) responsable to make the character move.
    /// </summary>
    public class MovementPipeline<T, C>
    {
        private event MovementBehaviour<T> movementPipeline;
        private T target;
        private C caller;

        public MovementPipeline(T target, C caller)
        {
            this.target = target;
            this.caller = caller;

            //var flags = BindingFlags.GetField | BindingFlags.NonPublic;
            var fields = caller.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance );
            var observableFields = fields.Where(f => CheckForAttribute(f));

            foreach (var item in observableFields)
            {
                Debug.Log(item);
            }
                                                      
        }

        private bool CheckForAttribute(FieldInfo f)
        {
            return Attribute.GetCustomAttribute(f, typeof(DirectionFieldAttribute)) != null;
        }

        public void ComposeBehaviour(MovementBehaviour<T> newBehaviour)
        {
            movementPipeline += newBehaviour;
        
        }
        public void RemoveBehaviour(MovementBehaviour<T> oldBehaviour)
        {
            movementPipeline -= oldBehaviour;
        
        }

        public void Execute(){
            movementPipeline(ref target);
        }


    }

}