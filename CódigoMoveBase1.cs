using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBase2 : MonoBehaviour
{
    private bool moveDown = false;

    public float velocidade = 3f;
    public Transform pontoA;
    public Transform pontoB;

    void Update()
    {
        Debug.Log(moveDown);

        if (this.transform.position.x > pontoA.position.x)
        {
            moveDown = true;
        }
        else if (this.transform.position.x < pontoB.position.x)
        {
            moveDown = false;
        }

        if (moveDown)
        {
            transform.position = new Vector2(transform.position.x - velocidade * Time.deltaTime, transform.position.y);
        }
        else if (!moveDown)
        {
            transform.position = new Vector2(transform.position.x + velocidade * Time.deltaTime, transform.position.y);
        }
    }
}
