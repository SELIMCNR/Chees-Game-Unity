using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;
    private GameObject reference = null;
    private int matrixX;
    private int matrixY;

    public bool attack = false;

    void Start()
    {
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f); // Red color for attack plates
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("control");

        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
            if (cp.name == "beyaz_þah") controller.GetComponent<Game>().winner("siyah");
            if (cp.name == "siyah_þah") controller.GetComponent<Game>().winner("beyaz");

            Destroy(cp);
        }

        // Update the game state
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Karakter>().GetxTahta(),
            reference.GetComponent<Karakter>().GetyTahta());

        // Set new coordinates for the piece
        reference.GetComponent<Karakter>().SetxTahta(matrixX);
        reference.GetComponent<Karakter>().SetyTahta(matrixY);
        reference.GetComponent<Karakter>().kordinat();

        // Update the new position in the game state
        controller.GetComponent<Game>().SetPosition(reference);

        // Destroy all move plates
        reference.GetComponent<Karakter>().DestroyMovePlate();

        // Uncomment if you have implemented a turn-based system
         controller.GetComponent<Game>().NextTurn();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
