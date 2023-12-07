using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadChaperSelected : MonoBehaviour
{
    [SerializeField] ScriptTableChapter chapter;

    public TextMeshProUGUI nameChapter;
    public Image background;
    public TextMeshProUGUI waveText;

    private void Start()
    {
        LoadChapter(_SceneManager.instance.chapter.ToString());
    }
    public void LoadChapter(string _nameChapter)
    {
        chapter = Resources.Load<ScriptTableChapter>("ScriptTables/Chapters/chapter" + _nameChapter);

        nameChapter.text = chapter.nameChapter;
        background.sprite = chapter.background;
        waveText.text = "Max wave: " + chapter.waveCurrent + "/ " + chapter.waveMax; 
    }
}
