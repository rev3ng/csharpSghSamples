using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FaceRecognitionSGHSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        //Cognitive services subscription key
        private const string subscriptionKey = "01ca1863c57541848912fd9daa8daf22";

        //Cognitive services endpoint
        private const string faceEndpoint =
            "https://westeurope.api.cognitive.microsoft.com";

        //FaceApi client
        private readonly IFaceClient faceClient = new FaceClient(
            new ApiKeyServiceClientCredentials(subscriptionKey),
            new System.Net.Http.DelegatingHandler[] { });
        
        // The list of detected faces.
        private IList<DetectedFace> faceList;
        
        // The list of descriptions for the detected faces.
        private string[] faceDescriptions;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void LoadImageClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
