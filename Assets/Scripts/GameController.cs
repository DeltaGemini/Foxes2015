using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    private static GameController instance;

    public static GameController Instance
    {
        get
        {
            if (!instance && !isDestroying)
            {
                instance = FindObjectOfType<GameController>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private static bool isDestroying = false;
    void OnDestroy()
    {
        isDestroying = true;
    }


    public List<FoxAction> actions;

    void Start()
    {

    }
}
