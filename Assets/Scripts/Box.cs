using UnityEngine;
using System.Collections;

public class Box : Occupant
{
    protected Grid grid;
    protected bool isPicked;

    void Start()
    {
        grid = FindObjectOfType<Grid>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D col = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (col == this.collider2D)
            {
                isPicked = !isPicked;
                StartCoroutine(UpdateState());
            }
        }      
    }

    protected virtual IEnumerator UpdateState()
    {
        if (isPicked)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3((int)mousePosition.x, (int)mousePosition.y, 0f);
        }
        else
        {
            //rigidbody2D.isKinematic = false;
        }

        yield return null;
    }
}
