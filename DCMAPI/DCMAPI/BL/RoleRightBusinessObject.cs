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
    public class RoleRightBusinessObject
    {
        public ObjectResultSet<DefaultModel> CreateRoleRight(UpdateRoleRightsArgs args)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();

            SqlParameter[] paramList = null;
            var roleRights = args.role_rights.Where(x => x.data_view == true || x.data_edit == true || x.data_delete == true || x.data_add == true).ToList();
            if (roleRights.Count > 0)
                result = DeleteRoleRights(roleRights[0].role_id);
            if (result.success)
            {
                foreach (var r in roleRights)
                {

                    paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Add),
                                                            new SqlParameter("@role_id1", r.role_id),
                                                            new SqlParameter("@menu_id1", r.menu_id),
                                                            new SqlParameter("@data_view1", r.data_view),
                                                            new SqlParameter("@data_add1", r.data_add),
                                                            new SqlParameter("@data_edit1", r.data_edit),
                                                            new SqlParameter("@data_delete1", r.data_delete),
                                                            new SqlParameter("@user_name1", r.user_name)
                    };
                    using (var ctx = new DCMContext())
                    {
                        using (var transaction = ctx.Database.BeginTransaction())
                        {
                            try
                            {
                                string strQuery = "EXEC USP_ROLE_RIGHTS " + MiscUtils.GenerateParameterString(paramList.ToList());
                                ctx.Database.ExecuteSqlCommand(strQuery, paramList);
                                transaction.Commit();
                                result.success = true;
                                result.status = "success";
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                result.success = false;
                                result.status = ex.Message;
                            }
                        }
                    }
                    paramList = null;
                }
            }
            return result;
        }

        public ObjectResultSet<List<RoleRightModel>> GetRoleRightDetail(RoleRightInputArgs args)
        {
            ObjectResultSet<List<RoleRightModel>> result = new ObjectResultSet<List<RoleRightModel>>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Retrive),
                                                            new SqlParameter("@role_id1", args.role_id)
            };

            SingleResultSet<RoleRightModel> sglresult = new SingleResultSet<RoleRightModel>();
            try
            {
                using (var dc = new DCMContext())
                {
                    string strQuery = "EXEC USP_ROLE_RIGHTS " + MiscUtils.GenerateParameterString(paramList.ToList());
                    sglresult = dc.GetSingleResultSet<RoleRightModel>(strQuery, paramList);
                }
                var input = sglresult.ResultSet.ToArray();
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
        public IEnumerable<RoleRightModel> BuildTree(RoleRightModel current, RoleRightModel[] allItems)
        {
            var childs = allItems.Where(c => c.menu_parent == current.menu_id).ToArray();
            foreach (var child in childs)
                child.items = BuildTree(child, allItems);
            current.items = childs;
            return childs;
        }

        public ObjectResultSet<DefaultModel> EditRoleRight(UpdateRoleRightsArgs args)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            SqlParameter[] paramList = null;
            var roleRights = args.role_rights.Where(x => x.data_view == true || x.data_edit == true || x.data_delete == true || x.data_add == true).ToList();
            if (roleRights.Count > 0)
                result = DeleteRoleRights(roleRights[0].role_id);
            if (result.success)
            {
                foreach (var r in roleRights)
                {

                    paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Add),
                                                            new SqlParameter("@role_id1", r.role_id),
                                                            new SqlParameter("@menu_id1", r.menu_id),
                                                            new SqlParameter("@data_view1", r.data_view),
                                                            new SqlParameter("@data_add1", r.data_add),
                                                            new SqlParameter("@data_edit1", r.data_edit),
                                                            new SqlParameter("@data_delete1", r.data_delete),
                                                            new SqlParameter("@user_name1", r.user_name)
                    };

                    using (var ctx = new DCMContext())
                    {
                        using (var transaction = ctx.Database.BeginTransaction())
                        {
                            try
                            {
                                string strQuery = "EXEC USP_ROLE_RIGHTS " + MiscUtils.GenerateParameterString(paramList.ToList());
                                ctx.Database.ExecuteSqlCommand(strQuery, paramList);
                                transaction.Commit();
                                result.success = true;
                                result.status = "success";
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                result.success = false;
                                result.status = ex.Message;
                            }
                        }
                    }
                    paramList = null;
                }
            }
            return result;
        }

        private ObjectResultSet<DefaultModel> DeleteRoleRights(int role_id)
        {
            ObjectResultSet<DefaultModel> result = new ObjectResultSet<DefaultModel>();
            SqlParameter[] paramList = new SqlParameter[] { new SqlParameter("@mode1", MiscUtils.Action.Delete),
                                                            new SqlParameter("@role_id1", role_id)
            };
            try
            {
                using (var ctx = new DCMContext())
                {
                    using (var transaction = ctx.Database.BeginTransaction())
                    {
                        try
                        {
                            string strQuery = "EXEC USP_ROLE_RIGHTS " + MiscUtils.GenerateParameterString(paramList.ToList());
                            ctx.Database.ExecuteSqlCommand(strQuery, paramList);
                            transaction.Commit();
                            result.success = true;
                            result.status = "success";
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            result.success = false;
                            result.status = ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.status = ex.Message;
                throw;
            }
            return result;
        }
    }
}