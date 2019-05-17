using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FeatureManager))]
public class FeatureManagerEditor : Editor
{
    FeatureManager featureManager;

    #region Control Scripts
    bool controlScriptsOpen;
    SerializedProperty controlledProp, gazeProp, leftPointProp, rightPointProp, leftFreeGrabProp, rightFreeGrabProp, smoothMovementProp, dashMovementProp, teleportMovementProp, waypointMovementProp, smoothRotationProp, snapRotationProp;
    #endregion
    
    SerializedProperty gazeDurationProp, gazeRadiusProp;

    #region Movement
    bool movementOpen;
    string[] moveTypes;
    string[] waypointTypes;
    SerializedProperty moveTypeProp, moveTypeSelectedIndexProp, 
        smoothMoveSpeedProp, 
        dashMoveSpeedProp, dashMoveDistProp, dashMoveStoppingDistProp, 
        teleportMoveSpeedProp, teleportMoveDistProp, teleportRotationSpeedProp,
        waypointTypeSelectedIndexProp, waypointMoveSpeedProp, waypointRotationSpeedProp;
    #endregion

    #region Rotation
    bool rotationOpen;
    string[] rotTypes;
    SerializedProperty rotTypeProp, rotTypeSelectedIndexProp,
        smoothRotSpeedProp,
        snapRotRotationDegreeProp, snapRotRotationFrequencyProp;
    #endregion

    #region  Object Interaction
    bool objInteractionOpen;
    string[] objInteractionTypes, grabTypes;
    SerializedProperty objInteractionProp, objInteractionSelectedIndexProp,
        grabTypeProp, grabTypeSelectedIndexProp, objGrabRadiusProp, grabDistanceEnabledProp, grabDistanceLerpSpeedProp;
    #endregion

