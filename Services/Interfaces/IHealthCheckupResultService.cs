﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Entities;
namespace Services.Interfaces
{
    public interface IHealthCheckupResultService
    {
        //Thien
        List<HealthCheckupResult> getAllHealthCheckupResultByStudentId(int studentId);

        //Thien -Admin
        List<HealthCheckupResult> CreateHealthCheckupResultByHealthCheckupId(List<HealthCheckupResult> healthCheckupResults);

        List<HealthCheckupResult> getAllHealthCheckupResultByHealthCheckupId(HealthCheckup healthCheckup);
        void UpdateHealthCheckupResult(HealthCheckupResult item);

    }
}
