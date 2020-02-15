using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    /*
     * A custom first person camera that can be attached and used on any 
     * object in the game environment.
     */

    #region Variables

    private float rotationSmoothTime, yaw, pitch;
    private Vector3 currentPosition, currentRotation, rotationSmoothVelocity;
    private Vector2 pitchMinMax;
    private GameStateManager gameState;

    [Header("Camera Settings")]
    public Transform target;
    public float mouseLookSensitivity;

    #endregion Variables

    void Start ()
    {
        gameState = FindObjectOfType<GameStateManager>();
        gameState.setCursorActive(false);
        mouseLookSensitivity = 5;
        rotationSmoothTime = 0.12f;
        pitchMinMax = new Vector2(-40, 85);
	}

    void LateUpdate()
    {
        //Ensures the proper rotation of the camera along the x and y axes
        yaw += Input.GetAxis("Mouse X") * mouseLookSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseLookSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        //Clamps the angle of rotation so the player moves the camera more realistically
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        Vector3 euler = transform.eulerAngles;
        euler.x = 0;

        //Sets the default position for the camera on the attached object to be as close to the face as possible
        target.eulerAngles = euler;
    }

    //Just checks if the camera is looking at the specified game object
    public bool isLookingAt(GameObject obj)
    {
        bool isLooking = false;

        Vector3 origin, direction;
        Ray ray;
        RaycastHit hit;

        origin = Camera.main.transform.position;
        direction = Camera.main.transform.forward;
        ray = new Ray(origin, direction);

        if(Physics.Raycast(ray, out hit) && hit.collider == obj.GetComponent<Collider>())
        {
            isLooking = true;
        }

        else
        {
            isLooking = false;
        }

        return isLooking;
    }

    //If a specified game object is within a certain distance of the camera and the camera is directly facing it, returns true.
    public bool distanceFromObject(GameObject obj, float maxDist)
    {
        bool isHitting = false;
        Vector3 origin, direction;
        Ray ray;
        RaycastHit hit;
        float maxDistance = maxDist;

        origin = Camera.main.transform.position;
        direction = Camera.main.transform.forward;
        ray = new Ray(origin, direction);

        if (Physics.Raycast(ray, out hit, maxDistance) && hit.collider == obj.GetComponent<Collider>())
        {
            Debug.DrawRay(ray.direction, hit.point, Color.red);
            isHitting = true;
        }

        else
        {
            isHitting = false;
        }

        return isHitting;
    }

    #region Getters and Setters

    public Vector3 getCurrentRotation()
    {
        return currentRotation;
    }

    public float getMouseLookSensitivity()
    {
        return mouseLookSensitivity;
    }

    #endregion Getters and Setters

    public CameraData setCameraData()
    {
        CameraData data = new CameraData();

        data.rotationSmoothTime = rotationSmoothTime;
        data.yaw = yaw;
        data.pitch = pitch;
        data.currentPosition = currentPosition;
        data.currentRotation = currentRotation;
        data.rotationSmoothVelocity = rotationSmoothVelocity;
        data.pitchMinMax = pitchMinMax;
        data.target = target;
        data.mouseLookSensitivity = mouseLookSensitivity;

        return data;
    }
}
#region Serialization

[System.Serializable]
public struct CameraData
{
    public float rotationSmoothTime, yaw, pitch;
    public Vector3 currentPosition, currentRotation, rotationSmoothVelocity;
    public Vector2 pitchMinMax;
    public Transform target;
    public float mouseLookSensitivity;
}

    #endregion Serialization