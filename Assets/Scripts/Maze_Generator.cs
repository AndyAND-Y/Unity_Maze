using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags]
public enum Zid
{
    Sus = 1,
    Dreapta = 2,
    Jos = 4,
    Stanga = 8,
    All = 15,

    Vizitat = 128,
}

public struct Pozitie
{
    public int X;
    public int Y;

}

public struct Vecin
{
    public Pozitie poz;
    public Zid zid;
}


public class Maze_Generator
{   

    private static List < Vecin > Get_vecini( Pozitie curent, Zid[,] labirint, int width,int height)
    {
        var lista = new List<Vecin>();

        if( curent.X>0)
        {
            if (!labirint[curent.X - 1, curent.Y].HasFlag(Zid.Vizitat))
            {
                lista.Add(new Vecin
                {
                    poz = new Pozitie
                    {
                        X = curent.X - 1,
                        Y = curent.Y
                    },
                    zid = Zid.Stanga
                });

            }

        }

        if (curent.Y > 0)
        {
            if (!labirint[curent.X , curent.Y -1].HasFlag(Zid.Vizitat))
            {
                lista.Add(new Vecin
                {
                    poz = new Pozitie
                    {
                        X = curent.X ,
                        Y = curent.Y - 1
                    },
                    zid = Zid.Jos
                });

            }

        }

        if (curent.Y < height - 1)
        {
            if (!labirint[curent.X , curent.Y + 1 ].HasFlag(Zid.Vizitat))
            {
                lista.Add(new Vecin
                {
                    poz = new Pozitie
                    {
                        X = curent.X ,
                        Y = curent.Y + 1
                    },
                    zid = Zid.Sus
                });

            }

        }

        if (curent.X < width - 1)
        {
            if (!labirint[curent.X + 1, curent.Y].HasFlag(Zid.Vizitat))
            {
                lista.Add(new Vecin
                {
                    poz = new Pozitie
                    {
                        X = curent.X + 1,
                        Y = curent.Y
                    },
                    zid = Zid.Dreapta
                });

            }

        }

        return lista;

    }

    private static Zid Get_invers(Zid wall)
    {
        switch (wall)
        {
            case Zid.Dreapta: return Zid.Stanga;
            case Zid.Stanga: return Zid.Dreapta;
            case Zid.Sus: return Zid.Jos;
            case Zid.Jos: return Zid.Sus;
            default: return Zid.Stanga;
        }
    }


    private static Zid[,] Get_Maze(Zid[,] labirint ,int width ,int height)
    {
        var Stiva = new Stack<Pozitie>();
        var rng = new System.Random();

        var pozitie = new Pozitie { X = rng.Next(0, width), Y = rng.Next(0, height) };

        labirint[pozitie.X, pozitie.Y] |= Zid.Vizitat;

        Stiva.Push(pozitie);

        while( Stiva.Count > 0)
        {
            var curent = Stiva.Pop();
            var vecini = Get_vecini(curent, labirint, width, height);

            if( vecini.Count > 0)
            {
                Stiva.Push(curent);

                var Index = rng.Next(0, vecini.Count);

                var Vecin_random = vecini[Index];

                var poz_vecin = Vecin_random.poz;

                labirint[curent.X, curent.Y] &= ~ Vecin_random.zid;
                labirint[poz_vecin.X, poz_vecin.Y] &= ~Get_invers(Vecin_random.zid);

                labirint[poz_vecin.X, poz_vecin.Y] |= Zid.Vizitat;

                Stiva.Push(poz_vecin);
            }
        }
        return labirint;


    }



    public static Zid[,]  Generate(int width,int height)
    {
        Zid[,] labirint = new Zid[width, height];
        Zid Initial = Zid.All;

        for (int i=0;i<width;i++)
            for (int j=0;j<height;j++)
            {
                labirint[i, j] = Initial;
            }

        return Get_Maze(labirint, width, height);

    }



}
