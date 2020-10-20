using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay.MovementSystem
{

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float maxAcceleration;
        [SerializeField] [ObservableOnPipeline] private float maxSpeed;

        private Rigidbody rb;
        private MovementPipeline<PlayerMovement> movementPipeline;
        [ObservableOnPipeline] private Vector3 dir;
        [ObservableOnPipeline] private float maxSpeedChange;

        private void Awake() 
        {
            rb = this.GetComponent<Rigidbody>();
            movementPipeline = new MovementPipeline<PlayerMovement>(rb, this);
            maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;
        }

        private void Update() {
            dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        private void FixedUpdate() {
            movementPipeline.Execute();
        }

    }

}