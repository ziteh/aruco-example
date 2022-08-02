using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Aruco;

namespace Aruco_test
{
    public partial class Form1 : Form
    {
        private readonly VideoCapture _videoCapture = new VideoCapture();

        private readonly Dictionary _dictionary = new Dictionary(Dictionary.PredefinedDictionaryName.Dict4X4_50);

        private readonly MCvScalar _markerBorderColor = new MCvScalar(20, 255, 20);

        public Form1()
        {
            InitializeComponent();
            Application.Idle += ProcessFrame;
        }

        private void ProcessFrame(object sender, EventArgs args)
        {
            var img = _videoCapture.QueryFrame();

            try
            {
                var corners = new VectorOfVectorOfPointF();
                var ids = new VectorOfInt();
                var rejectedPoints = new VectorOfVectorOfPointF();

                ArucoInvoke.DetectMarkers(img,
                                          _dictionary,
                                          corners,
                                          ids,
                                          DetectorParameters.GetDefault(),
                                          rejectedPoints);

                ArucoInvoke.DrawDetectedMarkers(img, corners, ids, _markerBorderColor);
            }
            catch
            { /* Do nothing. */ }

            pictureBox1.Image = img.ToBitmap();
        }
    }
}