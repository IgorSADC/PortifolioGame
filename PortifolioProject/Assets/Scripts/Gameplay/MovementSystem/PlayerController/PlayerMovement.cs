﻿using UnityEngine;
using Utils.Extensions; //For mouse position on main camera
using Gameplay.MovementSystem;
using Gameplay.MovementSystem.Behaviours;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxAcceleration =1f;
    [SerializeField] [ObservableOnPipeline] private float maxSpeed;
    [ObservableOnPipeline] private Vector3 dir;
    [ObservableOnPipeline] private float maxSpeedChange;
    public float rotateSpeed = 5f;

    private Rigidbody rb;
    private MovementPipeline<Rigidbody,PlayerMovement> movementPipeline;
    private DefaultBehavioursRigidbody<PlayerMovement> behavioursRigidbody;

    private Camera mainCamera;

    private void Awake() 
    {
        rb = this.GetComponent<Rigidbody>();
        movementPipeline = new MovementPipeline<Rigidbody,PlayerMovement>(rb, this);
        behavioursRigidbody = new DefaultBehavioursRigidbody<PlayerMovement>(movementPipeline);
        behavioursRigidbody.ComposeBehaviours(RigibodyBehaviours.ChangeVelocity,
                                            RigibodyBehaviours.ClampVelocity);

        maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;
        mainCamera = Camera.main;

    }

    private void Update()
    {
        dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        dir = Vector3.ClampMagnitude(dir, 1);
        RotateToMouse();
    }
    private void FixedUpdate() {
        movementPipeline.Execute();
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

}

