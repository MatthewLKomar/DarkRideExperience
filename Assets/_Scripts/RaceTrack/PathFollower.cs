using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathFollower : MonoBehaviour
{
    public Racetrack Racetrack;
    public float Speed = 10.0f;
    public float Distance = 0.0f;
    public bool Loop = true;

    //MLKomar: this is a singleton because I am running into a bug and I want to go home and not deal with it rn
    public static PathFollower follower;

    private void Awake() {
        if (follower != null && follower != this) {
            Destroy(follower);
        } else {
            follower = this;
        }
    }


    private void FixedUpdate()
    {
        if (Racetrack == null) return;
        
        // Advance along racetrack
        Distance += Speed * Time.fixedDeltaTime;
        
        // Loop/clamp logic
        float length = Racetrack.Path.TotalLength;
        Distance = Loop ? Distance - Mathf.Floor(Distance / length) * length : Mathf.Clamp(Distance, 0, length);
        
        // Find segment for given z, and the Z offset from start of segment
        float segmentZOffset;
        RacetrackSegment segment = Racetrack.Path.GetSegmentAndOffset(Distance, out segmentZOffset);
        
        // Get transformation matrix for segment
        Matrix4x4 segmentToTrack = segment.GetSegmentToTrack(segmentZOffset);
        Matrix4x4 segmentToWorld = Racetrack.transform.localToWorldMatrix * segmentToTrack;
        
        // Position object on racetrack
        transform.position = segmentToWorld.MultiplyPoint(new Vector3(0.0f, 0.0f, segmentZOffset));
        transform.rotation = Quaternion.LookRotation(segmentToWorld.GetColumn(2), segmentToWorld.GetColumn(1));
    }
}
