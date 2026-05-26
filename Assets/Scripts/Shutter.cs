using System;
using System.Collections;
using UnityEngine;

public class Shutter : MonoBehaviour
{
    public bool open;
    //public Vector3 openPosition;
    //public Vector3 closePosition;
    public GameObject[] pins;
    public Vector3[] pinPositions;
    private BowlingPin detection;
    public GameObject pinPrefab;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        open = true;
        pinPositions = new Vector3[pins.Length];
        int index = 0;

        foreach (GameObject pin in pins)
        {
            pinPositions[index] = pin.transform.position;
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            //transform.position = openPosition;
            animator.SetBool("Open", true);
        }
        else if(!open)
        {
            //transform.position = closePosition;
            animator.SetBool("Open", false);
        }

        foreach (GameObject pin in pins)
        {
            detection = pin.GetComponentInChildren<BowlingPin>();

            if (detection.down)
            {
                ManageDoor();
            }
        }
    }

    private void ManageDoor()
    {
        open = false;
        StartCoroutine(ResetAfterDelay(5));
        StartCoroutine(ResetPinsDelay(2));
    }

    IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        open = true;
    }

    IEnumerator ResetPinsDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // First, store references to all existing pins
        GameObject[] oldPins = pins;

        for (int i = 0; i < oldPins.Length; i++)
        {
            // Destroy old pin
            Destroy(oldPins[i]);

            // Instantiate new pin at stored position
            pins[i] = Instantiate(pinPrefab, pinPositions[i], Quaternion.Euler(-90, 0, 0));
        }
    }
}
