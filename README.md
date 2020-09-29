# Brum Fancy Notifications

API to add basic but fancy notifications to your WinForms application. 

## Installation
Add [this package](https://www.nuget.org/packages/SieBRUM.FancyAlert) to your WinForms project.

## Usage
### Example form
```cs
using BrumCustomAlerts;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExampleNamespace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BrumAlertFactory.OpenAlert("This is a success notification!", AlertType.Success, 5000, AlertLocation.TopLeft);
        }
    }
}
```

### Possible function calls
```cs
// Default success notification
BrumAlertFactory.OpenAlert("This is a success notification!", AlertType.Success);

// Default error notification with custom life time and pre-set location
BrumAlertFactory.OpenAlert("This is an error notification!", AlertType.Error, 5000, AlertLocation.BottomRight);

// Info box with custom colors, but using the default info icon
BrumAlertFactory.OpenAlert("This is an info notification!",Color.Yellow, Color.Black, AlertType.Info, 5000, AlertLocation.TopLeft);

// Completely custom notification with default life time
BrumAlertFactory.OpenAlert("My custom notification!", Color.WhiteSmoke, Color.Red, myImage, alertLocation: AlertLocation.BottomRight);
```

# Demo
![](https://media.giphy.com/media/ctaDg4t34ehZpUp5lp/giphy.gif)
