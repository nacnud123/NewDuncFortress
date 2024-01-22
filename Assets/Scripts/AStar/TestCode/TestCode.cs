using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

namespace DuncFortress.AStar
{
    public class TestCode : MonoBehaviour
    {
        private Transform startPos, endPos;
        public Node startNode { get; set; }
        public Node goalNode { get; set; }

        public ArrayList pathArray;

       public GameObject objStartCube, objEndCube;

        private float elapsedTime = 0.0f;
        public float intervalTime = 1.0f; //Interval time between path finding

        public GameObject tank;
        public int index = 0;
        public Vector3 nextPos;
        public int speed = 5;

        public bool shouldMove = false;

        public bool reachedDestination = false;

        // Use this for initialization
        void Start()
        {
            objStartCube = GameObject.FindGameObjectWithTag("Start");
            objEndCube = GameObject.FindGameObjectWithTag("End");

            tank.transform.position = objStartCube.transform.position;
            nextPos = tank.transform.position;

            //AStar Calculated Path
            pathArray = new ArrayList();
            FindPath();


        }

        // Update is called once per frame
        void Update()
        {
            
            if (tank.transform.position == nextPos) // Move Tank
            {
                if (index < pathArray.Count)
                {
                    Node temp = (Node)pathArray[index];
                    nextPos = temp.position;

                    tank.transform.position = Vector3.MoveTowards(tank.transform.position, nextPos, speed * Time.deltaTime);
                    index += 1;

                }
            }
            else
            {
                Debug.Log("Moving");
                tank.transform.position = Vector3.MoveTowards(tank.transform.position, nextPos, speed * Time.deltaTime);
            }
            if(tank.transform.position == endPos.position)
            {
                reachedDestination = true;
            }
        }

        void FindPath()
        {
            startPos = tank.transform;
            endPos = objEndCube.transform;

            //Assign StartNode and Goal Node
            startNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(startPos.position)));
            goalNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(endPos.position)));

            pathArray = AStar.FindPath(startNode, goalNode);
        }

        public void runTemp(Node _endTile)
        {
            reachedDestination = false;
            Debug.Log("runTemp called");
            index = 0;
            pathArray.Clear();
            objStartCube.transform.position = tank.transform.position;
            nextPos = tank.transform.position;

            objEndCube.transform.position = _endTile.position;
            
            FindPath();

            shouldMove = true;
            //tank.transform.position = objStartCube.transform.position;
            //nextPos = objStartCube.transform.position;
        }

        void OnDrawGizmos()
        {
            if (pathArray == null)
                return;

            if (pathArray.Count > 0)
            {
                int index = 1;
                foreach (Node node in pathArray)
                {
                    if (index < pathArray.Count)
                    {
                        Node nextNode = (Node)pathArray[index];
                        Debug.DrawLine(node.position, nextNode.position, Color.green);
                        index++;
                    }
                };
            }
        }
    }
}