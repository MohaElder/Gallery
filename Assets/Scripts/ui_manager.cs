using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_manager : MonoBehaviour
{
    public GameObject creditOverlay;
    public GameObject welcomeOverlay;
    // Update is called once per frame
    void Update()
    {
        //press anywhere to start functionality
        if (Input.GetMouseButtonDown(0)){
            welcomeOverlay.SetActive(false);
            creditOverlay.SetActive(false);
            Bank.started = true;
        }

        /**
        //press anywhere to start functionality
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(Bank.started == false)
            {
                Bank.started = true;
                creditOverlay.SetActive(false);
            }
            else
            {
                creditOverlay.SetActive(true);
                Bank.started = false;
            }
        }
        **/
    }
}
