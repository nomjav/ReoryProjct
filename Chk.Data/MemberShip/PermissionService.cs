using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System;

//namespace AcademyLockSmith.Data.MemberShip
//{
//    public class PermissionService
//    {
//        public bool Authorize(PermissionRecord permission)
//        {
//            return Authorize(permission, ALSContext.Current.User);
//        }
//        public virtual bool Authorize(PermissionRecord permission, User user)
//        {
//            if (permission == null)
//                return false;

//            if (user == null)
//                return false;
//            return Authorize(permission.SystemName, user);
//        }
//        public virtual bool Authorize(string permissionRecordSystemName, User user)
//        {
//            if (String.IsNullOrEmpty(permissionRecordSystemName))
//                return false;

//            var userRole = user.Role;
//            if (userRole.Active)
//            {
//                //if (Authorize(permissionRecordSystemName, userRole))
//                //    //yes, we have such permission
//                //    return true;

//                return user.PermissionRecords.Any(permission => permission.SystemName.Equals(permissionRecordSystemName, StringComparison.InvariantCultureIgnoreCase));
//            }
//            //no permission found
//            return false;
//        }
//        protected virtual bool Authorize(string permissionRecordSystemName, Role userRole)
//        {
//            if (String.IsNullOrEmpty(permissionRecordSystemName))
//                return false;

//            foreach (var permission1 in userRole.PermissionRecords)
//                if (permission1.SystemName.Equals(permissionRecordSystemName, StringComparison.InvariantCultureIgnoreCase))
//                    return true;

//            return false;

//        }

//        public bool ReconciledJobsChange(PermissionRecord permission)
//        {
//            User user =  ALSContext.Current.User;
//            if (String.IsNullOrEmpty(permission.SystemName))
//                return false;

//            var userRole = user.Role;
//            if (user.RoleId == 1001)
//            {
//                if (!user.PermissionRecords.Any(p => p.SystemName.Equals(permission.SystemName, StringComparison.InvariantCultureIgnoreCase)) && !userRole.PermissionRecords.Any(p => p.SystemName.Equals(permission.SystemName, StringComparison.InvariantCultureIgnoreCase)))
//                {
//                    return false;
//                }
//                else if (user.PermissionRecords.Any(p => p.SystemName.Equals(permission.SystemName, StringComparison.InvariantCultureIgnoreCase)) && userRole.PermissionRecords.Any(p => p.SystemName.Equals(permission.SystemName, StringComparison.InvariantCultureIgnoreCase)))
//                {
//                    return true;
//                }
//                else if (!user.PermissionRecords.Any(p => p.SystemName.Equals(permission.SystemName, StringComparison.InvariantCultureIgnoreCase)) && userRole.PermissionRecords.Any(p => p.SystemName.Equals(permission.SystemName, StringComparison.InvariantCultureIgnoreCase)))
//                {
//                    return false;
//                }
//                else if (user.PermissionRecords.Any(p => p.SystemName.Equals(permission.SystemName, StringComparison.InvariantCultureIgnoreCase)) && !userRole.PermissionRecords.Any(p => p.SystemName.Equals(permission.SystemName, StringComparison.InvariantCultureIgnoreCase)))
//                {
//                    return true;
//                }
//            }
//            //no permission found
//            return false;

//        }
//    }
//}
