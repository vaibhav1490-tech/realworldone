using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontrolle : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform pl1, pl2;

    public float normalzoom,distancezoom;

    private Camera cmr;

    public float distancebetweenplayer;
    private void Start()
    {
        cmr = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        distancebetweenplayer = (pl1.position - pl2.position).magnitude;
        if (distancebetweenplayer > 10)
        {
            cmr.orthographicSize = Mathf.Lerp(normalzoom, distancezoom, 2);
        }

        else
        {
            cmr.orthographicSize = Mathf.Lerp(distancezoom, normalzoom, 2);
        }
    }
}
