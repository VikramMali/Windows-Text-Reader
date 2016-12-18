using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Windows.Forms;

namespace Windows_Text_Reader
{
    public partial class Form1 : Form
    {
        //SpeechSynthesizer Class Provides access to the functionality of an installed a speech synthesis engine.   
        SpeechSynthesizer speechSynthesizerObj;

        KeyboardHook hook = new KeyboardHook();

        public Form1()
        {
            InitializeComponent();
            // register the event that is fired after the key press.
            hook.KeyPressed +=
                new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            // register the control + alt + F12 combination as hot key.
            hook.RegisterHotKey(Windows_Text_Reader.ModifierKeys.Control | Windows_Text_Reader.ModifierKeys.Alt,
                Keys.R);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            speechSynthesizerObj = new SpeechSynthesizer();
            foreach (InstalledVoice iv in speechSynthesizerObj.GetInstalledVoices())
            {
                comboBox1.Items.Add(iv.VoiceInfo.Name);
            }
            comboBox1.SelectedIndex = 0;
            btn_Resume.Enabled = false;
            btn_Pause.Enabled = false;
            btn_Stop.Enabled = false;
        }

        private void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            //Keyboard.SimulateKeyStroke('c', ctrl: true);
            //SendKeys.Send("^c");
            //SendCtrlC(GetForegroundWindow());
            richTextBox1.Text = Clipboard.GetText();
            TextSelectionReader txreader = new TextSelectionReader();
            richTextBox1.Text = txreader.TryGetSelectedTextFromActiveControl();
            //richTextBox1.Text = GetTextFromFocusedControl();
            //Disposes the SpeechSynthesizer object   
            speechSynthesizerObj.Dispose();
            if (richTextBox1.Text != "")
            {
                speechSynthesizerObj = new SpeechSynthesizer();
                speechSynthesizerObj.SelectVoice(comboBox1.SelectedItem.ToString());
                //Asynchronously speaks the contents present in RichTextBox1   
                speechSynthesizerObj.SpeakAsync(richTextBox1.Text);
                btn_Pause.Enabled = true;
                btn_Stop.Enabled = true;
            }
        }

        private void btn_Speak_Click(object sender, EventArgs e)
        {
            //Disposes the SpeechSynthesizer object   
            speechSynthesizerObj.Dispose();
            if (richTextBox1.Text != "")
            {
                speechSynthesizerObj = new SpeechSynthesizer();
                speechSynthesizerObj.SelectVoice(comboBox1.SelectedItem.ToString());
                //Asynchronously speaks the contents present in RichTextBox1   
                speechSynthesizerObj.SpeakAsync(richTextBox1.Text);
                btn_Pause.Enabled = true;
                btn_Stop.Enabled = true;
            }
        }

        private void btn_Pause_Click(object sender, EventArgs e)
        {
            if (speechSynthesizerObj != null)
            {
                //Gets the current speaking state of the SpeechSynthesizer object.   
                if (speechSynthesizerObj.State == SynthesizerState.Speaking)
                {
                    //Pauses the SpeechSynthesizer object.   
                    speechSynthesizerObj.Pause();
                    btn_Resume.Enabled = true;
                    btn_Speak.Enabled = false;
                }
            }
        }

        private void btn_Resume_Click(object sender, EventArgs e)
        {
            if (speechSynthesizerObj != null)
            {
                if (speechSynthesizerObj.State == SynthesizerState.Paused)
                {
                    //Resumes the SpeechSynthesizer object after it has been paused.   
                    speechSynthesizerObj.Resume();
                    btn_Resume.Enabled = false;
                    btn_Speak.Enabled = true;
                }
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            if (speechSynthesizerObj != null)
            {
                //Disposes the SpeechSynthesizer object   
                speechSynthesizerObj.Dispose();
                btn_Speak.Enabled = true;
                btn_Resume.Enabled = false;
                btn_Pause.Enabled = false;
                btn_Stop.Enabled = false;
            }
        }
    }
}
