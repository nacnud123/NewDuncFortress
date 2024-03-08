using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class MouseController : MonoBehaviour
{

    public GameObject stockPile;

    public GameObject wall;

    [Header("Zoom Settings")]
    public float zoomSpeed = .05f;
    public float zoomMin = .001f;
    public float zoomMaz = 2.0f;
    public float dragZensitivity = 1.0f;
    public float zoomTarget = 11f;

    [Header("Movment Settings")]
    public bool isDragging = false;
    private Vector3 lastFramePosition;
    private Vector3 currFramePosition;
    public Vector3 camPosition;


    private void Start()
    {
        camPosition = this.transform.position;
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

                if (hit.collider.GetComponent<Furniture>())
                {
                    hit.collider.GetComponent<Furniture>().destroy();
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
                //Debug.Log(targetPos);
                Vector3 temp = GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(targetPos));
                temp.z = 0;
                
                Instantiate(BuildingController.init.currentBuildingObj, temp, Quaternion.identity);

                if(BuildingController.init.currBuildings == BuildingController.Buildings.Stockpile)
                    GridManager.init.getNodeFromVec3(temp).parentGameNode.tileInv.isStockpile = true;

                /*if (BuildingController.init.currBuildings == BuildingController.Buildings.Wall || BuildingController.init.currBuildings == BuildingController.Buildings.Door)
                {
                    RoomManager.init.Flood(0, 0);
                    Debug.Log("done");
                    RoomManager.init.Find();
                    Debug.Log($"New rooms! size: {RoomManager.init.rooms.Count}");
                    RoomManager.init.resetValues();

                }*/

            }
        }
    }

    private void FixedUpdate()
    {
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(2))
        {
            Vector3 diff = lastFramePosition - currFramePosition;

            if (diff != Vector3.zero)
            {
                Camera.main.transform.Translate(diff);
            }
        }

        if (Camera.main.orthographicSize != zoomTarget)
        {
            float target = Mathf.Lerp(Camera.main.orthographicSize, zoomTarget, 10 * Time.deltaTime);
            Camera.main.orthographicSize = Mathf.Clamp(target, 3f, 25f);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0) { zoom(.1f); }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) { zoom(-.1f); }

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    private void zoom(float ammount)
    {
        zoomTarget = Camera.main.orthographicSize - (zoomSpeed * (Camera.main.orthographicSize * ammount));
    }

}
