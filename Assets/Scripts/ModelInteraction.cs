using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInteraction : MonoBehaviour
{
    //GameObject model;
    Vector3 minScale;
    Vector3 maxScale;
    float initDist = 1f;
    bool scaleBoundsSet = false;
    Dictionary<GameObject, List<Vector3>> modelDict = new Dictionary<GameObject, List<Vector3>>();

    // Start is called before the first frame update
    void Start()
    {
        //model = GameObject.FindGameObjectWithTag("ModelInteract");
        //minScale = transform.localScale * 0.5f;
        //maxScale = transform.localScale * 2f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetTouch(0).deltaPosition.x + " " + Input.GetTouch(0).deltaPosition.y);
        //transform.Rotate(0f, 0f, Input.GetTouch(0).deltaPosition.x * speedAdj);
        //transform.Rotate(Input.GetTouch(0).deltaPosition.y * speedAdj, 0f, Input.GetTouch(0).deltaPosition.x * speedAdj);

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
                    //Debug.Log(Input.GetTouch(0).deltaPosition.x + " " + Input.GetTouch(0).deltaPosition.y);
                    GameObject model = hit.collider.gameObject;
                    model.transform.Rotate(0f, 0f, t1.deltaPosition.x * speedAdj);
                    //transform.Rotate(Input.GetTouch(0).deltaPosition.x, Input.GetTouch(0).deltaPosition.y, 0f);
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
            //Vector3 initScale = transform.localScale;
            Vector3 initScale;

            if (Physics.Raycast(raycast, out hit))
            {
                // if the midpoint hits the object
                if (hit.collider.tag == "ModelInteract")
                {
                    GameObject model = hit.collider.gameObject;
                    initScale = model.transform.localScale;
                    if (!scaleBoundsSet)
                    {
                        minScale = initScale * 0.5f;
                        maxScale = initScale * 2f;
                        scaleBoundsSet = true;
                    }
                    // if touchphase is began, get the intial distance for scale
                    if (t1.phase == TouchPhase.Began || t2.phase == TouchPhase.Began)
                    {
                        Vector2 t1PosInit = t1.position;
                        Vector2 t2PosInit = t2.position;
                        initDist = Vector2.Distance(t1PosInit, t2PosInit);
                    }
                    // if touchphase is end, get the new distances for scale and then scale the object
                    else if (t1.phase == TouchPhase.Moved || t2.phase == TouchPhase.Moved)
                    {
                        Vector2 t1PosMove = t1.position;
                        Vector2 t2PosMove = t2.position;

                        float moveDist = Vector2.Distance(t1PosMove, t2PosMove);
                        float scale = (moveDist / initDist);
                        if (((initScale.x < minScale.x || initScale.y < minScale.y || initScale.z < minScale.z) && scale > 1.0f) ||
                            ((initScale.x > maxScale.x || initScale.y > maxScale.y || initScale.z > maxScale.z) && scale < 1.0f) || 
                            (((initScale.x > minScale.x && initScale.y > minScale.y && initScale.z > minScale.z) &&
                            (initScale.x < maxScale.x && initScale.y < maxScale.y && initScale.z < maxScale.z))))
                        {
                            initScale *= scale;
                            model.transform.localScale = initScale * scale;
                        }
                    }
                }
            }
        }

        
    }
}
