using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace Homework_week9
{
    public partial class Form1 : Form
    {
        int num1 = 0;
        int num2 = 0;
        int num3 = 0;
        int alarmMin = 0;
        SoundPlayer sp;

        bool isStarted = false; //시작한 상태인지 확인
        bool isPaused = false;  // 일시정지/진행중 토글
        //사운드 넣기

        public Form1()
        {
            InitializeComponent();
            sp = new SoundPlayer(Properties.Resources.bell);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isStarted)
            {
                if (radioButton3.Checked)
                {
                    try
                    {
                        alarmMin = int.Parse(textBox2.Text);

                        if (alarmMin <= 0)
                        {
                            textBox1.Text = "입력오류";
                            return;
                        }
                    }
                    catch
                    {
                        textBox1.Text = "입력오류";
                        return;
                    }
                    
                    num1 = alarmMin / 60;
                    num2 = alarmMin % 60;
                    sp.Stop();
                }

                timer1.Enabled = true;
                isStarted = true;
                button1.Text = "일시정지";
            }
            else if (isPaused)
            {
                timer1.Enabled = true;
                isPaused = false;
                button1.Text = "일시정지";
            }
            else
            {
                timer1.Enabled = false;
                isPaused = true;
                button1.Text = "재개";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void reset()
        {
            timer1.Enabled = false;
            isStarted = false;
            isPaused = false;
            button1.Text = "시작";
            num1 = 0; num2 = 0; num3 = 0;
            textBox1.Text = "0 : 0 : 0";
            sp.Stop();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            reset();
            timer1.Interval = 1000;
            timer1.Enabled = true;
            textBox2.Visible = false;
            label1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            reset();
            timer1.Interval = 16;
            textBox2.Visible = false;
            label1.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            reset();
            textBox2.Text = "";
            timer1.Interval = 1000;
            textBox2.Visible = true;
            label1.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //시계
            if (radioButton1.Checked)
            {
                    num1 = DateTime.Now.Hour;
                    num2 = DateTime.Now.Minute;
                    num3 = DateTime.Now.Second;

                    textBox1.Text = num1 + " : " + num2 + " : " + num3;
            }
            //스탑워치 : 분-초-밀리초
            else if (radioButton2.Checked)
            {
                num3 += 2;
                if (num3 >= 100)
                {
                    num3 = 0; num2 += 1;
                }
                if (num2 >= 60)
                {
                    num2 = 0; num1 += 1;
                }

                textBox1.Text = num1 + " : " + num2 + " : " + num3;
            }
            //타이머 : 시-분-초
            else if (radioButton3.Checked)
            {
                num3 -= 1;
                if (num3 < 0)
                {
                    num2 -= 1;
                    num3 = 59;
                    if (num2 < 0 && num1 > 0)
                    {
                        num1 -= 1;
                        num2 = 59;
                    }
                    else if(num2 < 0 && num1 == 0)
                    {
                        textBox1.Text = "0 : 0 : 0";
                        reset();
                        sp.PlayLooping();
                    }
                }
                textBox1.Text = num1 + " : " + num2 + " : " + num3;
            }
            else
            {
                textBox1.Text = "Fuck You";
            }
        }
    }
}
