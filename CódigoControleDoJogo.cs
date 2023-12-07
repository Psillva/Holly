using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControleDoJogo : MonoBehaviour
{
    public bool gameLigado = false;
    [SerializeField] private GameObject Menucredito;
    [SerializeField] private GameObject Menuprincipal;
    [SerializeField] private GameObject Menufolhaprincipal;
    [SerializeField] private GameObject Menupause;
    [SerializeField] private GameObject telaVitoria;
    public GameObject telaGameOver;
    public Vector3 posInicial;

    public AudioSource emissorGamerOver2;
    public AudioClip AudioGameOver2;
    public AudioSource emissorWin;
    public AudioClip AudioWin;

  


    // Start is called before the first frame update
    void Start()
    {
        gameLigado = false;
        Time.timeScale = 0;
        emissorGamerOver2 = GetComponent<AudioSource>();
        

        emissorWin = GetComponent<AudioSource>();
       

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Reiniciar();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Pausar();
        }
    }

    public bool EstadoDoJogo()
    {
        return gameLigado;
    }
    public void LigaJogo()
    {
        Time.timeScale = 1;
        gameLigado = true;
    }

    public void PersonagemMorreu()
    {
        telaGameOver.SetActive(true);
        Time.timeScale = 0;
        gameLigado = false;
        emissorGamerOver2.clip = AudioGameOver2;
        emissorGamerOver2.PlayOneShot(emissorGamerOver2.clip);

    }

    public void JogadorVenceu()
    {
        telaVitoria.SetActive(true);
        Time.timeScale = 0;
        gameLigado = false;
        emissorWin.clip = AudioWin;
        emissorWin.PlayOneShot(emissorWin.clip);
    }
    public void Reiniciar()
    {
        SceneManager.LoadScene(0);

    }
    public void MenuFolhaPrincipal()
    {
        Menuprincipal.SetActive(false);
        Menufolhaprincipal.SetActive(true); 
    }

    public void FecharFolhaPrincipal()
    {
        Menuprincipal.SetActive(true);
        Menufolhaprincipal.SetActive(false);
    }
    public void MenuCredito()
    {
        Menucredito.SetActive(true);
        Menuprincipal.SetActive(false);

    }
    public void FecharCredito()
    {
        Menucredito.SetActive(false);
        Menuprincipal.SetActive(true);

    }
    public void Pausar()
    {
        Menupause.SetActive(true);
        Time.timeScale = 0;
    }
    public void ReniciarPause()
    {
        Menupause.SetActive(false);
        Time.timeScale = 1;
    }

    public void SairDaPartida()
    {
        Menuprincipal.SetActive(true);
        Menupause.SetActive(false);
        Reiniciar();
    }


    public void SaiDoJogo()
    {
        Application.Quit();
    }
}
