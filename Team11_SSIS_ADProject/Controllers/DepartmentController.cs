﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.Service;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentService departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }
        public ActionResult Create()
        {
            var department = new DepartmentViewModel();
            return View("DepartmentForm", department);
        }
        // GET: Department
        public ActionResult Index()
        {
            Department d = new Department();
            var viewModel = new DepartmentViewModel()
            {

                DepartmentCode = d.DepartmentCode,
                DepartmentName = d.DepartmentName,
                DepartmentContactName = d.DepartmentContactName,
                DepartmentPhone = d.DepartmentPhone,
                DepartmentFax = d.DepartmentFax,
                DepartmentHeadName = d.DepartmentHeadName,
                DepartmentRepresentative = d.DepartmentRepresentative,

                Departments = departmentService.GetAll()
            };

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Save(Department department)
        {
            departmentService.Save(department);

            return RedirectToAction("Index", "Department");
        }
        public ActionResult Edit(string id)
        {
            var d = departmentService.Get(id);
            var department = new DepartmentViewModel()
            {
                DepartmentCode = d.DepartmentCode,
                DepartmentName = d.DepartmentName,
                DepartmentContactName = d.DepartmentContactName,
                DepartmentPhone = d.DepartmentPhone,
                DepartmentFax = d.DepartmentFax,
                DepartmentHeadName = d.DepartmentHeadName,
                DepartmentRepresentative = d.DepartmentRepresentative
            };

            return View("DepartmentForm", department);
        }

        public ActionResult Delete(string id)
        {
            var department = departmentService.Get(id);

            departmentService.Delete(id);

            return RedirectToAction("Index", "Department");
        }
    }
}