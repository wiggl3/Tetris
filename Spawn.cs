using UnityEngine;
using System.Collections;
using System.Collections.Generic;/*biblioteca que libera o usod e listas*/

public class Spawn : MonoBehaviour {

	public int newPiece;
	public Transform[]/*indica array(lista)*/ Create;/*criando peças*/
	public List<GameObject> showNextPiece;//lista dos objetos

	// Use this for initialization
	void Start () {
		/*(faz a peça surgir e ser aleatoria)Instantiate (Create [Random.Range(Range ignora o valor maximo ali dentro(Cria um indice e sempre diminuium numero dele)) (0, 7)] (randomiza um dentro da array)
		             ,transform.position(para saber a posiçao do spawn)
		             ,Quaternion.identity(coloca na rotaçao padrao));*/
		newPiece = Random.Range (0, 7);//dando um valor para a proima peça
		next ();//chama a funçao de proxima peça
	}

    public void next()/*Para chamar a proxima peça*/
    {
        //Instantiate(Create[Random.Range(0, 7)], transform.position, Quaternion.identity);
		Instantiate(Create[newPiece], transform. position, Quaternion.identity);//muda o create para puxar a proxima peça ja calculada e nao pra calcular dentro dele.
		//instancia a peça antes de zerar decimal novo

		newPiece = Random.Range (0, 7);//da um novo valor a variavel para ja saber qual e a proxima

		for (int i = 0; i < showNextPiece.Count; i++)//vasculha a lista e conta os objetos 
		{
			showNextPiece[i].SetActive(false);//mantem as peças sumidas enquanto nao souber qual sera a proxima.
		}

		showNextPiece[newPiece].SetActive(true);//faz a proxima peça aparecer buscando a funçao de peça nova e perguntando a ela qual e 
	}
	

}
