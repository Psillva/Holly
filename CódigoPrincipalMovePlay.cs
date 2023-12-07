using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{    //Vari�veis 

    //Pega o componente Rigidbody
    public Rigidbody2D corpoPlayer;
    public float velocidadePlayer; // recebe velocidade do player
    public int quantidadeDePulo = 1;
    public bool taNoChao;
    public Transform detectaChao;
    public LayerMask oQueEChao;
    public GameObject player;
    public Animator animator;

    //Apontar direção
    public Vector3 facingRight;
    public Vector3 facingLeft;
    //ColetarCenoura
    public int cenoura = 0;
    public Text cenouraTexto;

    //Texto da vida e vida do player
    public int Vidas = 3;
    public Text vidasText;

    //Dano do personagem
    private bool podeDano = true;
    private float tempoDeDano;

    //Posição Inicial do player
    public Vector3 posInicial;

    //Tempo do vento
     public float tempoDoVento;
     public float time;
     public float ventoTime;

    //Coleta dados do script ContoleDoJogo
    public ControleDoJogo genJ;
    public Vento ativoVento;

    //Imagem de aviso
    public float TempoDaImagem = 1;
    public Image _Imagem;
    

    //Sons
    private AudioSource emissorSom;
    public AudioClip AudioImagem;

    private AudioSource emissorPulo;
    public AudioClip AudioPulo;

    public AudioSource emissorGamerOver;
    public AudioClip AudioGamerOver;

    private AudioSource emissorColeta;
    public AudioClip AudioColeta;









    // Start is called before the first frame update
    void Start()
    {
        facingLeft = transform.localScale;
        facingRight = transform.localScale;
        facingLeft.x = facingLeft.x * -1;
        ModTexto();

        posInicial = new Vector3(-5, -1.56f, 0);
        transform.position = posInicial;


        genJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControleDoJogo>();
        animator = GetComponent<Animator>();

        //Som e Imagem
        _Imagem.enabled = false;
        emissorSom = GetComponent<AudioSource>();
        emissorPulo = GetComponent<AudioSource>();
        emissorGamerOver = GetComponent<AudioSource>();
        
        emissorColeta = GetComponent<AudioSource>();


        
        

    }

    // Programa Principal
    void Update()
    {
        time = time + Time.deltaTime;
        

        if (genJ.EstadoDoJogo() == true)
        {
            Movimentacao();
            ApontarDirecao();
            Pular();
            Dano();
            Vento();
            Alerta();
            taNoChao = Physics2D.OverlapCircle(detectaChao.position, 0.3f, oQueEChao);

            if (Input.GetKey(KeyCode.R))
            {
                ReiniciarPartida();
            }
        }
    }

    //Chances que o player tem
    void modVidas()
    {
        vidasText.text = "Vidas: " + Vidas.ToString() + "x";
    }
    //Contador de cenoura
    void ModTexto()
    {
        cenouraTexto.text = cenoura.ToString() + " x";
    }


    //Fun��o que movimenta o player
    void Movimentacao()
    {
        velocidadePlayer = Input.GetAxis("Horizontal") * 3.5f;
        corpoPlayer.velocity = new Vector2(velocidadePlayer, corpoPlayer.velocity.y);

        if(velocidadePlayer != 0)
        {
            animator.SetBool("taAndando",true);
        }else{
            animator.SetBool("taAndando", false);
        }
    }

    void Vento()
    {
        Debug.Log("vento");

        if (time > 10)
        {
            
            StartCoroutine(TemporizadorVento());

        }
        
    }
        
    void Alerta()
        {
            if (time >= 9 && time < 9.9f)
            {
                StartCoroutine(EsperarTempo());
            }
        }
       

   

    IEnumerator TemporizadorVento()
    {
        Debug.Log("ventoIE");
        corpoPlayer.velocity -= new Vector2(1.5f, 0);
        yield return new WaitForSeconds(10);
        time = 0f;
        ;
    }


    IEnumerator EsperarTempo()
    {
        _Imagem.enabled = true;
        emissorSom.PlayOneShot(emissorSom.clip);
        yield return new WaitForSeconds(TempoDaImagem);
        yield return new WaitForSeconds(10);
        _Imagem.enabled = false;
        time = 0f;
    }





    // Fun��o para mudar a dire��o para onde o player est� se movendo
    void ApontarDirecao()
    {
        if (velocidadePlayer > 0)
        {
            transform.localScale = facingRight;
        }
        else if (velocidadePlayer < 0)
        {
            transform.localScale = facingLeft;
        }
    }


    //Fun��o para o player pular 
    void Pular()
    {
        
        if (Input.GetButtonDown("Jump") && taNoChao == true)
        {
            corpoPlayer.velocity = Vector2.up * 8;
            emissorPulo.clip = AudioPulo;
            emissorPulo.PlayOneShot(emissorPulo.clip);
            animator.SetBool("taPulando", true);
            
        }
        if (Input.GetButtonDown("Jump") && taNoChao == false && quantidadeDePulo > 0)
        {

             animator.SetBool("taPulando", true);
            emissorPulo.clip = AudioPulo;
            emissorPulo.PlayOneShot(emissorPulo.clip);
            corpoPlayer.velocity = Vector2.up * 8;
            quantidadeDePulo --;
            
        }
        if (taNoChao && corpoPlayer.velocity.y <= 0)
        {
            quantidadeDePulo = 1;
           animator.SetBool("taPulando", false);
            
            
        }

    }

    //Fun��o intera��o Trigger
    void OnTriggerEnter2D(Collider2D Gatilho)
    {

        if (Gatilho.gameObject.tag == "Cenoura")
        {
            emissorColeta.clip = AudioColeta;
            emissorColeta.PlayOneShot(emissorColeta.clip);
            Destroy(Gatilho.gameObject);
            cenoura++;
            ModTexto();
            if (cenoura >= 3)
            {
                Vidas++;
                cenoura = 0;
                vidasText.text = "Vidas " + Vidas.ToString();
                
            }
        }
        if (Gatilho.gameObject.tag == "Poço")
        {
           
            if (podeDano == true)
            {
                emissorGamerOver.clip = AudioGamerOver;
                emissorGamerOver.PlayOneShot(emissorGamerOver.clip);
                Vidas--;
                podeDano = true;
                tempoDeDano = 0;
                time = 0f; 
                if (Vidas <= 0)
                {
                    genJ.PersonagemMorreu();
                }
                else
                {
                    Inicializar();
                }
            }
        }
        if (Gatilho.gameObject.tag == "CheckPoint")
        {
            Debug.Log("Foi");
            posInicial = Gatilho.gameObject.transform.position;
            Destroy(Gatilho.gameObject);
        }
        if (Gatilho.gameObject.tag =="CasaVitória")
        {
            genJ.JogadorVenceu();
        }

    }

    //Fun��o intera��o Colis�o 
    private void OnCollisionEnter2D(Collision2D Colisao)
    {
        if (Colisao.gameObject.tag == "Espinho")
        {
            _Imagem.enabled = false;
            if (podeDano == true)
            {
                emissorGamerOver.clip = AudioGamerOver;
                emissorGamerOver.PlayOneShot(emissorGamerOver.clip);
                Vidas--;
                podeDano = true;
                tempoDeDano = 0;
                time = 0f;
               
                if (Vidas <= 0)
                {
                    genJ.PersonagemMorreu();
                }
                else
                {
                    Inicializar();
                }
            }
        }
    }


    //Fun��o tempo do pulo do player
    void TemporizadorDano()
    {
        tempoDeDano += Time.deltaTime;
        if (tempoDeDano > 0.2f)
        {
            podeDano = true;
            tempoDeDano = 0;
        }
    }



    //Fun��o de Dano
    void Dano()
    {
        if (podeDano == true)
        {
            TemporizadorDano();
        }
    }



    void Morrer()
    {
        Vidas--;
        vidasText.text = "Vidas " + Vidas.ToString();
        if (Vidas <= 0)
        {
            //transform.position = posInicial;
            genJ.PersonagemMorreu();
        }

    }


    //Inicializar 
    public void Inicializar()
    {
        //transform.position = new Vector3(-5, -1.56f, transform.position.z);
        vidasText.text = "Vidas " + Vidas.ToString();
        transform.position = posInicial;

    }
    public void ReiniciarPartida()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
