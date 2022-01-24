using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    InputActions playerInput;
    InputAction Move;

    Rigidbody2D rb;

    [SerializeField]
    float walk_speed;

    [SerializeField]
    FloatReference White;

    [SerializeField]
    float white_scale;

    [SerializeField]
    FloatReference Red;

    Vector3 startPos;

    [SerializeField]
    float JumpForce;
    bool isGrounded;

    bool isEnergizing;

    [SerializeField]
    LayerMask GroundCheckLayerMask;

    public Collider2D OnTopOf;

    [SerializeField]
    float energyTransferCurve;

    void Awake () {
        playerInput = new InputActions ();
        rb = GetComponent<Rigidbody2D> ();

        White.Variable.SetValue (100);
        Red.Variable.SetValue (100);

        startPos = transform.position;
    }

    void OnEnable () {
        Move = playerInput.Player.Move;
        Move.Enable ();

        playerInput.Player.Interact.performed += DoInteract;
        playerInput.Player.Interact.Enable ();

        playerInput.Player.Jump.started += Jump;
        playerInput.Player.Jump.Enable ();

        playerInput.Player.Increase_Decrease.started += StartEnergizing;
        playerInput.Player.Increase_Decrease.canceled += StopEnergizing;

        playerInput.Player.Increase_Decrease.Enable ();
    }

    void OnDisable () {
        Move.Disable ();
        playerInput.Player.Interact.Disable ();
        playerInput.Player.Jump.Disable ();
        playerInput.Player.Increase_Decrease.Disable ();
    }

    void CheckIsGrounded () {
        var hit = Physics2D.CircleCast (transform.position, .25f, new Vector2 (0, -1f), 3, GroundCheckLayerMask);
        isGrounded = hit;

        if (hit)
            OnTopOf = hit.collider;
        else
            OnTopOf = null;
    }

    void Update () {
        White.Variable.SetValue (Mathf.Max (0, (100 - (transform.position.x - startPos.x) * white_scale)));
        CheckIsGrounded ();

    }

    void FixedUpdate () {
        Vector2 moveAxes = Move.ReadValue<Vector2> ();

        if (White > 0)
            rb.velocity = new Vector2 (moveAxes.x * walk_speed * Time.deltaTime, rb.velocity.y);

        if (OnTopOf != null) {
            //if we are top of an object we can interact with.
            if (OnTopOf.CompareTag ("Red") && isEnergizing) {
                var Elevator = OnTopOf.GetComponent<Elevator> ();
                var transferAmt = energyTransferCurve * playerInput.Player.Increase_Decrease.ReadValue<float> ();
                if (Elevator.energy + transferAmt <= Elevator.energyPotential &&
                    Elevator.energy + transferAmt >= 0) 
                {
                    Red.Variable.ApplyChange (-transferAmt);
                    Elevator.energy += transferAmt;
                    OnTopOf.transform.position = Vector3.Lerp (Elevator.minPoint, Elevator.maxPoint, Mathf.InverseLerp (0, Elevator.energyPotential, Elevator.energy));;
                }
            }
        }
    }
    /*
    INPUT CALLBACKS
    */
    void DoInteract (InputAction.CallbackContext obj) {

    }

    void Jump (InputAction.CallbackContext obj) {
        if (isGrounded) {
            rb.AddForce (new Vector2 (0.0f, JumpForce), ForceMode2D.Impulse);
        }
    }

    void StartEnergizing (InputAction.CallbackContext obj) {
        Debug.Log("Start");
        isEnergizing = true;

    }
    void StopEnergizing (InputAction.CallbackContext obj) {
        Debug.Log("Stop");
        isEnergizing = false;
    }
}