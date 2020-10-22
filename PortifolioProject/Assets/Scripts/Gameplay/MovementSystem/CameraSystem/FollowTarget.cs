using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.MovementSystem;
using Gameplay.MovementSystem.Behaviours;

public class FollowTarget : MonoBehaviour
{
    public Transform target;

    public bool copyOffsetFromPosition = true;
    public Vector3 offset;
    [SerializeField] [ObservableOnPipeline] private float maxSpeed;
    [SerializeField] [ObservableOnPipeline] private float maxSpeedChange;
    [ObservableOnPipeline] private Vector3 dir;

    private Rigidbody rb;
    private MovementPipeline<Rigidbody,FollowTarget> movementPipeline;
    private DefaultBehavioursRigidbody<FollowTarget> behavioursRigidbody;


//Strangely this makes differente. You can see it by profiling the project. Unity doesn't holds the transform, it actually calls a method everytime you type transform.
    private Transform myTransformReference;
    

    private void Start() {
        myTransformReference = transform;
        if(copyOffsetFromPosition)
            offset = myTransformReference.position;
        rb = GetComponent<Rigidbody>();
        movementPipeline = new MovementPipeline<Rigidbody,FollowTarget>(rb, this);
        behavioursRigidbody = new DefaultBehavioursRigidbody<FollowTarget>(movementPipeline);
        behavioursRigidbody.ComposeBehaviours(RigibodyBehaviours.ChangeVelocity,
                                            RigibodyBehaviours.ClampVelocity);
    }

    private void Update() {
        var targetPosition = target.position + offset;
        dir = targetPosition - myTransformReference.position;
    }
    private void FixedUpdate() {
        movementPipeline.Execute();
    }

}
