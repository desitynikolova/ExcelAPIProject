using AutoMapper;
using AutoMapper.Configuration;
using Date;
using Models;
using Quartz;
using Services.Common;
using Services.Implementation;
using Services.Interfaces;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppServer.ApiServices
{
    public class SchedFolderReaderJob : IJob
    {
        private ApplicationDbContext _dbContext;
        public IFilesReader Reader { get; }
        public ITransfer Transfer { get; }
        public IDatabaseTransfer Database { get; }
        public SchedFolderReaderJob(ApplicationDbContext dbContext, IFilesReader reader, ITransfer transfer, IDatabaseTransfer database)
        {
            _dbContext = dbContext;
            Reader = reader;
            Transfer = transfer;
            Database = database;
        }
        public async Task Execute(IJobExecutionContext context)
        {

            FilesReader filesReader = new FilesReader();
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                string[] files = Directory.GetFiles(DirectoryString.FolderDirectory);

                var lastReadedFile = dbContext.LastReadedFiles.OrderByDescending(x => x.LastRead);

                foreach (var file in files)
                {
                    var fileDate = Path.GetFileNameWithoutExtension(file);

                    if (lastReadedFile.Count() == 0)
                    {
                        var models = filesReader.FileReader(file).ToList();
                        Transfer transfer = new Transfer();
                        var data = transfer.ModelHandler(models);

                        var config = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<Region, RegionViewModel>().ReverseMap();
                            cfg.CreateMap<Country, CountryViewModel>().ReverseMap();
                            cfg.CreateMap<Order, OrderViewModel>().ReverseMap();
                            cfg.CreateMap<Sales, SalesViewModel>().ReverseMap();
                        });

                        var mapper = new Mapper(config);
                        DatabaseTransfer databaseTransfer = new DatabaseTransfer(new Date.ApplicationDbContext(), mapper);
                        await databaseTransfer.TransferAsync(data, fileDate);
                    }

                    else
                    {
                        var last = lastReadedFile.First().LastRead.ToShortDateString();

                        if (DateTime.Parse(last) < DateTime.Parse(fileDate))
                        {
                            var models = filesReader.FileReader(file).ToList();
                            Transfer transfer = new Transfer();
                            var data = transfer.ModelHandler(models);

                            var config = new MapperConfiguration(cfg =>
                            {
                                cfg.CreateMap<Region, RegionViewModel>().ReverseMap();
                                cfg.CreateMap<Country, CountryViewModel>().ReverseMap();
                                cfg.CreateMap<Order, OrderViewModel>().ReverseMap();
                                cfg.CreateMap<Sales, SalesViewModel>().ReverseMap();
                            });

                            var mapper = new Mapper(config);
                            DatabaseTransfer databaseTransfer = new DatabaseTransfer(new ApplicationDbContext(), mapper);
                            await databaseTransfer.TransferAsync(data, fileDate);
                        }
                    }
                }
            }
        }
    }
}