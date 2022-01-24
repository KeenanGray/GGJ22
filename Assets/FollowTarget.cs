using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {
    [SerializeField]
    Transform target;

    //Use the background to calculate boundary sizes
    SpriteRenderer Background;
    Camera cam;
    //Follow the target, keeping the camera in bounds

    // Start is called before the first frame update
    void Start () {
        Background = GameObject.Find ("Background").GetComponent<SpriteRenderer> ();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update () {
        var xPos = target.position.x;
        var yPos = target.position.y;

        Vector2 BackgroundSize = Background.size * Background.transform.localScale;
        print (Background.size);
        
        float screenAspect = (float) Screen.width / (float) Screen.height;
        float camHalfHeight = cam.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        float camWidth = 2.0f * camHalfWidth;

        if (xPos < 0)
            xPos = Mathf.Max (-BackgroundSize.x/2 + camHalfWidth, xPos);
        else
            xPos = Mathf.Min (BackgroundSize.x/2 - camHalfWidth, xPos);

        transform.position = new Vector3 (xPos, target.position.y, transform.position.z);
    }
}