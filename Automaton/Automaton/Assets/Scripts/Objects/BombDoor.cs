using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Represents an orange door, checks if it is colliding with a moving bomb cube and destroys objects accordingly

public class BombDoor : MonoBehaviour
{
    private PickUpObject pickUp;

    public GameObject topPanel;
    public GameObject bottomPanel;
    public AudioSource popEffect;

    void Start()
    {
        pickUp = GameObject.FindObjectOfType<PickUpObject>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<BombCube>() && pickUp.isThrowing)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            popEffect.Play();
            GameObject.Destroy(topPanel);
            GameObject.Destroy(bottomPanel);
            GameObject.Destroy(other.gameObject);
        }
    }
}
