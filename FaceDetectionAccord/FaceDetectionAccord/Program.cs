using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Imaging.Filters;
using Accord.Vision.Detection;
using Accord.Vision.Detection.Cascades;
using AForge.Imaging;

namespace FaceDetectionAccord
{
    class Program
    {
        static void Main(string[] args)
        {
            //http://www.codeproject.com/Tips/561129/Face-Detection-with-Lines-of-Code-VB-NET

            //describing Viola Jones here : http://makematics.com/research/viola-jones/

            //choosing scaling factor : http://www.mathworks.com/help/vision/ref/vision.cascadeobjectdetector-class.html#btc108o

            string fileName = "9_r.jpg";

            var image = new Bitmap("C:/temp/FaceDetection/"+fileName);
            var cascade = new FaceHaarCascade();
            var detector = new HaarObjectDetector(cascade,30);
            detector.SearchMode = ObjectDetectorSearchMode.Average;
            detector.Suppression = 3;
            detector.MaxSize =new Size(image.Width,image.Height);
            int scalingValue = image.Width > image.Height ? image.Width : image.Height;
            detector.ScalingFactor = scalingValue / (scalingValue-0.5f);
           
            detector.ScalingMode = ObjectDetectorScalingMode.GreaterToSmaller;
            detector.UseParallelProcessing = true;
            detector.Suppression = 1;
            var sw = new Stopwatch();
            sw.Start();
 
            Rectangle[] faceObjects = detector.ProcessFrame(image);
           var p = new Pen(Color.Aqua,10);
            
            var graphicRect = Graphics.FromImage(image);
           
            foreach (var face in faceObjects)
            {
                graphicRect.DrawRectangle(p, face);
            }
            graphicRect.Dispose();
            image.Save("C:/temp/FaceDetection/Results/Average_3/"+fileName);

            sw.Stop();
        }
    }
}
