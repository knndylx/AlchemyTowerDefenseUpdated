using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using AlchemyTowerDefense.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace AlchemyTowerDefense.GameData
{
    public class Map
    {
        TextureDict textureDict;
        TextureDict decoTextureDict;

        //creates a new array of tiles based on row and height
        //List<Tile> terrainTiles = new List<Tile>();

        //y, x OR row, column
        public Tile[,] terrainTiles = new Tile[15, 20];
        public List<Decoration> Decorations { get; private set; }


        //size of the terrain tiles that will be used
        int size = 64;

        public void LoadContent(ContentManager c, string fileName = null)
        {
            textureDict = new TextureDict(c, "tiles");
            decoTextureDict = new TextureDict(c, "decos");
            Decorations = new List<Decoration>();
            if (fileName == null)
            {
                terrainTiles = CreateBlankMap();
            }
            else
            {
                LoadFromFile("Map.txt");
            }
            
        }

        public Tile[,] CreateBlankMap()
        {
            Tile[,] terrainTiles = new Tile[15,20];
            string tileType = "blank";

            int x = 0;
            int y = 0;
            for (x = 0; x < 20; x++)
            {
                for (y = 0; y < 15; y++)
                {
                    terrainTiles[y,x] = new Tile(new Rectangle(x * size, y * size, size, size), textureDict.dict[tileType]);

                }
            }
            return terrainTiles;
        }



        //generates the Map based on the file specified
        //public void LoadFromFile(string mapFileName)
        //{
        //
        //    Console.Write("loading in map");
        //    //List<List<string>> mapText = new List<List<string>>();
        //    //Tile[,] terrainTiles = new Tile[15,20];
        //    terrainTiles = new Tile[15,20];
        //
        //    List<string> mapText = GetTerrainTextFromFile(mapFileName);
        //
        //    int x = 0;
        //    int y = 0;
        //    //load the tiles
        //    for (x = 0; x < 20; x++)
        //    {
        //        for (y = 0; y < 15; y++)
        //        {
        //            string tileType = mapText[x*y];
        //            if(x == 2 && y == 5)
        //                ChangeTile(x,y,textureDict.dict["towerDefense_tile069"]);
        //            else
        //                ChangeTile(x,y, textureDict.dict["blank"]);
        //
        //        }
        //    }
        //    //load the decorations
        //    List<string> decorationText = GetDecoTextFromFile(mapText);
        //    Decorations.Clear();
        //    for(int i = 0; i < decorationText.Count / 4; i++)
        //    {
        //        Texture2D textureForDeco = decoTextureDict.dict[decorationText[i * 4]];
        //        int rectX = int.Parse(decorationText[i * 4 + 1]);
        //        int rectY = int.Parse(decorationText[i * 4 + 2]);
        //        float rot = float.Parse(decorationText[i * 4 + 3]);
        //        Decorations.Add(new Decoration(new Rectangle(rectX, rectY, size, size), textureForDeco));
        //    }
        //    //return terrainTiles;
        //}

        //get the decoration text from the Map list used for loading the Map

        public void LoadFromFile(string filename)
        {
            List<string> terrainText = GetTerrainTextFromFile("Map.txt");
            terrainTiles = CreateBlankMap();

            int tileCount = 0;
            for (var y = 0; y < 15; y++)
            {
                for (var x = 0; x < 20; x++)
                {
                    string tiletype = terrainText[tileCount];
                    ChangeTile(x,y,textureDict.dict[tiletype]);
                    tileCount++;
                }
            }
        }

        private List<string> GetDecoTextFromFile(List<string> mapText)
        {
            List<string> decoText = new List<string>();
            int i = 0;
            while(mapText[i] != "====")
            {
                i++;
            }
            i++;
            for(int counter = i; i < mapText.Count; i++)
            {
                decoText.Add(mapText[i]);
            }
            return decoText;
        }

        //save to file
        public void SaveToFile(string mapFileName)
        {
            using (var sw = new StreamWriter(mapFileName))
            {
                foreach(Tile t in terrainTiles)
                {
                    string writeName = FormatTextureNameString(t.texture.Name);
                    sw.WriteLine(writeName);
                }
                //sw.WriteLine("====");
                //foreach(Decoration d in Decorations)
                //{
                //    string writeName = FormatTextureNameString(d.Texture.Name);
                //    sw.WriteLine(writeName);
                //    sw.WriteLine(d.Rect.X);
                //    sw.WriteLine(d.Rect.Y);
                //    sw.WriteLine(d.rotation);
                //}
                sw.Close();
            }
        }

        public string FormatTextureNameString(string tName)
        {
            char[] cArray = tName.ToCharArray();
            int i = 0;
            while(cArray[i] != '/')
            {
                i++;
            }
            i++;
            return tName.Remove(0,i);
        }

        //get the strings of the tile types from a text file
        private List<string> GetTerrainTextFromFile(string mapFileName)
        {
            List<string> mapText = new List<string>();

            string line;
            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(mapFileName);
            while ((line = file.ReadLine()) != null)
            {
                mapText.Add(line);
            }
            file.Close();
            return mapText;
        }

        public void ChangeTile(int x, int y, Texture2D t)
        {
            terrainTiles[y,x] = new Tile(new Rectangle(x * size, y * size, size, size), t); 
        }

        public void PaintDecoration(int posx, int posy, Texture2D t)
        {
            Decorations.Add(new Decoration(new Rectangle(posx /* - (size/2)*/, posy /*- (size/2)*/, size, size), t));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Tile t in terrainTiles)
            {
                t.draw(spriteBatch);
            }
            foreach(Decoration d in Decorations)
            {
                d.Draw(spriteBatch);
            }
        }

    }
}
