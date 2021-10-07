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

namespace WindowsFormsAppMultiThread
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public delegate void GoProgressBarHandler(ProgressBar progressBar);
        public event GoProgressBarHandler GoProgressBarHandlerEvent;

        private void button1_Click(object sender, EventArgs e)
        {
            //////Action act = () => TestWork();

            TestWork();
        }


        private void TestWork()
        {
            Parallel.Invoke(
                ()=> GoProgressBar(progressBar1),
                () => GoProgressBar(progressBar2),
                () => GoProgressBar(progressBar3),
                () => GoProgressBar(progressBar4)
                );
        }

        private void GoProgressBar(ProgressBar progressBar)
        {

            //GoProgressBarHandlerEvent += new GoProgressBarHandler(OnFormGoProgressBarHandler);





            //GoProgressBarHandlerEvent(progressBar);

            Action<object> action = OnFormGoProgressBarHandler;

            //Thread thread = new Thread(new ParameterizedThreadStart(action));

            //thread.Start(progressBar);

            //action(progressBar);

            action.BeginInvoke(progressBar,null, null);

        }

        private static void OnFormGoProgressBarHandler(object progressBar)
        {

            ProgressBar _progressBar = (ProgressBar)progressBar;

            _progressBar.Invoke(new MethodInvoker(() => _progressBar.Value = 0));
            _progressBar.Invoke(new MethodInvoker(() => _progressBar.Minimum = 0));
            _progressBar.Invoke(new MethodInvoker(() => _progressBar.Maximum = 1000));

            _progressBar.Invoke(new MethodInvoker(() => _progressBar.Step = 1));

            //progressBar.Value = 0;
            //progressBar.Minimum = 0;
            //progressBar.Maximum = 100;

            //progressBar.Step = 1;

            while (_progressBar.Value < _progressBar.Maximum - 1)
            {
                _progressBar.Invoke(new MethodInvoker(() => ++_progressBar.Value));
                Thread.Sleep(1);
            }

            

            _progressBar.Invoke(new MethodInvoker(() => _progressBar.Value = _progressBar.Maximum));
            
        }
    }
}
