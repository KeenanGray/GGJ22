using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display_Value : MonoBehaviour {

    Slider slider;
    [SerializeField]
    FloatReference FloatRef;
    // Start is called before the first frame update
    void Start () {
        slider = GetComponent<Slider> ();
    }

    // Update is called once per frame
    void Update () {
        slider.value = Mathf.InverseLerp (0, 100, FloatRef.Value);
    }
}