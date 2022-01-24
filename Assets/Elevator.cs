using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {
    [HideInInspector]
    public Vector3 minPoint;
    [HideInInspector]
    public Vector3 maxPoint;
    public float energy;
    public float energyPotential;

    public float height;

    // Start is called before the first frame update
    void Start () {
        minPoint = transform.position;
        maxPoint = minPoint + new Vector3 (0, height, 0);
    }

    // Update is called once per frame
    void Update () {

    }
}