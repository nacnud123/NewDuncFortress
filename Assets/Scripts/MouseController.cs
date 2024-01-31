using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{

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
    }

}
