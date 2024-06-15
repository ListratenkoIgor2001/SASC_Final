using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;

using SASC_Final.Models;
using SASC_Final.ViewModels.Base;

using System.ComponentModel;
using Xamarin.Forms;

using System;
using System.Timers;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using Newtonsoft.Json.Linq;
using ZXing.Net.Mobile.Forms;
using ZXing.Common;
using Xamarin.Forms.Shapes;


namespace SASC_Final.ViewModels
{
    public class QRGeneratorViewModel : BaseViewModel
    {
        private string _qrCodeString;
        public bool TimerAlive;
        private int _remainingSeconds;

        public QRGeneratorViewModel()
        {
            TimerAlive = true;
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);
            GetNewQRCode();
            _remainingSeconds = 10;
        }
        private bool OnTimerTick()
        {
            _remainingSeconds--;
            OnPropertyChanged(nameof(TimerText));
            if (_remainingSeconds <= 0 && TimerAlive)
            {
                GetNewQRCode();
                _remainingSeconds = 10;
            }
            return TimerAlive;
        }
        public string QRCodeString
        {
            get { return _qrCodeString; }
            set { SetProperty(ref _qrCodeString, value); }
        }

        public string TimerText
        {
            get { return $"{_remainingSeconds} секунд осталось"; }
        }

        private void GetNewQRCode()
        {
            var info = GetInfo();
            QRCodeString = info;
        }

        private string GetInfo()
        {
            var StudentInfo = new StudentInfo();
            return JsonConvert.SerializeObject(StudentInfo);
        }
        /*
        private ImageSource qrCodeImage;
        private string qrCodeContent = "Initial Content";

        public ImageSource QRCodeImage
        {
            get => qrCodeImage;
            set
            {
                qrCodeImage = value;
                OnPropertyChanged(nameof(QRCodeImage));
            }
        }

        public ICommand StartTimerCommand { get; }

        public QRGeneratorViewModel()
        {
            StartTimerCommand = new Command(StartTimer);
        }

        private void StartTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                GenerateQRCode(qrCodeContent);
                qrCodeContent = Guid.NewGuid().ToString(); // Обновление содержимого для следующего QR-кода
                return true; // Возвращение true для повторного запуска таймера
            });
        }

        private void GenerateQRCode(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("QR code content cannot be empty");
}

            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Width = 300,
                    Height = 300,
                    Margin = 1
                }
            };

            var qrCode = writer.Write(qrCodeContent);
            QRCodeImage = ImageSource.FromStream(() =>
            {
                var stream = new System.IO.MemoryStream();
                qrCode.Save(stream, BarcodeFormat.QR_CODE);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return stream;
            });
        }
        public ICommand GenerateQRCodeCommand { get; }

        private string _QRValue = "11001";
        public string QRValue{ 
            get => _QRValue; 
            set => SetProperty(ref _QRValue, value); 
        }
        public QRGeneratorViewModel()
        {
            GenerateQRCodeCommand = new Command(ExecuteGenerateQRCodeCommand);
            ExecuteGenerateQRCodeCommand();
        }

  
        */
    }
}
