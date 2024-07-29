﻿using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application.InterfaceServices;
using QuanLyKhoaHoc.Application.Payloads.RequestModels.ChuDeRequests;
using QuanLyKhoaHoc.Application.Payloads.RequestModels.KhoaHocRequests;

namespace QuanLyKhoaHoc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILoaiKhoaHocService _loaiKhoaHocService;
        private readonly ILoaiBaiVietService _loaiBaiVietService;
        private readonly IQuyenHanService _quyenHanService;
        private readonly IKhoaHocService _khoaHocService;
        private readonly IChuDeService _chuDeService;

        public UserController(ILoaiKhoaHocService loaiKhoaHocService,
                              ILoaiBaiVietService loaiBaiVietService,
                              IQuyenHanService quyenHanService,
                              IKhoaHocService khoaHocService,
                              IChuDeService chuDeService)
        {
            _loaiKhoaHocService = loaiKhoaHocService;
            _loaiBaiVietService = loaiBaiVietService;
            _quyenHanService = quyenHanService;
            _khoaHocService = khoaHocService;
            _chuDeService = chuDeService;
        }


        [HttpGet("GetAllLoaiKhoaHoc")]
        public async Task<IActionResult> GetAllLoaiKhoahocs()
        {
            return Ok(await _loaiKhoaHocService.GetAllLoaiKhoahocs());
        }

        [HttpGet("GetAllLoaiBaiViets")]
        public async Task<IActionResult> GetAllLoaiBaiViets()
        {
            return Ok(await _loaiBaiVietService.GetAllLoaiBaiViets());
        }

        [HttpGet("GetAllQuyenHan")]
        public async Task<IActionResult> GetAllQuyenHans()
        {
            return Ok(await _quyenHanService.GetAllQuyenHans());
        }

        [HttpGet("GetAllsKhoaHoc")]
        public async Task<IActionResult> GetAllsKhoaHoc(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(await _khoaHocService.GetAlls(pageSize, pageNumber));
        }

        [HttpGet("GetKhoaHocByName")]
        public async Task<IActionResult> GetKhoaHocByName([FromQuery] string tenKhoaHoc)
        {
            return Ok(await _khoaHocService.GetKhoaHocByName(tenKhoaHoc));
        }

        [HttpGet("SearchPagedKhoaHocsByName")]
        public async Task<IActionResult> SearchPagedKhoaHocsByName(string search, int page = 1, int pageSize = 10)
        {
            var result = await _khoaHocService.SearchPagedKhoaHocsByName(search, page, pageSize);
            return Ok(result);
        }

        [HttpPost("ThemKhoaHoc")]
        public async Task<IActionResult> ThemKhoaHoc([FromForm] Request_ThemKhoaHoc request)
        {
            return Ok(await _khoaHocService.ThemKhoaHoc(request));
        }

        [HttpPut("SuaThongTinKhoaHoc")]
        public async Task<IActionResult> SuaThongTinKhoaHoc([FromForm] Request_SuaKhoaHoc request)
        {
            return Ok(await _khoaHocService.CapNhatThongTinKhoaHoc(request));
        }

        [HttpDelete("XoaKhoaHoc/{khoaHocId}")]
        public async Task<IActionResult> XoaKhoaHoc([FromRoute] int khoaHocId)
        {
            return Ok(await _khoaHocService.XoaKhoaHoc(khoaHocId));
        }

        [HttpGet("GetAllsChuDe")]
        public async Task<IActionResult> GetAllsChuDe(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(await _chuDeService.GetAlls(pageSize, pageNumber));
        }

        [HttpGet("GetChuDeById")]
        public async Task<IActionResult> GetChuDeById([FromRoute] int chuDeId)
        {
            return Ok(await _chuDeService.GetChuDeById(chuDeId));
        }

        [HttpPost("ThemChuDe")]
        public async Task<IActionResult> ThemChuDe([FromBody] Request_AddChuDe request)
        {
            return Ok(await _chuDeService.AddChuDe(request));
        }

        [HttpPut("CapNhatChuDe")]
        public async Task<IActionResult> CapNhatChuDe([FromBody] Request_EditChuDe request)
        {
            return Ok(await _chuDeService.UpdateChuDe(request));
        }

    }
}
