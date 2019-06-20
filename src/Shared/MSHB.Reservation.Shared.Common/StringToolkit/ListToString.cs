using System;
using System.Collections.Generic;
using System.Linq;

namespace MSHB.Reservation.Shared.Common.StringToolkit
{
    public class ListToString
    {
        public static string GetStringValueOfList(List<Guid> ids)
        {
            string seIdList = "";
            if (ids == null || !ids.Any()) return seIdList;
            foreach (Guid seId in ids)
            {
                if (!string.IsNullOrWhiteSpace(seIdList))
                    seIdList += ",";
                seIdList += "'" + seId + "'";
            }
            return seIdList;
        }

        public static string GetStringValueOfList(List<string> ids)
        {
            string seIdList = "";
            if (ids == null || !ids.Any()) return seIdList;
            foreach (string seId in ids)
            {
                if (!string.IsNullOrWhiteSpace(seIdList))
                    seIdList += ",";
                seIdList += "'" + seId + "'";
            }
            return seIdList;
        }

        public static string GetStringValueOfList(List<int> ids)
        {
            string seIdList = "";
            if (ids == null || !ids.Any()) return seIdList;
            foreach (int seId in ids)
            {
                if (!string.IsNullOrWhiteSpace(seIdList))
                    seIdList += ",";
                seIdList += "'" + seId + "'";
            }
            return seIdList;
        }

        public static string GetStringValueOfList(List<long> ids)
        {
            string seIdList = "";
            if (ids == null || !ids.Any()) return seIdList;
            foreach (long seId in ids)
            {
                if (!string.IsNullOrWhiteSpace(seIdList))
                    seIdList += ",";
                seIdList += "'" + seId + "'";
            }
            return seIdList;
        }
    }
}
