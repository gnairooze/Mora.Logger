using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mora.Logger.MsSqlLogger
{
    internal class LogDataModel
    {
        #region properties
        [Key]
        public long ID { get; set; }
        [Required]
        [Index(IsUnique =true)]
        public Guid BusinessID { get; set; }
        [Required]
        [Index]
        public Guid Group { get; set; }
        [Required]
        public int Counter { get; set; }
        [Required]
        [Index]
        public byte LogTypeID { get; set; }
        [Required]
        [MaxLength(500)]
        public string Who { get; set; }
        [Required]
        public string What { get; set; }
        [Required]
        public DateTime When { get; set; }
        [Required]
        [MaxLength(30)]
        public string ReferenceName { get; set; }
        [Required]
        [MaxLength(30)]
        public string ReferenceValue { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        #endregion

        #region constructors
        public LogDataModel()
        {

        }

        public LogDataModel(ILogger.LogModel model)
        {
            BusinessID = model.BusinessID;
            Counter = model.Counter;
            CreatedOn = model.CreatedOn;
            Group = model.Group;
            ID = model.ID;
            LogTypeID = (byte)model.LogType;
            ReferenceName = model.ReferenceName;
            ReferenceValue = model.ReferenceValue;
            What = model.What;
            When = model.When;
            Who = model.Who;
        }
        #endregion

        public ILogger.LogModel ToBusinessModel()
        {
            ILogger.LogModel model = new ILogger.LogModel();

            model.BusinessID = BusinessID;
            model.Counter = Counter;
            model.CreatedOn = CreatedOn;
            model.Group = Group;
            model.ID = ID;
            model.LogType = (ILogger.TypeOfLog)LogTypeID;
            model.ReferenceName = ReferenceName;
            model.ReferenceValue = ReferenceValue;
            model.What = What;
            model.When = When;
            model.Who = Who;

            return model;
        }
    }
}
