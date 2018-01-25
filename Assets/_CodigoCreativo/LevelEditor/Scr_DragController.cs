//ruth sofia brown
//git rsofia
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Scr_DragController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Ray ray;
    private RaycastHit hit;
    private Vector3 tempPosition; //variable to store mouse temporary pos
    GameObject levelEditor;

    void Start()
    {
        levelEditor = GameObject.FindObjectOfType<Scr_LevelEditor>().gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        levelEditor.SetActive(false);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag.Contains("LE"))
            {
                tempPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hit.collider.transform.position = new Vector3(tempPosition.x, tempPosition.y, hit.collider.transform.position.z);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        levelEditor.SetActive(true);
    }
}
