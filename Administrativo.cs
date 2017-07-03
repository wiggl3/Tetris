using UnityEngine;
using System.Collections;
using UnityEngine.UI;//biblioteca da interface.Se nao buscar nao da pra usar score
using UnityEngine.SceneManagement;//biblioteca de troca de cena.

public class Administrativo : MonoBehaviour {

	public static int altura = 20;
	public static int largura = 10;

	public int scorePiece = 0;
	public int scoreLine = 0; //funçao de pontuaçao
	public int totalScore = 0;

	public Text scorePieceTxt;
	public Text scoreLineTxt; //nomeia a variavel de texto
	public Text TotalScoreTxt;

	public int difficultyPoints;/*pntos que serao somados para aumentar a velocidade*/
	public float difficulty = 1;/*velocidade*/

    public static Transform[,]/*virgula torna a array bidimensional*/ grid = new Transform[largura, altura];


	void Update()
	{
		scorePieceTxt.text = scorePiece.ToString ();
		scoreLineTxt.text = scoreLine.ToString();
		TotalScoreTxt.text = totalScore.ToString ();
	}

	public bool inGrid /*Na grade*/ (Vector2 position)
	{
		return ((int)position.x >= 0
			&& (int)position.x < largura 
			&& (int)position.y >= 0);
		/*Pega a posiçao passada e verifica se x e maior que 0, menor que a largura e se y e menor que 0*/
	}

	public Vector2 rounds/*Arredonda*/(Vector2 Nr/*Numero arredondado*/)
	{
		return new Vector2 (Mathf.Round/*funçao de arredondamento*/ (Nr.x),
		                   Mathf.Round (Nr.y));
	}

    public void updateGrid(Movimentacao tetrisPiece)/*passa por todos os quadrados em x e y*/
    {
        for (int y = 0; y < altura; y++)/*passa por toda a grade em y(começa em zero por que a grade começa em -1 mas zera pelo array)*/
        {
            for (int x = 0; x < largura; x++)/*passa por toda a grade em x(começa em zero por que a grade começa em -1 mas zera pelo array)*/
            {
                if (grid [x, y] != null)/*verifica se no ponto onde ele está procurando há alguma peça*/
                {
                    if(grid[x,y].parent == tetrisPiece.transform)/*caso não esteja vazio e não seja a peça que estou mexendo*/
                    {
                    grid[x,y] = null;/*transforma a cordenada em nula(vazia) para ele poder continuar movendo*/
                    }
                }
            }
        }

        foreach (Transform piece/*cada quadradinho*/ in tetrisPiece.transform)/*passa por cada quadrado da peça*/
        {
            Vector2 position = rounds(piece.position);/*arredonda o valor da peça*/

            if (position.y < altura)/*verifica se a peça está dentro da grade. */
            {
                /*Se está dentro da grade*/
                grid[(int/*precaução*/)position.x, (int/*precaução*/)position.y] = piece;/*diz para a grade se está ou não com peça*/
            }
        }
    }

    public Transform gridPositionTransform(Vector2 position) /*busca a posição na grade e diz se é valida ou não*/
    {
        if (position.y > altura - 1)/*array tira um numero, então 20 vira 19. Por isso o menos. */
        {
            return null; /*Se o numero estiver acima de 19, returna nulo, ou seja, não faz nada.Ele está fora da grade */
        }

        else /*se está dentro da grade, grava a posição*/
        {
            return grid[(int)position.x, (int)position.y];
        }
    }

    public bool fullLine(int y)/*linha completa*/
    { 
        for (int x = 0; x < largura; x++)
        {
        if(grid[x,y] == null)/*verifica se tem alguma coisa na linha. Se não tiver, retorna falso*/
             {
               return false;
             }
        }
        return true; /*significa que a linha ta cheia*/
    }

    public void deleteBox(int y)
    {
        for (int x = 0; x < largura; x++)/*faz o loop pela linha novamente*/
        {
            Destroy(grid[x, y].gameObject);/*destroi caso a linha esteja cheia(igual a largura)*/

            grid[x, y] = null;
        }
    }

    public void moveDownLine(int y)
    {
        for (int x = 0; x < largura; x++)
        {
            if (grid[x, y] != null)/*verifica se tem ou não um objeto para ser jogado para baixo*/
            {
                grid[x, y - 1] = grid[x, y];/*desce uma linha quando destroi*/
                grid[x, y] = null;

                grid[x, y - 1].position += new Vector3(0, -1, 0);/*pega o ponto position dos objetos e manda para baixo*/
            }

         }
    }

    public void moveAllLines(int y)
    { 
        for (int i = y; i < altura; i++)
        {
          moveDownLine(i);
        }
    }

    public void erasesLine()/*apaga toda a linha*/
    { 
    for(int y = 0; y < altura; y++)/*loop no eixo y*/
        {
        if (fullLine(y))
           {
            deleteBox(y);
            moveAllLines(y + 1);/*verifica todas a slinhas que precisam ir para baixo.*/
                y--;/*verifica uma linha e vai para a de baixo*/
				scoreLine += 100;//dando 100 pontos quando apaga linha
				totalScore = scoreLine + scorePiece;
				difficultyPoints += 100;/*sera zerado quando chegar ou passa de 1000*/
           }
        }
    }
	public bool upGrid(Movimentacao pieceTetro)//pergunta se esta aciama da grade
	{
		for (int x = 0; x < largura; x++)
		{
			foreach(Transform box in pieceTetro.transform)//busca quadrado por quadrado centro da peça
			{
				Vector2 position = rounds(box.position);//cria um vector2 para arredondar

				if (position.y > altura -1)//se a posiçao estiver acima da grid
				{
					return true;
				}
			}
		}
		return false;
	}

	public void gameOver()
	{
		SceneManager.LoadScene ("gameOver");//chama a cena do game over no final
	}
}
