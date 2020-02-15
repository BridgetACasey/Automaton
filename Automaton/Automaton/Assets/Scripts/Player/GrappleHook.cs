using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Responsible for checking if the player can grapple and initiating grappling
//Additionally checks where all possible grapple points are in the current scene

public class GrappleHook : MonoBehaviour
{
    private CameraManager camera;
    private PlayerMovement playerMove;
    private GameObject player;
    private HeadsUpDisplay hudScript;
    private KeyManager keyManager;
    private Interactable interaction;
    private float hookRotationTime;
    private Vector3 hookStartPosition;
    private Quaternion hookRotation;

    public bool canGrapple, isGrappling;
    private Vector3 startArcPoint, endArcPoint;

    [SerializeField]
    GameObject swingArea;

    [SerializeField]
    Transform grapplePoint, grappleHandTrans;

    int awayDistance;

    LineRenderer lineRenderer;

    [Header("Grapple Tools")]
    public AudioSource grappleEffect;
    public GameObject[] grappleHandles;
    public GameObject currentHandle;
    public GameObject grappleHoldPoint;
    public GameObject grappleHand;

    [Range(5, 30)]
    public int radius;

    [Range(10, 180)]
    public float angle;

    void Start()
    {
        grappleHand.transform.parent = grappleHoldPoint.transform;

        camera = GameObject.FindObjectOfType<CameraManager>();
        keyManager = GameObject.FindObjectOfType<KeyManager>();
        hudScript = GameObject.FindObjectOfType<HeadsUpDisplay>();
        playerMove = GameObject.FindObjectOfType<PlayerMovement>();
        lineRenderer = GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");

        grappleHandles = GameObject.FindGameObjectsWithTag("Grapple_Handle");
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        hookRotationTime = 10f;

        canGrapple = false;
        isGrappling = false;
    }


    void Update()
    {   
        checkCanGrapple();

        if (isGrappling)
        {
            hookStartPosition = player.transform.position - grappleHand.transform.position;
            hookRotation = Quaternion.LookRotation(-hookStartPosition);
            grappleHand.transform.rotation = Quaternion.Slerp(grappleHand.transform.rotation, hookRotation, hookRotationTime);
            hookRotationTime = hookRotationTime + Time.deltaTime;

            transform.position = grapplePoint.position;
            lineRenderer.SetPosition(0, grappleHoldPoint.transform.position);
            lineRenderer.startWidth = 0.2f;
            lineRenderer.SetPosition(1, swingArea.transform.position);

            StartCoroutine(fall());
        }
    }

    private bool checkCanGrapple()
    {
        for(int counter = 0; counter < grappleHandles.Length; counter++)
        {
            if(grappleHandles[counter].GetComponentInChildren<MeshCollider>().bounds.Intersects(player.GetComponent<Collider>().bounds))
            {
                interaction = grappleHandles[counter].GetComponent<Interactable>();

                if (interaction == hudScript.getCurrentInteractingObject())
                {
                    canGrapple = true;
                    
                    grappleHandles[counter].GetComponent<Interactable>().setCommandText("Grapple");
                    grappleHandles[counter].GetComponent<Interactable>().setKey(keyManager.buttonCodes["Grapple"]);

                    if (!isGrappling)
                    {
                        currentHandle = grappleHandles[counter];
                        grapple();
                    }
                }

                else
                {
                    canGrapple = false;
                }
            }
        }

        return canGrapple;
    }

    private void grapple()
    {
        if(canGrapple)
        {
            if(Input.GetKey(keyManager.buttonCodes["Grapple"]))
            {
                isGrappling = true;

                if (currentHandle != null)
                {
                    grappleEffect.Play();
                    playerMove.setGravity(2.8f);
                    grappleHand.transform.parent = currentHandle.transform;
                    grappleHand.transform.position = currentHandle.transform.position;
                    swingArea.transform.position = new Vector3(currentHandle.transform.position.x, currentHandle.transform.position.y + radius, currentHandle.transform.position.z);
                    swingArea.transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y + 90, 0));
                    swingArea.SetActive(true);
                    lineRenderer.enabled = true;
                }
            }
        }
    }

    private IEnumerator fall()
    {
        yield return new WaitForSeconds(0.1f);

        if(isGrappling)
        {
            if (!Input.GetKey(keyManager.buttonCodes["Grapple"]))
            {
                grappleHand.transform.parent = grappleHoldPoint.transform;
                grappleHand.transform.position = grappleHoldPoint.transform.position;
                grappleHand.transform.rotation = grappleHoldPoint.transform.rotation;
                isGrappling = false;
                swingArea.SetActive(false);
                lineRenderer.enabled = false;
                currentHandle = null;
                StopAllCoroutines();
                StartCoroutine(adjustGravity(0.35f));
            }
        }
    }

    public IEnumerator adjustGravity(float adjustment)
    {
        float defaultGravity;

        defaultGravity = playerMove.getGravity();
        playerMove.setGravity(playerMove.getGravity() * adjustment);
        yield return new WaitForSeconds(2);
        playerMove.setGravity(defaultGravity);
    }

    public bool getCanGrapple()
    {
        return canGrapple;
    }
}
