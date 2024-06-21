using System;
using System.Collections.Generic;
using System.Text;

using SASC_Final.Models;
using SASC_Final.Models.Common;
using SASC_Final.ViewModels.Base;

namespace SASC_Final.ViewModels
{
    public class AttendanceStudentViewModel : BaseViewModel
    {         
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _fio;
        public string Fio
        {
            get { return _fio; }
            set { SetProperty(ref _fio, value); }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set { SetProperty(ref _imageUrl, value); }
        }

        private bool _isPresent;
        public bool IsPresent
        {
            get { return _isPresent; }
            set
            {
                var now = DateTime.Now;
                RegisteredAt = value ? $"{now.ToString("HH:mm")}" : null;
                SetProperty(ref _isPresent, value);
            }
        }

        private string _registeredAt;
        public string RegisteredAt
        {
            get { return _registeredAt; }
            set { SetProperty(ref _registeredAt, value); }
        }

        public AttendanceStudent studentModel;

        public AttendanceStudentViewModel() { }

        public AttendanceStudentViewModel(AttendanceStudent student)
        {
            studentModel = student;
            Id = student.Id;
            Fio = student.LastName + " " +student.FirstName;
            ImageUrl = student.ImageUrl;
            IsPresent = student.IsPresent;
            RegisteredAt = student.AttendanceTime;
        }

    }
}