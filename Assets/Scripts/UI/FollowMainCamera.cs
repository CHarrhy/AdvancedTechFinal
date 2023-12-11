using UnityEngine;

public class FollowMainCamera : MonoBehaviour
{
    public Transform target; // Player's main camera transform

    void Update()
    {
        if (target != null)
        {
            // Update the position and rotation of the death camera to match the main camera
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}
