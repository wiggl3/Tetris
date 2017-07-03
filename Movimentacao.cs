using UnityEngine;
using System.Collections;

public class Movimentacao : MonoBehaviour {

	public bool canTurn; /*Pode rodar*/
	public bool turn360; /*Quanto roda*/

	public float fall;

    public float velocidade;
    public float timer;

	Administrativo adm;//chama todo o findobjectoftype pr dentro de uma unica funçao com outro nome
	Spawn spa;

	// Use this for initialization
	void Start () {
        timer = velocidade;

		adm = GameObject.FindObjectOfType<Administrativo> ();//ativa a funçao(referencia)
		spa = GameObject.FindObjectOfType<Spawn> (); 
	}
	
	// Update is called once per frame
	void Update () {

		if (adm.difficultyPoints > 1000) /*se o ponto de dificuldade for igual a 1000*/
		{
			//adm.difficultyPoints = 0;(pont0s de dificuldade zerara)
			adm.difficultyPoints -= 1000; //assim ele so diminuira 1000 aos inves de zerar. Melhorando a contagem quando acontecer combos. 
			adm.difficulty += .5f;/*velocidade aumentara*/
		}

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow))/*Se o jogador solta alguma tecla, ele recomeça o timer(corrigindo bug de andar pro lado)*/
        {
            timer = velocidade;
        }
		if(Input.GetKey/*"GetKey" detecta continuamente,"GetKeyDown"Quando clica(detecta uma vez só)*/(KeyCode.RightArrow))
		{
            timer += Time.deltaTime; /*Time.deltaTime vai aumentando o valor("faz correr")*/
            if (timer > velocidade) /*a peça se move sempre que o timer for maior que a velocidade*/
            {
                transform.position += new Vector3/*Vector tres referi-se a x,y,z*/(1, 0, 0);
                timer = 0;/*faz com que a velocidade sempre retorne para zero depois da função acontecer(sempre retorna)*/
            }
			/*Se estiver valida (dentro do grid))*/if (validatesPosition())
			{
                //FindObjectOfType<Administrativo>().updateGrid(this/*pega o valor da peça que tá se momento, joga pra função e executa*/);
				adm.updateGrid(this);//declara a funçao nova
			}

			/*Se nao estiver valida(fora do grid)*/else
			{
				transform.position += new Vector3(-1,0,0);
			}
		}

		if (Input.GetKey (KeyCode.LeftArrow))
		{
            timer += Time.deltaTime;
            if (timer > velocidade)
            {
                transform.position += new Vector3(-1, 0, 0);
                timer = 0;
            }
			if (validatesPosition())
			{
                //FindObjectOfType<Administrativo>().updateGrid(this);
				adm.updateGrid(this);
			}
			else
			{
				transform.position += new Vector3(1,0,0);
			}
		}

		if (Input.GetKey(KeyCode.DownArrow)/*||se Time.time - fall >= 1joga a peça para baixo*/) 
		{
            timer += Time.deltaTime;
            if(timer > velocidade)
            {
			   transform.position += new Vector3(0,-1,0);
               timer = 0;
            }
			if(validatesPosition())
			{
                //FindObjectOfType<Administrativo>().updateGrid(this);
				adm.updateGrid(this);
			}
			else
			{
				transform.position += new Vector3(0,1,0);
                //FindObjectOfType<Administrativo>().erasesLine();/*função de apagar a linha chama todas as outras*/
				adm.erasesLine();

				if(adm.upGrid(this))
				{
					adm.gameOver();
				}

				adm.scorePiece += 10;
				adm.difficultyPoints += 10;
				adm.totalScore = adm.scoreLine + adm.scorePiece; 
                enabled = false;
                //FindObjectOfType<Spawn>().next();
				spa.next ();
			}
			//fall = Time.time;Quanto tempo se passou do inicio do jogo
			//Quando Time.time for igual a 1, diminuira de 1 e vai começar de novo
		}

        if (Time.time - fall >= (1 / adm.difficulty) /*&& !Input.GetKey(KeyCode.DownArrow)*/)/*Tira a função da tecla de queda para não bugar*/
        {
            transform.position += new Vector3(0, -1, 0);
            /*Verifica se a posição é valida (correção do bug de passar por baixo)*/if (validatesPosition())
            {
                //FindObjectOfType<Administrativo>().updateGrid(this);
				adm.updateGrid(this);
            }
            else
            { 
            transform.position += new Vector3(0, 1, 0);
            //FindObjectOfType<Administrativo>().erasesLine();
			adm.erasesLine();

			if (adm.upGrid(this))
			{
			      adm.gameOver();
			}

			adm.scorePiece += 10;
			adm.difficultyPoints += 10;/*soma 10 pontos de dificuldade junto aos 10 do toque de uma peça em outra*/
			adm.totalScore = adm.scoreLine + adm.scorePiece; 
            /*desabilita a peça*/enabled = false;
            spa.next();
            }
            fall = Time.time;
        }

		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			//transform.Rotate (0,0,-90);
			checkTurn();
		}
	}

	void checkTurn () /*Checa se roda*/ 
	{
		if (canTurn)/*Caso possa rodar*/ 
		{
			if(!turn360)/*Se roda ou nao em 360 graus*/
			{
				if (transform.rotation.z < 0)/*Se a posiçao da rotaçao for maior que 0*/
				{
					transform.Rotate (0,0,90);/*posiçao sera igual 90*/

					if(validatesPosition())/*Caso a posiçao seja valida*/
					{
                        //FindObjectOfType<Administrativo>().updateGrid(this);
						adm.updateGrid(this);
					}
					else/*caso nao seja, posiçao sera igual -90 graus*/
					{
						transform.Rotate(0,0,-90);
					}
				}

				else/*Se a posiçao de rotaçao nao for maior que 0*/
				{
					transform.Rotate (0,0,-90);/*Sera igual -90 graus*/

					if(validatesPosition())
					{
                        //FindObjectOfType<Administrativo>().updateGrid(this);
						adm.updateGrid(this);
					}
					else
					{
						transform.Rotate (0,0,90);/*Caso nao seja, sera igual 90 graus*/
					}
				}
			}
			else /*Se ela nao pode rodar 360 graus*/
			{
				transform.Rotate(0,0,-90);/*Passara de 0 para -90 senpre*/

				if(validatesPosition())
				{
                    //FindObjectOfType<Administrativo>().updateGrid(this);
					adm.updateGrid(this);
				}
				else
				{
					transform.Rotate (0,0,90);/*Somara 90 para ficar igual a 0 e nao passar da grid*/
				}

			}
		}
	}

	/*Verifica se a posiçao e falsa*/bool validatesPosition()/*Posiçao em que pode ficar*/
	{
		foreach/*loop*/ (Transform child in transform)/*filhos dos gameobjects*/ 
		{
			//Vector2 blockPos (Transforma a posiçao arredondada de cada quadrado)= FindObjectOfType<Administrativo>()(Buscando outros script).rounds(child.position);(funçao que foi buscada)
			Vector2 blockPos = adm.rounds(child.position);
			//(Verifica se a posiçao arredondada esta dentro da grade)if (FindObjectOfType<Administrativo>().inGrid (blockPos)== false)(busca a posiçao ja arredondada)
			if (adm.inGrid (blockPos) == false)
			{
				return false;
				/*Se nao estiver ele para o codigo*/
			}

            //if (FindObjectOfType<Administrativo>().gridPositionTransform(blockPos(passando pra ela o que deve usar)) != null(verifica se não é nulo) && FindObjectOfType<Administrativo>().gridPositionTransform(blockPos).parent != transform)
			if (adm.gridPositionTransform(blockPos) != null && adm.gridPositionTransform(blockPos).parent !=transform)
            {
                return false; /*se não for negativo e nem a peça que esta movendo, posição nao é valida(tem algum bloco ali)*/
            }
		}
		return true;
		/*Se estiver ele continua o codigo*/
	}
}
