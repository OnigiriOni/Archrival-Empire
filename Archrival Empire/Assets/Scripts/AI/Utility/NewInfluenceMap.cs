using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////////////////////////////////
//////  C L A S S    N O T    I N    U S E
//////////////////////////////////////////////////////////////////////////////////

public class NewInfluenceMap : MonoBehaviour
{
    // The size of all sides of the map.
    public int influenceMapSize;

    // The space between two grid positions.
    public int influenceMap_gridSize;

    // The width and height of the map.
    private int influenceMap_width;
    private int influenceMap_height;

    // The decay of the influence over two grid positions.
    public float influenceDecay;

    // The amount of updates per second.
    public float updateFrequency;
    private float updateFrequencyLeft = 0F;

    // A list of all objects that influences the map.
    private List<PlayerObject> playerObjects;

    // The influence map and the influence map buffer.
    private GridPosition[,] map;
    private GridPosition[,] mapBuffer;
    
    // The texture to visualise the influence spread over the map.
    private Texture2D influenceMap_texture;

    // The momentum is used for the influence spread calculation.
    float momentum = 5F;


    private void Start()
    {
        playerObjects = new List<PlayerObject>();

        CreateMap();
    }
    
    private void Update()
    {
        updateFrequencyLeft -= Time.deltaTime;

        if (updateFrequencyLeft <= 0)
        {
            Fill();
            UpdatePlayerObjects();
            UpdateMap();
            UpdateBuffer();

            updateFrequencyLeft = updateFrequency;
        }
    }


    /// <summary>
    /// Set the grid positions and connect it to its neighbors.
    /// </summary>
    private void CreateMap()
    {
        // Make the influence map a square.
        influenceMap_width = influenceMapSize;
        influenceMap_height = influenceMapSize;

        // Set the map and the texture size.
        map = new GridPosition[influenceMap_gridSize, influenceMap_gridSize];
        mapBuffer = new GridPosition[influenceMap_gridSize, influenceMap_gridSize];
        influenceMap_texture = new Texture2D(influenceMap_gridSize, influenceMap_gridSize);

        // Calculate the distance between each grid position.
        float gridDistance_normal = influenceMap_width / influenceMap_gridSize;
        float gridDistance_diagonal = Mathf.Sqrt(gridDistance_normal) + gridDistance_normal;

        // Create the influence map grid positions.
        for (int x = 0; x < influenceMap_gridSize; x++)
        {
            for (int z = 0; z < influenceMap_gridSize; z++)
            {
                map[x, z] = new GridPosition(x, z);
            }
        }

        // Set the connections for each point.
        for (int x = 0; x < influenceMap_gridSize; x++)
        {
            for (int z = 0; z < influenceMap_gridSize; z++)
            {
                // Left.
                if (x > 0)
                {
                    map[x, z].connections.Add(new KeyValuePair<GridPosition, float>(map[x - 1, z], gridDistance_normal));
                }
                // Right.
                if (x < influenceMap_gridSize - 1)
                {
                    map[x, z].connections.Add(new KeyValuePair<GridPosition, float>(map[x + 1, z], gridDistance_normal));
                }
                // Up.
                if (z > 0)
                {
                    map[x, z].connections.Add(new KeyValuePair<GridPosition, float>(map[x, z - 1], gridDistance_normal));
                }
                // Down.
                if (z < influenceMap_gridSize - 1)
                {
                    map[x, z].connections.Add(new KeyValuePair<GridPosition, float>(map[x, z + 1], gridDistance_normal));
                }
                // Diagonal Left - Up.
                if (x > 0 && z > 0)
                {
                    map[x, z].connections.Add(new KeyValuePair<GridPosition, float>(map[x - 1, z - 1], gridDistance_diagonal));
                }
                // Diagonal Right - Down.
                if (x < influenceMap_gridSize - 1 && z < influenceMap_gridSize - 1)
                {
                    map[x, z].connections.Add(new KeyValuePair<GridPosition, float>(map[x + 1, z + 1], gridDistance_diagonal));
                }
                // Diagonal Right - Up.
                if (x < influenceMap_gridSize - 1 && z > 0)
                {
                    map[x, z].connections.Add(new KeyValuePair<GridPosition, float>(map[x + 1, z - 1], gridDistance_diagonal));
                }
                // Diagonal Left - Down.
                if (x > 0 && z < influenceMap_gridSize - 1)
                {
                    map[x, z].connections.Add(new KeyValuePair<GridPosition, float>(map[x - 1, z + 1], gridDistance_diagonal));
                }
            }
        }

        // Set the buffer.
        for (int x = 0; x < influenceMap_gridSize; x++)
        {
            for (int z = 0; z < influenceMap_gridSize; z++)
            {
                mapBuffer[x, z] = map[x, z];
            }
        }
    }

    /// <summary>
    /// Set the influence according to its strength and delay on all grid positions on the entire map.
    /// </summary>
    private void UpdateMap()
    {
        for (int x = 0; x < influenceMap_gridSize; x++)
        {
            for (int z = 0; z < influenceMap_gridSize; z++)
            {
                float maxInfluence = 0.0F;
                float minInfluence = 0.0F;

                List<KeyValuePair<GridPosition, float>> connections = map[x, z].connections;
                foreach (KeyValuePair<GridPosition, float> connection in connections)
                {
                    for (int i = 0; i < connection.Key.influences.Count; i++)
                    {
                        float influenceValue = mapBuffer[connection.Key.mapPosition_x, connection.Key.mapPosition_z].influences[i].Value * Mathf.Exp(-influenceDecay * connection.Value);
                        maxInfluence = Mathf.Max(influenceValue, maxInfluence);
                        minInfluence = Mathf.Min(influenceValue, minInfluence);
                    }
                }

                for (int i = 0; i < map[x, z].influences.Count; i++)
                {
                    if (Mathf.Abs(minInfluence) > maxInfluence)
                    {
                        //map[x, z].influences[i].Value = Mathf.Lerp(mapBuffer[x, z].influences[i].Value, minInfluence, momentum);
                        map[x, z].influences[i] = new KeyValuePair<Player, float>(map[x, z].influences[i].Key, Mathf.Lerp(mapBuffer[x, z].influences[i].Value, minInfluence, momentum));
                    }
                    else
                    {
                        //map[x, z].influences[i].Value = Mathf.Lerp(mapBuffer[x, z].influences[i].Value, maxInfluence, momentum);
                        map[x, z].influences[i] = new KeyValuePair<Player, float>(map[x, z].influences[i].Key, Mathf.Lerp(mapBuffer[x, z].influences[i].Value, maxInfluence, momentum));
                    }
                }
            }
        }
    }

