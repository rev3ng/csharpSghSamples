using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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

            if (Uri.IsWellFormedUriString(faceEndpoint, UriKind.Absolute))
            {
                faceClient.Endpoint = faceEndpoint;
            }
        }

        private async void LoadImageClick(object sender, RoutedEventArgs e)
        {
            //Create new file picker for image loading
            var picker = new FileOpenPicker();

            //add jpg file filter
            picker.FileTypeFilter.Add(".jpg");

            //set start location
            picker.SuggestedStartLocation = PickerLocationId.Desktop;

            //open file picker and load file
            StorageFile file = await picker.PickSingleFileAsync();

            if (file == null)
            {
                return;
            }

            BitmapImage bitmap = new BitmapImage(new Uri(file.Path));

            faceList = await UploadAndDetecFaces(file);

        }

        private async Task<IList<DetectedFace>> UploadAndDetecFaces(StorageFile file)
        {
            // The list of Face attributes to return.
            IList<FaceAttributeType> faceAttributes = new List<FaceAttributeType>();

            var values = Enum.GetValues(typeof(FaceAttributeType)).Cast<FaceAttributeType>();
            foreach (FaceAttributeType x in values)
            {
                faceAttributes.Add(x);
            }

            // Call the Face API.
            try
            {
                using (Stream imageFileStream = await file.OpenStreamForReadAsync())
                {
                    // The second argument specifies to return the faceId, while
                    // the third argument specifies not to return face landmarks.
                    IList<DetectedFace> faceList =
                        await faceClient.Face.DetectWithStreamAsync(
                            imageFileStream, true, false, faceAttributes);
                    return faceList;
                }
            }
            // Catch and display Face API errors.
            catch (APIErrorException f)
            {
                //MessageBox.Show(f.Message);
                return new List<DetectedFace>();
            }
            // Catch and display all other errors.
            catch (Exception e)
            {
                //MessageBox.Show(e.Message, "Error");
                return new List<DetectedFace>();
            }
        }

    }
}
