﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHoc.Application.Handle.HandleImage;
using QuanLyKhoaHoc.Application.Handle.HandlePagination;
using QuanLyKhoaHoc.Application.InterfaceServices;
using QuanLyKhoaHoc.Application.Payloads.Mappers;
using QuanLyKhoaHoc.Application.Payloads.RequestModels.StudentRequests;
using QuanLyKhoaHoc.Application.Payloads.ResponseModels.DataStudent;
using QuanLyKhoaHoc.Application.Payloads.Responses;
using QuanLyKhoaHoc.Domain.Entities;
using QuanLyKhoaHoc.Domain.InterfaceRepositories;
using static QuanLyKhoaHoc.Domain.Validations.ValidateInput;

namespace QuanLyKhoaHoc.Application.ImplementServices
{
    public class StudentService : IStudentService
    {
        private readonly IBaseRepository<HocVien> _baseStudentRepository;
        private readonly StudentConverter _studentConverter;

        public StudentService(IBaseRepository<HocVien> baseStudentRepository,
                              StudentConverter studentConverter)
        {
            _baseStudentRepository = baseStudentRepository;
            _studentConverter = studentConverter;
        }

        public async Task<PageResult<DataResponseStudent>> GetAlls(int pageSize, int pageNumber)
        {
            var students = await _baseStudentRepository.GetAllAsync().Result.ToListAsync();
            var query = students.Select(x => _studentConverter.EntityToDTO(x)).AsQueryable();
            var result = Pagination.GetPagedData(query, pageSize, pageNumber);
            return result;
        }
        public async Task<PageResult<DataResponseStudent>> SearchPagedStudents(string keyword, int pageNumber, int pageSize)
        {

            var query = await _baseStudentRepository.GetAllAsync(hv =>
                hv.HoTen.ToLower().Contains(keyword.ToLower()) ||
                hv.Email.ToLower().Contains(keyword.ToLower())
            );


            var pagedData = Pagination.GetPagedData(query, pageSize, pageNumber);


            var convertedItems = pagedData.Data.Select(hocVien => _studentConverter.EntityToDTO(hocVien)).AsQueryable();


            return new PageResult<DataResponseStudent>(convertedItems, pagedData.TotalItems, pagedData.TotalPages, pageNumber, pageSize);
        }
        public async Task<ResponseObject<DataResponseStudent>> CreateSudent(Request_CreateStudent request)
        {
            try
            {
                if (!IsValidEmail(request.Email))
                {
                    return new ResponseObject<DataResponseStudent>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Email không hợp lệ",
                        Data = null
                    };
                }
                if (!IsValidPhoneNumber(request.SoDienThoai))
                {
                    return new ResponseObject<DataResponseStudent>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Số điện thoại không hợp lệ",
                        Data = null
                    };
                }

                var student = new HocVien
                {
                    HinhAnh = await HandleUploadImage.Upfile(request.HinhAnh),
                    Email = request.Email,
                    HoTen = request.HoTen,
                    NgaySinh = request.NgaySinh,
                    PhuongXa = request.PhuongXa,
                    QuanHuyen = request.QuanHuyen,
                    SoDienThoai = request.SoDienThoai,
                    SoNha = request.SoNha,
                    TinhThanh = request.TinhThanh,
                };

                student = await _baseStudentRepository.CreateAsync(student);

                return new ResponseObject<DataResponseStudent>
                {
                    Status = StatusCodes.Status201Created,
                    Message = "Tạo thành công",
                    Data = _studentConverter.EntityToDTO(student)
                };
            }
            catch (Exception e)
            {

                return new ResponseObject<DataResponseStudent>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Error : " + e.Message,
                    Data = null
                };
            }
        }
        public async Task<ResponseObject<DataResponseStudent>> UpdateSudent(int studentId, Request_UpdateStudent request)
        {
            try
            {
                var student = await _baseStudentRepository.GetByIdAsync(studentId);
                if (student == null)
                {
                    return new ResponseObject<DataResponseStudent>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "không tồn tại học viên này",
                        Data = null
                    };
                }
                if (!IsValidEmail(request.Email))
                {
                    return new ResponseObject<DataResponseStudent>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Email không hợp lệ",
                        Data = null
                    };
                }
                if (!IsValidPhoneNumber(request.SoDienThoai))
                {
                    return new ResponseObject<DataResponseStudent>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Số điện thoại không hợp lệ",
                        Data = null
                    };
                }


                student.HinhAnh = await HandleUploadImage.Upfile(request.HinhAnh);
                student.Email = request.Email;
                student.HoTen = request.HoTen;
                student.NgaySinh = request.NgaySinh;
                student.PhuongXa = request.PhuongXa;
                student.QuanHuyen = request.QuanHuyen;
                student.SoDienThoai = request.SoDienThoai;
                student.SoNha = request.SoNha;
                student.TinhThanh = request.TinhThanh;


                student = await _baseStudentRepository.UpdateAsync(student);

                return new ResponseObject<DataResponseStudent>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Cập nhật học viên thành công",
                    Data = _studentConverter.EntityToDTO(student)
                };
            }
            catch (Exception e)
            {

                return new ResponseObject<DataResponseStudent>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Error : " + e.Message,
                    Data = null
                };
            }
        }
        public async Task<string> DeleteStudent(int studentId)
        {
            var student = await _baseStudentRepository.GetByIdAsync(studentId);
            if (student == null)
            {
                return "Học viên không tồn tại";
            }
            await _baseStudentRepository.DeleteAsync(studentId);
            return "Xoá thành công";
        }
    }
}
