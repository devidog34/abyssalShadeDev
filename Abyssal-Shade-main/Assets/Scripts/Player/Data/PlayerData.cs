using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Holds data values for player controller & state machine
 * 
 * Jeff Stevenson
 * 2.25.25
 */

[CreateAssetMenu(menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    public float swimSpeed;
    public float strafeSpeed;
    public float rollSpeed;
    public float turnSpeed;
    public float dashSpeed;
    public float dashCooldown;
    public float dashBufferWindow;
    public float maxTurnZRotation;

    [Space]
    [Header("Camera Controls")]
    [Range(0f, 1f)]
    public float lookSensitivity;
    [Range(0, 1f)]
    public float zoomSensitivity;
    public float minZoomDistance;
    public float maxZoomDistance;

    [Space]
    [Header("Boid Interactions")]
    public float defaultBoidCollectionDistance;
    public float dashBoidCollectionDistance;
    public float dashBoidCollectionTime;
}
