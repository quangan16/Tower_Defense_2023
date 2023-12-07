using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePersist : MonoBehaviour
{
    public static GamePersist instance;
    public bool showedResultPanel;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
        
    }

    private void OnEnable()
    {
        DataPersist.LoadData();
    }

}
