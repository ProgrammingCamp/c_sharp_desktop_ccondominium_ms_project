using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condominium_Management_System
{
    public class SecurityPolicy
    {
        public bool IsInRole(string username, string role)
        {
            CondominiumManagementSystemDBEntities entity = new Condominium_Management_System.CondominiumManagementSystemDBEntities();
            int count = entity.tblUserRoles.Where(x => x.tblUser.Username == username && x.tblRole.Title == role).Count();

            if(count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
