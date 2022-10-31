using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atea.Interfaces;
using Atea.Models;

namespace Atea.Services
{
    public class LogService : ILogService
    {
        private readonly ILogStorageProvider _logStorageProvider;

        public LogService(ILogStorageProvider logStorageProvider)
        {
            _logStorageProvider = logStorageProvider;
        }

        public Task LogRequestAsync(Root response, string id)
        {
            return _logStorageProvider.LogRequestAsync(response, id);
        }

        public IEnumerable<ApiResponseEntity> GetLogs(DateTime from, DateTime to)
        {
            return _logStorageProvider.GetLogs(from, to);
        }
    }
}