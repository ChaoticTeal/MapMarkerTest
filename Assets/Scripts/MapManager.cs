using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private GameObject miniMap;
    [SerializeField]
    private GameObject maxiMap;
    [SerializeField]
    private StarterAssets.FirstPersonController playerController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            miniMap.SetActive(!miniMap.activeSelf);
            maxiMap.SetActive(!maxiMap.activeSelf);
            playerController.enabled = !maxiMap.activeSelf;
            Cursor.lockState = maxiMap.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
