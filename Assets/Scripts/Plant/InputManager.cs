using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Camera mainCam;
    private Vector3 lastPos;
    public LayerMask layerMask;
    public Grid grid;
    public Vector3 GetSelectionMap()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 50, layerMask))
        {
            lastPos = hit.point;
        }
        return lastPos;
    }
}
