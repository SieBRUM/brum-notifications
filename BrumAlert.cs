using BrumCustomAlerts.Properties;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BrumCustomAlerts
{
    internal partial class BrumAlert : Form
    {
        int interval = 0;
        AlertLocation alertLocation;

        #region Adding TOPMOST without stealing focus
        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        private const int WS_EX_TOPMOST = 0x00000008;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= WS_EX_TOPMOST;
                createParams.ExStyle |= 0x80;
                return createParams;
            }
        }
        #endregion

        #region constructors
        public BrumAlert(string message, AlertType type, int delay, AlertLocation alertLocation)
        {
            InitializeComponent();
            lblMessage.Text = message;
            this.alertLocation = alertLocation;

            switch (type)
            {
                case AlertType.Success:
                    pbMain.Image = Resources.success_white;
                    BackColor = Color.LimeGreen;
                    ForeColor = Color.White;
                    lblMessage.ForeColor = Color.White;
                    break;
                case AlertType.Info:
                    pbMain.Image = Resources.info_white;
                    BackColor = Color.DeepSkyBlue;
                    ForeColor = Color.White;
                    lblMessage.ForeColor = Color.White;
                    break;
                case AlertType.Warning:
                    pbMain.Image = Resources.warning_white;
                    BackColor = Color.Orange;
                    ForeColor = Color.White;
                    lblMessage.ForeColor = Color.White;
                    break;
                case AlertType.Error:
                    pbMain.Image = Resources.error_white;
                    BackColor = Color.Crimson;
                    ForeColor = Color.WhiteSmoke;
                    lblMessage.ForeColor = Color.White;
                    break;
                default:
                    throw new Exception("Unkown setting!");
            }

            Show();
            timerClose.Interval = delay;
            timerClose.Start();
        }

        public BrumAlert(string message, Color foregroundColor, Color backgroundColor, AlertType type, int delay, AlertLocation alertLocation)
        {
            InitializeComponent();
            lblMessage.Text = message;
            this.alertLocation = alertLocation;

            switch (type)
            {
                case AlertType.Success:
                    pbMain.Image = Resources.success_white;
                    break;
                case AlertType.Info:
                    pbMain.Image = Resources.info_white;
                    break;
                case AlertType.Warning:
                    pbMain.Image = Resources.warning_white;
                    break;
                case AlertType.Error:
                    pbMain.Image = Resources.error_white;
                    break;
                default:
                    throw new Exception("Unkown setting!");
            }
            BackColor = backgroundColor;
            lblMessage.ForeColor = foregroundColor;

            Show();
            timerClose.Interval = delay;
            timerClose.Start();
        }

        public BrumAlert(string message, Color foregroundColor, Color backgroundColor, Bitmap image, int delay, AlertLocation alertLocation)
        {
            InitializeComponent();
            lblMessage.Text = message;
            this.alertLocation = alertLocation;

            pbMain.Image = image;
            BackColor = backgroundColor;
            lblMessage.ForeColor = foregroundColor;

            Show();
            timerClose.Interval = delay;
            timerClose.Start();
        }

        #endregion
        private void alert_Load(object sender, EventArgs e)
        {
            switch (alertLocation)
            {
                case AlertLocation.TopRight:
                    Top = 0 - this.Height - 50;
                    Left = Screen.PrimaryScreen.Bounds.Width - this.Width - 50;
                    break;
                case AlertLocation.TopMiddle:
                    Top = 0 - this.Height - 50;
                    Left = (Screen.PrimaryScreen.Bounds.Width / 2) - (Width / 2);
                    break;
                case AlertLocation.TopLeft:
                    Top = 0 - this.Height - 50;
                    Left = 50;
                    break;
                case AlertLocation.BottomRight:
                    Top = Screen.PrimaryScreen.Bounds.Height;
                    Left = Screen.PrimaryScreen.Bounds.Width - this.Width - 50;
                    break;
                case AlertLocation.BottomMiddle:
                    Top = Screen.PrimaryScreen.Bounds.Height;
                    Left = (Screen.PrimaryScreen.Bounds.Width / 2) - (Width / 2);
                    break;
                case AlertLocation.BottomLeft:
                    Top = Screen.PrimaryScreen.Bounds.Height;
                    Left = 50;
                    break;
                default:
                    break;
            }
        }

        private void OnClose(object sender, EventArgs e)
        {
            timerClosing.Start();
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            timerClosing.Start();
        }

        private void timerShow_Tick(object sender, EventArgs e)
        {
            if (alertLocation == AlertLocation.TopLeft || alertLocation == AlertLocation.TopRight || alertLocation == AlertLocation.TopMiddle)
            {
                if (Top < this.Height - 30)
                {
                    Top += interval;
                    interval += 1;
                }
                else
                {
                    timerShow.Stop();
                }
            }
            else
            {
                if (Top > Screen.PrimaryScreen.Bounds.Height - this.Height - 55)
                {
                    Top -= interval;
                    interval += 2;
                }
                else
                {
                    timerShow.Stop();
                }
            }
        }

        private void timerClosing_Tick(object sender, EventArgs e)
        {
            if (Opacity > 0)
            {
                Opacity -= 0.1;
            }
            else
            {
                Close();
            }
        }
    }

    public class BrumAlertFactory
    {
        /// <summary>
        /// Default alert factory. 
        /// </summary>
        /// <param name="message">Alert message text</param>
        /// <param name="type">Alert type</param>
        /// <param name="delay">Delay in miliseconds. Default 2000 miliseconds</param>
        /// <param name="alertLocation">Alert location. Default BottomRight</param>
        public static void OpenAlert(string message, AlertType type, int delay = 2000, AlertLocation alertLocation = AlertLocation.BottomRight)
        {
            BrumAlert alert = new BrumAlert(message, type, delay, alertLocation);
        }
        /// <summary>
        /// Open custom made alert with custom image
        /// </summary>
        /// <param name="message">Alert message text</param>
        /// <param name="foregroundColor">Text color</param>
        /// <param name="backgroundColor">Background color</param>
        /// <param name="image">Alert icon</param>
        /// <param name="delay">Delay in miliseconds. Default 2000 miliseconds</param>
        /// <param name="alertLocation">Alert location. Default BottomRight</param>
        public static void OpenAlert(string message, Color foregroundColor, Color backgroundColor, Bitmap image, int delay = 2000, AlertLocation alertLocation = AlertLocation.BottomRight)
        {
            BrumAlert alert = new BrumAlert(message, foregroundColor, backgroundColor, image, delay, alertLocation);
        }

        /// <summary>
        /// Open custom alert with pre-set icon
        /// </summary>
        /// <param name="message">Alert message text</param>
        /// <param name="foregroundColor">Text color</param>
        /// <param name="backgroundColor">Background color</param>
        /// <param name="type">Alert type for pre-set image</param>
        /// <param name="delay">Delay in miliseconds. Default 2000 miliseconds</param>
        /// <param name="alertLocation"Alert location. Default BottomRight></param>
        public static void OpenAlert(string message, Color foregroundColor, Color backgroundColor, AlertType type, int delay = 2000, AlertLocation alertLocation = AlertLocation.BottomRight)
        {
            BrumAlert alert = new BrumAlert(message, foregroundColor, backgroundColor, type, delay, alertLocation);
        }
    }


    public enum AlertType
    {
        Success,
        Info,
        Warning,
        Error
    }

    public enum AlertLocation
    {
        TopRight,
        TopMiddle,
        TopLeft,
        BottomRight,
        BottomMiddle,
        BottomLeft
    }
}
