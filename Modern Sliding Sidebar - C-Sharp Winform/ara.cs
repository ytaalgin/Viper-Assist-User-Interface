using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Windows.Forms;
using System.Linq;

namespace Modern_Sliding_Sidebar___C_Sharp_Winform
{
    public partial class ara : Form
    {
        private HttpClient httpClient = new HttpClient();
        public ara()
        {
            InitializeComponent();
        }

        private async void btnFindHospitals_Click(object sender, EventArgs e)
        {
            string apiKey = "AIzaSyAQUpnnQ7M9n_ospMXyQ63Xt0eZq147ppw"; // Google Cloud Platform'dan aldığınız API anahtarınız
            string address = txtAddress.Text; // Kullanıcının girdiği adres

            // Adresi enlem ve boylama dönüştürme
            string geocodeUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={apiKey}";
            var geocodeResponse = await httpClient.GetAsync(geocodeUrl);
            var geocodeJson = await geocodeResponse.Content.ReadAsStringAsync();
            var geocodeData = JsonConvert.DeserializeObject<GeocodeResponse>(geocodeJson);

            if (geocodeData.status == "OK" && geocodeData.results.Any())
            {
                var location = geocodeData.results.First().geometry.location;

                // Enlem ve boylamı kullanarak en yakın hastaneleri bulma
                string placesUrl = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={location.lat},{location.lng}&radius=5000&type=hospital&key={apiKey}";
                var placesResponse = await httpClient.GetAsync(placesUrl);
                var placesJson = await placesResponse.Content.ReadAsStringAsync();
                var placesData = JsonConvert.DeserializeObject<PlacesApiResponse>(placesJson);

                if (placesData.status == "OK" && placesData.results.Any())
                {
                    dataGridViewHospitals.Invoke(new Action(() =>
                    {
                        dataGridViewHospitals.DataSource = placesData.results.Select(h => new
                        {
                            Hastane = h.name,
                            Adres = h.vicinity
                        }).ToList();
                    }));
                }
                else
                {
                    MessageBox.Show("Hastane bulunamadı.");
                }
            }
            else
            {
                MessageBox.Show("Geocode bilgisi alınamadı.");
            }
        }

        private void ara_Load(object sender, EventArgs e)
        {
            dataGridViewHospitals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string apiKey = "AIzaSyAQUpnnQ7M9n_ospMXyQ63Xt0eZq147ppw"; // Google Cloud Platform'dan aldığınız API anahtarınız
            string address = txtAddress.Text; // Kullanıcının girdiği adres

            // Adresi enlem ve boylama dönüştürme
            string geocodeUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={apiKey}";
            var geocodeResponse = await httpClient.GetAsync(geocodeUrl);
            var geocodeJson = await geocodeResponse.Content.ReadAsStringAsync();
            var geocodeData = JsonConvert.DeserializeObject<GeocodeResponse>(geocodeJson);

            if (geocodeData.status == "OK" && geocodeData.results.Any())
            {
                var location = geocodeData.results.First().geometry.location;

                // Enlem ve boylamı kullanarak en yakın hastaneleri bulma
                string placesUrl = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={location.lat},{location.lng}&radius=5000&type=pharmacy&key={apiKey}";
                var placesResponse = await httpClient.GetAsync(placesUrl);
                var placesJson = await placesResponse.Content.ReadAsStringAsync();
                var placesData = JsonConvert.DeserializeObject<PlacesApiResponse>(placesJson);

                if (placesData.status == "OK" && placesData.results.Any())
                {
                    dataGridViewHospitals.Invoke(new Action(() =>
                    {
                        dataGridViewHospitals.DataSource = placesData.results.Select(h => new
                        {
                            Eczane = h.name,
                            Adres = h.vicinity
                        }).ToList();
                    }));
                }
                else
                {
                    MessageBox.Show("Eczane bulunamadı.");
                }
            }
            else
            {
                MessageBox.Show("Geocode bilgisi alınamadı.");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string apiKey = "AIzaSyAQUpnnQ7M9n_ospMXyQ63Xt0eZq147ppw"; // Google Cloud Platform'dan aldığınız API anahtarınız
            string address = txtAddress.Text; // Kullanıcının girdiği adres

            // Adresi enlem ve boylama dönüştürme
            string geocodeUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={apiKey}";
            var geocodeResponse = await httpClient.GetAsync(geocodeUrl);
            var geocodeJson = await geocodeResponse.Content.ReadAsStringAsync();
            var geocodeData = JsonConvert.DeserializeObject<GeocodeResponse>(geocodeJson);

            if (geocodeData.status == "OK" && geocodeData.results.Any())
            {
                var location = geocodeData.results.First().geometry.location;

                // Enlem ve boylamı kullanarak en yakın hastaneleri bulma
                string placesUrl = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={location.lat},{location.lng}&radius=5000&type=pharmacy&key={apiKey}&opennow=true";
                var placesResponse = await httpClient.GetAsync(placesUrl);
                var placesJson = await placesResponse.Content.ReadAsStringAsync();
                var placesData = JsonConvert.DeserializeObject<PlacesApiResponse>(placesJson);

                if (placesData.status == "OK" && placesData.results.Any())
                {
                    dataGridViewHospitals.Invoke(new Action(() =>
                    {
                        dataGridViewHospitals.DataSource = placesData.results.Select(h => new
                        {
                            Eczane = h.name,
                            Adres = h.vicinity
                        }).ToList();
                    }));
                }
                else
                {
                    MessageBox.Show("Nöbetçi eczane bulunamadı.");
                }
            }
            else
            {
                MessageBox.Show("Geocode bilgisi alınamadı.");
            }
        }
    }

    // Geocoding API yanıtı için sınıflar
    public class GeocodeResponse
    {
        public string status { get; set; }
        public GeocodeResult[] results { get; set; }
    }

    public class GeocodeResult
    {
        public Geometry geometry { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    // Places API yanıtı için sınıflar
    public class PlacesApiResponse
    {
        public string status { get; set; }
        public PlaceResult[] results { get; set; }
    }

    public class PlaceResult
    {
        public string name { get; set; }
        public string vicinity { get; set; }
    }
}

