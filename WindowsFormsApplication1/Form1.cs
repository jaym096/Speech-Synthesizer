using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //object for speech sysntheziser
        SpeechSynthesizer voice;
        int flag = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.ShowAlways = true;
            tt.SetToolTip(this.button_play, "play");
            tt.SetToolTip(this.button_pause, "pause");
            tt.SetToolTip(this.button_save, "save");
            tt.SetToolTip(this.button_opentext, "open text file");
            tt.SetToolTip(this.button_cancel, "cancel");
            tt.SetToolTip(this.button_playfile, "play an audio");

            richTextBox1.Text = "Enter your text here to convert it into speech";
            voice = new SpeechSynthesizer();
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                comboBox3.Items.Add(lang.Culture.DisplayName);
            }
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button_pause_Click_1(object sender, EventArgs e)
        { 
            voice.Pause(); //stops speaking
            flag = 1;
        }

        private void voice_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            
        }

        private void button_play_Click(object sender, EventArgs e)
        {
            //play button
            
            if (flag == 1)
            {
                voice.Resume();  //resumes speaking from where it was paused
            }
            else
            {
                voice.Volume = trackBar1.Value;  //sets volume
                 try  
                {
                    if (richTextBox1.Text == "")
                    {
                        MessageBox.Show("textbox is empty, please add text", "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (comboBox1.SelectedIndex == 0) 
                        {
                            voice.SelectVoiceByHints(VoiceGender.Male);
                            voice.SpeakAsync(richTextBox1.Text);  //synchronise with text give output as speech
                            voice.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(voice_SpeakCompleted);  //SpeakCompleted event
                        }
                        else if (comboBox1.SelectedIndex == 1)
                        {
                            voice.SelectVoiceByHints(VoiceGender.Female);
                            voice.SpeakAsync(richTextBox1.Text);  //synchronise with text give output as speech
                            voice.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(voice_SpeakCompleted);  //SpeakCompleted event
                        }
                        else
                        {
                            MessageBox.Show("please select a gender", "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("there was an error", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_cancel_Click_1(object sender, EventArgs e)
        {
            //clears textbox and richTextbox
            richTextBox1.Clear();
        }

        private void button_opentext_Click_1(object sender, EventArgs e)
        {
            //to open text file from computer
            OpenFileDialog opnfile = new OpenFileDialog();

            opnfile.Filter = "Text files|*.txt|Microsoft Word Document (.docx)|*.docx";
            if (opnfile.ShowDialog() == DialogResult.OK)
            {
                string filetxt = File.ReadAllText(opnfile.FileName);
                richTextBox1.Text = filetxt;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //to show next panel 
            pictureBox2.Enabled = false;
            pictureBox2.Visible = false;
            pictureBox3.Enabled = false;
            pictureBox3.Visible = false;
            label3.Visible = false;
            //picture boxes gets invisible and disabled
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            SpeechSynthesizer voice = new SpeechSynthesizer();
            voice.Volume = trackBar1.Value;
            SaveFileDialog savefile = new SaveFileDialog();

            savefile.Filter = "wave files|*.wav";
            savefile.InitialDirectory = @"C:\Users\Jay\Documents\Visual Studio 2012\Projects\WindowsFormsApplication1\WindowsFormsApplication1\AudioFiles";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                voice.SetOutputToWaveFile(savefile.FileName);
                voice.Speak(richTextBox1.Text);
                voice.SetOutputToDefaultAudioDevice();
            }
            else
            {

            }
        }

        private void button_playfile_Click(object sender, EventArgs e)
        {
            player x = new player();
            x.Show();
        }

        private void cancel_music_Click(object sender, EventArgs e)
        {
            voice.SpeakAsyncCancelAll();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
