using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaDelJuego : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    GameObject[,] matriz; //grid
    public Color baseColor;
    public Color jugador1;
    public Color jugador2;
    public Color IA;
    public TextMesh m3Dtext;
    public TextMesh ronda;
    public float distance = 0;
    bool turno;
    int turnos = 1;
    void Start()
    {
        matriz = new GameObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject esfera = GameObject.CreatePrimitive(PrimitiveType.Sphere) as GameObject;
                esfera.GetComponent<Renderer>().material.color = baseColor;
                esfera.transform.position = new Vector3(x * distance, y * distance, 0f);
                matriz[x, y] = esfera;
            }
        }
    }  
    void Update()
    {
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!turno)
        {
            m3Dtext.text = "Jugador = 1.";
        }
        else
            m3Dtext.text = "Jugador = 2.";
        ronda.text = ("Turno = " + turnos);
        PintarFicha(mPosition);
    }
    void PintarFicha(Vector3 position)
    {
        int x = (int)((position.x + 0.5f) / distance);
        int y = (int)((position.y + 0.5f) / distance);
        if (Input.GetMouseButtonDown(0))
        {
            if (x >=0 && y >=0 && x < width && y< height)
            {
                GameObject esfera = matriz[x, y];
                if (esfera.GetComponent<Renderer>().material.color == baseColor)
                {
                    Color colorAUsar = Color.clear;
                    if (!turno)
                    {
                        
                        colorAUsar = jugador1;
                        esfera.GetComponent<Renderer>().material.color = jugador1;
                        turno = true;
                        Verificar(x, y, colorAUsar);
                        Verificar2(x, y, colorAUsar);
                    }
                    else 
                    {
                        
                        colorAUsar = jugador2;
                        esfera.GetComponent<Renderer>().material.color = jugador2;
                        turno = false;
                        Verificar(x, y, colorAUsar);
                        Verificar2(x, y, colorAUsar);
                    }
                    if (turnos % 2 == 0)
                    {
                        print("Estoy en el si de turnos");
                        int iro = Random.Range(0, 3);
                        switch (iro) // Swith que verifica el numero dado anterior.
                        {
                            case 0: // Primera opcion
                                int n = 1;
                                Regla(n);
                                break;
                            case 1: // Segunda opcion
                                int nn = 2;
                                Regla(nn);
                                break;
                            case 2: // Segunda opcion
                                int m = 3;
                                Regla(m);
                                break;
                        }
                    }
                }
                turnos++;
            }
        }
    }
    public void Verificar(int s, int q, Color colorVerificar)
    {
        int gana, verifica;
        verifica = 0;
        gana = 0;
        //horizontal
        if (verifica == 0)
        {
            for (int i = s - 3; i < s + 4; i++)
            {
                if (i < 0 || i >= width)
                    continue;
                GameObject esfera = matriz[i, q];
                if (esfera.GetComponent<Renderer>().material.color == colorVerificar)
                {
                    gana++;
                    if (gana == 4)
                    {
                        print("ganas");
                    }
                }
                else
                {
                    gana = 0;
                }
            }
            verifica++;
            gana = 0;
        }
        // vertical
        if (verifica ==1)
        {
            for (int j = q - 3; j < q + 4; j++)
            {
                if (j < 0 || j >= height)
                    continue;
                GameObject esfera = matriz[s, j];

                if (esfera.GetComponent<Renderer>().material.color == colorVerificar)
                {
                    gana++;
                    if (gana == 4)
                    {
                        print("ganas");
                    }
                }
                else
                {
                    gana = 0;
                }
            }
            gana = 0;
        }
    }
    public void Verificar2(int X, int Y, Color colorVerificar)
    {
        int gana, verifica;
        verifica = 0;
        gana = 0;

        //diagonal ariba
        if (verifica == 0)
        {
            int j = Y - 3;
            for (int i = X - 3; i <= X + 3; i++)
            {
                if ((i >= 0 && i < width) && (j >= 0 && j < height))
                {
                    GameObject esfera = matriz[i, j];
                    if (esfera.GetComponent<Renderer>().material.color == colorVerificar)
                    {
                        // print("Esfera que compara es:" + esfera.GetComponent<Renderer>().material.color + "color de jugador" + colorVerificar);
                        gana++;
                        if (gana == 4)
                        {
                            print("ganas");
                        }
                    }
                    else
                    {
                        gana = 0;
                    }
                }
                if (j < 0 || j < width)
                    j++;
            }
            
            verifica++;
            gana = 0;
        }
        
        // diagonal abajo
        if (verifica ==1)
        {
            int k = Y + 3;
            for (int i = X - 3; i <= X + 3; i++)
            {
                if ((i >= 0 && i < width) && (k >= 0 && k < height))
                {
                    GameObject esfera = matriz[i, k];
                    if (esfera.GetComponent<Renderer>().material.color == colorVerificar)
                    {
                        gana++;
                        if (gana == 4)
                        {
                            print("ganas");
                        }
                    }
                    else
                    {
                        gana = 0;
                    }
                }
                if (k <0 || k<= width)
                k--;
            }
            
            gana = 0;
        }
    }
    public void Regla(int m)
    {
        if (m == 1)
        {
            int x = Random.Range(0, 10);
            int y = Random.Range(0, 10);
            int i = Random.Range(0, 10);
            int j = Random.Range(0, 10);
            GameObject esfera = matriz[x, y];
            GameObject esfera2 = matriz[i, j];
            if (esfera.GetComponent<Renderer>().material.color == baseColor)
            {
                esfera.GetComponent<Renderer>().material.color = IA;
                esfera2.GetComponent<Renderer>().material.color = IA;
            }
        }
        else if (m==2)
        {
            int randon = Random.Range(0, 2);
            print("Randon es:"+ randon);
            switch (randon) // Swith que verifica el numero dado anterior.
            {
                case 0: // Primera opcion
                    turno = false;
                    break;
                case 1: // Segunda opcion
                    turno = true;
                    break;
            }
        }
        else
        {
            int x = Random.Range(0, 10);
            int y = Random.Range(0, 10);
            int i = Random.Range(0, 10);
            int j = Random.Range(0, 10);
            int g = Random.Range(0, 10);
            int h = Random.Range(0, 10);
            GameObject esfera = matriz[x, y];
            GameObject esfera2 = matriz[i, j];
            GameObject esfera3 = matriz[g, h];
            if (esfera.GetComponent<Renderer>().material.color == baseColor)
            {
                esfera.GetComponent<Renderer>().material.color = IA;
                esfera2.GetComponent<Renderer>().material.color = IA;
                esfera3.GetComponent<Renderer>().material.color = IA;
            }
        }
    }
}