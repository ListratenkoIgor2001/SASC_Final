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
using SASC_Final.Services.Interfaces;
using System.Threading.Tasks;

namespace SASC_Final.ViewModels
{
    public class QRScannerViewModel : BaseViewModel 
    {
        private AppData appData;
        private ICryptografy _cryptografy;
        private bool isScanning = false;
        public bool IsScanning
        {
            get { return isScanning; }
            set { SetProperty(ref isScanning, value); }
        }        

        private bool useFrontCamera = false;
        public bool UseFrontCamera
        {
            get { return useFrontCamera; }
            set { SetProperty(ref useFrontCamera, value); }
        }

        public ICommand StartScanningCommand { get; }
        private ISimpleAudioPlayer _player { get; set;}
        public Action GoBack;

        public QRScannerViewModel()
        {
            appData = DependencyService.Get<AppData>();
            _cryptografy = DependencyService.Get<ICryptografy>();
            UseFrontCamera = appData.Settings.UseFrontCamera;
            _player = CrossSimpleAudioPlayer.Current;
            LoadSound("beep.mp3");
            StartScanningCommand = new Command<ZXingScannerView>(ExecuteStartScanningCommand);
        }



        private void PlaySound(string fileName)
        {
            _player.Load(fileName);
            _player.Play();
        }
        private void LoadSound(string fileName)
        {
            _player.Load(fileName);
        }

        public void OnScanResult(Result result)
        {
            isScanning = false;
            Device.BeginInvokeOnMainThread(async () =>
            {
                var now = DateTime.Now;
                try
                {
                    var resultString = _cryptografy.Decode(result.Text);                    
                    //var resultString = result.Text;
                    Error = result.Text;
                    //DisplayError();
                    var x = JsonConvert.DeserializeObject<StudentInfo>(resultString);
                    //_player.Play();
                    //var handletrueResult = HandleStudentTrue(x,now);
                    var handleResult = HandleStudent(x, now);

                }
                catch (JsonException ex)
                {
                    Crashes.TrackError(ex);
                    PlaySound("beep(2).mp3");
                    Error = "Ошибка. QR-код не содержит данных студента либо повреждён.";
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

        private int HandleStudent(StudentInfo info, DateTime now)
        {
            var studs = appData.CurrentStudents.GetItems();
            var ats = appData.CurrentAttendances.GetItems();
            var device = studs.Where(x => x.DeviceHash == info.DeviceHash).FirstOrDefault();
            if (device!=null)
            {
                if (device.Id == info.Id) return 0;
                //beep bepp bepp
                //устройство уже было зарегистрированно

                PlaySound("beep(3).mp3");
                Error = "Данное устройство уже было зарегистрированно.";
                DisplayError();
                return 1;
            }
            if (appData.CurrentLessons.CurrentItem.LessonId != info.LessonId)
            {
                //beep beep
                //QR код для другого занятия
                PlaySound("beep(2).mp3");
                Error = "QR код для другого занятия.";
                DisplayError();
                return 2;

            }
            var student = ats.Select(x => x).FirstOrDefault(x => x.studentModel.Id == info.Id);
            if (student == null)
            {
                //beep beep
                //студент не найден в списке посещения

                PlaySound("beep(3).mp3");
                Error = "Cтудент не найден в списке посещения.";
                DisplayError();
                return 3;
            }
            if (info.TimeExp < now)
            {
                PlaySound("beep(3).mp3");
                Error = "Срок действия QR-кода истёк.";
                DisplayError();
                return 4;
            }
            studs.Add(info);

            PlaySound("beep.mp3");
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

        #region Deprecated
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

        private int HandleStudentTrue(StudentInfo info, DateTime now)
        {
            var AppData = DependencyService.Get<AppData>();


            var studs = AppData.CurrentStudents.GetItems();
            var ats = AppData.CurrentAttendances.GetItems();
            var student = ats.Select(x => x).FirstOrDefault(x => x.studentModel.Id == info.Id);
            Error = JsonConvert.SerializeObject(info) + $"\n present before: {ats.Select(x => x).Where(x => x.IsPresent).Count()}";


            studs.Add(info);
            //_player.Play();
            student.IsPresent = true;


            Error += $"\n present after: {ats.Select(x => x).Where(x => x.IsPresent).Count()}";
            DisplayError();
            return 0;
        }
        private MobileBarcodeScanningOptions scanningOptions;
        public MobileBarcodeScanningOptions ScanningOptions
        {
            get { return scanningOptions; }
            set { SetProperty(ref scanningOptions, value); }
        }
        private MobileBarcodeScanningOptions GetScanningOptions()
        {
            return new MobileBarcodeScanningOptions
            {
                DelayBetweenContinuousScans = 1500,
                UseFrontCameraIfAvailable = useFrontCamera,
                AutoRotate = true,
                TryHarder = true,
                PossibleFormats = new List<ZXing.BarcodeFormat>
                {
                    ZXing.BarcodeFormat.QR_CODE
                }
            };
        }
        #endregion
    }
}
