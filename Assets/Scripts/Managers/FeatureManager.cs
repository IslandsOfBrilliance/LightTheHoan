using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureManager : MonoBehaviour
{
    public static FeatureManager Instance;

    public Gaze gaze;
    public float gazeDuration;
    public float gazeRadius;

    public InteractionController leftPoint;
    public InteractionController rightPoint;
    public float pointRadius;

    #region Movement
    public enum MoveType
    {
        None,
        Smooth,
        Dash,
        Teleport,
        Waypoint
    };

    [Space, Header("Movement")]
    public MoveType moveType;
    [HideInInspector] public int moveTypeSelectedIndex;

    public SmoothMovement smoothMovement;
    public float smoothMoveSpeed;

    public DashMovement dashMovement;
    public float dashMoveSpeed;
    public float dashMoveDist;
    public float dashMoveStoppingDist;

    [Space]
    public TeleportMovement teleportMovement;
    public float teleportMoveSpeed;
    public float teleportMoveDist;
    public float teleportRotationSpeed;
    public bool teleportRightHand;
    public bool teleportWithRotation;

    [Space]
    public WaypointMovement waypointMovement;
    public enum WaypointType
    {
        Gaze,
        Point
    }
    public WaypointType waypointType;
    public int waypointTypeSelectedIndex;
    public float waypointMoveSpeed;
    public float waypointRotaionSpeed;
    public bool waypointRightHand;
    #endregion

    #region Rotation
    public enum RotationType
    {
        None,
        Smooth,
        Snap,
    };

    [Space, Header("Rotation")]
    public RotationType rotationType;
    public int rotTypeSelectedIndex;

    public SmoothRotation smoothRotation;
    public float smoothRotSpeed;

    public SnapRotation snapRotation;
    public float snapRotRotaionDegree;
    public float snapRotRotationFrequency;

    #endregion

    #region ObjectInteraction
    public enum ObjectInteraction
    {
        Gaze,
        Point,
        Grab
    };

    [Header("Interaction")]
    public ObjectInteraction objectInteraction;
    public int objInteractionSelectedIndex;

    #region Grab
    public enum GrabType
    {
        Free,
        Point
    }

    public GrabType grabType;
    public int grabTypeSelectedIndex;

    public float objGrabRadius;

    public FreeGrab leftFreeGrab;
    public FreeGrab rightFreeGrab;

    public bool distanceGrab;
    public float distanceGrabLerpSpeed;
    #endregion
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    public void SetInteractionState(bool state)
    {
        if(leftPoint.interactionTags.Contains("POI"))
            leftPoint.interactionTags.Remove("POI");

        if (rightPoint.interactionTags.Contains("POI"))
            rightPoint.interactionTags.Remove("POI");

        if (gaze.interactionTags.Contains("POI"))
            gaze.interactionTags.Remove("POI");

        switch (objectInteraction)
        {
            case ObjectInteraction.Grab:
                leftFreeGrab.IsActive = state;
                leftFreeGrab.grabRadius = objGrabRadius;

                rightFreeGrab.IsActive = state;
                rightFreeGrab.grabRadius = objGrabRadius;

                if (grabType == GrabType.Point)
                {
                    leftFreeGrab.PointGrab = state;
                    rightFreeGrab.PointGrab = state;
                }

                if (distanceGrab)
                {
                    leftFreeGrab.PointGrab = state;
                    rightFreeGrab.PointGrab = state;

                    leftFreeGrab.DistanceGrab = state;
                    leftFreeGrab.lerpSpeed = distanceGrabLerpSpeed;

                    rightFreeGrab.DistanceGrab = state;
                    rightFreeGrab.lerpSpeed = distanceGrabLerpSpeed;
                }
                break;
            case ObjectInteraction.Gaze:
                gaze.IsActive = state;
                gaze.gazeDuration = gazeDuration;
                gaze.gazeRadius = gazeRadius;

                gaze.interactionTags.Add("Grab");
                break;
            case ObjectInteraction.Point:
                leftPoint.IsActive = state;
                rightPoint.IsActive = state;

                leftPoint.interactionTags.Add("Grab");
                rightPoint.interactionTags.Add("Grab");
                break;
        }
    }

    public void SetMoveActive(bool state)
    {
        switch (moveType)
        {
            case MoveType.Smooth:
                smoothMovement.IsActive = state;
                smoothMovement.speed = smoothMoveSpeed;
                break;
            case MoveType.Dash:
                dashMovement.IsActive = state;
                dashMovement.speed = dashMoveSpeed;
                dashMovement.distance = dashMoveDist;
                dashMovement.stoppingDistance = dashMoveStoppingDist;
                break;
            case MoveType.Teleport:
                teleportMovement.IsActive = state;

                if (teleportWithRotation)
                    teleportMovement.WithRotation = state;

                teleportMovement.SetHandedness(teleportRightHand);

                teleportMovement.movementSpeed = teleportMoveSpeed;
                teleportMovement.moveDistance = teleportMoveDist;
                teleportMovement.rotationSpeed = teleportRotationSpeed;
                break;
            case MoveType.Waypoint:
                waypointMovement.IsActive = state;
                waypointMovement.SetHandedness(waypointRightHand);
                waypointMovement.movementSpeed = waypointMoveSpeed;
                waypointMovement.rotationSpeed = waypointRotaionSpeed;

                leftPoint.IsActive = state;
                rightPoint.IsActive = state;

                if(waypointType == WaypointType.Point)
                {
                    leftPoint.IsActive = state;
                    rightPoint.IsActive = state;

                    leftPoint.interactionTags.Add("Waypoint");
                    rightPoint.interactionTags.Add("Waypoint");
                }
                else
                {
                    gaze.IsActive = true;
                    gaze.gazeDuration = gazeDuration;
                    gaze.gazeRadius = gazeRadius;
                    gaze.interactionTags.Add("Waypoint");
                }
                break;
        }
    }

    public void SetRotationActive(bool state)
    {
        switch (rotationType)
        {
            case RotationType.Smooth:
                smoothRotation.IsActive = state;
                smoothRotation.rotationSpeed = smoothRotSpeed;
                break;
            case RotationType.Snap:
                snapRotation.IsActive = state;
                snapRotation.rotationDegree = snapRotRotaionDegree;
                snapRotation.rotationFrequency = snapRotRotationFrequency;
                break;
        }
    }
}
