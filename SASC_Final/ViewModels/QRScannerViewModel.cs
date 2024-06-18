using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using SASC_Final.ViewModels.Base;
using Xamarin.Forms;

using ZXing.Net.Mobile.Forms;

using ZXing;
using SASC_Final.Models;
using Newtonsoft.Json;
using Microsoft.AppCenter.Crashes;
using System.Diagnostics;
using System.Linq;
using Plugin.SimpleAudioPlayer;
using System.IO;
using System.Numerics;
using System.Reflection;
using ZXing.Mobile;

namespace SASC_Final.ViewModels
{
    public class QRScannerViewModel : BaseViewModel 
    {
        private bool isScanning = false;

        public bool IsScanning
        {
            get { return isScanning; }
            set { SetProperty(ref isScanning, value); }
        }
        public ICommand StartScanningCommand { get; }
        private readonly ISimpleAudioPlayer _player;

        private MobileBarcodeScanningOptions scanningOptions;

        public MobileBarcodeScanningOptions ScanningOptions
        {
            get { return scanningOptions; }
            set { SetProperty(ref scanningOptions, value); }
        }

        public QRScannerViewModel()
        {
            _player = CrossSimpleAudioPlayer.Current;
            //LoadSound("beep.mp3");
            ScanningOptions = new MobileBarcodeScanningOptions
            {
                AutoRotate = true,
                TryHarder = true,
                PossibleFormats = new List<ZXing.BarcodeFormat>
                {
                    ZXing.BarcodeFormat.QR_CODE
                }
            };
            StartScanningCommand = new Command<ZXingScannerView>(ExecuteStartScanningCommand);
        }
        private void LoadSound(string fileName)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream("SASC_Final." + fileName);
            _player.Load(audioStream);
        }
        private void ExecuteStartScanningCommand(ZXingScannerView scannerView)
        {
            if (!isScanning)
            {
                scannerView.IsScanning = true;
                scannerView.IsAnalyzing = true;
                isScanning = true;
            }
            else
            {
                scannerView.IsScanning = false;
                scannerView.IsAnalyzing = false;
                isScanning = false;
            }
        }

        // Handle the scan result event
        public void OnScanResult(Result result)
        {
            // Handle your scan result here
            isScanning = false;
            Device.BeginInvokeOnMainThread(async () =>
            {
                var now = DateTime.Now;
                try
                {
                    //beep
                    var resultString = result.Text;
                    Error = result.Text;
                    DisplayError();
                    var x = JsonConvert.DeserializeObject<StudentInfo>(resultString);
                    var handletrueResult = HandleStudentTrue(x,now);
                    var handleResult = HandleStudent(x,now);

                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    Error = ex.Message;
                    DisplayError();
                    Debug.WriteLine(ex);
                }

                //await Application.Current.MainPage.DisplayAlert("Scanned result", result.Text, "OK");
            });
        }

        private int HandleStudentTrue(StudentInfo info, DateTime now) 
        {
            var AppData = DependencyService.Get<AppData>();


            var studs = AppData.CurrentStudents.GetItems();
            var ats = AppData.CurrentAttendances.GetItems();
            var student = ats.Select(x => x).FirstOrDefault(x => x.studentModel.Id == info.Id);
            Error = JsonConvert.SerializeObject(info) + $"\n present before: {ats.Select(x=>x).Where(x=>x.IsPresent).Count()}";


            studs.Add(info);
            //_player.Play();
            student.IsPresent = true;


            Error += $"\n present after: {ats.Select(x => x).Where(x => x.IsPresent).Count()}";
            DisplayError();
            return 0;
        }
        private int HandleStudent(StudentInfo info, DateTime now)
        {

            var AppData = DependencyService.Get<AppData>();

            var studs = AppData.CurrentStudents.GetItems();
            var ats = AppData.CurrentAttendances.GetItems();
            if (studs.Select(x => x.DeviceHash).Contains(info.DeviceHash))
            {
                //beep bepp bepp
                //устройство уже было зарегистрированно
                //_player.Play(); _player.Play(); _player.Play();
                Error = "Данное устройство уже было зарегистрированно.";
                DisplayError();
                return 1;
            }
            if (AppData.SelectedLesson.LessonId != info.LessonId)
            {
                //beep beep
                //QR код для другого занятия
                //_player.Play(); _player.Play(); _player.Play();
                Error = "QR код для другого занятия.";
                DisplayError();
                return 2;

            }
            var student = ats.Select(x => x).FirstOrDefault(x => x.studentModel.Id == info.Id);
            if (student == null)
            {
                //beep beep
                //студент не найден в списке посещения
                //_player.Play(); _player.Play(); _player.Play();
                Error = "Cтудент не найден в списке посещения.";
                DisplayError();
                return 3;
            }
            if (info.TimeExp < now)
            {
                //_player.Play(); _player.Play();
                Error = "Срок действия QR-кода истёк.";
                DisplayError();
                return 4;
            }
            studs.Add(info);
            //_player.Play();
            student.IsPresent = true;
            return 0;
        }
        public Action DisplayError;
        private string error;
        public string Error
        {
            get => error;
            set => SetProperty(ref error, value);
        }
    }
}
