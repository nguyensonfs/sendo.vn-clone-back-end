﻿using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application.InterfaceServices;
using QuanLyKhoaHoc.Application.Payloads.RequestModels.TypeOfCourseRequests;

namespace QuanLyKhoaHoc.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TypeOfCoursesController : ControllerBase
    {
        private readonly ITypeOfCourseService _typeOfCourseService;

        public TypeOfCoursesController(ITypeOfCourseService typeOfCourseService)
        {
            _typeOfCourseService = typeOfCourseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(await _typeOfCourseService.GetAllTypeOfCourses(pageSize,pageNumber));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Request_CreateTypeOfCourse request)
        {
            return Ok(await _typeOfCourseService.CreateTypeOfCourse(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Upadate(int id, Request_UpdateTypeOfCourse request)
        {
            return Ok(await _typeOfCourseService.UpdateTypeOfCourse(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _typeOfCourseService.DeleteTypeOfCourse(id));
        }

    }
}
