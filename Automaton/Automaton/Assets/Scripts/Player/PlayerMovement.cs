using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*
     * A custom player movement class responsible for logging and carrying
     * out all of Delta's basic movements in-game.
     */

    #region Variables

    private KeyManager keys;
    private GameStateManager gameState;
    private DialogueManager dialogue;
    private RaycastHit groundHit;
    private bool onGround, inputJump, isMoving, canMove;
    private Vector3 groundingClamp, velocity, move, lastVelocity, rawVelocity;
    private float currentGroundSlope, minGroundDistance;

    [Header("Base Settings")]
    public float playerHeight;
    public float playerRadius;

    [Header("Movement")]
    public float movementSpeed;
    public bool smooth;
    public float smoothSpeed;

    [Header("Physics")]
    public float gravity;

    [Header("Jumping")]
    public float jumpHeight;
    public float jumpForce;
    public float jumpDecrease;
    public float jumpBarrier;
    public float jumpSpeed;

    [Header("Slopes")]
    [Range(0, 180)]
    public float minSlope;
    [Range(0, 180)]
    public float maxSlope;
    [Range(0, 180)]
    public float maxModifiedSlope;
    public float newMinGroundDistance;
    public float oldMinGroundDistance;

    [Header("Ground Settings")]
    public float maxGroundDistance;
    public Vector3 groundCheckPoint;
    public float groundCheckRadius;
    public float pushupDistance;

    [Header("Obstacles")]
    public float maxStepHeight;
    public Vector3 liftSlopePoint;
    public float liftSlopeRadius;

    [Header("References")]
    public LayerMask ignorePlayer;
    public CapsuleCollider capsuleCollider;

    #endregion Variables

    private void Start()
    {
        //Sets the default values for all variables

        keys = GameObject.FindObjectOfType<KeyManager>();
        gameState = GameObject.FindObjectOfType<GameStateManager>();
        dialogue = GameObject.FindObjectOfType<DialogueManager>();

        isMoving = false;

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            canMove = false;
        }

        else
        {
            canMove = true;
        }

        playerHeight = 1.56f;
        playerRadius = 0.45f;
        movementSpeed = 7.5f;

        smooth = true;
        smoothSpeed = 9f;
        gravity = 2.8f;

        groundHit = new RaycastHit();
        inputJump = false;
        jumpHeight = 0;
        jumpForce = 6.9f;
        jumpDecrease = 3.3f;
        jumpBarrier = 2;
        jumpSpeed = 1.01f;

        minSlope = 0;
        maxSlope = 75;
        maxModifiedSlope = 45f;
        newMinGroundDistance = 4f;
        oldMinGroundDistance = 2f;
        maxGroundDistance = 50;
        groundCheckPoint = new Vector3(0, -0.5f, 0);
        groundCheckRadius = 0.35f;
        pushupDistance = 0f;

        maxStepHeight = 1.57f;
        liftSlopePoint = new Vector3(0, 1.13f, 0);
        liftSlopeRadius = 0.2f;
    }

    private void Update()
    {
        if (canMove)
        {
            baseMove();
            checkJumping();
            complexMove();
            checkGravity();
        }

        checkAnimation();
        checkGround();
        checkCollisions();
    }

    #region Movement

    //Basic, 8 directional movement using the WASD keys
    private void baseMove()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        velocity += move;
    }

    //Advanced movement building upon baseMove(), this time taking into account the speed and current velocity of the player
    private void complexMove()
    {
        Vector3 vel = new Vector3(velocity.x, velocity.y, velocity.z) * movementSpeed;
        vel = transform.TransformDirection(vel);
        transform.position += vel * Time.deltaTime;
        lastVelocity = vel * Time.deltaTime;
        rawVelocity = vel;

        velocity = Vector3.zero;
    }

    #endregion Movement

    #region Animation

    private void checkAnimation()
    {
        if (!move.Equals(Vector3.zero) && !gameState.getInMenu() && !dialogue.inDialogue)
        {
            isMoving = true;
   
            this.gameObject.GetComponent<AudioSource>().enabled = true;
            this.gameObject.GetComponentInChildren<Animator>().Play("DeltaWalkCycle");
        }

        else if(move.Equals(Vector3.zero) || gameState.getInMenu() || dialogue.inDialogue)
        {
            isMoving = false;

            this.gameObject.GetComponent<AudioSource>().enabled = false;
            this.gameObject.GetComponentInChildren<Animator>().Play("Start");
        }
    }

    #endregion Animation

    #region Gravity

    //Checks whether or not the player should be falling
    private void checkGravity()
    {
        if (!onGround)
        {
            velocity.y -= gravity;
        }
    }

    #endregion Gravity

    #region GroundChecking / Snapping

    private void checkGround()
    {
        Ray ray = new Ray(transformPoint(liftSlopePoint), Vector3.down);

        //Uses a temporary raycast to check if the player is close to colliding with the ground
        RaycastHit temporaryHit = new RaycastHit();

        if (Physics.SphereCast(ray, liftSlopeRadius, out temporaryHit, maxGroundDistance, ignorePlayer))
        {
            snapToGround(temporaryHit);
        }

        else
        {
            onGround = false;
        }
    }

    //Determines where the player will snap to once they are within range of the ground
    private void snapToGround(RaycastHit temporaryHit)
    {
        float currentSlope = Vector3.Angle(temporaryHit.normal, Vector3.up);

        if (currentSlope >= maxModifiedSlope)
        {
            minGroundDistance = newMinGroundDistance;
        }

        else
        {
            minGroundDistance = oldMinGroundDistance;
        }

        /*
         * Checks if the player is on a slope. If the slope is near vertical, then it is considered a wall 
         * and the player will not snap to it. 
         */
        if (currentSlope >= minSlope && currentSlope <= maxSlope)
        {
            groundingClamp = new Vector3(transform.position.x, temporaryHit.point.y + groundCheckRadius / 2, transform.position.z);
            Collider[] collisions = new Collider[3];
            int num = Physics.OverlapSphereNonAlloc(transformPoint(groundCheckPoint), groundCheckRadius, collisions, ignorePlayer);

            onGround = false;

            for (int counter = 0; counter < num; counter++)
            {

                //If the player is colliding with a ground object, then they will snap to it
                if (collisions[counter].transform == temporaryHit.transform)
                {
                    groundHit = temporaryHit;
                    currentGroundSlope = currentSlope;
                    onGround = true;
                    if (groundHit.point.y <= transform.position.y + maxStepHeight && inputJump == false)
                    {
                        if (!smooth)
                            transform.position = new Vector3(transform.position.x, (groundHit.point.y + playerHeight / 2 + pushupDistance), transform.position.z);

                        else
                            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, (groundHit.point.y + playerHeight / 2 + pushupDistance), transform.position.z), smoothSpeed * Time.deltaTime);
                    }

                    break;
                }

            }


            if (num <= 1 && inputJump == false && temporaryHit.distance <= minGroundDistance)
            {
                if (collisions[0] != null)
                {
                    Ray ray = new Ray(transformPoint(liftSlopePoint), Vector3.down);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, minGroundDistance, ignorePlayer))
                    {
                        if (hit.transform != collisions[0].transform)
                        {
                            onGround = false;
                            return;
                        }
                    }
                }

                onGround = true;

                if (temporaryHit.point.y <= transform.position.y + maxStepHeight && inputJump == false)
                {
                    if (!smooth)
                        transform.position = new Vector3(transform.position.x, (temporaryHit.point.y + playerHeight / 2 + pushupDistance), transform.position.z);
                    else
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, (temporaryHit.point.y + playerHeight / 2 + pushupDistance), transform.position.z), smoothSpeed * Time.deltaTime);
                }

                return;
            }
        }
    }

    #endregion

    #region Jumping


    private void checkJumping()
    {
        bool canJump = false;

        canJump = !Physics.Raycast(new Ray(transform.position, Vector3.up), jumpBarrier, ignorePlayer);

        if (onGround && jumpHeight > 0.2f || jumpHeight <= 0.2f && onGround)
        {
            jumpHeight = 0;
            inputJump = false;
        }

        if (onGround && canJump)
        {
            if (keys.checkButtonCode("Jump"))
            {
                inputJump = true;
                jumpHeight += jumpForce;
            }
        }

        else
        {
            if (!onGround)
            {
                jumpHeight -= (jumpHeight * jumpDecrease * Time.deltaTime);
            }
        }

        velocity.y += jumpHeight;
    }

    #endregion Jumping

    #region Collision

    private void checkCollisions()
    {
        Collider[] overlaps = new Collider[4];

        int numerator = Physics.OverlapSphereNonAlloc(transform.TransformPoint(capsuleCollider.center), capsuleCollider.radius, overlaps, ignorePlayer, QueryTriggerInteraction.UseGlobal);

        for (int counter = 0; counter < numerator; counter++)
        {
            Transform trans = overlaps[counter].transform;
            Vector3 direction;
            float distance;

            if (Physics.ComputePenetration(capsuleCollider, transform.position, transform.rotation, overlaps[counter], trans.position, trans.rotation, out direction, out distance))
            {
                Vector3 penetrationVector = direction * distance;
                Vector3 velocityProjected = Vector3.Project(velocity, -direction);
                transform.position = transform.position + penetrationVector;
                velocity -= velocityProjected;
            }
        }
    }

    #endregion Collision

    #region Directions

    private Vector3 setSurfaceDirection(Vector3 direction, RaycastHit rayHit)
    {
        Vector3 vectDirection = transform.TransformDirection(direction);
        Vector3 temp = Vector3.Cross(rayHit.normal, vectDirection);
        Vector3 myDirection = Vector3.Cross(temp, rayHit.normal);

        return myDirection;
    }

    private Vector3 transformPoint(Vector3 point)
    {
        return transform.TransformPoint(point);
    }

    private Vector3 transformDirection(Vector3 direction)
    {
        return transform.TransformDirection(direction);
    }

    #endregion Directions

    public void setGravity(float gravity)
    {
        this.gravity = gravity;
    }

    public float getGravity()
    {
        return gravity;
    }

    public void setCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public bool getCanMove()
    {
        return canMove;
    }

    

    #region Serialization

    //Initialises all player movement data to be later saved to a JSON file
    public PlayerMovementData setPlayerData()
    {
        PlayerMovementData data = new PlayerMovementData();

        //data.groundHit = groundHit;
        data.rawVelocity_x = this.rawVelocity.x;
        data.rawVelocity_y = this.rawVelocity.y;
        data.rawVelocity_z = this.rawVelocity.z;
        data.currentGroundSlope = this.currentGroundSlope;
        data.grounded = this.onGround;
        data.groundingClamp_x = this.groundingClamp.x;
        data.groundingClamp_y = this.groundingClamp.y;
        data.groundingClamp_z = this.groundingClamp.z;
        data.velocity_x = this.velocity.x;
        data.velocity_y = this.velocity.y;
        data.velocity_z = this.velocity.z;
        data.jumpHeight = this.jumpHeight;
        data.move_x = this.move.x;
        data.move_y = this.move.y;
        data.move_z = this.move.z;
        data.inputJump = this.inputJump;
        data.lastVelocity_x = this.lastVelocity.x;
        data.lastVelocity_y = this.lastVelocity.y;
        data.lastVelocity_z = this.lastVelocity.z;

        return data;
    }
}

[System.Serializable]
public struct PlayerMovementData
{
    //public RaycastHit groundHit;
    public float currentGroundSlope, jumpHeight;
    public bool grounded, inputJump;

    public float groundingClamp_x, groundingClamp_y, groundingClamp_z;
    public float velocity_x, velocity_y, velocity_z;
    public float lastVelocity_x, lastVelocity_y, lastVelocity_z;
    public float rawVelocity_x, rawVelocity_y, rawVelocity_z;
    public float move_x, move_y, move_z;
    //public Vector3 groundingClamp, velocity, lastVelocity, rawVelocity, move;
}

#endregion Serialization