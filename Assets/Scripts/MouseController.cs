using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class MouseController : MonoBehaviour
{

    public GameObject stockPile;

    public GameObject wall;

    [Header("Zoom Settings")]
    private float zoom;
    private float zoomMultiplyer = 4f;
    public float minZoom = 2f;
    public float maxZoom = 12f;
    private float vel = 0f;
    private float smoothTime = .25f;

    [Header("Movment Settings")]
    private Vector3 lastPosition;
    public float mouseSensitivity;

    private void Start()
    {
        zoom = Camera.main.orthographicSize;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Vector3 targetPos = hit.point;
                //Debug.Log(targetPos);
                if (hit.collider.GetComponent<Tree>())
                {
                    Debug.Log("Clicked on tree!");
                    JobQueue.init.Enqueue(new Gather(0,hit.collider.GetComponent<Entity>()));
                }
                if (hit.collider.GetComponent<Person>())
                {
                    GameManager.init.selectedPlayer = hit.transform.gameObject;
                }
                else
                {
                    GameManager.init.selectedPlayer = null;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Mouse 1");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Vector3 targetPos = hit.point;
                Debug.Log(targetPos);
                Vector3 temp = GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(targetPos));
                temp.z = 0;
                
                Instantiate(BuildingController.init.currentBuildingObj, temp, Quaternion.identity);

                if(BuildingController.init.currBuildings == BuildingController.Buildings.Stockpile)
                    GridManager.init.getNodeFromVec3(temp).parentGameNode.tileInv.isStockpile = true;

            }
        }
    }

    private void FixedUpdate()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplyer;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, zoom, ref vel, smoothTime);

        if (Input.GetMouseButtonDown(2))
        {
            lastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 delta = lastPosition - Input.mousePosition;
            transform.Translate(delta.x * mouseSensitivity, delta.y * mouseSensitivity, 0);
            lastPosition = Input.mousePosition;
        }
    }

}
