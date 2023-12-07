using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="NewChapter", fileName ="Chapter")]
public class ScriptTableChapter : ScriptableObject
{
    public string nameChapter;
    public Sprite background;
    public int waveMax;
    public int waveCurrent;
}
