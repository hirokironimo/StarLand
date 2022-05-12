using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlatformerMovementController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput playerInput;

    private PlayerInputActions playerInputActions;

    [Header("Jump Tuning")]
    [SerializeField]
    [Tooltip("This is the velocity that the player is set to when the jump button is released. A smaller damping velocity will make a more responsive jump.")]
    private float dampingVelocity = 1f;
    [SerializeField]
    private float minJumpHeight = 5f;
    [SerializeField]
    private float maxJumpHeight = 10f;

    [Header("Gravity Tuning")]
    [SerializeField]
    private float gravityScaleRising = 2f;
    [SerializeField]
    private float gravityScaleFalling = 4f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScaleRising;
    }
    private void OnEnable()
    {
        // Uses the input system and C# events to trigger actions.
        // C# events must be subscribed to and then unsubscribed from on disable.
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Jump.canceled += JumpDamp;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        rb.velocity = new Vector2(rb.velocity.x, maxJumpHeight);
        rb.gravityScale = gravityScaleRising;
    }
    private void JumpDamp(InputAction.CallbackContext context)
    {
        Debug.Log(rb.velocity.y);
        if (rb.velocity.y > minJumpHeight)
        {
            rb.velocity = new Vector2(rb.velocity.x, minJumpHeight);
        }
        else if (rb.velocity.y > dampingVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, dampingVelocity);
        }
    }


    // Update is called once per frame
    void Update()
    {
        rb.gravityScale = (rb.velocity.y > 0) ? gravityScaleRising : gravityScaleFalling;
    }
}
