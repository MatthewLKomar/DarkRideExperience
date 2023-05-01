using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float MaxThrowForce = 0.0f;

    struct ThrowData
    {
        public Vector3 ThrowVelocity;
        public float Angle;
        public float DeltaXZ;
        public float DeltaY;

        public ThrowData(Vector3 initialVelocity, float angle, float deltaXZ, float deltaY) : this()
        {
            InitialVelocity = initialVelocity;
            Angle = angle;
            DeltaXZ = deltaXZ;
            DeltaY = deltaY;
        }

        public Vector3 InitialVelocity { get; }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private ThrowData CalculateThrowData(Vector3 TargetPosition, Vector3 StartPosition)
    {
        // v == initial velociy, assumes max speed for now
        // x = distance to travel on x/z plane only
        // y = difference in altiudes from thrown point to target hit point
        // g = gravity

        Vector3 displacement = new Vector3(TargetPosition.x, TargetPosition.y, TargetPosition.z) - StartPosition;
        float deltaY = TargetPosition.y - StartPosition.y;
        float deltaXZ = displacement.magnitude; //TODO: What's magnitude?
        print($"Deltas: ({deltaXZ}, {deltaY})");

        //  find lowest initial launch velocity with other magic formula from wiipedia 
        // v^2 / g = y + ? (y^2 + x^2_
        // meaning... v = ?(g * (y + ? (y^2 + x^2)))

        print($"Gravity: {Physics.gravity.y}\r\nDeltaY: {deltaY}\r\rnDeltaXZ: {deltaXZ}");
        float gravity = Mathf.Abs(Physics.gravity.y); //makes sure gravity is always positive
        float throwStrength = Mathf.Clamp(
            Mathf.Sqrt(
                gravity 
                * (deltaY + Mathf.Sqrt(Mathf.Pow(deltaY, 2)
                + Mathf.Pow(deltaXZ, 2)))), 0.01f, MaxThrowForce);
        Debug.Log($"Minimum (clamped) throw strength: {throwStrength}");
        float angle = Mathf.PI / 2f - (0.5f * (Mathf.PI / 2 - (deltaY / deltaXZ)));
        Debug.Log($"Magic Throwing angle:{angle}");

        Vector3 initialVelocity =
            Mathf.Cos(angle) * throwStrength * displacement.normalized
            + Mathf.Sin(angle) * throwStrength * Vector3.up;
        Debug.Log($"Initial Velocity: {initialVelocity}");

        return new ThrowData(initialVelocity, angle, deltaXZ, deltaY);
    }
}
