using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class RoomManager : MonoBehaviour
{
    public Room outside;

    public List<Room> rooms;

    public static RoomManager init;

    [SerializeField] private float fillDelay = 0.2f;

    private void Awake()
    {
        init = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        outside = new Room();

        rooms = new List<Room>();

        rooms.Add(outside);
    }

    private void Flood(int x, int y)
    {
        if (x >= 0 && x < GridManager.init.numOfColumns && y >= 0 && y < GridManager.init.numOfRows)
        {
            if(GridManager.init.nodes[x, y].parentGameNode.seen == false && GridManager.init.nodes[x, y].parentGameNode.tileFurniture == null)
            {
                GridManager.init.nodes[x, y].parentGameNode.seen = true;

                Flood(x + 1, y);
                Flood(x - 1, y);

                Flood(x, y + 1);
                Flood(x, y - 1);

                Flood(x + 1, y - 1);
                Flood(x - 1, y - 1);

                Flood(x - 1, y + 1);
                Flood(x + 1, y + 1);
            }
        }
        
    }

    private void Find()
    {
        for(int i = 0; i < GridManager.init.numOfColumns; i++)
        {
            for (int j = 0; j < GridManager.init.numOfRows; j++)
            {
                if (GridManager.init.nodes[i, j].parentGameNode.seen == false && GridManager.init.nodes[i, j].parentGameNode.tileFurniture == null)
                {
                    

                    ArrayList nodeNeighbours = new ArrayList();
                    GridManager.init.GetNeighbours(GridManager.init.nodes[i, j], nodeNeighbours);
                    
                    for(int q = 0; q < nodeNeighbours.Count; q++) // If a room already exists asign it to the node.
                    {
                        if(((Node)nodeNeighbours[q]).currRoom != null)
                        {
                            GridManager.init.nodes[i, j].currRoom = ((Node)nodeNeighbours[q]).currRoom;
                            GridManager.init.nodes[i, j].currRoom.AssignNode(GridManager.init.nodes[i, j]);
                        }
                    }

                    if(GridManager.init.nodes[i, j].currRoom == null)
                    {
                        Room newRoom = new Room();
                        newRoom.ID = Random.Range(10, 501);
                        Debug.Log($"New room with ID: {newRoom.ID}");
                        rooms.Add(newRoom);
                        GridManager.init.nodes[i, j].currRoom = newRoom;
                        GridManager.init.nodes[i, j].currRoom.AssignNode(GridManager.init.nodes[i, j]);
                    }
                }
            }
        }
    }

    private void resetValues()
    {
        for (int i = 0; i < GridManager.init.numOfColumns; i++)
        {
            for (int j = 0; j < GridManager.init.numOfRows; j++)
            {
                GridManager.init.nodes[i, j].parentGameNode.seen = false;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            rooms.Clear(); // Don't do this please change
            rooms.Add(outside);
            Flood(0, 0);
            Debug.Log("done");
            Find();
            Debug.Log($"New rooms! size: {rooms.Count}");
            resetValues();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            
            
            
        }

    }



}
