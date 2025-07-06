using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Services.Implementations;
using Services.Interfaces;
using DataAccessLayer;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Implementations;
using Services.Interfaces;
using System;

namespace WPF.SchoolMedicalManagementSystem.ViewModels
{
    public partial class CreateHealthCheckupViewModel : ObservableObject
    {

        private readonly IHealthCheckupService _service = new HealthCheckupService();
        //private PrnEduHealthContext _context = new PrnEduHealthContext();
        private readonly string _currentUserId;

        [ObservableProperty]
        private string? checkupName;
        [ObservableProperty]
        private DateTime? checkupDate;
        [ObservableProperty]
        private string? description;

        public Action<HealthCheckup>? OnSaved { get; set; }
        public Action? CloseAction { get; set; }

        public CreateHealthCheckupViewModel(string currentUserId)
        {
            //_service = new HealthCheckupService(); // hoặc inject từ ngoài vào
            _currentUserId = currentUserId;
        }

        [RelayCommand]
        private void Save()
        {
            if (string.IsNullOrWhiteSpace(CheckupName) || !CheckupDate.HasValue)
            {
                // Thông báo lỗi nếu cần
                return;
            }

            var entity = new HealthCheckup
            {
                CheckupName = this.CheckupName,
                CheckupDate = DateOnly.FromDateTime(this.CheckupDate.Value),
                Description = this.Description,
                CreatedBy = Guid.Parse("88498BC0-0130-442C-ABE8-46AD5BF0BA61")
            };

            //entity.CreatedBy = Guid.Parse(_currentUserId);

            _service.AddHealthCheckup(entity);
            

            OnSaved?.Invoke(entity);
            CloseAction?.Invoke();
        }
    }
}