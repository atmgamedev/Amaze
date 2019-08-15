using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBuilder : MonoBehaviour
{
    private int[] RandomPerm4()
    {
        int[] newList = { 0, 1, 2, 3};
        for (int i = 0; i < 3; i++)
        {
            int k = Random.Range(0, 4 - i) + i;
            int tmp = newList[k];
            newList[k] = newList[i];
            newList[i] = tmp;
        }
        return newList;
    }

    int[] dx = { 0, 0, 1, -1 };
    int[] dy = { 1, -1, 0, 0 };
    bool inrange(int x, int y)
    {
        return (2 < x && x < 98 && 2 < y && y < 98);
    }
    public GameObject[] tileTypes;
    int[,] map = new int[100, 100];
    int[,] vis = new int[100, 100];
    void Awake()
    {
        List<int> q = new List<int>();
        q.Add(50 * 100 + 50);
        int h = 0, t = 1;
        vis[50, 50] = 1;
        map[50, 50] = 1;
        int g = -1, best = -1;
        while (h < t)
        {
            if (Random.Range(0, 100) > 30)
            {
                int tmp = q[t - 1];
                q[t - 1] = q[h];
                q[h] = tmp;
            }

            int x = q[h] / 100, y = q[h] % 100;
            int[] ord = RandomPerm4();
            for (int dd = 0; dd < 4; dd++)
            {   
                int d = ord[dd];
                int tx = x + dx[d];
                int ty = y + dy[d];
                int ttx = tx + dx[d];
                int tty = ty + dy[d];
                if (inrange(ttx, tty) && vis[ttx, tty] == 0)
                {
                    vis[ttx, tty] = vis[x, y] + 1;
                    map[tx, ty] = 1;
                    map[ttx, tty] = 1;
                    t += 1;
                    q.Add(ttx * 100 + tty);
                    if (vis[ttx, tty] > best) {
                        best = vis[ttx, tty];
                        g = ttx * 100 + tty;
                    }
                }
            }
            h += 1;
        }
        map[g / 100, g % 100] = 2;

        for (int x = 0; x < 100; x++)
            for (int y = 0; y < 100; y++)
            {
                GameObject toBuild = tileTypes[map[x, y]];
                GameObject doneTile = Instantiate(toBuild, new Vector3(x - 50, 0f, y - 50), Quaternion.identity) as GameObject;
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
