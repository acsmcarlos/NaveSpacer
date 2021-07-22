using NaveSpacer.GameLogic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace NaveSpacer.UI
{
    public partial class FormPrincipal : Form {

        //bitmap onde as coisas serão desenhadas onde eu desenho tudo para
        //depois jogar na tela de formulário, ele vai guardar uma imagem para minha tela
        Bitmap screenBuffer { get; set; }

        /*vamos criar uma Referência para o background*/
        Background background { get; set; }

        public FormPrincipal()
        {
            InitializeComponent();
            this.screenBuffer = new Bitmap(Media);
            this.background = new Background();            /**/
        }
    }
}
