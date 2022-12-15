using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInteraction : MonoBehaviour
{
    float initDist = 1f;
    bool scaleBoundsSet = false;
    Dictionary<GameObject, List<Vector3>> modelDict = new Dictionary<GameObject, List<Vector3>>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] interactables = GameObject.FindGameObjectsWithTag("ModelInteract");
        foreach (GameObject g in interactables)
        {
            // rotations might not need to be stored the way the current size and limits do - no need to check
            Vector3 modelInitScale = g.transform.localScale;
            Vector3 modelMinScale = modelInitScale * 0.5f;
            Vector3 modelMaxScale = modelInitScale * 2.0f;

            // 0 = initial/current size, 1 = min size, 2 = max size
            List<Vector3> modelLimits = new List<Vector3>() {
                modelInitScale, modelMinScale, modelMaxScale
            };
            modelDict.Add(g, modelLimits);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // tap and drag for rotation
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            Touch t1 = Input.GetTouch(0);
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            float speedAdj = 0.3f;

            if (Physics.Raycast(raycast, out hit))
            {
                if (hit.collider.tag == "ModelInteract")
                {
                    GameObject model = hit.collider.gameObject;
                    model.transform.Rotate(0f, 0f, -t1.deltaPosition.x * speedAdj);
                }

            }
        }

        // pinch/zoom for rescaling
        // if there's 2 touchpoints --> get the initial distance w/ began, get updated distance w/ moved and calc scaling
        if (Input.touchCount == 2)
        {
            // calculate the midpoint btwn the 2 touches to see if the object is targeted
            Touch t1 = Input.GetTouch(0);
            Touch t2 = Input.GetTouch(1);
            Vector2 direction = t1.position - t2.position;
            Vector2 midpoint = t1.position + (direction / 2f);
            Ray raycast = Camera.main.ScreenPointToRay(midpoint);
            RaycastHit hit;

            // if the midpoint ray hits something
            if (Physics.Raycast(raycast, out hit))
            {
                // if the midpoint hits the object
                if (hit.collider.tag == "ModelInteract")
                {
                    // identify the object hit and initialize the var that holds the limits/values used for the object
                    GameObject model = hit.collider.gameObject;
                    List<Vector3> currModelLimits;
                    currModelLimits = modelDict[model];

                    // if touchphase is began, get the intial distance for scale - doesn't require model-specific limits
                    if (t1.phase == TouchPhase.Began || t2.phase == TouchPhase.Began)
                    {
                        Vector2 t1PosInit = t1.position;
                        Vector2 t2PosInit = t2.position;
                        initDist = Vector2.Distance(t1PosInit, t2PosInit);
                    }
                    // if touchphase is moved, get the new distances for scale and then scale the object - requires model-specific limits
                    else if (t1.phase == TouchPhase.Moved || t2.phase == TouchPhase.Moved)
                    {
                        // calculate how much to scale the object
                        Vector2 t1PosMove = t1.position;
                        Vector2 t2PosMove = t2.position;
                        float moveDist = Vector2.Distance(t1PosMove, t2PosMove);
                        float scale = (moveDist / initDist);

                        // pull out the values for readability
                        Vector3 initScale = currModelLimits[0];
                        Vector3 minScale = currModelLimits[1];
                        Vector3 maxScale = currModelLimits[2];

                        // conditions - size is below the min and scaling up, size is above the max and scaling down, or within the bounds
                        // if one of these is true, modify the size
                        if (((initScale.x <= minScale.x || initScale.y <= minScale.y || initScale.z <= minScale.z) && scale > 1.0f) ||
                            ((initScale.x >= maxScale.x || initScale.y >= maxScale.y || initScale.z >= maxScale.z) && scale < 1.0f) || 
                            (((initScale.x > minScale.x && initScale.y > minScale.y && initScale.z > minScale.z) &&
                            (initScale.x < maxScale.x && initScale.y < maxScale.y && initScale.z < maxScale.z))))
                        {
                            // transform the object
                            // ok check that the transformation won't yank it outside of its limits
                            Vector3 newScale = initScale * scale;
                            // if anything's bigger, lock to max
                            if (newScale.x >= maxScale.x || newScale.y >= maxScale.y || newScale.z >= maxScale.z)
                            {
                                model.transform.localScale = maxScale;
                            }
                            // if anything's smaller, lock to min
                            else if (newScale.x <= minScale.x || newScale.y <= minScale.y || newScale.z <= minScale.z)
                            {
                                model.transform.localScale = minScale;
                            }
                            // otherwise transform
                            else
                            {
                                model.transform.localScale = initScale * scale;
                            }

                            // save the updated values to the object
                            initScale = model.transform.localScale;
                            currModelLimits[0] = initScale;
                            modelDict[model] = currModelLimits;
                        }   
                    }
                }
            }
        }

        
    }
}
