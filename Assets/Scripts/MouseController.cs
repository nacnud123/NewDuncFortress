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
    private Vector3 lastFramePosition;
    private Vector3 currFramePosition;
    public Vector3 camPosition;

    [Header("Dragging stuff")]
    public GameObject previewObj;
    public RectTransform selectionBox;
    private Vector2 startPos;
    private Vector2 endPosition;

    private void Start()
    {
        camPosition = this.transform.position;
        startPos = Vector2.zero;
        endPosition = Vector2.zero;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            
            DrawVisuals();
        }
        if (Input.GetMouseButtonUp(0))
        {
            selectStuff();
            startPos = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisuals();
        }
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
                if (hit.collider.GetComponent<Tree>())
                {
                    Debug.Log("Clicked on tree!");
                    JobQueue.init.Enqueue(new Gather(0, hit.collider.GetComponent<Entity>()));
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

                    /*RoomManager.init.Flood(0, 0);
                    Debug.Log("done");
                    RoomManager.init.Find();
                    Debug.Log($"New rooms! size: {RoomManager.init.rooms.Count}");
                    RoomManager.init.resetValues();*/
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

        foreach(Entity e in GameManager.init.entities)
        {
            if (e.GetComponent<Tree>())
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(e.transform.position);

                if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
                {
                    JobQueue.init.Enqueue(new Gather(0, e));
                }
            }
            
        }
    }
}