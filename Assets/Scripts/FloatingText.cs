using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private Transform mainCam;
    [SerializeField] private Transform unit;
    [SerializeField]  private Transform worldSpaceCanvas;
    private RectTransform _rectTransform;

    public Vector3 offset;
    
    void Start()
    {
        this._rectTransform = GetComponent<RectTransform>();
        mainCam = Camera.main.transform;
        unit = transform.parent;
        worldSpaceCanvas = GameObject.FindGameObjectWithTag("WorldSpaceCanvas").transform;
        GetComponent<TextMeshProUGUI>().autoSizeTextContainer = true;
        
      // GetComponent<RectTransform>().sizeDelta = new Vector2(dPanel.GetComponent<RectTransform>().sizeDelta.x, 73 + (50 * PanelArttir(soru.dSikki.Length)));
        transform.SetParent(worldSpaceCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        // We make the text face the camera:
        
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);
        transform.position = unit.position + offset;
        //_rectTransform.anchoredPosition = new Vector2(unit.position.x + offset.x, unit.position.y + offset.y);
        //_rectTransform.localPosition = new Vector2(unit.position.x + offset.x, unit.position.y + offset.y);
    }
}
