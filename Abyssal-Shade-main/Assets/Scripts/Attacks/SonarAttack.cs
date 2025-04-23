using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarAttack : MonoBehaviour
{
    [Header("Sonar Attack Settings")]
    [Tooltip("Prefab for the sonar pulse effect. This should be your 'sonarEffect' prefab.")]
    public GameObject sonarEffect;

    [Tooltip("Transform from which the sonar pulse will be spawned.")]
    public Transform shootPoint;

    [Tooltip("Time (in seconds) between each sonar pulse when holding left click. Must be greater than 0.")]
    public float pulseInterval = 0.5f;

    [Tooltip("Lifetime (in seconds) of each sonar pulse. Pulses are automatically destroyed after this time.")]
    public float sonarLifetime = 2.0f;

    // Reference to the coroutine so we can stop it when needed.
    private Coroutine pulseCoroutine;

    private void Update()
    {
        // When left mouse button is pressed down, start the pulsed sonar attack.
        if (Input.GetMouseButtonDown(0))
        {
            if (pulseCoroutine == null)
            {
                pulseCoroutine = StartCoroutine(PulseRoutine());
            }
        }

        // When left mouse button is released, stop the pulsed sonar attack.
        if (Input.GetMouseButtonUp(0))
        {
            if (pulseCoroutine != null)
            {
                StopCoroutine(pulseCoroutine);
                pulseCoroutine = null;
            }
        }
    }

    private IEnumerator PulseRoutine()
    {
        // Ensure a safe pulseInterval.
        float interval = (pulseInterval > 0f) ? pulseInterval : 0.1f;

        while (true)
        {
            // Determine the spawn position and rotation:
            // If shootPoint is assigned, use its position and rotation.
            // Otherwise, fall back to the player's transform.
            Vector3 spawnPosition = (shootPoint != null) ? shootPoint.position : transform.position;
            Quaternion spawnRotation = (shootPoint != null) ? shootPoint.rotation : transform.rotation;

            // Instantiate the sonar pulse.
            if (sonarEffect != null)
            {
                GameObject pulse = Instantiate(sonarEffect, spawnPosition, spawnRotation);
                Destroy(pulse, sonarLifetime);
            }
            else
            {
                Debug.LogWarning("SonarAttack: sonarEffect prefab is not assigned.");
            }

            yield return new WaitForSeconds(interval);
        }
    }
}

