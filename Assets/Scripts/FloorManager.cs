using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private GameObject floorLayer;
    [SerializeField] public float[] floorLayerChangingScales;

    [SerializeField] private GameObject[][] floorRows;
    
    [SerializeField] private GameObject[] firstRow;
    [SerializeField] private GameObject[] secondRow;
    [SerializeField] private GameObject[] thirdRow;
    [SerializeField] private GameObject[] fourthRow;
    [SerializeField] private GameObject[] fifthRow;
    [SerializeField] private GameObject[] sixthRow;

    [SerializeField] public float[] rowTimers;
    [SerializeField] private float currentTime;
    [SerializeField] public int currentRow = 0;
    [SerializeField] private float timeBetweenFragmentFalls;
    [SerializeField] private float fragmentFallingSpeed;
    [SerializeField] private float fallingWarningTime;

    [SerializeField] private GameObject colorExample;
    [SerializeField] private Color floorColor;

    private void Awake()
    {
        floorRows = new GameObject[6][];
        
        floorRows[0] = firstRow;
        floorRows[1] = secondRow;
        floorRows[2] = thirdRow;
        floorRows[3] = fourthRow;
        floorRows[4] = fifthRow;
        floorRows[5] = sixthRow;

        floorColor = colorExample.GetComponent<MeshRenderer>().material.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = rowTimers[currentRow];
        foreach (var floorFragment in floorRows[currentRow])
        {
            string colorTag = "_Color";
            Material floorFragmentMaterial = floorFragment.GetComponent<MeshRenderer>().material;
            floorFragmentMaterial.SetColor("_BaseColor", Color.magenta);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        
        if (currentTime <= 0f && currentRow <=5)
        {
            float timeBetweenFragmentFallsAcumulated = 0f;
            foreach (var floorFragment in floorRows[currentRow])
            {
                StartCoroutine(MakeFragmentFall(fragmentFallingSpeed, floorFragment, timeBetweenFragmentFallsAcumulated));
                timeBetweenFragmentFallsAcumulated += timeBetweenFragmentFalls;
            }

            StartCoroutine(SetFloorCollisionChangeTimer(fallingWarningTime, currentRow));
            currentRow++;
            currentTime = rowTimers[currentRow];
        }
    }

    /*
    IEnumerator MakeRowFall(GameObject[] row)
    {
        foreach (var floorFragment in row)
        {
            StartCoroutine(MakeFragmentFall(fragmentFallingSpeed, floorFragment));

            yield return new WaitForSeconds(timeBetweenFragmentFalls);
        }
    }
    */

    IEnumerator SetFloorCollisionChangeTimer(float timeUntilChange, int row)
    {
        yield return new WaitForSeconds(timeUntilChange);
        floorLayer.transform.localScale = new Vector3(floorLayerChangingScales[row], floorLayer.transform.localScale.y, floorLayerChangingScales[row]);
    }
    
    IEnumerator MakeFragmentFall (float fallingTime, GameObject floorFragment, float timeUntiLFall)
    {
        float internalTimer = 0f;
        float internalSwitchTimerMax = fallingWarningTime/20f;
        float internalSwitchTimer = 0f;
        string colorTag = "_BaseColor";
        Material floorFragmentMaterial = floorFragment.GetComponent<MeshRenderer>().material;
        floorFragmentMaterial.SetColor(colorTag, Color.magenta);
        bool isPink= true;

        while (internalTimer < fallingWarningTime)
        {
            internalTimer += Time.deltaTime;
            internalSwitchTimer -= Time.deltaTime;
            
            if (internalTimer >= fallingWarningTime/2f)
            {
                if (isPink && internalSwitchTimer < 0f)
                {
                    floorFragmentMaterial.SetColor(colorTag, floorColor);
                    internalSwitchTimerMax -= fallingWarningTime / 300f;
                    if (internalSwitchTimerMax > 0.15f)
                    {
                        internalSwitchTimer = internalSwitchTimerMax;
                    }
                    else
                    {
                        internalSwitchTimer = 0.15f;
                    }
                    isPink = false;
                }
                else if (!isPink && internalSwitchTimer < 0f)
                {
                    floorFragmentMaterial.SetColor(colorTag, Color.magenta);
                    internalSwitchTimerMax -= fallingWarningTime / 300f;
                    if (internalSwitchTimerMax > 0.15f)
                    {
                        internalSwitchTimer = internalSwitchTimerMax;
                    }
                    else
                    {
                        internalSwitchTimer = 0.15f;
                    }
                    isPink = true;
                }
            }
            yield return null;
        }
        floorFragmentMaterial.SetColor(colorTag, Color.magenta);
        //timeUntilFall varies from fragment to fragment, making them take turns to fall
        yield return new WaitForSeconds(timeUntiLFall);
        floorFragmentMaterial.SetColor(colorTag, floorColor);
        
        //Falling of the fragment
        Vector3 startingPos  = floorFragment.transform.position;
        Vector3 finalPos = floorFragment.transform.position + (-transform.up * 7.5f);
        float elapsedTime = 0;
        while (elapsedTime < fallingTime)
        {
            floorFragment.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / fallingTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
