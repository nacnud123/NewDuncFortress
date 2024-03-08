using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Room
{
    public enum RoomType
    {
        Bedroom = 0,
        CommonRoom = 1,
        DiningRoom = 2,
        Hospital = 3,
        Barracks = 4,
        Storeroom = 5
    }


    public List<Node> roomNodes = new List<Node>();
    public int ID;
    public RoomType type;

    #region roomStatVars
    public float roomStat;
    public float clean;
    public float space;
    public float beauty;
    #endregion

    public bool isOutside()
    {
        return this == RoomManager.init.outside;
    }

    public void AssignNode(Node node)
    {
        if (roomNodes.Contains(node))
        {
            // This tile already in this room.
            return;
        }

        if (node.currRoom != null)
        {
            // Belongs to some other room.
            node.currRoom.roomNodes.Remove(node);
        }

        node.currRoom = this;
        roomNodes.Add(node);
    }

    /// <summary>
    /// Update the room stats and type.
    /// </summary>
    public void updateRoom() // Maybe refactor so its only one function
    {
        checkRoomType();
        updateStats();
    }

    /// <summary>
    /// Checks to type of the room. Should be called after a new item is placed
    /// </summary>
    private void checkRoomType() // TODO: Reafactor
    {
        if (isOutside()) // Is this needed?
            return;

        Debug.Log($"Update room type on room {this.ID}");

        int beds = 0, din = 0, rec = 0; 


        //Checks how many beds, how many dining objects (chairs & tables), & how many fun items there are (chess boards or something fun). 
        foreach(Node tile in roomNodes)
        {
            if (tile.parentGameNode.tileFurniture != null)
            {
                if (tile.parentGameNode.tileFurniture.type == "Bed") { beds += 1; }
                if (tile.parentGameNode.tileFurniture.type == "Dining") { din += 1; }
                if (tile.parentGameNode.tileFurniture.type == "Fun") { rec += 1; }
            }
        }


        // After checking see what type of room it is.
        if(beds == 1) { type = RoomType.Bedroom; }
        else if(beds > 1) { type = RoomType.Barracks; }

        if(din >= 1) { type = RoomType.DiningRoom; }

        if(rec >= 1) { type = RoomType.CommonRoom; }

        Debug.Log($"{ID}: B: {beds} D: {din} R: {rec}");
        Debug.Log($"{ID}: Type: {(int)type}");

    }

    /// <summary>
    /// Updates the beauty, space, and overall roomstat of the room. Should be called after a new item is placed
    /// </summary>
    private void updateStats()
    {
        if (isOutside()) // TODO: Is this needed?
            return;


        Debug.Log($"Update stats on room {this.ID}");
        foreach (Node tile in roomNodes)
        {
            if(tile.parentGameNode.tileFurniture != null)
            {
                beauty += tile.parentGameNode.tileFurniture.beautyVal;
            }
            space += 1;
        }

        roomStat = beauty + space;

        Debug.Log($"{ID}: B: {beauty} S: {space} RS: {roomStat}");

    }

        public void UnassignNode(Node node)
    {
        if (roomNodes.Contains(node) == false)
        {
            // This tile in not in this room.
            return;
        }

        node.currRoom = null;
        roomNodes.Remove(node);
    }

    public void ReturnTilesToOutsideRoom()
    {
        for (int i = 0; i < roomNodes.Count; i++)
        {
            // Assign to outside.
            roomNodes[i].currRoom = RoomManager.init.outside;
        }

        roomNodes.Clear();
    }

    public bool isRoomEmpty()
    {
        return roomNodes.Count == 0;
    }
}
