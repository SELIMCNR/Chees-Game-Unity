using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karakter : MonoBehaviour
{
    public GameObject controller;
    public GameObject hareket;

    private int xTahta = -1;
    private int yTahta = -1;

    private string player;

    public Sprite siyah_þah, siyah_vezir, siyah_at, siyah_kale, siyah_fil, siyah_piyon;
    public Sprite beyaz_þah, beyaz_vezir, beyaz_at, beyaz_kale, beyaz_fil, beyaz_piyon;

    // Start is called before the first frame update
    public void etkinleþtir()
    {
        controller = GameObject.FindGameObjectWithTag("control");
        if (controller == null)
        {
            Debug.LogError("Controller not found!");
            return;
        }

        kordinat();
        switch (this.name)
        {
            case "siyah_þah": this.GetComponent<SpriteRenderer>().sprite = siyah_þah; player = "siyah"; break;
            case "siyah_vezir": this.GetComponent<SpriteRenderer>().sprite = siyah_vezir; player = "siyah"; break;
            case "siyah_at": this.GetComponent<SpriteRenderer>().sprite = siyah_at; player = "siyah"; break;
            case "siyah_kale": this.GetComponent<SpriteRenderer>().sprite = siyah_kale; player = "siyah"; break;
            case "siyah_fil": this.GetComponent<SpriteRenderer>().sprite = siyah_fil; player = "siyah"; break;
            case "siyah_piyon": this.GetComponent<SpriteRenderer>().sprite = siyah_piyon; player = "siyah"; break;
            case "beyaz_þah": this.GetComponent<SpriteRenderer>().sprite = beyaz_þah; player = "beyaz"; break;
            case "beyaz_vezir": this.GetComponent<SpriteRenderer>().sprite = beyaz_vezir; player = "beyaz"; break;
            case "beyaz_at": this.GetComponent<SpriteRenderer>().sprite = beyaz_at; player = "beyaz"; break;
            case "beyaz_kale": this.GetComponent<SpriteRenderer>().sprite = beyaz_kale; player = "beyaz"; break;
            case "beyaz_fil": this.GetComponent<SpriteRenderer>().sprite = beyaz_fil; player = "beyaz"; break;
            case "beyaz_piyon": this.GetComponent<SpriteRenderer>().sprite = beyaz_piyon; player = "beyaz"; break;
        }
    }

    public void kordinat()
    {
        float x = xTahta;
        float y = yTahta;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetxTahta()
    {
        return xTahta;
    }
    public int GetyTahta()
    {
        return yTahta;
    }
    public void SetxTahta(int x)
    {
        xTahta = x;
    }

    public void SetyTahta(int y)
    {
        yTahta = y;
    }

    private void OnMouseUp()
    {
        if(controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer()==player)
        {
            DestroyMovePlate();
            initiateMovePlate();
        }
 
    }

    public void DestroyMovePlate()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void initiateMovePlate()
    {
        switch (this.name)
        {
            case "siyah_vezir":
            case "beyaz_vezir":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(0, -1);
                LineMovePlate(-1, 0);
                LineMovePlate(-1, -1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "siyah_at":
            case "beyaz_at":
                LMovePlate();
                break;
            case "siyah_fil":
            case "beyaz_fil":
                LineMovePlate(-1, -1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "siyah_þah":
            case "beyaz_þah":
                SurroundMovePlate();
                break;
            case "siyah_kale":
            case "beyaz_kale":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(0, -1);
                LineMovePlate(-1, 0);
                break;
            case "siyah_piyon":
                PiyonMovePlate(xTahta, yTahta + 1);
                break;
            case "beyaz_piyon":
                PiyonMovePlate(xTahta, yTahta - 1);
                break;
        }
    }

    public void LineMovePlate(int xIntrement, int yIntrement)
    {
        if (controller == null)
        {
            Debug.LogError("Controller is null in LineMovePlate.");
            return;
        }

        Game sc = controller.GetComponent<Game>();
        if (sc == null)
        {
            Debug.LogError("Game script not found on controller.");
            return;
        }

        int x = xTahta + xIntrement;
        int y = yTahta + yIntrement;

        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIntrement;
            y += yIntrement;
        }

        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) != null && sc.GetPosition(x, y).GetComponent<Karakter>().player != player)
        {
            MovePlateAttackSpawn(x, y);
        }
    }

    public void LMovePlate()
    {
        PointMovePlate(xTahta + 1, yTahta + 2);
        PointMovePlate(xTahta - 1, yTahta + 2);
        PointMovePlate(xTahta + 2, yTahta - 1);
        PointMovePlate(xTahta + 2, yTahta + 1);
        PointMovePlate(xTahta + 1, yTahta - 2);
        PointMovePlate(xTahta - 1, yTahta - 2);
        PointMovePlate(xTahta - 2, yTahta + 1);
        PointMovePlate(xTahta - 2, yTahta - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xTahta, yTahta + 1);
        PointMovePlate(xTahta, yTahta - 1);
        PointMovePlate(xTahta - 1, yTahta);
        PointMovePlate(xTahta - 1, yTahta - 1);
        PointMovePlate(xTahta - 1, yTahta + 1);
        PointMovePlate(xTahta + 1, yTahta + 1);
        PointMovePlate(xTahta + 1, yTahta - 1);
        PointMovePlate(xTahta + 1, yTahta);
    }

    public void PointMovePlate(int x, int y)
    {
        if (controller == null)
        {
            Debug.LogError("Controller is null in PointMovePlate.");
            return;
        }

        Game sc = controller.GetComponent<Game>();
        if (sc == null)
        {
            Debug.LogError("Game script not found on controller.");
            return;
        }

        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (sc.GetPosition(x, y).GetComponent<Karakter>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void PiyonMovePlate(int x, int y)
    {
        if (controller == null)
        {
            Debug.LogError("Controller is null in PiyonMovePlate.");
            return;
        }

        Game sc = controller.GetComponent<Game>();
        if (sc == null)
        {
            Debug.LogError("Game script not found on controller.");
            return;
        }

        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }
            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<Karakter>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }
            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<Karakter>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(hareket, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(hareket, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}
