using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Represents the HUD
//Checks if the player is currently looking at an object with an 'Interactable' component and displays appropriate text
//Also responsible for calling the animated notification text in the top right corner

public class HeadsUpDisplay : MonoBehaviour
{
    private CameraManager camera;
    private GameObject player;
    private Interactable interactingObject;

    [Header("Interaction")]
    public GameObject buttonText;
    public GameObject commandText;

    [Header("Menus")]
    public GameObject crosshair;
    public GameObject expandedCrosshair;
    public GameObject notificationText;
    public GameObject HUD;

    void Start()
    {
        camera = GameObject.FindObjectOfType<CameraManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        checkInteractables();

        if (interactingObject != null)
        {
            crosshair.SetActive(false);
            expandedCrosshair.SetActive(true);
        }

        else
        {
            crosshair.SetActive(true);
            expandedCrosshair.SetActive(false);
        }
    }

    public void checkInteractables()
    {
        Vector3 origin, direction;
        Ray ray;
        RaycastHit hit;

        origin = Camera.main.transform.position;
        direction = Camera.main.transform.forward;
        ray = new Ray(origin, direction);

        if(Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(origin, direction, Color.red, 30);
            Interactable hitItem = hit.collider.GetComponent<Interactable>();

            if (hitItem != null && hitItem.isActiveAndEnabled && camera.distanceFromObject(hitItem.gameObject, hitItem.getDistance()))
            {
                buttonText.GetComponent<Text>().text = hitItem.gameObject.GetComponent<Interactable>().getKeyText();
                commandText.GetComponent<Text>().text = hitItem.gameObject.GetComponent<Interactable>().getCommandText();
                interactingObject = hitItem;
                buttonText.SetActive(true);
                commandText.SetActive(true);
            }

            else
            {
                interactingObject = null;
                buttonText.SetActive(false);
                commandText.SetActive(false);
            }
        }
    }

    public IEnumerator notify(string text)
    {
        notificationText.GetComponent<Text>().text = text;
        notificationText.GetComponent<Animator>().SetBool("isNotifying", true);
        yield return new WaitForSeconds(3);
        notificationText.GetComponent<Animator>().SetBool("isNotifying", false);
        StopCoroutine(notify(text));
    }

    #region Getters and Setters

    public Interactable getCurrentInteractingObject()
    {
        return interactingObject;
    }

    #endregion Getters and Setters
}
