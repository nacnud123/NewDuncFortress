using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class RoomManager : MonoBehaviour
{
    public Room outside = new Room();

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
        //outside = new Room();

        rooms = new List<Room>();

        rooms.Add(outside);
    }


    /// <summary>
    /// Flood Fill. Won't detect nodes that are within a room. Check bug comment
    /// </summary>
    /// <param name="x">Start X pos</param>
    /// <param name="y">Start Y pos</param>
    public void Flood(int x, int y)
    {
        // If the position of x and y are a wall or other object then it will fail. TODO: Fix or prevent
        if (x >= 0 && x < GridManager.init.numOfColumns && y >= 0 && y < GridManager.init.numOfRows)
        {
            if(GridManager.init.nodes[x, y].parentGameNode.seen == false && GridManager.init.nodes[x, y].parentGameNode.tileFurniture == null)
            {
                GridManager.init.nodes[x, y].parentGameNode.seen = true;
                GridManager.init.nodes[x, y].parentGameNode.sr.color = Color.red;

                if(!GridManager.init.nodes[x, y].currRoom.isOutside())
                {
                    Room oldRoom = new Room();
                    Debug.Log($"Return node at {x}, {y} to outside");
                    oldRoom = GridManager.init.nodes[x, y].currRoom;
                    GridManager.init.nodes[x, y].currRoom.UnassignNode(GridManager.init.nodes[x, y]);
                    GridManager.init.nodes[x, y].currRoom = outside;

                    if (oldRoom.isRoomEmpty())
                    {
                        Debug.Log($"Room with {oldRoom.ID} has been removed because it is empty");
                        rooms.Remove(oldRoom);
                    }
                }

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

    /// <summary>
    /// After checking for enclosed spaces. Now assign them to a room, make a new room, or return them to the outside. Check bug comment
    /// </summary>
    public void Find()
    {
        //Bugs: returning to the oustide is not tested yet. TODO: Fix by adding a way to remove walls.

        for(int i = 0; i < GridManager.init.numOfColumns; i++)
        {
            for (int j = 0; j < GridManager.init.numOfRows; j++)
            {
                if (GridManager.init.nodes[i, j].parentGameNode.seen == false && GridManager.init.nodes[i, j].parentGameNode.tileFurniture == null)
                {
                    //TODO: Need way to return room back to the outside

                    ArrayList nodeNeighbours = new ArrayList();
                    GridManager.init.GetNeighbours(GridManager.init.nodes[i, j], nodeNeighbours); 
                    
                    for(int q = 0; q < nodeNeighbours.Count; q++) // If a room already exists asign it to the node.
                    {
                        if(((Node)nodeNeighbours[q]).currRoom != outside)
                        {
                            GridManager.init.nodes[i, j].currRoom = ((Node)nodeNeighbours[q]).currRoom;
                            GridManager.init.nodes[i, j].currRoom.AssignNode(GridManager.init.nodes[i, j]);
                            GridManager.init.nodes[i, j].parentGameNode.sr.color = ((Node)nodeNeighbours[q]).currRoom.roomColor;
                        }
                    }

                    if(GridManager.init.nodes[i, j].currRoom == outside) // If a room does not exist make a new room and assign the node to it
                    {
                        Room newRoom = new Room();
                        newRoom.ID = Random.Range(10, 501);
                        newRoom.roomColor = Random.ColorHSV();
                        Debug.Log($"New room with ID: {newRoom.ID}");
                        rooms.Add(newRoom);
                        GridManager.init.nodes[i, j].currRoom = newRoom;
                        GridManager.init.nodes[i, j].currRoom.AssignNode(GridManager.init.nodes[i, j]);
                        GridManager.init.nodes[i, j].parentGameNode.sr.color = newRoom.roomColor;

                        // Maybe call updateRoom / update stats
                    }

                    /*if (returnToOutside) // if the room should be returned to the outside. Bug
                    {
                        Debug.Log("Return node to outside");
                        oldRoom = GridManager.init.nodes[i, j].currRoom;
                        GridManager.init.nodes[i, j].currRoom.UnassignNode(GridManager.init.nodes[i, j]);
                        GridManager.init.nodes[i, j].currRoom = outside;

                        if (oldRoom.isRoomEmpty())
                        {
                            Debug.Log($"Room with {oldRoom.ID} has been removed because it is empty");
                            rooms.Remove(oldRoom);
                        }
                    }*/
                }
            }
        }
    }

    public void resetValues() // Resets all of the values. So it can all be checked again. TODO: Maybe find new way?
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
        if (Input.GetKeyDown(KeyCode.F)) // Testing code
        {
            Flood(0, 0);
            Debug.Log("done");
            Find();
            Debug.Log($"New rooms! size: {rooms.Count}");
            resetValues();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Find();
            Flood(0, 0);
            resetValues();
        }

    }



}
