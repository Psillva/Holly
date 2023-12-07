using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBase : MonoBehaviour
{
    private bool moveDown = false;

    public float velocidade = 3f;
    public Transform pontoA;
    public Transform pontoB;

    void Update()
    {
        Debug.Log(moveDown);

        if (this.transform.position.y > pontoA.position.y)
        {
            moveDown = true;
        }
        else if (this.transform.position.y < pontoB.position.y)
        {
            moveDown = false;
        }

        if (moveDown)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - velocidade * Time.deltaTime);
        }
        else if (!moveDown)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + velocidade * Time.deltaTime);
        }
    }
}
