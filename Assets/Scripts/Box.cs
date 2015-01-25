﻿using UnityEngine;
using System.Collections;

public class Box : Occupant
{

    private Grid grid;
    bool isPicked;
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
            }
        }

        if(isPicked)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3((int)mousePosition.x, (int)mousePosition.y, 0f);
        }
        else
        {
            //if(grid)
        }
    }
}
