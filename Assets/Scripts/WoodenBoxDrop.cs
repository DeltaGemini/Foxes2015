using UnityEngine;
using System.Collections;

public class WoodenBoxDrop : Box
{

    protected override IEnumerator UpdateState()
    {
        StartCoroutine(base.UpdateState());

        yield return null;
    }
}
