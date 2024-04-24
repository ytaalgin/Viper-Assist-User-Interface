using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms.Gauge;
using Microsoft.Office.Interop.Excel;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using Syncfusion.Pdf; //pdf kaydetmeyi sağlayan kütüphane
using Syncfusion.Pdf.Graphics;
using System.IO;
using Firebase.Database;
using Firebase.Auth;
using InTheHand.Net;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;

namespace Modern_Sliding_Sidebar___C_Sharp_Winform
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;

        private BluetoothClient bluetoothClient;
        private BluetoothDeviceInfo[] discoveredDevices;
        private BluetoothClient connectedClient; // Bağlı cihaz için

        private List<string[]> pendingLogData = new List<string[]>(); //excel verilerini tut

        private bool isLoggedIn = false;

        bool sideBar_Expand = true;
        private Guna.UI.WinForms.GunaButton selectedButton;

        private string filePath = "login_status.txt";

        private FirebaseAuthProvider authProvider; // FirebaseAuthProvider nesnesi tanımlandı
        private FirebaseAuth auth; // FirebaseAuth nesnesi tanımlandı
        private FirebaseClient firebase; // FirebaseClient nesnesi tanımlandı

        private const string FirebaseApiKey = "AIzaSyBU-ycXkqvtazIW2L6Ofc7r2GgP-TC5mhk";
        private const string FirebaseDatabaseURL = "https://viper-assist-9e5fb-default-rtdb.europe-west1.firebasedatabase.app";

        public Form1()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzAyNjgzNkAzMjM0MmUzMDJlMzBnaS90Tm5vUFpXMXFDWE8rRHp5QmZRTnY0Y3h2TW1FVVZScW1kU0s3WkpjPQ==");

            InitializeComponent();
            // DataGridView'i başlat
            InitializeDataGridView();

            InitializeBluetoothClient();

            selectedButton = Orders_Button;
            SetButtonColor(selectedButton);

            tabPage1.Text = ""; //tabcontrol1 tabpage'de üstteki buton yazılarını sil
            tabPage2.Text = "";
            tabPage3.Text = "";
            tabPage4.Text = "";
            tabPage5.Text = "";
            tabPage6.Text = "";

            tabControl1.ItemSize = new Size(1, 1); //tabcontroldeki butonları yok et

            // Check and show user profile panel
            CheckAndShowUserProfilePanel();

            dgvErrorLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvErrorLog.Columns.Add("TimeColumn", "Zaman");
            dgvErrorLog.Columns.Add("ErrorLogColumn", "Hata Mesajları");
        }

        private void LogError(string errorMessage)
        {
            dgvErrorLog.Rows.Add(DateTime.Now.ToString(), errorMessage);
        }

        private void InitializeBluetoothClient()
        {
            try
            {
                bluetoothClient = new BluetoothClient();
            }
            catch (PlatformNotSupportedException ex)
            {
                LogError("Bluetooth desteği bulunamadı veya yeterli değil. Lütfen Bluetooth'u etkinleştirin veya güncelleyin.\nHata: " + ex.Message);
            }
        }

        private void CheckAndShowUserProfilePanel()
        {
            if (File.Exists(filePath))
            {
                string loginStatus = File.ReadAllText(filePath);
                if (bool.TryParse(loginStatus, out isLoggedIn))
                {
                    if (isLoggedIn)
                    {
                        DisplayUserProfilePanel();
                        // Oturum açık olduğunda UID'yi göster
                        ShowCurrentUserUid();
                    }
                }
                else
                {
                    MessageBox.Show("Dosya içeriği beklenen formatta değil.");
                }
            }
            else
            {
                // Dosya yoksa, oturum kapalı demektir.
                isLoggedIn = false;
            }
        }

        private void InitializeDataGridView()
        {
            // DataGridView'e iki sütun ekle
            dataGridView2.Columns.Add("DateTime", "Tarih");
            dataGridView2.Columns.Add("Status", "Durum");

            // DataGridView'in düzenini ayarla
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // DataGridView'e iki sütun ekle
            dataGridView3.Columns.Add("DateTime", "Tarih");
            dataGridView3.Columns.Add("Status", "Durum");

            // DataGridView'in düzenini ayarla
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // DataGridView'e iki sütun ekle
            dataGridView4.Columns.Add("DateTime", "Tarih");
            dataGridView4.Columns.Add("Status", "Durum");

            // DataGridView'in düzenini ayarla
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // DataGridView'e iki sütun ekle
            dataGridView5.Columns.Add("DateTime", "Tarih");
            dataGridView5.Columns.Add("Status", "Durum");

            // DataGridView'in düzenini ayarla
            dataGridView5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView5.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            // Güncelle butonuna tıklandığında DataGridView'e veri ekleyin
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string status = "Başarıyla kaydedildi!";

            // DataGridView'e yeni satır ekle
            dataGridView2.Rows.Insert(0, dateTime, status);
            dataGridView3.Rows.Insert(0, dateTime, status);
            dataGridView4.Rows.Insert(0, dateTime, status);
            dataGridView5.Rows.Insert(0, dateTime, status);

            // En üstteki satıra kaydır
            dataGridView2.FirstDisplayedScrollingRowIndex = 0;
            dataGridView3.FirstDisplayedScrollingRowIndex = 0;
            dataGridView4.FirstDisplayedScrollingRowIndex = 0;
            dataGridView5.FirstDisplayedScrollingRowIndex = 0;

            // Excel'e veriyi eklemek için listeye ekle
            pendingLogData.Add(new string[] { dateTime, status, "Günlük" });
            pendingLogData.Add(new string[] { dateTime, status, "Haftalık" });
            pendingLogData.Add(new string[] { dateTime, status, "Aylık" });
            pendingLogData.Add(new string[] { dateTime, status, "Yıllık" });

            // Excel'e veriyi işle
            ProcessPendingLogData();
        }

        private void ProcessPendingLogData()
        {
            try
            {
                // Excel dosyasının adını belirle
                string excelFileName = "dusmealgilamalog.xlsx";

                // Excel dosyasının yolu
                string excelFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), excelFileName);

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Workbook excelWorkbook;

                if (File.Exists(excelFilePath))
                {
                    // Dosya zaten varsa, var olan dosyayı aç
                    excelWorkbook = excelApp.Workbooks.Open(excelFilePath);
                }
                else
                {
                    // Dosya yoksa, yeni bir dosya oluştur
                    excelWorkbook = excelApp.Workbooks.Add();
                    InitializeExcelHeader((Worksheet)excelWorkbook); // Başlık eklemek için ayrı bir metod kullanıldı
                    excelWorkbook.SaveAs(excelFilePath);
                }

                foreach (var logData in pendingLogData)
                {
                    string dateTime = logData[0];
                    string status = logData[1];
                    string dataGridViewName = logData[2];

                    // Sayfa adını kontrol et
                    if (excelWorkbook.Sheets.Cast<Worksheet>().Any(sheet => sheet.Name == dataGridViewName))
                    {
                        Worksheet excelWorksheet = excelWorkbook.Sheets[dataGridViewName];

                        // Excel'e veriyi eklemek için
                        int rowIndex = excelWorksheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell).Row + 1;

                        // Hücrelere değerleri ekleyin
                        excelWorksheet.Cells[rowIndex, 1] = dateTime;
                        excelWorksheet.Cells[rowIndex, 2] = status;
                    }
                    else
                    {
                        // Sayfa yoksa, yeni bir sayfa oluştur
                        Worksheet newWorksheet = (Worksheet)excelWorkbook.Sheets.Add();
                        newWorksheet.Name = dataGridViewName;
                        InitializeExcelHeader(newWorksheet); // Başlık eklemek için ayrı bir metod kullanıldı

                        // Excel'e veriyi eklemek için
                        int rowIndex = newWorksheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell).Row + 1;

                        // Hücrelere değerleri ekleyin
                        newWorksheet.Cells[rowIndex, 1] = dateTime;
                        newWorksheet.Cells[rowIndex, 2] = status;
                    }
                }

                // Excel dosyasını kaydet
                excelWorkbook.Save();
                excelApp.Quit();

                // İşlenen verileri temizle
                pendingLogData.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Excel'e kaydedilirken bilinmeyen bir hata oluştu. Hata Detayı: {ex.Message}");
                return;
            }
        }

        // InitializeExcelHeader metodu - Excel'e başlık ekle
        private void InitializeExcelHeader(Worksheet excelWorksheet)
        {
            // Başlık ekleme işlemleri burada gerçekleştirilir
            excelWorksheet.Cells[1, 1] = "Tarih";
            excelWorksheet.Cells[1, 2] = "Durum";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowCurrentUserUid();
        }

        private void gunaPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Close_Button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Timer_Sidebar_Menu_Tick(object sender, EventArgs e)
        {
            if (sideBar_Expand)
            {
                SideBar.Width -= 10;
                if (SideBar.Width == SideBar.MinimumSize.Width)
                {
                    sideBar_Expand = false;
                    Timer_Sidebar_Menu.Stop();
                }
            }
            else
            {
                SideBar.Width += 10;
                if (SideBar.Width == SideBar.MaximumSize.Width)
                {
                    sideBar_Expand = true;
                    Timer_Sidebar_Menu.Stop();
                }
            }
        }

        private void Menu_Button_Click(object sender, EventArgs e)
        {
            Timer_Sidebar_Menu.Start();
        }

        private void SetButtonColor(Guna.UI.WinForms.GunaButton button)
        {
            // Seçili butonun rengini değiştir
            button.BackColor = Color.MidnightBlue;

            // Daha önce seçilen butonun rengini eski haline getir
            if (selectedButton != null && selectedButton != button)
            {
                selectedButton.BackColor = Color.Transparent; // Varsayılan arka plan rengi
            }

            // Yeni seçili butonu sakla
            selectedButton = button;
        }

        private void ChangeTabAndColor(Guna.UI.WinForms.GunaButton button, TabPage tabPage)
        {
            // Tab Page değişikliği
            tabControl1.SelectedTab = tabPage;

            // Buton rengi güncelleme
            SetButtonColor(button);
        }

        private void Home_Button_Click(object sender, EventArgs e)
        {
            ChangeTabAndColor(Home_Button, tabPage2);
        }

        private void Orders_Button_Click(object sender, EventArgs e)
        {
            ChangeTabAndColor(Orders_Button, tabPage1);
        }

        private void Customers_Button_Click(object sender, EventArgs e)
        {
            ChangeTabAndColor(Customers_Button, tabPage3);
        }

        private void Statistics_Button_Click(object sender, EventArgs e)
        {
            ChangeTabAndColor(Statistics_Button, tabPage4);
        }

        private void About_Button_Click(object sender, EventArgs e)
        {
            infoForm infoForm = new infoForm();
            infoForm.Show();
        }

        private void OpenWebsite(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Web sitesi açılırken bir hata oluştu: " + ex.Message);
            }
        }

        private void Help_Button_Click(object sender, EventArgs e)
        {
            OpenWebsite("https://www.cyberanalog.com");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hastaekle hastaekle = new hastaekle();
            hastaekle.Show();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            // Raporun masaüstüne kaydedileceği PDF dosyasının adı ve yolu
            string masaustuYolu = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string pdfDosyaYolu = Path.Combine(masaustuYolu, "DusmeAnalizRaporu.pdf");

            // PDF dokümanı oluştur
            using (PdfDocument document = new PdfDocument())
            {
                // Sayfa ekleyin
                PdfPage page = document.Pages.Add();

                // PdfGraphics nesnesi alın
                PdfGraphics graphics = page.Graphics;

                // Rapor içeriğini eklemek için özel bir fonksiyonu çağırın
                RaporuOlustur(graphics);

                // PDF dosyasını masaüstüne kaydet
                document.Save(pdfDosyaYolu);

                MessageBox.Show("PDF raporu başarıyla masaüstüne kaydedildi: " + pdfDosyaYolu, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RaporuOlustur(PdfGraphics graphics)
        {
            MemoryStream imageStream = new MemoryStream();
            chart2.SaveImage(imageStream, ChartImageFormat.Png);
            Image chartImage = Image.FromStream(imageStream);

            // Resmi PDF'e ekleyin
            PdfImage pdfImage = PdfImage.FromImage(chartImage);
            graphics.DrawImage(pdfImage, new PointF(10, 10));

            MemoryStream imageStream2 = new MemoryStream();
            chart3.SaveImage(imageStream2, ChartImageFormat.Png);
            Image chartImage2 = Image.FromStream(imageStream2);

            // Resmi PDF'e ekleyin
            PdfImage pdfImage2 = PdfImage.FromImage(chartImage2);
            graphics.DrawImage(pdfImage2, new PointF(10, 200));
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;
        }

        private void gunaButton6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage6;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private string GetCurrentUserUid()
        {
            if (auth?.User != null)
            {
                return auth.User.LocalId;
            }
            else
            {
                // Kullanıcı oturum açmamışsa veya auth nesnesi null ise null döndür
                return null;
            }
        }

        private void ShowCurrentUserUid()
        {
            // Oturum açık ve kullanıcının UID'si varsa göster
            if (isLoggedIn)
            {
                string uid = GetCurrentUserUid();
                if (uid != null)
                {
                    label14.Text = "ID: " + uid;
                }
                else
                {
                    label14.Text = "User not authenticated.";
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string enteredUsername = textBox1.Text;
            string enteredPassword = textBox2.Text;

            // FirebaseAuthProvider nesnesini oluşturun
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(FirebaseApiKey));

            try
            {
                // FirebaseAuth nesnesini oluşturun ve oturum açma işlemini gerçekleştirin
                auth = await authProvider.SignInWithEmailAndPasswordAsync(enteredUsername, enteredPassword);

                // FirebaseClient nesnesini oluşturun
                firebase = new FirebaseClient(FirebaseDatabaseURL);

                ShowUserProfilePanel();

                ShowCurrentUserUid();

                // Save the user credentials to Firebase Realtime Database
                SaveUserCredentials(enteredUsername, enteredPassword);
            }
            catch (FirebaseAuthException)
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı.");
            }
        }

        private async void SaveUserCredentials(string username, string password)
        {
            var firebase = new FirebaseClient(FirebaseDatabaseURL);

            // Kullanıcı verilerini Firebase Realtime Database'e kaydet
            await firebase
                .Child("users")
                .PostAsync(new Data { Username = username, Password = password });
        }

        private void ShowUserProfilePanel()
        {
            // Set the login status to true
            isLoggedIn = true;

            // Save the login status
            SaveLoginStatus();

            ShowCurrentUserUid();

            // Show UserProfilePanel
            DisplayUserProfilePanel();
        }

        private void SaveLoginStatus()
        {
            File.WriteAllText(filePath, isLoggedIn.ToString());
        }

        private void ClearCredentialsFile()
        {
            File.Delete(filePath);
        }

        private void DisplayUserProfilePanel()
        {
            if (isLoggedIn)
            {
                userProfilePanel.Visible = true;
            }
            else
            {
                userProfilePanel.Visible = false;
            }
        }

        private void gunaButton16_Click(object sender, EventArgs e)
        {

        }

        private void gunaButton21_Click(object sender, EventArgs e)
        {
            // Oturumu kapat
            if (auth != null)
            {
                auth = null; // auth nesnesini sıfırla
            }

            // FirebaseClient'ı sıfırla
            firebase = null;

            // UserProfilePanel'ı gizle
            userProfilePanel.Visible = false;

            // Text dosyasındaki verileri sil
            ClearCredentialsFile();

            MessageBox.Show("Oturum kapatıldı!");
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Cihazlar cihazlar = new Cihazlar();
            cihazlar.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            hesapturuForm hesapturuForm = new hesapturuForm();
            hesapturuForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenWebsite("https://www.cyberanalog.com/#clients");
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            ara ara = new ara();
            ara.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void gunaButton20_Click(object sender, EventArgs e)
        {
            telefonekle telefonekle = new telefonekle();
            telefonekle.Show();
        }

        public void VeriEkle(Kisi yeniKisi)
        {
            // DataGridView'e veriyi ekle
            dataGridView9.Rows.Add(yeniKisi.Ad, yeniKisi.Telefon);

            dataGridView1.Rows.Add(yeniKisi.Id, yeniKisi.Cihaz, yeniKisi.Isim);
        }

        private void label14_Click(object sender, EventArgs e)
        {
            // Label kontrolünün Text özelliğini Clipboard'a kopyala
            Clipboard.SetText(label14.Text);

            // Kopyalandı mesajı göster
            MessageBox.Show("Metin kopyalandı!");
        }

        private void gunaButton22_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage11;
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void gunaButton23_Click(object sender, EventArgs e)
        {
            Cihazlar cihazlar = new Cihazlar();
            cihazlar.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            RefreshDeviceList();
        }

        private void RefreshDeviceList()
        {
            listBoxDevices.Items.Clear();
            if (bluetoothClient != null)
            {
                try
                {
                    discoveredDevices = bluetoothClient.DiscoverDevices(10, true, true, false);

                    foreach (BluetoothDeviceInfo deviceInfo in discoveredDevices)
                    {
                        listBoxDevices.Items.Add(deviceInfo.DeviceName ?? "Unknown Device");
                    }
                }
                catch (Exception ex)
                {
                    LogError("Bluetooth etkin değil veya bir hata oluştu. Lütfen Bluetooth'u etkinleştirin ve yeniden deneyin.\nHata: " + ex.Message);
                }
            }
            else
            {
                LogError("Bluetooth desteği bulunamadı. Lütfen etkimleştirin ve programı yeniden başlatın!");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (listBoxDevices.SelectedIndex != -1)
            {
                BluetoothDeviceInfo selectedDeviceInfo = discoveredDevices[listBoxDevices.SelectedIndex];
                BluetoothAddress selectedDeviceAddress = selectedDeviceInfo.DeviceAddress;

                BluetoothClient client = new BluetoothClient();
                BluetoothEndPoint endPoint = new BluetoothEndPoint(selectedDeviceAddress, BluetoothService.SerialPort);

                try
                {
                    client.Connect(endPoint);
                    LogError("Bağlantı başarılı!");
                }
                catch (Exception ex)
                {
                    LogError("Bağlantı hatası: " + ex.Message);
                }
            }
            else
            {
                LogError("Lütfen bir cihaz seçin.");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (connectedClient != null && connectedClient.Connected)
            {
                connectedClient.Close();
                LogError("Bağlantı kesildi.");
            }
            else
            {
                LogError("Bağlantı zaten kesilmiş veya yok.");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string ipAddress = txtIpAddress.Text.Trim();
            string portText = txtPort.Text.Trim();
            int port;

            if (string.IsNullOrEmpty(ipAddress))
            {
                LogError("IP adresi girmelisiniz.");
                return;
            }

            if (!int.TryParse(portText, out port))
            {
                LogError("Geçerli bir port numarası girin.");
                return;
            }

            ConnectToWifiModule(ipAddress, port);
        }

        private void ConnectToWifiModule(string ipAddress, int port)
        {
            try
            {
                client = new TcpClient();
                client.Connect(ipAddress, port);
                stream = client.GetStream();

                richTextBox2.Text = $"{ipAddress}:{port}";
                LogError("Wi-Fi modülüne başarıyla bağlandınız.");
            }
            catch (Exception ex)
            {
                LogError("Bağlantı hatası: " + ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (client != null)
                {
                    client.Close();
                    client.Dispose();
                    LogError("Bağlantı kesildi.");
                }
                else
                {
                    LogError("Bağlı bir bağlantı yok.");
                }
            }
            catch (Exception ex)
            {
                LogError("Bağlantı kesme hatası: " + ex.Message);
            }
        }

        private void gunaButton24_Click(object sender, EventArgs e)
        {

            try
            {
                if (client != null)
                {
                    client.Close();
                    client.Dispose();
                    LogError("Bağlantı kesildi.");
                }
                else
                {
                    LogError("Bağlı bir bağlantı yok.");
                }

                if (connectedClient != null && connectedClient.Connected)
                {
                    connectedClient.Close();
                    LogError("Bağlantı kesildi.");
                }
                else
                {
                    LogError("Bağlantı zaten kesilmiş veya yok.");
                }
            }
            catch (Exception ex)
            {
                LogError("Bağlantı kesme hatası: " + ex.Message);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            dgvErrorLog.Rows.Clear();
        }
    }

    internal class Data
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class Kisi
    {
        public string Ad { get; set; }
        public string Telefon { get; set; }
        public string Id { get; set; }
        public string Cihaz { get; set; }
        public string Isim { get; set; }
    }
}