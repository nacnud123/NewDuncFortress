using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class Room
{
    public List<Node> roomNodes = new List<Node>();
    public int ID;


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
}
