﻿using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoaHoc.Application.Payloads.RequestModels.TypeOfCourseRequests
{
    public class Request_CreateTypeOfCourse
    {
        [Required(ErrorMessage ="TenLoaiKhoaHoc là bắt buộc")]
        public string TenLoaiKhoaHoc { get; set; } = string.Empty;
    }
}
