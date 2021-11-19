using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class keyword : MonoBehaviour
{
    public string[] keywords;
    private KeywordRecognizer recognizer;
    public bool reconociendo;
    public dictate reconocedorDictado;

    public bool encendido;

    public Material materialApagado;
    public Material materialEncendido;
    public Material materialRed;


    public Material materialRandom;

    public Renderer materialActual;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        administarReconocedor();
        
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args) {
        Debug.Log ("Keyword: " + args.text + " Confianza: " + args.confidence + " Momento en el que se inicio: " + args.phraseStartTime + " Duracion: "  + args.phraseDuration);



        if (encendido) {
            switch (args.text) {
                case "Apagar":
                
                    encendido = false;
                    materialActual.material = materialApagado;

                    if (reconocedorDictado.texto.text.Length != 0) {
                        reconocedorDictado.texto.text = "";
                    }
                
                break;

                case "Cambiar Canal":

                        materialRandom.color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));
                        materialActual.material = materialRandom;
                    break;
                case "Canal 1": 

                        materialActual.material = materialEncendido;
                    break;
                
                case "Canal 2": 

                        materialActual.material = materialRed;
                    break;
                default:
                    Debug.Log(args.text + " no es una orden");
                break;
                
            }
        } else if (args.text == "Encender") {
            materialActual.material = materialEncendido;
            encendido = true;
        }
        
            
            

        
    
    }


    private void empezarKeyword() {
        recognizer = new KeywordRecognizer(keywords);

        recognizer.OnPhraseRecognized += OnPhraseRecognized;
    }
    private void administarReconocedor() {
        if (!reconociendo && !reconocedorDictado.dictando) {
            if (Input.GetKeyDown("f")) {
                empezarKeyword();
                reconociendo = true;
                //reconocedorDictado.parar();

                recognizer.Start();
            } 

            
        } else if (reconociendo){
            if (Input.GetKeyDown("f")) {
                reconociendo = false;
                recognizer.Stop();
                parar();
            } 
        }
    }
    
    public void parar() {
        recognizer.Dispose();
    }
}
