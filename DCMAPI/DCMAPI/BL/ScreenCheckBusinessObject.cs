using DCMAPI.Arguments;
using DCMAPI.Common;
using DCMAPI.DBAccess;
using DCMAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DCMAPI.BL
{
    public class ScreenCheckBusinessObject
    {
        public ObjectResultSet<List<ScreenCheckModel>> CheckScreenDetail(ScreenCheckInputArgs args)
        {
            ObjectResultSet<List<ScreenCheckModel>> result = new ObjectResultSet<List<ScreenCheckModel>>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@role_id1", args.role_id),
                                                            new SqlParameter("@screen_name1", args.screen_name),

            };
            SingleResultSet<ScreenCheckModel> sglresult = new SingleResultSet<ScreenCheckModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_CHECK_SCREEN_ACCESS " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<ScreenCheckModel>(strQuery, paramList);
                }
                result.data = sglresult.ResultSet.ToList();
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
                throw;
            }
            return result;
        }

        public ObjectResultSet<List<ScreenModel>> GetScreenDetail(ScreenCheckInputArgs args)
        {
            ObjectResultSet<List<ScreenModel>> result = new ObjectResultSet<List<ScreenModel>>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Retrive)

            };
            SingleResultSet<ScreenModel> sglresult = new SingleResultSet<ScreenModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_SCREEN " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<ScreenModel>(strQuery, paramList);
                }
                result.data = sglresult.ResultSet.ToList();
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
                throw;
            }
            return result;
        }

        public ObjectResultSet<List<MenuModel>> GetMenuDetail(RoleRightInputArgs args)
        {
            ObjectResultSet<List<MenuModel>> result = new ObjectResultSet<List<MenuModel>>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Retrive),
                                                            new SqlParameter("@role_id1", args.role_id)
            };

            SingleResultSet<MenuModel> sglresult = new SingleResultSet<MenuModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_ROLE_RIGHTS " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<MenuModel>(strQuery, paramList);
                }

                var input = sglresult.ResultSet.Where(x => x.role_id == args.role_id).ToArray();
                var roots = input.Where(i => i.menu_parent == 0);
                foreach (var root in roots)
                    BuildTree(root, input);
                result.data = roots.ToList();
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
                throw;
            }

            return result;
        }

        public IEnumerable<MenuModel> BuildTree(MenuModel current, MenuModel[] allItems)
        {
            var childs = allItems.Where(c => c.menu_parent == current.menu_id).ToArray();
            foreach (var child in childs)
                child.items = BuildTree(child, allItems);
            current.items = childs;
            return childs;
        }
    }
}