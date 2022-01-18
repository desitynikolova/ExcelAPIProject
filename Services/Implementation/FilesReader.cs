using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using Date;
using Services.Interfaces;
using Services.ViewModels;

namespace Services.Implementation
{
    public class FilesReader : IFilesReader
    {
        public List<TransferViewModel> FileReader(string dir)
        {
            using (var reader = new StreamReader(dir))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                try
                {
                    var records = csv.GetRecords<TransferViewModel>().ToList();
                    return records;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}