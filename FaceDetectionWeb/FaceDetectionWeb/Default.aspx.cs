using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Accord.Vision.Detection;
using Accord.Vision.Detection.Cascades;

namespace FaceDetectionWeb
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = "";
        }

        //public void ddlSearchMode_SelectedIndexChanged(Object sender, EventArgs e)
        //{
        //    ddlSupperession.Enabled = ddlSearchMode.Text == "1";   
        //}

        public void btnSubmit_Click(Object sender, EventArgs e)
        {
            var searchMode = ddlSearchMode.Text;
            var supperession = ddlSupperession.Text;
            var scalingMode = ddlScalingMode.Text;
            var sourceFolder = txtSource.Text;
            var targetFolder = txtTarget.Text;
            var fileType = ddlFileType.Text;
            if (! FormValidation(sourceFolder, targetFolder))
            {
                lblError.Text = "Enter source and target path";
                lblError.ForeColor = Color.Red;
                return;
            }
            var searchVal = ObjectDetectorSearchMode.Default;
            switch (searchMode)
            {
                case "1":
                    searchVal = ObjectDetectorSearchMode.Default;
                    break;
                case "2":
                    searchVal = ObjectDetectorSearchMode.Average;
                    break;
                case "3":
                    searchVal = ObjectDetectorSearchMode.NoOverlap;
                    break;
                case "4":
                    searchVal = ObjectDetectorSearchMode.Single;
                    break;
            }
            var supperessionVal = Convert.ToInt32(supperession);
            var scalingVal = scalingMode == "1"
                ? ObjectDetectorScalingMode.SmallerToGreater
                : ObjectDetectorScalingMode.GreaterToSmaller;
            int fileCount = Directory.GetFiles(sourceFolder, "*." + fileType).Length;

            var sw = new Stopwatch();
            sw.Start();
            
            foreach (string f in Directory.EnumerateFiles(sourceFolder, "*." + fileType))
            {
                string file = f.Replace("\\", "/");
                string fileName = Path.GetFileName(file);
                var image = new Bitmap(file);
                Detect(image, searchVal, supperessionVal, scalingVal, targetFolder+"/" + fileName, true);
            }

            sw.Stop();
            lblError.Text = string.Format("{0} {1} files processed in {2} seconds", fileCount, fileType,sw.ElapsedMilliseconds/1000);
            lblError.ForeColor = Color.Green;

        }
        private void  Detect(Bitmap image,ObjectDetectorSearchMode searchMode, int supperession,ObjectDetectorScalingMode scalingMode,string targetPath,bool parallelProcessing)
        {
            //http://www.codeproject.com/Tips/561129/Face-Detection-with-Lines-of-Code-VB-NET

            //describing Viola Jones here : http://makematics.com/research/viola-jones/

            //choosing scaling factor : http://www.mathworks.com/help/vision/ref/vision.cascadeobjectdetector-class.html#btc108o

            var detector = new HaarObjectDetector(new FaceHaarCascade(), 30);
            detector.SearchMode = searchMode;
            if (searchMode == ObjectDetectorSearchMode.Average)
                detector.Suppression = supperession;
            detector.MaxSize = new Size(image.Width, image.Height);
            detector.ScalingMode = scalingMode;
            detector.UseParallelProcessing = parallelProcessing;

            int scalingValue = image.Width > image.Height ? image.Width : image.Height;
            detector.ScalingFactor = scalingValue / (scalingValue - 0.5f);

            Rectangle[] faceObjects = detector.ProcessFrame(image);
            var p = new Pen(Color.Aqua, 10);

            var graphicRect = Graphics.FromImage(image);

            foreach (var face in faceObjects)
            {
                graphicRect.DrawRectangle(p, face);
            }
            graphicRect.Dispose();
            image.Save(targetPath);
        }

        private bool FormValidation(string source,string target)
        {
            if (source == "" || target == "")
                return false;
            return true;
        }

    }
}