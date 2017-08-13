using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlchemyTowerDefense.GameData
{
    public class Path
    {
        /// <summary>
        /// Nodes are what connect each segment of the path to each other
        /// </summary>
        public class PathNode
        {
            public int x { get; private set; }
            public int y { get; private set; }
            public PathNode previousNode, nextNode;
            private Texture2D texture = GlobalConfig.Textures.Icons["pathnode"];

            public PathNode(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public PathNode(int x, int y, PathNode pNode)
            {
                this.x = x;
                this.y = y;
                previousNode = pNode;
            }

            /// <summary>
            /// Draws the node in a color that depends on if its a start node, end node, or in the middle
            /// </summary>
            /// <param name="spriteBatch"></param>
            public void Draw(SpriteBatch spriteBatch)
            {
                if (previousNode == null)
                {
                    spriteBatch.Draw(texture, new Rectangle(x - 16 ,y - 16 ,texture.Width,texture.Height), Color.Green);
                } else if (nextNode == null)
                {
                    spriteBatch.Draw(texture, new Rectangle(x - 16, y - 16, texture.Width, texture.Height), Color.Red);
                }
                else
                {
                    spriteBatch.Draw(texture, new Rectangle(x - 16, y - 16, texture.Width, texture.Height), Color.White);
                }
            }
        }

        private PathNode startNode;

        /// <summary>
        /// Connects two nodes together if the connection is value
        /// </summary>
        /// <param name="s">Starting node</param>
        /// <param name="e">Ending Node</param>
        public void MakeSegment(PathNode s, PathNode e)
        {
            if (TestConnectionValid(s,e))
            {
                s.nextNode = e;
                e.previousNode = e;
            }
        }

        /// <summary>
        /// Tests if the connection between two nodes is valid
        /// </summary>
        /// <param name="s">Starting node</param>
        /// <param name="e">Ending node</param>
        /// <returns>Returns true if the connection is valid. Else returns false.</returns>
        public bool TestConnectionValid(PathNode s, PathNode e)
        {
            if (s.nextNode != null || e.previousNode != null) return false;
            return true;
        }

        /// <summary>
        /// Adds a node to the Path node. Can only add if either the x or y are the same for the previous node
        /// </summary>
        /// <param name="node">Node that you would like to add to</param>
        public void AddNode(int x, int y)
        {
            if (startNode == null)
            {
                startNode = new PathNode(x, y);
            }
            else
            {
                if(GetLastNode().x == x || GetLastNode().y == y)
                    GetLastNode().nextNode = new PathNode(x,y,GetLastNode());
            }
        }

        /// <summary>
        /// Get the last node in the path
        /// </summary>
        /// <returns></returns>
        public PathNode GetLastNode()
        {
            PathNode currentNode = startNode;
            while(currentNode.nextNode != null)
            {
                currentNode = currentNode.nextNode;
            }
            return currentNode;
        }

        /// <summary>
        /// Deletes the last node in the sequence. If there is only one node it deletes every node from the path
        /// </summary>
        public void DeleteLastNode()
        {
            Console.WriteLine("deleting node");
            if (startNode == null) return;
            if (GetLastNode() == startNode)
            {
                startNode = null;
            }
            else
            {
                PathNode last = GetLastNode();
                PathNode newLast = last.previousNode;
                newLast.nextNode = null;
            }
        }

        /// <summary>
        /// Draws the path
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            PathNode currentNode = startNode;
            int lineWidth = 4;
            Texture2D lineTexture = GlobalConfig.Textures.Icons["pathline"];
            Texture2D nodeTexture = GlobalConfig.Textures.Icons["pathnode"];

            while (currentNode != null)
            {
                currentNode.Draw(spriteBatch);

                //draw the lines connecting the nodes
                if (currentNode.x == currentNode.previousNode?.x)
                {
                    //if the previous node is below the current node on the x axis
                    if (currentNode.y > currentNode.previousNode.y)
                    {
                        spriteBatch.Draw(lineTexture,new Rectangle(currentNode.previousNode.x - (lineWidth / 2),
                                                                   currentNode.previousNode.y + (nodeTexture.Height/2),
                                                                   lineWidth,
                                                                   (currentNode.y - currentNode.previousNode.y - nodeTexture.Height)), Color.White);
                    }
                    //if the previous node is below the current node on the x axis
                    if (currentNode.y < currentNode.previousNode.y)
                    {
                        spriteBatch.Draw(lineTexture, new Rectangle(currentNode.x - (lineWidth / 2),
                                                                    currentNode.y + (nodeTexture.Height/2),
                                                                    lineWidth,
                                                                    (currentNode.previousNode.y - currentNode.y - nodeTexture.Height)), Color.White);
                    }
                }
                //else if the nodes are aligned on the y-axis
                if (currentNode.y == currentNode.previousNode?.y)
                {
                    //if the previous node is to the right of the current node
                    if (currentNode.x < currentNode.previousNode.x)
                    {
                        spriteBatch.Draw(lineTexture, new Rectangle(currentNode.x + (nodeTexture.Width /2),
                                                                    currentNode.y - (lineWidth/2),
                                                                    (currentNode.previousNode.x - currentNode.x - nodeTexture.Width),
                                                                    lineWidth), Color.White);
                    }
                    //if the previous node is to the left of the current node
                    if (currentNode.x > currentNode.previousNode.x)
                    {
                        spriteBatch.Draw(lineTexture, new Rectangle(currentNode.previousNode.x + (nodeTexture.Width / 2),
                                                                    currentNode.previousNode.y - (lineWidth / 2),
                                                                    (currentNode.x - currentNode.previousNode.x - nodeTexture.Width),
                                                                    lineWidth), Color.White);
                    }
                }
                currentNode = currentNode.nextNode;
            }
        }
    }
}
