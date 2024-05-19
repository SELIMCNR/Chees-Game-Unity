using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject þah;

    private GameObject[,] position = new GameObject[8, 8];
    private GameObject[] siyahTakim = new GameObject[16];
    private GameObject[] beyazTakim = new GameObject[16];

    private string mevcutOyuncu = "beyaz";

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        if (þah == null)
        {
            Debug.LogError("þah prefab is not assigned in the Inspector!");
            return;
        }

        siyahTakim = new GameObject[]
        {
          Create("siyah_kale",0,0),Create("siyah_at",1,0),
          Create("siyah_fil",2,0),Create("siyah_vezir",3,0),
          Create("siyah_þah",4,0),Create("siyah_fil",5,0),
          Create("siyah_at",6,0),Create("siyah_kale",7,0),
          Create("siyah_piyon",0,1), Create("siyah_piyon",1,1),
           Create("siyah_piyon",2,1), Create("siyah_piyon",3,1),
            Create("siyah_piyon",4,1), Create("siyah_piyon",5,1),
             Create("siyah_piyon",6,1), Create("siyah_piyon",7,1),
        };
        beyazTakim = new GameObject[]
        {
          Create("beyaz_kale",0,7),Create("beyaz_at",1,7),
          Create("beyaz_fil",2,7),Create("beyaz_vezir",3,7),
          Create("beyaz_þah",4,7),Create("beyaz_fil",5,7),
          Create("beyaz_at",6,7),Create("beyaz_kale",7,7),
          Create("beyaz_piyon",0,6), Create("beyaz_piyon",1,6),
           Create("beyaz_piyon",2,6), Create("beyaz_piyon",3,6),
            Create("beyaz_piyon",4,6), Create("beyaz_piyon",5,6),
             Create("beyaz_piyon",6,6), Create("beyaz_piyon",7,6),
        };

        for (int i = 0; i < siyahTakim.Length; i++)
        {
            SetPosition(siyahTakim[i]);
            SetPosition(beyazTakim[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(þah, new Vector3(0, 0, -1), Quaternion.identity);
        if (obj == null)
        {
            Debug.LogError($"Failed to instantiate {name}");
            return null;
        }

        Karakter krt = obj.GetComponent<Karakter>();
        if (krt == null)
        {
            Debug.LogError($"{name} does not have a Karakter component attached!");
            return null;
        }

        krt.name = name;
        krt.SetxTahta(x);
        krt.SetyTahta(y);
        krt.etkinleþtir();
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogError("GameObject is null in SetPosition");
            return;
        }

        Karakter ob = obj.GetComponent<Karakter>();
        if (ob == null)
        {
            Debug.LogError("Karakter component is null in SetPosition");
            return;
        }

        position[ob.GetxTahta(), ob.GetyTahta()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        position[x, y] = null;
    }
    public GameObject GetPosition(int x,int y)
    {
        return position[x, y];
    }

    public bool PositionOnBoard(int x,int y)
    {
        if(x<0 || y<0 || x>=position.GetLength(0)||y>=position.GetLength(1))
            return false;
            return true;
    }

    public string GetCurrentPlayer()
    {
        return mevcutOyuncu;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }
    public void NextTurn()
    {
        if(mevcutOyuncu == "beyaz")
        {
            mevcutOyuncu = "siyah";
        }
        else
        {
            mevcutOyuncu = "beyaz";
        }
    }

    private void Update()
    {
        if(gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;
            SceneManager.LoadScene("Game");
        }
    }

    public void winner(string playerWinner)
    {
        gameOver = true;
        GameObject.FindGameObjectWithTag("Winner").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("Winner").GetComponent<Text>().text = playerWinner+ "Kazandý";
       GameObject.FindGameObjectWithTag("Restart").GetComponent<Text>().enabled = true;
    }
}
