using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class DungeonGenerator : MonoBehaviour
{
    // NOTE TO MATT: adding a //m comment to any line I write in case it somehow breaks something

    //Uses DFS algorithm

    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    [SerializeField] //m
    private GameObject enemyPrefab; //m

    [SerializeField] //m
    private GameObject target; //m

    public Vector2 size;
    public int startPos = 0;
    List<Cell> board;
    public GameObject roomPrefab;
    public Vector2 offset;
    public int currentCell;
    public GameObject ballPrefab;
    // Start is called before the first frame update
    void Start()
    {
        MazeGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateDungeon()
    {
        bool[] lastOne = new bool[] { false, true, false, false };
        for (int xSize = 0; xSize < size.x; xSize++)
        {
            
            for (int ySize = 0; ySize < size.y; ySize++)
            {
                var newRoom = Instantiate(roomPrefab, new Vector3(xSize * offset.x, 0, ySize * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                if (xSize == size.x - 1 && ySize == size.y - 1)
                {
                    newRoom.UpdateRoom(lastOne);
                }
                newRoom.UpdateRoom(board[Mathf.FloorToInt(xSize + ySize * size.x)].status);
                
            }
        }
    }

    void MazeGeneration()
    {
        board = new List<Cell>();
        for (int xSize = 0; xSize < size.x; xSize++)
        {
            for (int ySize = 0; ySize < size.y; ySize++)
            {
                board.Add(new Cell());
            }
        }
        currentCell = startPos;
        board[currentCell].status[0] = true;
        Stack<int> path = new Stack<int>();
        int whileLoopInt = 0;
        while (whileLoopInt < 1000)
        {
            whileLoopInt++;
            board[currentCell].visited = true;
            List<int> neighbors = CheckNeighbors(currentCell);
            if(neighbors.Count==0)
            {
                if(path.Count==0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                if (newCell > currentCell)
                {
                    //down or right
                    if (newCell - 1 == currentCell)
                    {
                        //right
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell +1 == currentCell)
                    {
                        //left
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }
            }
        }
        GenerateDungeon();

        GetComponent<NavMeshSurface>().BuildNavMesh(); //m
        Instantiate(enemyPrefab, target.transform.position, Quaternion.identity); //m
        Instantiate(ballPrefab, ballPrefab.transform.position, Quaternion.identity);
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();
        //uses its position on the board to check if it has a neighbor
        //if this would nt be outside the board and the spot has not been visited, addd it to neighbors

        //Check above
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell-size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - size.x));
        }

        //Check below
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }

        //Check right
        if ((cell+1) % size.x != 0 && !board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }

        //Check left
        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell -1));
        }

        return neighbors;
    }
}
