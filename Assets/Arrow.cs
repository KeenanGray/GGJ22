using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Arrow : MonoBehaviour {

    Vector3 Direction;

    InputActions playerInput;
    Transform end;

    InputAction rotate;

    float Angle;
    // Start is called before the first frame update
    void Awake () {
        playerInput = new InputActions ();
        end = transform.Find ("End");
        Angle = transform.parent.childCount;
    }

    void OnEnable () {
        rotate = playerInput.Player.Increase_Decrease;
        rotate.performed += ChangeDual;
        rotate.Enable ();
    }
    void OnDisable () {
        rotate = playerInput.Player.Increase_Decrease;
        rotate.Disable ();
    }

    // Update is called once per frame
    void Update () {
        Vector3 dir = (end.transform.position - transform.position).normalized;
        Debug.DrawLine (transform.position, transform.position + dir * 10, Color.red, Mathf.Infinity);
    }

    void ChangeDual (InputAction.CallbackContext obj) {
        transform.localEulerAngles += new Vector3(0,0,obj.ReadValue<float>()*90);
    }
}