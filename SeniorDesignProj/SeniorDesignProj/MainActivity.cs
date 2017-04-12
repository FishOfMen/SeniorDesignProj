using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace SeniorDesignProj
{
    [Activity(Label = "SeniorDesignProj", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        string outputText;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button inputButton = FindViewById<Button>(Resource.Id.submitButton);

            inputButton.Click += InputButton_Click;
        }

        private void InputButton_Click(object sender, EventArgs e)
        {
            EditText inputText = FindViewById<EditText>(Resource.Id.inputText);

            if (inputText.Text != "")
            {
                outputText = inputText.Text.ToString();
                string inputAlert = "You Entered: " + outputText;
                Toast.MakeText(this, inputAlert, ToastLength.Short).Show();

                Toast.MakeText(this, "Loading text into cloud missle...", ToastLength.Long).Show();
                Toast.MakeText(this, "Launching missle in...", ToastLength.Short).Show();
                Toast.MakeText(this, "3", ToastLength.Short).Show();
                Toast.MakeText(this, "2", ToastLength.Short).Show();
                Toast.MakeText(this, "1", ToastLength.Short).Show();
                Toast.MakeText(this, "Missle launched at cloud", ToastLength.Short).Show();

                using (var client = new HttpClient())
                {
                    var url = string.Format("https://zachproject.azurewebsites.net/api/GetText?code=b3kcEw65qT1lrSlWd9p1yHopfauecaYcdfeJHCGOqakb/r1fgQBOXw==&");

                    var content = new StringContent("{name:'" + outputText + "'}", Encoding.UTF8, "application/json");

                    var result = client.PostAsync(url, content).Result.EnsureSuccessStatusCode().Content.ReadAsStringAsync().Result;

                    JsonConvert.DeserializeObject<string>(result);

                }
            }
            else
            {
                string alert = "Please enter text";
                Toast.MakeText(this, alert, ToastLength.Short).Show();
            }

            
        }
    }
}

