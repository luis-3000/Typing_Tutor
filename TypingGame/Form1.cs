using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypingGame
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        Stats stats = new Stats();

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Adding a random key to the ListBox
            listBox1.Items.Add((Keys)random.Next(65, 90));
            if (listBox1.Items.Count > 7)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Game over");
                timer1.Stop();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // If the user pressed a key that's in the ListBox, remove it
            // and then make the game a little fater (to force the user to typer a little faster)
            if (listBox1.Items.Contains(e.KeyCode))
            {
                listBox1.Items.Remove(e.KeyCode);
                listBox1.Refresh();

                // Increase the difficulty as the player get more keys right
                if (timer1.Interval > 400)
                    timer1.Interval -= 10;
                if (timer1.Interval > 250)
                    timer1.Interval -= 7;
                if (timer1.Interval > 100)
                    timer1.Interval -= 2;

                difficultyProgressBar.Value = 800 - timer1.Interval;

                // Tue user Pressed a correct key, so update the Stats object by calling its Update() method  witht eh argument true
                stats.Update(true);
            }
            else
            {
                // the user pressed an incorrect key, so update the Stats object by calling its Update() method with the argument false
                stats.Update(false);
            }

            // Update the labels on the StatusStrip
            correctLabel.Text = "Correct: " + stats.Correct;
            missedLabel.Text = "Missed: " + stats.Missed;
            totalLabel.Text = "Total: " + stats.Total;
            accuracyLabel.Text = "Accuracy: " + stats.Accuracy + "%";
        }
    }
}
