﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuanLyKhoaHoc.Domain.Entities;
using QuanLyKhoaHoc.Domain.InterfaceRepositories;
using QuanLyKhoaHoc.Infrastructure.DataContexts;
using QuanLyKhoaHoc.Infrastructure.ImplementRepositories;

namespace QuanLyKhoaHoc.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnection = configuration.GetConnectionString("DefaultConnection");
            var workConnectionString = configuration.GetConnectionString("WorkConnection");
            
            string connectionString = "";
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment == "Work") {
                connectionString = workConnectionString;
            }

            services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IDBContext, AppDBContext>();
            services.AddScoped<IBaseRepository<LoaiKhoaHoc>, BaseRepository<LoaiKhoaHoc>>();
            services.AddScoped<IBaseRepository<LoaiBaiViet>, BaseRepository<LoaiBaiViet>>();
            services.AddScoped<IBaseRepository<QuyenHan>, BaseRepository<QuyenHan>>();
            services.AddScoped<IBaseRepository<KhoaHoc>, BaseRepository<KhoaHoc>>();
            services.AddScoped<IBaseRepository<ChuDe>, BaseRepository<ChuDe>>();
        }
    }
}
