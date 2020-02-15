using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Responsible for calculating the area in which the player swings on the grapple bars using some circle maths.
//Each grapple bar handle has its own swing area attached, and the angle changes depending on which direction the character is facing.

public class GrappleSwingArea : MonoBehaviour
{
    [SerializeField]
    public Transform player;
    public Transform grapplePoint;

    [Range(5, 30)]
    public int radius;

    [Range(10, 180)]
    public float angle;

    [SerializeField]
    float maxSpeed;
    float bobMoveSpeed;
    float moveAngle;
    float arcLength;
    float speedDirection;

    Vector3 right_arcPoint;
    Vector3 left_arcPoint;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }

    private void OnEnable()
    {
        StartCoroutine(performGrapple());
    }

    IEnumerator performGrapple()
    {
        makeGrappleCalculations();

        while(true)
        {
            bobMoveSpeed = Mathf.Lerp(bobMoveSpeed, speedDirection * maxSpeed, Time.deltaTime);
            moveAngle += bobMoveSpeed * Time.deltaTime;

            grapplePoint.position = transform.position + Quaternion.AngleAxis(moveAngle, transform.forward) * Vector3.down * radius;

            compareArcLength();

            yield return new WaitForEndOfFrame();
        }
    }

    private void makeGrappleCalculations()
    {
        grapplePoint.transform.position = new Vector3(player.position.x, player.position.y + radius, player.position.z);

        right_arcPoint = transform.position + Quaternion.AngleAxis(angle, transform.forward) * Vector3.down * radius;
        left_arcPoint = transform.position + Quaternion.AngleAxis(-angle, transform.forward) * Vector3.down * radius;

        grapplePoint.position = new Vector3(transform.position.x, transform.position.y - radius, transform.position.z);

        arcLength = (2 * Mathf.PI * radius * angle) / 360;

        moveAngle = 0;
        bobMoveSpeed = 0;
        speedDirection = -1;
    }

    private void compareArcLength()
    {
        float grappleAngle = 180 - Vector3.Angle(transform.up, grapplePoint.position - transform.position);

        float grappleArcLength = (2 * Mathf.PI * radius * grappleAngle) / 360;

        if (grappleArcLength > arcLength)
        {
            if (Vector3.Distance(grapplePoint.position, right_arcPoint) < Vector3.Distance(grapplePoint.position, left_arcPoint))
                speedDirection = -1;

            else
                speedDirection = 1;
        }
    }

    private void OnDisable()
    {
        StopCoroutine(performGrapple());
    }
}
