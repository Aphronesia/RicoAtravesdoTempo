using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Universo : MonoBehaviour
{
    [SerializeField]
    private GameObject Pinha, Bigorna, Coracao, Inicia, Pause, Morreu, Ganhou, Perdeu, Scoreboard, ScoreboardSave;
    private float escalaRandom;
    float[] possivelposicao = {-1.75f, 0f, 1.75f};
    int[] Spawnobjeto = {1, 2, 3, 4, 5, 6, 7, 8};
    public float SpawnRate;
    public int pontos, vidas, metaPoints;
    public bool Pausado, PodeSpawn;
    public Capivara rico;
    public Temporizador temporizador;
    public SoundControllerPinha soundPinha;
    public TMP_Text txtPontos, txtVidas;
    bool umavez = true;
    public ControlScenes controlScenes;
    public ScoreboardControl scoreboardControl;
    // Start is called before the first frame update
    void Start()
    {
        Pausado = false;
        PodeSpawn = false;
        pontos = 0;
        vidas = 3; 
        txtPontos.text = pontos.ToString();
        txtVidas.text = vidas.ToString();

        //procura o controlador de cena
        ControlScenes ScenesController = FindObjectOfType<ControlScenes>();
        if (ScenesController != null){controlScenes = ScenesController.GetComponent<ControlScenes>();}
        //else {Debug.LogError("Objeto indestrutível não encontrado!");}
        Inicia.SetActive(true);
        Scoreboard.SetActive(true);
        ScoreboardSave.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if ( vidas == 0 && umavez){
            soundPinha.PlaySound(2);
            PodeSpawn = false;
            rico.Morreu();
            umavez=false;
            StartCoroutine(Faleceu());
            temporizador.Parar();
        }
        //Debug.Log("jogo esta acontecendo");
    }
    IEnumerator Faleceu()
    {
        yield return new WaitForSeconds(3f);
        Morreu.SetActive(true);
        Scoreboard.SetActive(true);
        scoreboardControl.UpdateScoreText();
        ScoreboardSave.SetActive(true);
    }
    public void Tempofim()
    {
        PodeSpawn = false;
        if (pontos >= metaPoints){
            Ganhou.SetActive(true);
        }
        else{
            Perdeu.SetActive(true);
        }
        Scoreboard.SetActive(true);
        scoreboardControl.UpdateScoreText();
        ScoreboardSave.SetActive(true);
    }
    public void TempoPadrao()
    {
        SpawnRate = 1f;
    }
    public void TempoRapido()
    {
        SpawnRate = 0.5f;
    }
    public void TempoMedio()
    {
        SpawnRate = 0.75f;
    }
    public void Iniciar(){
        temporizador.Comeca();
        rico.Reviveu();
        Inicia.SetActive(false);
        Scoreboard.SetActive(false);
        ScoreboardSave.SetActive(false);
        pontos = 0;
        vidas = 3; 
        PodeSpawn = true;
        txtVidas.text = vidas.ToString(); 
        StartCoroutine(GeracaoComida());
        umavez=true;
    }
    public void Recomeca(){
        controlScenes.RestartGame();
    }
    public void Vitoria(){
        controlScenes.ChangeScene(2);
    }
    public void Sair(){
        controlScenes.QuitGame();
    }
    public void VoltarMenu(){
        controlScenes.ReturnHome();
    }
    public void Pausa(){
        if(!Pausado){
            temporizador.Parar();
            rico.Parar();
            Time.timeScale=0f;
            Pausado =true;
            Pause.SetActive(true);
            Scoreboard.SetActive(true);
        }
        else
        {
            Pause.SetActive(false);
            Scoreboard.SetActive(false);
            temporizador.Voltar();
            rico.Voltar();
            Time.timeScale=1f;
            Pausado = false;
        }
    }
    public void Addpontos()
    {
        soundPinha.PlaySound(0);
        pontos = pontos + 1;
        txtPontos.text = pontos.ToString();
    }
    public void Dano()
    {
        switch(vidas){
            case <= 0:
            txtVidas.text = vidas.ToString();
            break;
            case >0:
            temporizador.Dano();
            vidas = vidas - 1;
            txtVidas.text = vidas.ToString();
            soundPinha.PlaySound(1);
            break;
        }
    }
    public void Cura()
    {
        soundPinha.PlaySound(3);
        if( vidas <= 2){
            vidas++;
            txtVidas.text = vidas.ToString(); 
        }
    }
    IEnumerator GeracaoComida()
    {
        while (PodeSpawn == true){
            // Obtenha um índice aleatório entre 0 e o comprimento do vetor menos 1
            int indiceAleatorio = UnityEngine.Random.Range(0, possivelposicao.Length);
            int indiceSpawn = UnityEngine.Random.Range(0, Spawnobjeto.Length);
            int Spawn = Spawnobjeto[indiceSpawn];
            float posicaoaleatoria = possivelposicao[indiceAleatorio];
            switch (Spawn){
                case <=5:
                Instantiate(Pinha, new Vector3( posicaoaleatoria, 6 ,0),Quaternion.identity);
                break;
                case >5 and <=7:
                Instantiate(Bigorna, new Vector3( posicaoaleatoria, 6 ,0),Quaternion.identity);
                break;
                case >=8:
                Instantiate(Coracao, new Vector3( posicaoaleatoria, 6 ,0),Quaternion.identity);
                break;
            }
            yield return new WaitForSeconds(SpawnRate);
        }
    }
}
