using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Maze_Render : MonoBehaviour
{
    [SerializeField]
    [Range(1, 1000)]
    private int width = 10;

    [SerializeField]
    [Range(1, 1000)]
    private int height = 10;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform ex_zid = null;

    [SerializeField]
    private Transform ex_podea = null;

    private void Draw(Zid[,] labirint)
    {
        var podea = Instantiate(ex_podea, transform);

        podea.localScale = new Vector3(width, size * 0.3f, height);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var cell = labirint[i, j];
                var pozitie = new Vector3(-width / 2 + i, 0, -height / 2 + j);

                if (cell.HasFlag(Zid.Sus))
                {
                    var topWall = Instantiate(ex_zid, transform) as Transform;
                    topWall.position = pozitie + new Vector3(0, 0, size / 2);
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);


                }
                if (cell.HasFlag(Zid.Stanga))
                {
                    var leftWall = Instantiate(ex_zid, transform) as Transform;
                    leftWall.position = pozitie + new Vector3(-size / 2, 0, 0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);

                }
                if(i== width-1)
                {
                    if (cell.HasFlag(Zid.Dreapta))
                    {
                        var rightWall = Instantiate(ex_zid, transform) as Transform;
                        rightWall.position = pozitie + new Vector3(+size / 2, 0, 0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);



                    }

                }
                
                if(j == 0)
                {
                    if (cell.HasFlag(Zid.Jos))
                    {
                        var bottomWall = Instantiate(ex_zid, transform) as Transform;
                        bottomWall.position = pozitie + new Vector3(0, 0, -size / 2);
                        bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);

                    }

                }            

            }

        }

    }

    void Start()
    {
        var labirint = Maze_Generator.Generate(width, height);

        Draw(labirint);
    }

    void Update()
    {

    }
}
