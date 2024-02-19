using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuncFortress.AStar;

public class MouseController : MonoBehaviour
{

    public GameObject stockPile;

    public GameObject wall;

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
                Instantiate(stockPile, temp, Quaternion.identity);

                GridManager.init.getNodeFromVec3(temp).parentGameNode.tileInv.isStockpile = true;

            }
        }
        
        if (Input.GetMouseButtonDown(3))
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
                Instantiate(wall, temp, Quaternion.identity);

            }
        }
    }

}
