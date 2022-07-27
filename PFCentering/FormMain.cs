using PFCentering.Properties;
using System;
using System.Windows.Forms;

namespace PFCentering
{
    public partial class FormMain : Form
    {
        #region Private Fields

        private readonly FormPreview formPreview = new FormPreview();

        #endregion

        #region Public Methods

        public FormMain()
        {
            InitializeComponent();
            Settings settings = Settings.Default;
            formPreview.Initialize(settings.PreviewShadowColor, settings.PreviewShadowAlpha, settings.PreviewFrameColor);
            formPreview.VisibleChanged += new EventHandler(formPreview_VisibleChanged);
        }

        #endregion

        #region Private Methods

        private void formPreview_VisibleChanged(object sender, EventArgs e)
        {
            if (formPreview.IsDisposed)
            {
                return;
            }

            if (!formPreview.Visible && !Visible)
            {
                Show();
            }
        }

        #endregion

        // Designer's Methods

        private void buttonExec_Click(object sender, EventArgs e)
        {
            Hide();
            formPreview.ShowPreview();
        }

        private void buttonOneMore_Click(object sender, EventArgs e)
        {
            formPreview.DoCentering();
        }
    }
}
