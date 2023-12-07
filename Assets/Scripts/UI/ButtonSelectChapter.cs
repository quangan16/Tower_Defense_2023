using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectChapter : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private Button chapter;
    [SerializeField] private LoadChaperSelected loadChaperSelected;
    private void Start()
    {
        chapter.onClick.AddListener(() => SelectChapter());
    }
    public void SelectChapter()
    {
        loadChaperSelected.LoadChapter(index.ToString());
        _SceneManager.instance.ChangeChapter(index);
    }
}
