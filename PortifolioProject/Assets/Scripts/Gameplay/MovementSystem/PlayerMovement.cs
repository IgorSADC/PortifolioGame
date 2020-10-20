using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Extensions;

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

        public float rotateSpeed = 5f;

        private Camera mainCamera;

        private void Awake() 
        {
            rb = this.GetComponent<Rigidbody>();
            movementPipeline = new MovementPipeline<PlayerMovement>(rb, this);
            maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;
            mainCamera = Camera.main;
        }

        private void Update()
        {
            dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            RotateToMouse();
        }

    //PUT THIS ON THE MOVEMENT PIPELINE LATER
        private void RotateToMouse()
        {
            var step = rotateSpeed * Time.deltaTime;
            var diretion = mainCamera.GetMousePosition() - transform.position;
            diretion.y = 0;

            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,
                                                        diretion,
                                                        step, 1f));
        }

        private void FixedUpdate() {
            movementPipeline.Execute();
        }

    }

}