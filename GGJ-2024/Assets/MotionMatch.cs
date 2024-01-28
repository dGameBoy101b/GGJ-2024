using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionMatch : MonoBehaviour
{
    public float smoothness;
    public List<MotionMatchPair> motionMatchPairs;
    public List<RotationMatchPair> RotationMatchPairs;

    [Serializable] public struct MotionMatchPair
    {
        public Transform source;
        public Rigidbody rb;
    }
    
    [Serializable] public struct RotationMatchPair
    {
        public Transform source;
        public Transform target;
    }
    
    private void FixedUpdate()
    {
        foreach (MotionMatchPair motionMatchPair in motionMatchPairs)
        {
            Vector3 targetPosition = Vector3.Lerp(motionMatchPair.rb.position,motionMatchPair.source.position, smoothness );
            
            motionMatchPair.rb.Move(targetPosition, motionMatchPair.source.rotation);
        }

        foreach (var rotationMatchPair in RotationMatchPairs)
        {
            rotationMatchPair.target.localRotation = rotationMatchPair.source.localRotation;
        }
    }
}