    private void OnEnable()
    {
        featureManager = (FeatureManager)target;

        #region Control Scripts
        controlledProp = serializedObject.FindProperty("controlled");
        gazeProp = serializedObject.FindProperty("gaze");
        leftPointProp = serializedObject.FindProperty("leftPoint");
        rightPointProp = serializedObject.FindProperty("rightPoint");
        leftFreeGrabProp = serializedObject.FindProperty("leftFreeGrab");
        rightFreeGrabProp = serializedObject.FindProperty("rightFreeGrab");
        smoothMovementProp = serializedObject.FindProperty("smoothMovement");
        dashMovementProp = serializedObject.FindProperty("dashMovement");
        teleportMovementProp = serializedObject.FindProperty("teleportMovement");
        waypointMovementProp = serializedObject.FindProperty("waypointMovement");
        smoothRotationProp = serializedObject.FindProperty("smoothRotation");
        snapRotationProp = serializedObject.FindProperty("snapRotation");
        #endregion

        #region POI Interaction
        gazeDurationProp = serializedObject.FindProperty("gazeDuration");
        gazeRadiusProp = serializedObject.FindProperty("gazeRadius");
        #endregion

        #region Movement
        moveTypes = Enum.GetNames(typeof(FeatureManager.MoveType));
        waypointTypes = Enum.GetNames(typeof(FeatureManager.WaypointType));
        moveTypeProp = serializedObject.FindProperty("moveType");
        moveTypeSelectedIndexProp = serializedObject.FindProperty("moveTypeSelectedIndex");
        smoothMoveSpeedProp = serializedObject.FindProperty("smoothMoveSpeed");
        dashMoveSpeedProp = serializedObject.FindProperty("dashMoveSpeed");
        dashMoveDistProp = serializedObject.FindProperty("dashMoveDist");
        dashMoveStoppingDistProp = serializedObject.FindProperty("dashMoveStoppingDist");
        teleportMoveSpeedProp = serializedObject.FindProperty("teleportMoveSpeed");
        teleportMoveDistProp = serializedObject.FindProperty("teleportMoveDist");
        teleportRotationSpeedProp = serializedObject.FindProperty("teleportRotationSpeed");
        waypointTypeSelectedIndexProp = serializedObject.FindProperty("waypointTypeSelectedIndex");
        waypointMoveSpeedProp = serializedObject.FindProperty("waypointMoveSpeed");
        waypointRotationSpeedProp = serializedObject.FindProperty("waypointRotaionSpeed");
        #endregion

        #region Rotaion
        rotTypes = Enum.GetNames(typeof(FeatureManager.RotationType));
        rotTypeProp = serializedObject.FindProperty("rotationType");
        rotTypeSelectedIndexProp = serializedObject.FindProperty("rotTypeSelectedIndex");
        smoothRotSpeedProp = serializedObject.FindProperty("smoothRotSpeed");
        snapRotRotationDegreeProp = serializedObject.FindProperty("snapRotRotaionDegree");
        snapRotRotationFrequencyProp = serializedObject.FindProperty("snapRotRotationFrequency");
        #endregion

        #region Object Interaction
        objInteractionTypes = Enum.GetNames(typeof(FeatureManager.ObjectInteraction));
        grabTypes = Enum.GetNames(typeof(FeatureManager.GrabType));
        objInteractionProp = serializedObject.FindProperty("objectInteraction");
        objInteractionSelectedIndexProp = serializedObject.FindProperty("objInteractionSelectedIndex");
        grabTypeProp = serializedObject.FindProperty("grabType");
        grabTypeSelectedIndexProp = serializedObject.FindProperty("grabTypeSelectedIndex");
        objGrabRadiusProp = serializedObject.FindProperty("objGrabRadius");
        grabDistanceEnabledProp = serializedObject.FindProperty("distanceGrab");
        grabDistanceLerpSpeedProp = serializedObject.FindProperty("distanceGrabLerpSpeed");
        #endregion
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        FeatureManager fm = (FeatureManager)target;

        EditorGUILayout.BeginVertical("box");

        #region ControlScripts
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Control Scripts");
        if (GUILayout.Button("Toggle Control Scripts", GUILayout.MinWidth(25f), GUILayout.MaxWidth(1000f)))
            controlScriptsOpen = !controlScriptsOpen;

        if(controlScriptsOpen)
        {
            gazeProp.objectReferenceValue = EditorGUILayout.ObjectField("Gaze", gazeProp.objectReferenceValue, typeof(Gaze), true);
            leftPointProp.objectReferenceValue = EditorGUILayout.ObjectField("Left Hand Point", leftPointProp.objectReferenceValue, typeof(InteractionController), true);
            rightPointProp.objectReferenceValue = EditorGUILayout.ObjectField("Right Hand Point", rightPointProp.objectReferenceValue, typeof(InteractionController), true);
            leftFreeGrabProp.objectReferenceValue = EditorGUILayout.ObjectField("Left Free Grab", leftFreeGrabProp.objectReferenceValue, typeof(FreeGrab), true);
            rightFreeGrabProp.objectReferenceValue = EditorGUILayout.ObjectField("Right Free Grab", rightFreeGrabProp.objectReferenceValue, typeof(FreeGrab), true);
            smoothMovementProp.objectReferenceValue = EditorGUILayout.ObjectField("Smooth Movement", smoothMovementProp.objectReferenceValue, typeof(SmoothMovement), true);
            dashMovementProp.objectReferenceValue = EditorGUILayout.ObjectField("Dash Movement", dashMovementProp.objectReferenceValue, typeof(DashMovement), true);
            teleportMovementProp.objectReferenceValue = EditorGUILayout.ObjectField("Teleport Movement", teleportMovementProp.objectReferenceValue, typeof(TeleportMovement), true);
            waypointMovementProp.objectReferenceValue = EditorGUILayout.ObjectField("Waypoint Movement", waypointMovementProp.objectReferenceValue, typeof(WaypointMovement), true);
            smoothRotationProp.objectReferenceValue = EditorGUILayout.ObjectField("SmoothRotation", smoothRotationProp.objectReferenceValue, typeof(SmoothRotation), true);
            snapRotationProp.objectReferenceValue = EditorGUILayout.ObjectField("SnapRotation", snapRotationProp.objectReferenceValue, typeof(SnapRotation), true);
        }
        EditorGUILayout.EndVertical();
        #endregion
        EditorGUILayout.Space();

        #region Movement
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Movement");
        if (GUILayout.Button("Toggle Movement", GUILayout.MinWidth(25f), GUILayout.MaxWidth(1000f)))
            movementOpen = !movementOpen;

        if (movementOpen)
        {
            moveTypeSelectedIndexProp.intValue = EditorGUILayout.Popup("Movement Type", moveTypeSelectedIndexProp.intValue, moveTypes);
            serializedObject.ApplyModifiedProperties();
            fm.moveType = (FeatureManager.MoveType)(fm.moveTypeSelectedIndex);

            switch ((FeatureManager.MoveType)moveTypeSelectedIndexProp.intValue)
            {
                case FeatureManager.MoveType.Smooth:
                    smoothMoveSpeedProp.floatValue = EditorGUILayout.FloatField("Speed", smoothMoveSpeedProp.floatValue);
                    break;
                case FeatureManager.MoveType.Dash:
                    dashMoveSpeedProp.floatValue = EditorGUILayout.FloatField("Speed", dashMoveSpeedProp.floatValue);
                    dashMoveDistProp.floatValue = EditorGUILayout.FloatField("Move Distance", dashMoveDistProp.floatValue);
                    dashMoveStoppingDistProp.floatValue = EditorGUILayout.FloatField("Stopping Distance", dashMoveStoppingDistProp.floatValue);
                    break;
                case FeatureManager.MoveType.Teleport:
                    teleportMoveSpeedProp.floatValue = EditorGUILayout.FloatField("Speed", teleportMoveSpeedProp.floatValue);
                    teleportMoveDistProp.floatValue = EditorGUILayout.FloatField("Move Distance", teleportMoveDistProp.floatValue);
                    teleportRotationSpeedProp.floatValue = EditorGUILayout.FloatField("Rotation Speed", teleportRotationSpeedProp.floatValue);
                    break;
                case FeatureManager.MoveType.Waypoint:
                    waypointTypeSelectedIndexProp.intValue = EditorGUILayout.Popup("Waypoint Interaction Type", waypointTypeSelectedIndexProp.intValue, waypointTypes);
                    serializedObject.ApplyModifiedProperties();
                    fm.waypointType = (FeatureManager.WaypointType)(fm.waypointTypeSelectedIndex);
                    switch(fm.waypointType)
                    {
                        case FeatureManager.WaypointType.Gaze:
                            gazeDurationProp.floatValue = EditorGUILayout.FloatField("Gaze Duration", gazeDurationProp.floatValue);
                            gazeRadiusProp.floatValue = EditorGUILayout.FloatField("Gaze Radius", gazeRadiusProp.floatValue);
                            break;
                        case FeatureManager.WaypointType.Point:
                            break;
                        default:
                            break;
                    }
                    waypointMoveSpeedProp.floatValue = EditorGUILayout.FloatField("Speed", waypointMoveSpeedProp.floatValue);
                    waypointRotationSpeedProp.floatValue = EditorGUILayout.FloatField("Rotation Speed", waypointRotationSpeedProp.floatValue);
                    break;
                default:
                    break;
            }
        }
        EditorGUILayout.EndVertical();
        #endregion

        #region Rotation
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Rotation");
        if (GUILayout.Button("Toggle Rotation", GUILayout.MinWidth(25f), GUILayout.MaxWidth(1000f)))
            rotationOpen = !rotationOpen;

        if (rotationOpen)
        {
            rotTypeSelectedIndexProp.intValue = EditorGUILayout.Popup("Rotation Type", rotTypeSelectedIndexProp.intValue, rotTypes);
            serializedObject.ApplyModifiedProperties();
            fm.rotationType = (FeatureManager.RotationType)(fm.rotTypeSelectedIndex);

            switch ((FeatureManager.RotationType)rotTypeSelectedIndexProp.intValue)
            {
                case FeatureManager.RotationType.Smooth:
                    smoothRotSpeedProp.floatValue = EditorGUILayout.FloatField("Rotation Speed", smoothRotSpeedProp.floatValue);
                    break;
                case FeatureManager.RotationType.Snap:
                    snapRotRotationDegreeProp.floatValue = EditorGUILayout.FloatField("Rotation Degree", snapRotRotationDegreeProp.floatValue);
                    snapRotRotationFrequencyProp.floatValue = EditorGUILayout.FloatField("Rotation Frequency", snapRotRotationFrequencyProp.floatValue);
                    break;
                default:
                    break;
            }
        }
        EditorGUILayout.EndVertical();
        #endregion

        #region Object Interaction
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Object Interaction");
        if (GUILayout.Button("Toggle Object Interaction", GUILayout.MinWidth(25f), GUILayout.MaxWidth(1000f)))
            objInteractionOpen = !objInteractionOpen;

        if (objInteractionOpen)
        {
            objInteractionSelectedIndexProp.intValue = EditorGUILayout.Popup("Object Interaction Type", objInteractionSelectedIndexProp.intValue, objInteractionTypes);
            serializedObject.ApplyModifiedProperties();
            fm.objectInteraction = (FeatureManager.ObjectInteraction)(fm.objInteractionSelectedIndex);

            switch ((FeatureManager.ObjectInteraction)objInteractionSelectedIndexProp.intValue)
            {
                case FeatureManager.ObjectInteraction.Gaze:
                    gazeDurationProp.floatValue = EditorGUILayout.FloatField("Gaze Duration", gazeDurationProp.floatValue);
                    gazeRadiusProp.floatValue = EditorGUILayout.FloatField("Gaze Radius", gazeRadiusProp.floatValue);
                    break;
                case FeatureManager.ObjectInteraction.Grab:
                    grabDistanceEnabledProp.boolValue = EditorGUILayout.Toggle("Enable Distance Grab", grabDistanceEnabledProp.boolValue);
                    if (grabDistanceEnabledProp.boolValue == true)
                    {
                        grabDistanceLerpSpeedProp.floatValue = EditorGUILayout.FloatField("Lerp Speed", grabDistanceLerpSpeedProp.floatValue);
                        grabTypeSelectedIndexProp.intValue = (int)FeatureManager.GrabType.Point;
                    }
                    else
                        grabTypeSelectedIndexProp.intValue = EditorGUILayout.Popup("Grab Type", grabTypeSelectedIndexProp.intValue, grabTypes);

                    serializedObject.ApplyModifiedProperties();
                    fm.grabType = (FeatureManager.GrabType)(fm.grabTypeSelectedIndex);

                    objGrabRadiusProp.floatValue = EditorGUILayout.FloatField("Grab Radius", objGrabRadiusProp.floatValue);
                    break;
                default:
                    break;
            }
        }
        EditorGUILayout.EndVertical();
        #endregion

        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }
}