using UnityEngine;
using System.Collections;

public enum Direction { right, left}

public class FoxController : MonoBehaviour
{
    public int startX;
    public int startY;
    public Direction movingDirection = Direction.right;

    private int x;
    private int y;
    private Grid grid;



    void Start()
    {
        x = startX;
        y = startY;
        transform.position = new Vector3(startX + 0.5f, startY, 0f);
        grid = FindObjectOfType<Grid>();

        StartCoroutine(MakeNextMove());
    }

    IEnumerator MakeNextMove()
    {
        var nextTile = GetNavigableTile();
        while(nextTile == null)
        {
            yield return null;
            nextTile = GetNavigableTile();
        }

        var nextAction = GetNextAction(nextTile);
        DoAction(nextAction, nextTile);

        yield return new WaitForSeconds(nextAction.time);

        StartCoroutine(MakeNextMove());
    }

    void DoAction(FoxAction action, Tile nextTile)
    {
        switch(action.type)
        {
            case FoxActionType.Walk:
                StartCoroutine(MoveAction(action, nextTile));
                break;
            case FoxActionType.Jump:
                StartCoroutine(JumpAction(action, nextTile));
                break;
        }
    }

    IEnumerator MoveAction(FoxAction action, Tile nextTile)
    {
        float t = 0f;
        Vector3 initialPos = transform.position;
        Vector3 destination = new Vector3(nextTile.x + 0.5f, nextTile.y, 0f);

        while(t < action.time)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(initialPos, destination, t / action.time);
            yield return null;
        }

        this.x = nextTile.x;
        this.y = nextTile.y;
    }

    IEnumerator JumpAction(FoxAction action, Tile nextTile)
    {
        float t = 0f;
        Vector3 initialPos = transform.position;
        Vector3 destination = new Vector3(nextTile.x + 0.5f, nextTile.y, 0f);

        while (t < action.time)
        {
            t += Time.deltaTime;

            AnimationCurve curve = nextTile.y > this.y ? action.curveA : action.curveB;
            
            transform.position = new Vector3(Mathf.Lerp(initialPos.x, destination.x, t / action.time), 
                initialPos.y + curve.Evaluate(t / action.time), 
                initialPos.z);
            yield return null;
        }

        this.x = nextTile.x;
        this.y = nextTile.y;
    }

    public FoxAction GetNextAction(Tile tile)
    {
        if (tile.y == y)
            return GameController.Instance.actions.Find(x => x.type == FoxActionType.Walk);
        else if (Mathf.Abs(tile.y - y) == 1)
            return GameController.Instance.actions.Find(x => x.type == FoxActionType.Jump);
        else
            return GameController.Instance.actions.Find(x => x.type == FoxActionType.Stand);
    }

    public Tile GetNavigableTile()
    {
        if(movingDirection == Direction.left)
        {
            return GetNavigableTileAtX(x - 1, y);
        }
        else if(movingDirection == Direction.right)
        {
            return GetNavigableTileAtX(x + 1, y);
        }
        return null;
    }

    private Tile GetNavigableTileAtX(int x, int y)
    {
        if (x < 0 || x > grid.columns - 1)
        {
            FlipFox();
            return null;
        } 

        if (grid[x, y].isNavigable)
            return grid[x, y];
        else if (grid[x, y - 1].isNavigable)
            return grid[x, y - 1];
        else if (grid[x, y + 1].isNavigable)
            return grid[x, y + 1];

        return null;
    }

    private void FlipFox()
    {
        if (movingDirection == Direction.left)
            movingDirection = Direction.right;
        else
            movingDirection = Direction.left;
        transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
    }

}
