using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbVFX : MonoBehaviour
{
    public Transform player; // Reference to the player Transform

    public float rotSpeed_X = 10f;
    public float rotSpeed_Y = 10f;
    public float rotSpeed_Z = 10f;

    public float globalSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        // Check if player is assigned
        if (player != null)
        {
            // Update position to follow the player
            transform.position = player.position;

            // Apply rotation
            transform.Rotate(new Vector3(rotSpeed_X, rotSpeed_Y, rotSpeed_Z) * globalSpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Player Transform is not assigned.");
        }
    }
}
