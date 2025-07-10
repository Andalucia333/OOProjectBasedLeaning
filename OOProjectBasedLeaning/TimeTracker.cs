using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OOProjectBasedLeaning
{

    public interface TimeTracker
    {

        /// <summary>
        /// 出勤の時間を記録する。
        /// </summary>
        /// <param name="employeeId">従業員のID</param>
        /// <exception cref="InvalidOperationException">従業員がすでに仕事中の場合</exception>"
        void PunchIn(int employeeId);

        /// <summary>
        /// 退勤の時間を記録する。
        /// </summary>
        /// <param name="employeeId">従業員のID</param>
        /// <exception cref="InvalidOperationException">従業員が仕事中でない場合</exception>""
        void PunchOut(int employeeId);

        /// <summary>
        /// 仕事中かどうかを判定する。
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>仕事中の場合 true</returns>
        bool IsAtWork(int employeeId);

    }

    public class TimeTrackerModel : TimeTracker
    {

        /// <summary>
        /// Represents the company associated with the current context.
        /// </summary>
        /// <remarks>This field is initialized to a default instance of <see cref="NullCompany"/> to
        /// ensure         a non-null value. Use this field to access or modify the company information as
        /// needed.</remarks>
        private Company company = NullCompany.Instance;
        /// <summary>
        /// <DateTime.Today, <Employee.Id, DateTime.Now>>
        /// </summary>
        private Dictionary<int, Dictionary<DateTime, DateTime>> timestamp4PunchIn = new Dictionary<int, Dictionary<DateTime, DateTime>>();
        /// <summary>
        /// <DateTime.Today, <Employee.Id, DateTime.Now>>
        /// </summary>
        private Dictionary<int, Dictionary<DateTime, DateTime>> timestamp4PunchOut = new Dictionary<int, Dictionary<DateTime, DateTime>>();
        /// <summary>
        /// Represents the current operational mode of the system.
        /// </summary>
        /// <remarks>The mode determines the behavior of the system, such as whether it operates in "Punch
        /// In" mode or other modes.</remarks>
        private Mode mode = Mode.PunchIn;

        private enum Mode
        {
            PunchIn, // default
            PunchOut
        };

        public TimeTrackerModel(Company company)
        {

            this.company = company.AddTimeTracker(this);

        }

        public void PunchIn(int employeeId)
        {

            if (IsAtWork(employeeId))
            {

                // TODO
                //throw new InvalidOperationException("Employee is already at work.");

            }
            else
            {
                timestamp4PunchIn.Add(employeeId, CreateTimestamp(DateTime.Today));
            }
        }

        public void PunchOut(int employeeId)
        {

            if (!IsAtWork(employeeId))
            {

                // TODO
                //throw new InvalidOperationException("Employee is not at work.");

            }
            else
            {
                timestamp4PunchOut.Add(employeeId, CreateTimestamp(DateTime.Today));
            }
        }

        /// <summary>
        /// 打刻用の時間を生成します。
        /// </summary>
        /// <param name="employeeId">従業員のID</param>
        /// <returns>生成された Dictionary<int, DateTime> のオブジェクト</returns>
        private Dictionary<DateTime, DateTime> CreateTimestamp(DateTime today)
        {

            Dictionary<DateTime, DateTime> timestamp = new Dictionary<DateTime, DateTime>();
            timestamp.Add(today, DateTime.Now);

            return timestamp;

        }

        public bool IsAtWork(int employeeId)
        {

            if (timestamp4PunchInTodayDate(employeeId) == timestamp4PunchOutTodayDate(employeeId) + 1)
            {
                return true;
            }
            else
            {
                return false;
            }

            //return timestamp4PunchInCheckData(employeeId)
            //    && !timestamp4PunchOutCheckData(employeeId);
        }

        //public bool timestamp4PunchInCheckData(int employeeId)
        //{
        //    if (timestamp4PunchIn.ContainsKey(DateTime.Today))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public bool timestamp4PunchOutCheckData(int employeeId)
        //{
        //    if (timestamp4PunchOut.ContainsKey(DateTime.Today))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public int timestamp4PunchInTodayDate(int employeeId)
        {
            int cnt = 0;
            DateTime today = DateTime.Today;

            if (timestamp4PunchIn.TryGetValue(employeeId, out Dictionary<DateTime, DateTime> punchInData))
            {
                // 今日のデータが存在する場合、それを使用する
                foreach (var entry in punchInData)
                {
                    if (timestamp4PunchIn[employeeId].ContainsKey(today))
                    {
                        cnt++;
                    }
                }
            }

            return cnt;
        }
        public int timestamp4PunchOutTodayDate(int employeeId)
        {
            int cnt = 0;
            DateTime today = DateTime.Today;

            if (timestamp4PunchOut.TryGetValue(employeeId, out Dictionary<DateTime, DateTime> punchInData))
            {
                // 今日のデータが存在する場合、それを使用する
                foreach (var entry in punchInData)
                {
                    if (timestamp4PunchOut[employeeId].ContainsKey(today))
                    {
                        cnt++;
                    }
                }
            }

            return cnt;
        }

    }

    public class NullTimeTracker : TimeTracker, NullObject
    {

        private static NullTimeTracker instance = new NullTimeTracker();

        private NullTimeTracker()
        {

        }

        public static TimeTracker Instance { get { return instance; } }

        public void PunchIn(int employeeId)
        {

        }

        public void PunchOut(int employeeId)
        {

        }

        public bool IsAtWork(int employeeId)
        {

            return false;

        }

    }

}
