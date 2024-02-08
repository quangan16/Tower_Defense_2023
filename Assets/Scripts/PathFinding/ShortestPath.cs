using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
[Serializable]
public class ShortestPath
{
    public PathFinding.Point start;
    public PathFinding.Point end;
    public List<PathFinding.Point> points;
    //public List<PathFinding.PointFloat> pointsFloat;
    public static int[,] gridClone = new int[,]
    {
        { 0, 0, 0, 0, 0 ,0 , 0},
        { 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 ,0 , 0},
        { 0, 0, 0, 0, 0, 0, 0 },
    };
    public ShortestPath(PathFinding.Point start, PathFinding.Point end, List<PathFinding.Point> points)
    {
        this.start = start;
        this.end = end;
        this.points = points;
    }
}
