using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mora.Logger.ILogger;

namespace Mora.Logger.MsSqlLogger
{
    public class DbLogger : ILogger.ILog
    {
        private LoggerDbContext _Db = new LoggerDbContext();

        public bool CanAddError { get; set; }
        
        public bool CanAddInfo { get; set; }
        
        public bool CanAddWarning { get; set; }
        
        public Guid Log(LogModel model)
        {
            Guid businessID = Guid.Empty;

            bool isValid = validateModel(model);

            if (!isValid)
            {
                return businessID;
            }

            businessID = Guid.NewGuid();

            model.BusinessID = businessID;
            model.CreatedOn = DateTime.Now;

            LogDataModel dataModel = new LogDataModel(model);

            switch (model.LogType)
            {
                case ILogger.TypeOfLog.Error:
                    if (CanAddError)
                    {
                        _Db.LogData.Add(dataModel);
                        _Db.SaveChanges();
                    }
                    break;
                case ILogger.TypeOfLog.Warning:
                    if (CanAddWarning)
                    {
                        _Db.LogData.Add(dataModel);
                        _Db.SaveChanges();
                    }
                    break;
                case ILogger.TypeOfLog.Info:
                    if (CanAddInfo)
                    {
                        _Db.LogData.Add(dataModel);
                        _Db.SaveChanges();
                    }
                    break;
            }

            return businessID;
        }

        private bool validateModel(LogModel model)
        {
            if(model == null)
            {
                return false;
            }

            return true;
        }
    }
}
