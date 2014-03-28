using System;
using System.Collections.Generic;
using PuzzleEngineAlpha.Level;
using Microsoft.Xna.Framework;

namespace PlatformerPrototype.AI
{
    public class AStar : AI
    {
        #region Declarations

        enum NodeStatus { Open, Closed };
        TileMap TileMap;
        Dictionary<Vector2, NodeStatus> nodeStatus;
        List<PathNode> openList;
        Dictionary<Vector2, float> nodeCosts;
        const int CostStraight = 10;
        const int CostDiagonal = 15;

        #endregion

        #region Constructor

        public AStar(TileMap TileMap)
        {
            this.TileMap = TileMap;
            nodeStatus = new Dictionary<Vector2, NodeStatus>();
            openList = new List<PathNode>();
            nodeCosts = new Dictionary<Vector2, float>();
        }

        #endregion

        #region Helper Methods

        void AddNodeToOpenList(PathNode node)
        {
            int index = 0;
            float cost = node.TotalCost;

            while ((openList.Count > index) && (cost < openList[index].TotalCost))
            {
                index++;
            }

            openList.Insert(index, node);
            nodeCosts[node.GridLocation] = node.TotalCost;
            nodeStatus[node.GridLocation] = NodeStatus.Open;
        }

        List<PathNode> FindAdjacentNodes(PathNode currentNode, PathNode endNode)
        {
            List<PathNode> adjacentNodes = new List<PathNode>();

            int X = currentNode.GridX;
            int Y = currentNode.GridY;

            bool upLeft = true;
            bool upRight = true;
            bool downLeft = true;
            bool downRight = true;

            if ((X > 0) && (TileMap.CellIsPassable(X - 1, Y)))
            {
                adjacentNodes.Add(new PathNode(currentNode, endNode, new Vector2(X - 1, Y), CostStraight + currentNode.DirectCost));
            }
            else
            {
                upLeft = false;
                downLeft = false;
            }

            if ((X < TileMap.MapWidth - 1) && (TileMap.CellIsPassable(X + 1, Y)))
            {
                adjacentNodes.Add(new PathNode(currentNode, endNode, new Vector2(X + 1, Y), CostStraight + currentNode.DirectCost));
            }
            else
            {
                upRight = false;
                downRight = false;
            }


            if ((Y > 0) && (TileMap.CellIsPassable(X, Y - 1)))
            {
                adjacentNodes.Add(new PathNode(currentNode, endNode, new Vector2(X, Y - 1), CostStraight + currentNode.DirectCost));
            }
            else
            {
                upLeft = false;
                upRight = false;
            }

            if ((Y < TileMap.MapHeight - 1) && (TileMap.CellIsPassable(X, Y + 1)))
            {
                adjacentNodes.Add(new PathNode(currentNode, endNode, new Vector2(X, Y + 1), CostStraight + currentNode.DirectCost));
            }
            else
            {
                downLeft = false;
                downRight = false;
            }


            if ((upLeft) && (TileMap.CellIsPassable(X - 1, Y - 1)))
            {
                adjacentNodes.Add(new PathNode(currentNode, endNode, new Vector2(X - 1, Y - 1), CostDiagonal + currentNode.DirectCost));
            }

            if ((upRight) && (TileMap.CellIsPassable(X + 1, Y - 1)))
            {
                adjacentNodes.Add(new PathNode(currentNode, endNode, new Vector2(X + 1, Y - 1), CostDiagonal + currentNode.DirectCost));
            }

            if ((downLeft) && (TileMap.CellIsPassable(X - 1, Y + 1)))
            {
                adjacentNodes.Add(new PathNode(currentNode, endNode, new Vector2(X - 1, Y + 1), CostDiagonal + currentNode.DirectCost));
            }

            if ((downRight) && (TileMap.CellIsPassable(X + 1, Y + 1)))
            {
                adjacentNodes.Add(new PathNode(currentNode, endNode, new Vector2(X + 1, Y + 1), CostDiagonal + currentNode.DirectCost));
            }

            return adjacentNodes;
        }

        #endregion

        #region Public Methods

        public List<Vector2> FindPath(Vector2 startTile, Vector2 endTile)
        {
            if (!TileMap.CellIsPassable(endTile) || !TileMap.CellIsPassable(startTile))
            {
                return null;
            }

            openList.Clear();
            nodeCosts.Clear();
            nodeStatus.Clear();

            PathNode startNode;
            PathNode endNode;

            endNode = new PathNode(null, null, endTile, 0);
            startNode = new PathNode(null, endNode, startTile, 0);

            AddNodeToOpenList(startNode);

            while (openList.Count > 0)
            {
                PathNode currentNode = openList[openList.Count - 1];

                if (currentNode.IsEqualToNode(endNode))
                {
                    List<Vector2> bestPath = new List<Vector2>();
                    while (currentNode != null)
                    {
                        bestPath.Insert(0, currentNode.GridLocation);
                        currentNode = currentNode.ParentNode;
                    }
                    return bestPath;
                }

                openList.Remove(currentNode);
                nodeCosts.Remove(currentNode.GridLocation);

                foreach (PathNode possibleNode in FindAdjacentNodes(currentNode, endNode))
                {
                    if (nodeStatus.ContainsKey(possibleNode.GridLocation))
                    {
                        if (nodeStatus[possibleNode.GridLocation] == NodeStatus.Closed)
                        {
                            continue;
                        }

                        if (nodeStatus[possibleNode.GridLocation] == NodeStatus.Open)
                        {
                            if (possibleNode.TotalCost >= nodeCosts[possibleNode.GridLocation])
                            {
                                continue;
                            }
                        }
                    }

                    AddNodeToOpenList(possibleNode);
                }

                nodeStatus[currentNode.GridLocation] = NodeStatus.Closed;
            }

            return null;
        }
        #endregion

    }
}
