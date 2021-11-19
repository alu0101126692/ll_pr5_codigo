using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using TMPro;

public class dictate : MonoBehaviour
{


    private DictationRecognizer dictado;
    public bool dictando;
    public keyword reconocedorKeyword;
    public TextMeshPro texto;

    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        administarDictado();
    }


    private void empezarDictado() {

        dictado = new DictationRecognizer();

        dictado.DictationResult += (text, confidence) => {

            Debug.Log("Resultado reconocimiento " + text);
            texto.text = text;

        };

        dictado.DictationHypothesis += (text) => {

            Debug.Log("Hipotesis del texto " + text);
            texto.text = text + " (Hipotesis)";

        };

    }
    private void administarDictado() {
        if (reconocedorKeyword.encendido) { // Solo ejecutar esto si esta encedido
            if (!dictando && !reconocedorKeyword.reconociendo) {
                if (Input.GetKeyDown("g")) {
                    empezarDictado();
                    dictando = true;
                    Debug.Log(PhraseRecognitionSystem.Status);
                    if (PhraseRecognitionSystem.Status.ToString() != "Stopped")
                        PhraseRecognitionSystem.Shutdown(); // Tiene que apagarse esto antes de cada ejecucion ya que no puede ejecutarse a la vez que el dictado
                    
                    
                    dictado.Start();
                } 

                
            } else if (dictando){
                if (Input.GetKeyDown("g")) {
                    dictando = false;
                    dictado.Stop();
                    parar();
                } 
            }
        }
    }

    public void parar() {
        if (dictado != null)
            dictado.Dispose();
    }
}
