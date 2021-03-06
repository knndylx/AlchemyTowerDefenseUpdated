﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.GameData
{
    public class Map
    {

        //Content of the map
        //TODO - Fragile solution, binds the Size of the screen to a set number so no change of resolution is possible yet
        public Tile[,] TerrainTiles { get; private set; } = new Tile[15, 20];
        public List<Decoration> Decorations { get; private set; } = new List<Decoration>();
        public Path path = new Path();

        //Size of the tiles on the map
        private int Size;

        /// <summary>
        /// Constructor for the map. Creates a blank map unless a file name is specified
        /// </summary>
        /// <param name="size">Size of the tiles in the map</param>
        /// <param name="fileName">Name of the map file to load from. Default is null.</param>
        public Map(int size, string fileName = null)
        {
            Size = size;

            if (fileName == null)
            {
                TerrainTiles = MakeBlankTileGrid();
            }
            else
            {
                LoadFromFile("Map.txt");
            }
        }


        /// <summary>
        /// Makes a blank tile grid
        /// </summary>
        /// <returns>Returns a blank 20x15 tile grid</returns>
        public Tile[,] MakeBlankTileGrid()
        {
            Tile[,] terrainTiles = new Tile[15, 20];

            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    terrainTiles[y, x] = new Tile(new Rectangle(x * Size, y * Size, Size, Size), GlobalConfig.Textures.Tiles["blank"]);

                }
            }

            return terrainTiles;
        }

        #region Save and Load

        /// <summary>
        /// Load the map from a text file
        /// </summary>
        /// <param name="filename">File name to load the map from</param>
        public void LoadFromFile(string filename)
        {
            //calls the helper function that gets the terrain text from a file
            List<string> terrainText = GetTerrainTextFromFile(filename);
            List<string> decoText = GetDecoTextFromMapText(terrainText);
            List<string> pathText = GetPathTextFromMapText(terrainText);

            //clears the map and decorations
            TerrainTiles = MakeBlankTileGrid();
            Decorations = new List<Decoration>();

            //parse change the terrain tiles from the terrain text
            int tileCount = 0;
            for (var y = 0; y < 15; y++)
            {
                for (var x = 0; x < 20; x++)
                {
                    string tiletype = terrainText[tileCount];
                    ChangeTile(x,y,GlobalConfig.Textures.Tiles[tiletype]);
                    tileCount++;
                }
            }

            //parse and add the decorations from the deco text
            if(decoText.Count % 4 != 0)
                Console.WriteLine("error: not the right number of decos text in the file");

            for (int x = 0; x < decoText.Count / 4; x++)
            {
                string name = decoText[x * 4];
                int rectX = int.Parse(decoText[x * 4 + 1]);
                int rectY = int.Parse(decoText[x * 4 + 2]);
                float rotation = float.Parse(decoText[x * 4 + 3]);
                Texture2D texture = GlobalConfig.Textures.Decos[name];
                Decorations.Add(new Decoration(new Rectangle(rectX, rectY, Size, Size), texture, rotation));
            }

            //parse and add the path nodes from the path text
            if(pathText.Count % 2 != 0)
                Console.WriteLine("error: not the right number of lines in the path text file");

            for (int x = 0; x < pathText.Count / 2; x++)
            {
                int nodeX = int.Parse(pathText[x * 2]);
                int nodeY = int.Parse(pathText[x * 2 + 1]);
                path.AddNode(nodeX, nodeY);
            }
        }

        /// <summary>
        /// Save the map to a text file
        /// </summary>
        /// <param name="mapFileName">File name that you would like to use to save the file to</param>
        public void SaveToFile(string mapFileName)
        {
            using (var sw = new StreamWriter(mapFileName))
            {
                //save the tile data
                foreach(Tile t in TerrainTiles)
                {
                    string writeName = FormatTextureNameString(t.texture.Name);
                    sw.WriteLine(writeName);
                }

                //mark the end of the tile data and then save key information about the decorations
                //TODO - add the ability to make different sized decorations from the toolbox in the editor state
                sw.WriteLine("====");
                foreach(Decoration d in Decorations)
                {
                    string writeName = FormatTextureNameString(d.Texture.Name);
                    sw.WriteLine(writeName);
                    sw.WriteLine(d.Rect.X);
                    sw.WriteLine(d.Rect.Y);
                    sw.WriteLine(d.Rotation);
                }

                //mark the end of the deco data and then save information about the path
                sw.WriteLine("====");
                Path.PathNode currentNode = path.startNode;
                while (currentNode != null)
                {
                    sw.WriteLine(currentNode.x);
                    sw.WriteLine(currentNode.y);
                    currentNode = currentNode.nextNode;
                }
            }
        }

        #endregion

        #region Helper Methods

        private List<string> GetPathTextFromMapText(List<string> mapText)
        {
            List<string> pathText = new List<string>();

            int i = 0;
            while (mapText[i] != "====")
            {
                i++;
            }
            i++;
            while (mapText[i] != "====")
            {
                i++;
            }
            i++;
            for (int counter = i; counter < mapText.Count; counter++)
            {
                pathText.Add(mapText[counter]);
            }
            return pathText;
        }

        /// <summary>
        /// Helper method to get the decoration text from the information that the other helper function outputted
        /// </summary>
        /// <seealso cref="GetTerrainTextFromFile"/>
        /// <param name="mapText">List of the map text from the file</param>
        /// <returns>Returns a list of information in the form of strings about the decorations in the map text</returns>
        private List<string> GetDecoTextFromMapText(List<string> mapText)
        {
            List<string> decoText = new List<string>();
            int i = 0;
            //go until you reach the end of the tile data marked by "===="
            while (mapText[i] != "====")
            {
                i++;
            }
            i++;
            int j = i;
            while (mapText[j] != "====")
            {
                j++;
            }
            //for the rest of the text add it to the list which will be returned
            for (int counter = i; i < j; i++)
            {
                decoText.Add(mapText[i]);
            }
            return decoText;
        }

        /// <summary>
        /// Helper method that formats the name of the texture to save to the text file that makes loading easier. Takes the file path out of the texture data and just returns the name of the texture
        /// </summary>
        /// <param name="tName">string that you would like to format</param>
        /// <returns>Returns the texture name without the file path in front of it</returns>
        private string FormatTextureNameString(string tName)
        {
            char[] cArray = tName.ToCharArray();
            int i = 0;
            while (cArray[i] != '/')
            {
                i++;
            }
            i++;
            return tName.Remove(0, i);
        }

        /// <summary>
        /// Helper method that gets the terrain text from file when loading the map
        /// </summary>
        /// <param name="mapFileName">file name that you would like to load the data from</param>
        /// <returns>Returns a list of strings that has the decoration and tile data</returns>
        private List<string> GetTerrainTextFromFile(string mapFileName)
        {
            List<string> mapText = new List<string>();

            string line;
            // Read the file and add it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(mapFileName);
            while ((line = file.ReadLine()) != null)
            {
                mapText.Add(line);
            }
            file.Close();
            return mapText;
        }
        #endregion

        #region Editor Functions
        /// <summary>
        /// Change the tile at the location
        /// </summary>
        /// <param name="x">Grid X location</param>
        /// <param name="y">Grid Y location</param>
        /// <param name="t">Texture that you would like to change the tile to</param>
        public void ChangeTile(int x, int y, Texture2D t)
        {
            TerrainTiles[y,x] = new Tile(new Rectangle(x * Size, y * Size, Size, Size), t); 
        }

        /// <summary>
        /// Paint a tile at the location with a random rotation
        /// </summary>
        /// <param name="posx">Pixel position X</param>
        /// <param name="posy">Pixel position Y</param>
        /// <param name="t">Texture of the decoration that you are painting</param>
        public void PaintDecoration(int posx, int posy, Texture2D t)
        {
            Decorations.Add(new Decoration(new Rectangle(posx, posy, Size, Size), t));
        }
        
        /// <summary>
        /// Add a node to the path
        /// </summary>
        /// <param name="smallgridx">position x on the small grid coordinates</param>
        /// <param name="smallgridy">position y on the small grid coordinates</param>
        public void AddPathNode(int smallgridx, int smallgridy)
        {
            path.AddNode(smallgridx*Size/2, smallgridy*Size/2);
        }
        #endregion

        /// <summary>
        /// Draws the tiles and the decorations to the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Tile t in TerrainTiles)
            {
                t.Draw(spriteBatch);
            }
            foreach(Decoration d in Decorations)
            {
                d.Draw(spriteBatch);
            }
            //path.Draw(spriteBatch);
        }
    }
}
