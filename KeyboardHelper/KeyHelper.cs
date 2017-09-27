using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardHelper {
  public partial class KeyHelper : Form {
    SolidBrush brush;

    int ellipseX = 0;
    int ellipseY = 0;
    int ellipseWidth = 100;
    int ellipseHeight = 100;
    //int keyRow = 0;
    //int keyCol = 0;
    int whichKey = 0;
    string whichKeyStr = String.Empty;

    string[] keyNames = {
      "Escape",
      "D1", "D2", "D3", "D4", "D5",
      "Tab",
      "Q", "W", "E", "R", "T",
      "Backspace",
      "A", "S", "D", "F", "G",
      "LShiftKey",
      "Z", "X", "C", "V", "B",
      "D6", "D7", "D8", "D9", "D0", "OemMinus", // -
      "Y", "U", "I", "O", "P", "\\",
      "H", "J", "K", "L", "Oem1", "Oem7", // ; ,
      "N", "M",
      "Oemcomma", // ,
      "OemPeriod", // .
      "OemQuestion", // /
      "RShiftKey"
    };

    Tuple<int, int>[] keys = {

      // ================================ LEFT HAND ==================
      // Row 1 ESC, 1 2 3 4 5 TO 3
      //       = 6 7 8 9 0 -
      new Tuple<int, int>(30, 50), // ESC
        new Tuple<int, int>(100, 50), //1
        new Tuple<int, int>(150, 40),
        new Tuple<int, int>(200, 30),
        new Tuple<int, int>(250, 40),
        new Tuple<int, int>(300, 50), // 5
        //new Tuple<int, int>(350, 50), // TO 3

        // Row 2
        new Tuple<int, int>(30, 100), // Tab
        new Tuple<int, int>(100, 100), // Q
        new Tuple<int, int>(150, 90), // W
        new Tuple<int, int>(200, 80), // E
        new Tuple<int, int>(250, 90), // R
        new Tuple<int, int>(300, 100), // T
        //new Tuple<int, int>(350, 100), // [] 1
        
        //Row 3
        new Tuple<int, int>(30, 150), // Backspace
        new Tuple<int, int>(100, 150), // A
        new Tuple<int, int>(150, 140), // S
        new Tuple<int, int>(200, 130), // D
        new Tuple<int, int>(250, 140), // F
        new Tuple<int, int>(300, 150), // G

        //Row 4
        new Tuple<int, int>(30, 200), // L-Shift
        new Tuple<int, int>(100, 200), // Z
        new Tuple<int, int>(150, 190), // X
        new Tuple<int, int>(200, 180), // C
        new Tuple<int, int>(250, 190), // V
        new Tuple<int, int>(300, 200), // B

        // ========================== RIGHT HAND ====================

        new Tuple<int, int>(700, 40), // 6
        new Tuple<int, int>(750, 30), // 7
        new Tuple<int, int>(800, 20), // 8
        new Tuple<int, int>(850, 30), // 9
        new Tuple<int, int>(900, 40), // 0
        new Tuple<int, int>(950, 40), // -

        new Tuple<int, int>(700, 90), // Y
        new Tuple<int, int>(750, 80), // U
        new Tuple<int, int>(800, 70), // I
        new Tuple<int, int>(850, 80), // O
        new Tuple<int, int>(900, 90), // P
        new Tuple<int, int>(950, 90), // \

        new Tuple<int, int>(700, 140), // H
        new Tuple<int, int>(750, 130), // J
        new Tuple<int, int>(800, 120), // K
        new Tuple<int, int>(850, 130), // L
        new Tuple<int, int>(900, 140), // ;
        new Tuple<int, int>(950, 140), // '

        new Tuple<int, int>(700, 190), // N
        new Tuple<int, int>(750, 180), // M
        new Tuple<int, int>(800, 170), // ,
        new Tuple<int, int>(850, 180), // .
        new Tuple<int, int>(900, 190), // /
        new Tuple<int, int>(950, 190), // R-Shift

    };

    public KeyHelper() {
      InitializeComponent();
      InterceptKeys.KeyPressIntercepted += InterceptKeys_KeyPressIntercepted;
      brush = new SolidBrush(Color.Red);
    }

    private void InterceptKeys_KeyPressIntercepted(object sender, KeyPressInterceptedArgs e) {
      txtDebug.Invoke(new Action(() => {
        txtDebug.Text = "Pressed " + (Keys)e.KeyCode + " number " + e.KeyCode + "\r\n" + txtDebug.Text;
        whichKeyStr = ((Keys)e.KeyCode).ToString();
        pictureBox1.Invalidate();
      }));
    }

    public void DrawCircle() {
      this.Invoke(new Action(() => {
        pictureBox1.Invalidate();
      }));
    }

    private void button1_Click(object sender, EventArgs e) {
      this.DrawCircle();
    }

    private void pictureBox1_Paint(object sender, PaintEventArgs e) {
      //graphics.Clear(this.BackColor);
      var graphics = e.Graphics;
      var whichKeyObj = keyNames.Select((k, i) => new { index = i, key = k }).Where(ki => ki.key == whichKeyStr).FirstOrDefault();
      if(whichKeyObj != null) {
        graphics.FillEllipse(brush, new Rectangle(keys[whichKeyObj.index].Item1, keys[whichKeyObj.index].Item2, 50, 50));
      } else {
        txtDebug.Text = "Don't know what key \r\n" + txtDebug.Text;
      }
    }
  }
}
