using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay.MovementSystem
{

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float maxAcceleration;
        [SerializeField] private float maxSpeed;

        private Rigidbody rb;
        private MovementPipeline<Rigidbody, PlayerMovement> movementPipeline;
        [DirectionField] public Vector3 dir;
        private float maxSpeedChange;

        private void Awake() 
        {
            rb = this.GetComponent<Rigidbody>();
            movementPipeline = new MovementPipeline<Rigidbody, PlayerMovement>(rb, this);
            movementPipeline.ComposeBehaviour(ChangeVelocity);
            movementPipeline.ComposeBehaviour(ClampVelocity);
            maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;
        }

        private void Update() {
            dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        private void FixedUpdate() {
            movementPipeline.Execute();
        }

#region FunctionsComposingThePipeline
        private void ChangeVelocity(ref Rigidbody target)
        {
            var desiredVelocity = dir * maxSpeed;
            var speed = target.velocity;
            speed.x = Mathf.MoveTowards(speed.x, desiredVelocity.x, maxSpeedChange);
            speed.z = Mathf.MoveTowards(speed.z, desiredVelocity.z, maxSpeedChange);
            target.velocity = speed;
        }

        private void ClampVelocity(ref Rigidbody target)
        {
            target.velocity = Vector3.ClampMagnitude(target.velocity, maxSpeed);
        }


#endregion
    }

}