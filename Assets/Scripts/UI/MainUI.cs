using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public GameObject jobUI, buildingUI, selectionUI;

    public void openJobUI()
    {
        JobController.init.active = true;
        BuildingController.init.active = false;
        selectionUI.SetActive(false);
        jobUI.SetActive(true);
    }

    public void openBuildingUI()
    {
        BuildingController.init.active = true;
        JobController.init.active = false;
        buildingUI.SetActive(true);
        selectionUI.SetActive(false);
    }

    public void goBack()
    {
        BuildingController.init.active = false;
        JobController.init.active = false;

        if (jobUI.activeInHierarchy)
            jobUI.SetActive(false);

        if (buildingUI.activeInHierarchy)
            buildingUI.SetActive(false);

        selectionUI.SetActive(true);
    }

}
