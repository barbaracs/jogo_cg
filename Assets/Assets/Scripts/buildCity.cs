using UnityEngine;
using System.Collections;

public class buildCity : MonoBehaviour {
    public GameObject[] buildings;

    public GameObject xstreets;
    public GameObject zstreets;
    public GameObject crossroad;

    public GameObject car;

    public int mapWidth = 20;
    public int mapHeight = 20;

    int[,] mapgrid;
    //Inteiro para dar espaçamento entre os prédios
    int buildingsFootprint = 5;

    // Use this for initialization
    void Start()
    {
        float seed = 22;
        mapgrid = new int[mapWidth, mapHeight];

        //generate map data
        for (int h = 0; h < mapHeight; h++)
        {
            for (int w = 0; w < mapWidth; w++)
            {
                mapgrid[w, h] = (int)(Mathf.PerlinNoise(w / 10.0f + seed, h / 10.0f + seed) * 10);
            }
        }

        //Constrói ruas paralelas horizontais
        int x = 0;
        for (int n = 0; n < 50; n++)
        {
            for (int h = 0; h < mapHeight; h++)
            {
                mapgrid[x, h] = -1;
            }
            x += Random.Range(6, 10);
            if (x >= mapWidth) break;
        }

        //Ruas paralelas verticais
        int z = 0;
        for (int n = 0; n < mapWidth; n++)
        {
            for (int w = 0; w < mapWidth; w++)
            {
                Debug.Log(z);
                if (mapgrid[w, z] == -1) //Coloca rua de cruzamento
                    mapgrid[w, z] = -3;
                else
                    mapgrid[w, z] = -2;
            }
            z += Random.Range(3, 3);
            if (z >= mapHeight) break;
        }

        //generate city
        for (int h = 0; h < mapHeight; h++)
        {
            for (int w = 0; w < mapWidth; w++)
            {
                int result = mapgrid[w, h];
                Vector3 pos = new Vector3(w * buildingsFootprint, 0, h * buildingsFootprint);
                if (result < -2)
                    Instantiate(crossroad, pos, crossroad.transform.rotation);
                else if (result < -1)
                    Instantiate(xstreets, pos, xstreets.transform.rotation);
                else if (result < 0)
                    Instantiate(zstreets, pos, zstreets.transform.rotation);
                else if (result < 1)
                    Instantiate(buildings[0], pos, Quaternion.identity);
                else if (result < 2)
                    Instantiate(buildings[1], pos, Quaternion.identity);
                else if (result < 4)
                    Instantiate(buildings[2], pos, Quaternion.identity);
                else if (result < 6)
                    Instantiate(buildings[3], pos, Quaternion.identity);
                else if (result < 7)
                    Instantiate(buildings[4], pos, Quaternion.identity);
                else if (result < 10)
                    Instantiate(buildings[5], pos, Quaternion.identity);
            }
        }
        //Vector3 carPos = new Vector3(100, 0, 488);
        //Instantiate(car, carPos, Quaternion.identity);
    }
}
