using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Object holding the minimap on the UI")]
    private GameObject miniMap;
    [SerializeField]
    [Tooltip("Object holding the main map on the UI")]
    private GameObject maxiMap;
    [SerializeField]
    [Tooltip("The player's first-person controller component")]
    private StarterAssets.FirstPersonController playerController;

    // Update is called once per frame
    void Update()
    {
        /*  Using M as the map menu key as a placeholder
         *  This could be made more flexible by instead
         *  referencing a Map button defined in Input settings
         */ 
        if(Input.GetKeyDown(KeyCode.M))
        {
            // Swap the active states of the map and minimap
            miniMap.SetActive(!miniMap.activeSelf);
            maxiMap.SetActive(!maxiMap.activeSelf);
            // Disable player control if the map is active
            playerController.enabled = !maxiMap.activeSelf;
            // Unlock the cursor for marker placement
            Cursor.lockState = maxiMap.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
