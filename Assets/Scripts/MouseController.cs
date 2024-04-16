using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class MouseController : MonoBehaviour
{
    [Header("Zoom Settings")]
    public float zoomSpeed = .05f;
    public float zoomMin = .001f;
    public float zoomMaz = 2.0f;
    public float dragZensitivity = 1.0f;
    public float zoomTarget = 11f;

    [Header("Movment Settings")]
    private Vector3 lastFramePosition;
    private Vector3 currFramePosition;
    public Vector3 camPosition;

    [Header("Dragging stuff")]
    public GameObject previewObj;
    public RectTransform selectionBox;
    private Vector2 startPos;
    private Vector2 endPosition;

    private Vector3 dragStartPosition;
    private List<GameObject> dragPreviweGameObjects;
    private bool isDragging = false;


    private void Start()
    {
        camPosition = this.transform.position;
        dragPreviweGameObjects = new List<GameObject>();
        startPos = Vector2.zero;
        endPosition = Vector2.zero;
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Vector3 targetPos = hit.point;
                //Debug.Log(targetPos);

                if(JobController.init.active)
                {
                    if(JobController.init.jobSelected == JobController.Jobs.Gather)
                    {
                        if (hit.collider.GetComponent<Tree>())
                        {
                            Debug.Log("Clicked on tree!");
                            JobQueue.init.Enqueue(new Gather(0, hit.collider.GetComponent<Entity>()));
                        }
                        if (hit.collider.GetComponent<Rock>())
                        {
                            Debug.Log("Clicked on rock!");
                            JobQueue.init.Enqueue(new Gather(1, hit.collider.GetComponent<Entity>()));
                        }
                    }
                    else if (JobController.init.jobSelected == JobController.Jobs.Destroy)
                    {
                        if (hit.collider.GetComponent<Furniture>())
                        {
                            hit.collider.GetComponent<Furniture>().destroy();

                            /*RoomManager.init.Flood(0, 0);
                            Debug.Log("done");
                            RoomManager.init.Find();
                            Debug.Log($"New rooms! size: {RoomManager.init.rooms.Count}");
                            RoomManager.init.resetValues();*/
                        }
                    }

                }

                if(BuildingController.init.active)
                {
                    //Debug.Log(targetPos);
                    Vector3 temp = GridManager.init.GetGridCellCenter(GridManager.init.GetGridIndex(targetPos));
                    temp.z = 0;

                    if (GridManager.init.getNodeFromVec3(temp).parentGameNode.tileFurniture == null)
                    {
                        Instantiate(BuildingController.init.currentBuildingObj, temp, Quaternion.identity);

                        if (BuildingController.init.currBuildings == BuildingController.Buildings.Stockpile)
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
        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            if (startPos != endPosition)
                DrawVisuals();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (startPos != endPosition)
                selectStuff();
            startPos = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisuals();
        }
        //Camera Movement
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



        //UpdateDragging();
    }


    private void zoom(float ammount)
    {
        zoomTarget = Camera.main.orthographicSize - (zoomSpeed * (Camera.main.orthographicSize * ammount));
    }
   

    private void DrawVisuals()
    {
        Vector2 boxStart = startPos;
        Vector2 boxEnd = endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        selectionBox.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));

        selectionBox.sizeDelta = boxSize;
    }

    private void selectStuff()
    {
        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        Debug.Log($"Min: {min}, Max: {max}");

        if(JobController.init.active)
        {
            if(JobController.init.jobSelected == JobController.Jobs.Gather)
            {
                foreach (Entity e in GameManager.init.entities)
                {
                    if (e.GetComponent<Tree>() || e.GetComponent<Rock>())
                    {
                        int rId = 0;
                        if (e.GetComponent<Rock>())
                            rId = 1;

                        Vector3 screenPos = Camera.main.WorldToScreenPoint(e.transform.position);

                        if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
                        {
                            JobQueue.init.Enqueue(new Gather(rId, e));
                        }
                    }

                }
            }
            else if(JobController.init.jobSelected == JobController.Jobs.Destroy)
            {
                foreach (Entity e in GameManager.init.entities)
                {
                    if (e.GetComponent<Tree>())
                    {
                        Vector3 screenPos = Camera.main.WorldToScreenPoint(e.transform.position);

                        if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
                        {
                            e.GetComponent<Furniture>().destroy();
                        }
                    }

                }
            }
        }
        
    }
}