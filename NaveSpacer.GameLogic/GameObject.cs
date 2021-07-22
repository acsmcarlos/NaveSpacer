using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace NaveSpacer.GameLogic
{
    public abstract class GameObject {

        #region Game Object Properties

        public Bitmap Sprite { get; set; } // dimensão da figura (animado ou estático)
        public bool Active { get; set; } // ativo ou não dentro da tela - levou tiro ou nao
        public int Speed { get; set; } // velocidade de lomoção na tela
        public int Left { get; set; } // localização da margem esquerda
        public int Top { get; set; } // distancia do topo
        public int Width { get { return this.Sprite.Width; } } // largura -obtida a partir da imagem 
        public int Heith { get { return this.Sprite.Height; } } // altura - obtida a partir da imagem
        public Size Bounds { get; set; } // limites da tela onde o obj vai se mover
        public Rectangle Rectangle { get; set; } // localização do retangulo onde está o objeto
        public Stream Sound { get; set; } // som do jogo
        public Graphics Screen { get; set; } // tela de pintura, desenhar objeto na tela
        
        private SoundPlayer soundPlayer { get; set; } // player do som do jogo
        #endregion

        #region Game Object Method

        //Método que todos os objetos devem ter: método que corresponde a 
        //Captura da imagem que esse objeto precisa pra renderizar na tela
        public abstract Bitmap GetSprite();

        //Método construtor
        public GameObject(Size bounds/*limites*/, Graphics screen/*tela*/) {
            this.Bounds = bounds; 
            this.Screen = screen;
            this.Active = true; // começa sempre ativo
            this.soundPlayer = new SoundPlayer(); //objeto do visual studio pra tocar som
            this.Sprite = this.GetSprite(); 
            this.Rectangle = new Rectangle(this.Left, this.Top, this.Width, this.Heith);
        }

        /*Loop para gerar objetos aleatórios, as entradas de teclado, etc
        Atualizar os dados na tela*/
        public virtual void UpdateObject() {
            /*Retantulo que corresponde a posição do objeto que vai ser
             renderizado na tela, colocando a imagem no retangulo*/
            this.Rectangle = new Rectangle(this.Left, this.Top, this.Width, this.Heith);
            /*Tela de pintura que vai desenhar a imagem (sprite) na tela de pintura(retangulo)*/
            this.Screen.DrawImage(this.Sprite, this.Rectangle);
        }

        //Mover objetos para a esquerda (Left)
        public virtual void MoveLeft() { 
            
            if(this.Left > 0) {/*Se a coordenada esquerda não tiver colada na esquerda, 
                                * posso locomover para a esquerda*/
                this.Left -= this.Speed;
            }
        }

        //Mover objetos para a Direita (Right)
        public virtual void MoveRight() {
            /*Ver se ele nao atingiu a margem direita*/
            if (this.Left < this.Bounds.Width - this.Width) {
                this.Left += this.Speed;
            }
        }

        //Mover objetos para Baixo (Down)
        public virtual void MoveDown() {
            /*Aumentando a margem superior do objeto movendo se para  baixo*/
            this.Top += this.Speed;
        }

        //Mover objetos para cima (Up)
        public virtual void MoveUp() {
            /*Reduzir a margem superior movendo pra cima*/
            this.Top -= this.Speed;
        }

        /*Método para verificar se meu objeto está dentro da tela(fora dos limites).
        Quando a nave inimiga descer e sair das margens da tela, ele deve ser destruído 
        para não ocupar memória do pc. */ 
        public bool IsOutOfBounds() {
            return
                /*O topo é maior que o limte da tela + altura do objeto - saiu da  tela*/
                (this.Top > this.Bounds.Height + this.Heith) ||
                /*o topo for menor que - altura do objeto */
                (this.Top < -this.Heith) ||
                /*esquerda é maior que a largura da tela + a largura do objeto*/
                (this.Left > this.Bounds.Width + this.Width) ||
                /*sai pela esquerda, é menor que largura do objeto negativo*/
                (this.Left < -this.Width);
        }

        //Implementando o PlaySound
        public void PlaySound() {
            /*Passando para o objeto SoundPlayer o som do objeto jogo*/
            soundPlayer.Stream = this.Sound;
            /*método Play() do SoundPlayer() que toca o som*/
            soundPlayer.Play();
        }

        //Verificar se meu objeto está colidindo com outro objeto.
        //*parâmetro GameObject*
        public bool IsCollidingWith(GameObject gameObject) {
            /*Se o retangulo deste objeto tem intersecção do parametro GameObject que está vindo
             aí é tocando um som de colisão com o objeto*/
            if (this.Rectangle.IntersectsWith(gameObject.Rectangle)) {
                /*aí eu toco o som de colisão*/
                this.PlaySound();
                /*aí retorna true, afirmando que colidiu*/
                return true;
            }
            /*caso contrário retorna falso*/
            else return false;
        }

        //Método para destruir objetos (remover objeto do jogo)
        public void Destroy() {
            /*Destruir um objeto significa que ele Não está mais ativo
             não será mais renderizado na tela*/
            this.Active = false;
        }

        #endregion

    }
}
