using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantaRaySonarAttack : MonoBehaviour
{
    [Header("Sonar Attack Settings")]
    [Tooltip("The shoot point from where the sonar pulse will originate.")]
    public Transform shootPoint;

    [Tooltip("The Particle System that simulates the sonar pulse. This should be your 'sonarEffect' prefab.")]
    public ParticleSystem sonarEffect;

    [Tooltip("Time (in seconds) between each sonar pulse while holding left mouse button.")]
    public float pulseInterval = 0.5f;

    // Reference to the coroutine, so it can be started/stopped.
    private Coroutine pulseCoroutine;

    void Update()
    {
        // Always update the sonarEffect's position and rotation to match the shootPoint and the player's forward direction.
        if (sonarEffect != null && shootPoint != null)
        {
            sonarEffect.transform.position = shootPoint.position;
            // Align the sonar effect with the player's forward direction.
            sonarEffect.transform.rotation = Quaternion.LookRotation(transform.forward);
        }

        // Start the pulsed sonar attack when left mouse is pressed down.
        if (Input.GetMouseButtonDown(0))
        {
            if (pulseCoroutine == null)
            {
                pulseCoroutine = StartCoroutine(PulseRoutine());
            }
        }

        // Stop the pulsed sonar attack when left mouse is released.
        if (Input.GetMouseButtonUp(0))
        {
            if (pulseCoroutine != null)
            {
                StopCoroutine(pulseCoroutine);
                pulseCoroutine = null;
            }
        }
    }

    // Coroutine to repeatedly trigger the sonar pulse.
    IEnumerator PulseRoutine()
    {
        while (true)
        {
            // Restart the sonar effect by stopping any current emission and then playing it.
            if (sonarEffect != null)
            {
                // Optionally clear the previous pulse: This ensures the pulse resets.
                sonarEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                sonarEffect.Play();
            }
            else
            {
                Debug.LogWarning("MantaRaySonarAttack: sonarEffect is not assigned.");
            }

            // Wait for the specified pulse interval before triggering the next pulse.
            yield return new WaitForSeconds(pulseInterval);
        }
    }
}
