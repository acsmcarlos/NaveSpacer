using System.Drawing;
using System;

namespace NaveSpacer.GameLogic
{
    public class Background : GameObject {

        /*Construtor dessa classe. Tem que receber o tamanho da tela(limite=size) 
         e graphics que representa a tela
        que vai automaticamente chamar o método ancestral passando bounds e screen */
        public Background(Size bounds, Graphics g) : base(bounds, g)
        {
            this.Left = 0;
            this.Top = 0;
            this.Speed = 0;
        }

        public override Bitmap GetSprite()
        {
            return Media.Background;
        }
    }
}
