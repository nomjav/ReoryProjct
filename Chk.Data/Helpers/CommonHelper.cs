
using AcademyLockSmith.Data.MemberShip;
using Chakwal.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyLockSmith.Data.Helpers
{
  public class CommonHelper
    {
        //private static readonly UnitOfWork _unitOfWork = new UnitOfWork();
        //public void LogCallSlipChanges(long JobId, string LogMessage, string LogData, int? prevStatusId, int? newStatusId)
        //{
        //    try
        //    {
        //        UnitOfWork _unitOfWork = new UnitOfWork();
        //        double? prevStatusCode = null;
        //        double? newStatusCode = null;
        //        if (prevStatusId != null && newStatusId != null)
        //        {
        //            List<JobStatu> statusCodes = _unitOfWork.JobStatusRepository.Get(x => x.Id == prevStatusId || x.Id == newStatusId);
        //            prevStatusCode = statusCodes.Where(y => y.Id == prevStatusId).Select(y => y.Code).FirstOrDefault();
        //            newStatusCode = statusCodes.Where(y => y.Id == newStatusId).Select(y => y.Code).FirstOrDefault();
        //        }

        //        var jobLog = new JobLog
        //        {
        //            LogMessage = LogMessage,
        //            JobId = JobId,
        //            Data = LogData,
        //            TimeStamp = DateTime.Now,
        //            UserId = ALSContext.Current.User.Id,
        //            PrevStatus = prevStatusCode,
        //            NewStatus = newStatusCode
        //        };
        //        var unitOfWork = new UnitOfWork();
        //        unitOfWork.JobLogRepository.Insert(jobLog);
        //        unitOfWork.Save();
        //    }
        //    catch (Exception exception)
        //    {
        //        Logger.Logger.LogException(exception.Message, exception);
        //    }
        //}
    }
}
