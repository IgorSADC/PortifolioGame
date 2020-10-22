using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.MovementSystem;
public class FollowTarget : MonoBehaviour
{
    public Transform target;

    public bool copyOffsetFromPosition = true;
    public Vector3 offset;
    [SerializeField] [ObservableOnPipeline] private float maxSpeed;
    [SerializeField] [ObservableOnPipeline] private float maxSpeedChange;
    [ObservableOnPipeline] private Vector3 dir;

    private Rigidbody rb;
    private MovementPipeline<FollowTarget> movementPipeline;


//Strangely this makes differente. You can see it by profiling the project. Unity doesn't holds the transform, it actually calls a method everytime you type transform.
    private Transform myTransformReference;
    

    private void Start() {
        myTransformReference = transform;
        if(copyOffsetFromPosition)
            offset = myTransformReference.position;
        rb = GetComponent<Rigidbody>();
        movementPipeline = new MovementPipeline<FollowTarget>(rb, this);
    }

    private void Update() {
        var targetPosition = target.position + offset;
        dir = targetPosition - myTransformReference.position;
    }
    private void FixedUpdate() {
        movementPipeline.Execute();
    }

}