    /// <summary>
    /// Set the buffer to the previous influence map state.
    /// </summary>
    private void UpdateBuffer()
    {
        for (int x = 0; x < influenceMap_gridSize; x++)
        {
            for (int z = 0; z < influenceMap_gridSize; z++)
            {
                mapBuffer[x, z] = map[x, z];
            }
        }
    }

    /// <summary>
    /// Set the influence of all player objects to the current grid position.
    /// </summary>
    private void UpdatePlayerObjects()
    {
        // Clears the player objects list if something got destroyed.
        playerObjects.ForEach(x => { if (x == null) playerObjects.Remove(x); });

        // Set the influence a object has to the right grid position.
        foreach (PlayerObject playerObject in playerObjects)
        {
            CreateInfluence(playerObject.gridPosition_x, playerObject.gridPosition_z, playerObject.player, playerObject.influence);
        }
    }

    /// <summary>
    /// Adds influence a player object has to a grid position.
    /// </summary>
    /// <param name="x">The array index of the X axis.</param>
    /// <param name="z">The array index of the Z axis.</param>
    /// <param name="player">The player that is influencing the grid position.</param>
    /// <param name="value">The intensity of the influence of the player object.</param>
    public void CreateInfluence(int x, int z, Player player, float value)
    {
        //TODO: May cause issues because older influences of the same player remain.
        if (x >= 0 && x < map.Length && z >= 0 && z < map.Length)
        {
            // Check if a player already has influence in a grid position.
            bool isExisting = false;

            foreach (KeyValuePair<Player, float> influence in map[x, z].influences)
            {
                if (influence.Key.playerTag == player.playerTag)
                {
                    // Add the value in the main matrix.
                    map[x, z].influences.Remove(influence);
                    map[x, z].influences.Add(new KeyValuePair<Player, float>(player, value));

                    // Add the value in the buffer matrix.
                    foreach (KeyValuePair<Player, float> bufferInfluence in mapBuffer[x, z].influences)
                    {
                        if (bufferInfluence.Key.playerTag == player.playerTag)
                        {
                            mapBuffer[x, z].influences.Remove(bufferInfluence);
                            mapBuffer[x, z].influences.Add(new KeyValuePair<Player, float>(player, value));
                        }
                    }

                    isExisting = true;
                    break;
                }
            }

            if (isExisting == false)
            {
                // Set the influence in the main matrix.
                map[x, z].influences.Add(new KeyValuePair<Player, float>(player, value));

                // Set the influence in the buffer matrix.
                mapBuffer[x, z].influences.Add(new KeyValuePair<Player, float>(player, value));
            }

            foreach (KeyValuePair<Player, float> influence in map[x, z].influences)
            {
                Debug.Log(" : " + influence.Key.playerTag);
            }
            Debug.Log(mapBuffer[x, z].influences.Count);
            Debug.Log("*********************************");
        }
    }

    /// <summary>
    /// Adds a player object to the list of objects that can interact with the influence map.
    /// </summary>
    /// <param name="playerObject">The player object.</param>
    public void AddPlayerObject(PlayerObject playerObject)
    {
        // Seems to work and seems to not work...
        playerObjects.Add(playerObject);
    }

    /// <summary>
    /// Adds all player objects to the list of objects that can interact with the influence map.
    /// </summary>
    private void Fill()
    {
        PlayerObject[] list = FindObjectsOfType<PlayerObject>();
        for (int i = 0; i < list.Length; i++)
        {
            if (!playerObjects.Contains(list[i]))
            {
                playerObjects.Add(list[i]);
            }
        }
    }

    //private void Draw()
    //{
    //    //TODO: Clear the color of each position.
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int z = 0; z < height; z++)
    //        {
    //            Color color = map[x, z].color;
    //            float influence = map[x, z].influence;

    //            float red = color.r * influence;
    //            float green = color.g * influence;
    //            float blue = color.b * influence;

    //            color = new Color(red, green, blue);

    //            mapTexture.SetPixel(x, z, color);
    //        }
    //    }



    //    ////////////////////////////

    //    //float kk;
    //    //float jj;

    //    //for (int x = 0; x < width; x++)
    //    //{
    //    //    for (int y = 0; y < height; y++)
    //    //    {
    //    //        kk = ((float)width - (float)x) / (float)width;
    //    //        jj = ((float)width - (float)y) / (float)width;
    //    //        mapTexture.SetPixel(x, y, new Color(kk, jj, jj));
    //    //    }
    //    //}

    //    mapTexture.filterMode = FilterMode.Point;
    //    mapTexture.wrapMode = TextureWrapMode.Clamp;
    //    mapTexture.Apply();

    //    MeshRenderer mr = GetComponent<MeshRenderer>();
    //    mr.sharedMaterials[0].mainTexture = mapTexture;
    //}
}
