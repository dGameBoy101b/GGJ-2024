using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionMatch : MonoBehaviour
{
    public List<MotionMatchPair> motionMatchPairs;
    public float smoothness;

    [Serializable] public struct MotionMatchPair
    {
        public Transform source;
        public Rigidbody rb;
    }
    
    private void FixedUpdate()
    {
        foreach (MotionMatchPair motionMatchPair in motionMatchPairs)
        {
            Vector3 targetPosition = Vector3.Lerp(motionMatchPair.rb.position,motionMatchPair.source.position, smoothness );
            
            motionMatchPair.rb.Move(targetPosition, motionMatchPair.source.rotation);
        }
    }
}
