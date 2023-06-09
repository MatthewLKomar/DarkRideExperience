﻿using System;
using UnityEngine;

/// <summary>
/// A single curve within a Racetrack.
/// RacetrackCurves are created as immediate children to the Racetrack object which manages
/// laying them out and generating the track meshes from them.
/// </summary>
[SelectionBase]
public class RacetrackCurve : MonoBehaviour {

    [Header("Mode")]
    public RacetrackCurveType Type = RacetrackCurveType.Arc;

    [Header("Bezier")]
    [Tooltip("The end point of the Bezier curve")]
    public Vector3 EndPosition;

    [Tooltip("Distance from the start point to its control point. (As a fraction of the distance between the start and end points)")]
    [Range(0.0f, 1.0f)]
    public float StartControlPtDist = 0.35f;

    [Tooltip("Distance from the end point to its control point. (As a fraction of the distance between the start and end points)")]
    [Range(0.0f, 1.0f)]
    public float EndControlPtDist = 0.35f;

    [Header("Arc")]
    [RacetrackCurveLength]    
    public float Length = 50.0f;

    /// <summary>
    /// Euler angles defining the turn (Y axis), pitch (X axis) and bank (Z axis) angles.
    /// The y angle (turn) is relative. 
    /// X and Z angles are absolute and specify the angles at the END of the curve.
    /// The X and Z at the start of the curve are inherited from the previous curve and
    /// lerp to the current curve's values across the length of the curve.
    /// </summary>
    [Header("Shape")]
    [RacetrackCurveAngles]
    public Vector3 Angles = new Vector3();

    [Tooltip("How to interpolate between curve bank (Z) angles. Select 'inherit' to use the value defined on the Racetrack/Racetrack Group.")]
    public Racetrack1DInterpolationType BankAngleInterpolation = Racetrack1DInterpolationType.Inherit;

    public RacetrackWidening Widening;

    [Tooltip("How to interpolate between 'widening' values. Select 'inherit' to use the value defined on the Racetrack/Racetrack Group.")]
    public Racetrack1DInterpolationType WideningInterpolation = Racetrack1DInterpolationType.Inherit;

    [Tooltip("Defines the pivot when banking the track. The pivot point is not raised or lowered by banking.")]
    public float BankPivotX = 0.0f;

    [Header("Flags")]

    /// <summary>
    /// True to create a "Jump".
    /// No meshes are generated for curves flagged as jumps.
    /// </summary>
    [Tooltip("Don't create any meshes for this curve.")]
    public bool IsJump = false;

    /// <summary>
    /// True if the player can respawn on this curve.
    /// Useful to mark parts of the track where respawning makes it difficult/impossible
    /// to progress (e.g. not enough run-up for a jump)
    /// </summary>
    [Tooltip("Whether this curve is a suitable respawn point, for when the car falls off the track. Used by the RacetrackProgressTracker script component.")]
    public bool CanRespawn = true;

    [Tooltip("Align end of last mesh template clone with end of curve (by scaling along the Z component)")]
    public bool AlignMeshesToEnd = false;

    /// <summary>
    /// Template for generating meshes along the curve.
    /// If null, will inherit the template from the previous curve.
    /// </summary>
    [Header("Meshes")]
    [Tooltip("The template object supplying the meshes to warp to the racetrack curves. If null, will use the previous curve's template.")]
    public RacetrackMeshTemplate Template;

    [Header("Remove internal faces")]
    public RemoveInternalFacesOption RemoveStartInternalFaces;
    public RemoveInternalFacesOption RemoveEndInternalFaces;

    [Header("Terrain")]
    [Tooltip("Whether to raise the terrain up to the level of the track, if necessary. This affects the Racetrack Terrain Modifier component.")]
    public bool RaiseTerrain = false;
    [Tooltip("Whether to lower the terrain underneath the track, if necessary. This affects the Racetrack Terrain Modifier component.")]
    public bool LowerTerrain = true;

    /// <summary>
    /// The index of this curve within the Racetrack sequence
    /// </summary>
    [HideInInspector]
    public int Index;

    /// <summary>
    /// Find the track
    /// </summary>
    public Racetrack Track
    {
        get
        {
            if (transform.parent == null)
                throw new Exception("Curve (" + name + ") parent is not set. Parent should exist and have a 'Track' component.");
            var track = transform.parent.GetComponent<Racetrack>();
            if (track == null)
                throw new Exception("Curve (" + name + ") parent does not have a 'Racetrack' component.");
            return track;
        }
    }
}

public enum RacetrackCurveType
{
    Arc,            // Circle arc curve, defined by angle and length
    Bezier          // Bezier curve, defined by end point, angles and control points
}

public enum RemoveInternalFacesOption
{
    Auto,
    Yes,
    No
}
