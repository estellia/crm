using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using WebAPIServices.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System.Data;
using CommonModel;
using CommonModel.Models;

namespace WebAPIServices.Controllers
{
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/WebApiServices")]
    public class InterfaceController : ApiController
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        string userName = HttpContext.Current.User.Identity.Name;
        private string userid;
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Interface


        #region 根据Config中配置的用户名和密码创建超级管理员用户名和密码,只有此用户名和密码的人才能继续创建员工，其他人无法创建员工
        /// <summary>
        /// 根据Config中配置的用户名和密码创建超级管理员用户名和密码,只有此用户名和密码的人才能继续创建员工，其他人无法创建员工
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("AddAdminUser")]
        public async Task<MSGReturnModels> AddAdminUser(AddAdminUser token)
        {
            var errMSG = "";
            var returnModel = new MSGReturnModels();
            try
            {
                var localtoken = ConfigurationManager.AppSettings["Token"];
                if (localtoken != token.token)
                {
                    returnModel.ErrMSG = "token不对，你没有权限增加管理员用户";
                    return returnModel;
                }
                var adminUserName = ConfigurationManager.AppSettings["UserName"];
                var adminPassWord = ConfigurationManager.AppSettings["PassWord"];
                var isReset = ConfigurationManager.AppSettings["IsReset"];

                if (isReset == "false")
                {
                    var user = new ApplicationUser() { UserName = adminUserName, Email = adminUserName, SystemCode = "Admin", Active = true, Remark = "管理员用户" };
                    IdentityResult result = UserManager.Create(user, adminPassWord);

                    if (!result.Succeeded)
                    {
                        returnModel.ErrMSG = "创建用户失败" + string.Join(",", result.Errors);
                        return returnModel;
                    }

                }
                else
                {
                    var user =
                        UserManager.Users
                            .FirstOrDefault(p => p.Email == adminUserName && p.UserName == adminUserName);
                    var updateUser = new ApplicationUser() { UserName = adminUserName, Email = adminUserName };
                    UserManager.Delete(user);
                    IdentityResult result = UserManager.Create(user, adminPassWord);
                    returnModel.ErrMSG = "";
                    if (!result.Succeeded)
                    {
                        returnModel.ErrMSG = "创建用户失败" + string.Join(",", result.Errors);
                        return returnModel;
                    }

                }
            }
            catch (Exception ex)
            {
                returnModel.ErrMSG = "创建用户失败" + ex.Message;
                returnModel.SequenceId = null;
                return returnModel;
            }
            return returnModel;
        }

        #endregion

        #region 添加消息主题表和实体表，通过存储过程对SequenceID重新赋值

        /// <summary>
        /// 添加消息主题表和实体表，通过存储过程对SequenceID重新赋值
        /// </summary>
        /// <param name="MSG"></param>
        /// <returns></returns>
        [System.Web.Http.Route("AddMessage")]
        [HttpPost]
        public async Task<MSGReturnModels> AddMessage(MSGBindingModels MSG)
        {
            var returnModel = new MSGReturnModels();
            string errMsgCheckData = string.Empty; //errMsgCheckData=""表示数据无异常，否则表示数据校验错误
            try
            {
                userid = HttpContext.Current.User.Identity.GetUserId();
                var systemCode = UserManager.FindById(userid).SystemCode;
                var oMSG = MSG.OMSG;
                var MSG1 = MSG.MSG1;
                //数据初步验证
                var objectType = string.Empty;
                var transType = string.Empty;
                objectType = oMSG.ObjectType;
                transType = oMSG.TransType;
                if (systemCode.ToUpper() == "SAP")
                {
                    #region SAP接收
                    switch (objectType.ToUpper())
                    {
                        case "ITEMS": //SAP发起
                                      //商品信息同步数据校验
                            errMsgCheckData = CommonModel.Models.DataCheckSubmitBefore.ItemsDataCheck(transType, MSG1.Content);
                            break;
                        case "UDAOREW"://SAP发起
                                       //发货区域
                            errMsgCheckData = CommonModel.Models.DataCheckSubmitBefore.UDAOREWDataCheck(transType, MSG1.Content);
                            break;
                        case "WAREHOUSES"://SAP发起
                                          //仓库主数据
                            errMsgCheckData = CommonModel.Models.DataCheckSubmitBefore.WareshousesDataCheck(transType, MSG1.Content);
                            break;
                        case "UDAOITC"://SAP发起
                                       //物料分类主数据
                            errMsgCheckData = CommonModel.Models.DataCheckSubmitBefore.UDAOITCDataCheck(transType, MSG1.Content);
                            break;
                        case "OITT"://SAP发起
                                    //加工BOM
                            errMsgCheckData = CommonModel.Models.DataCheckSubmitBefore.UDAOITCDataCheck(transType, MSG1.Content);
                            break;
                        case "UDAOLIF"://SAP发起
                                       //线路主数据
                            errMsgCheckData = CommonModel.Models.DataCheckSubmitBefore.UDAOLIFDataCheck(transType, MSG1.Content);
                            break;
                        case "UDSORDER_P"://SAP发起
                                          //订单已分拣状态数据提交
                            errMsgCheckData = CommonModel.Models.DataCheckSubmitBefore.UDSORDER_PDataCheck(transType, MSG1.Content);
                            break;
                        case "UDIOOSO"://SAP发起
                                       //订单发货送态
                            errMsgCheckData = CommonModel.Models.DataCheckSubmitBefore.UDIOOSODataCheck(transType, MSG1.Content);
                            break;
                        case "UORDNL"://SAP发起
                                      //退货入库
                            errMsgCheckData = CommonModel.Models.DataCheckSubmitBefore.UORDNLDataCheck(transType, MSG1.Content);
                            break;
                        case "UORCT"://SAP发起
                                     //收款信息
                            errMsgCheckData = CommonModel.Models.DataCheckSubmitBefore.UORCTDataCheck(transType, MSG1.Content);
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
                else
                {
                    #region SAP接收部分
                    switch (objectType.ToUpper())
                    {
                        case "BusinessPartners"://SAP接收
                                                //会员资料
                            errMsgCheckData = CommonModel.Models.DataCheckSubmitBefore.BusinessPartnersDataCheck(transType, MSG1.Content);
                            break;
                        case "UDSORDR"://SAP接收
                                       //订单同步
                            {
                                if (transType.ToUpper() != "R")
                                {
                                    //订单新增，修改，取消，删除，关闭
                                    errMsgCheckData = CommonModel.Models.DataCheckSubmitBefore.UDSORDRDataCheck(transType, MSG1.Content);
                                }
                                else
                                {
                                    //订单更改配送日期
                                    errMsgCheckData = CommonModel.Models.DataCheckSubmitBefore.UDSORDER_RDataCheck(transType, MSG1.Content);
                                }
                                break;
                            }
                        case "UOINV"://SAP接收
                                     //开票信息
                                     //errMsgCheckData = Models.DataCheckSubmitBefore.UDSORDRDataCheck(transType, MSG1.Content);
                            break;
                        case "UORDNR"://SAP接收
                                     //开票信息
                                    errMsgCheckData = CommonModel.Models.DataCheckSubmitBefore.UORDNRDataCheck(transType, MSG1.Content);
                            break;
                        case "UORCRR"://SAP接收
                                      //退款请求
                                      //errMsgCheckData = Models.DataCheckSubmitBefore.UORDNRDataCheck(transType, MSG1.Content);
                            break;

                        case "UORCTR"://SAP接收
                                      //待收款请求列表
                                      //errMsgCheckData = Models.DataCheckSubmitBefore.UORDNRDataCheck(transType, MSG1.Content);
                            break;
                        default:
                            break;
                    }
                    #endregion 
                }

                //如果同步数据校验错误，则抛出异常
                if (!string.IsNullOrEmpty(errMsgCheckData))
                {
                    returnModel.ErrMSG = errMsgCheckData;
                    returnModel.SequenceId = null;
                    return returnModel;
                }


                var sqlcmd = String.Format(" EXEC DBO.U_IF_GetAutoKey 10 ");
                var sequenceId = _db.Database.SqlQuery<int>(sqlcmd).First();
                oMSG.SequenceID = sequenceId;
                oMSG.UpDateTime = DateTime.Now;
                MSG1.SequenceID = sequenceId;
                oMSG.FromSystem = systemCode;
                oMSG.Status = 0;
                _db.MSG1.Add(MSG1);
                _db.OMSG.Add(oMSG);
                _db.SaveChanges();

                returnModel.ErrMSG = "";
                returnModel.SequenceId = sequenceId;
                return returnModel;

            }
            catch (Exception ex)
            {
                Logger.Writer("AddMessage：\r\n" + ex.Message);
                throw new MyHttpException("AddMessage:", ex);

                //returnModel.ErrMSG = ex.Message + ex.InnerException.InnerException.Message;
                //returnModel.SequenceId = null;
                //return returnModel;
            }
        }
        #endregion


        #region 更新消息实体对象，包含主表和子表
        ///// <summary>
        ///// 更新消息实体对象，包含主表和子表
        ///// </summary>
        ///// <param name="MSG"></param>
        ///// <returns></returns>
        [System.Web.Http.Route("UpdateMessage")]
        [HttpPost]
        public async Task<MSGReturnModels> UpdateMessage(MSGBindingModels MSG)
        {
            var returnModel = new MSGReturnModels();
            try
            {
                var oMSG = MSG.OMSG;
                var MSG1 = MSG.MSG1;
                var objModel = _db.OMSG.Find(oMSG.SequenceID);

                //if (_db.OMSG.Find(oMSG.SequenceID) == null)
                if (objModel == null)
                {
                    returnModel.ErrMSG = "找不到相应的消息对象，无法进行更新";
                    return returnModel;
                }
                oMSG.UpDateTime = DateTime.Now;
                oMSG.Status = objModel.Status + 1;
                _db.MSG1.AddOrUpdate(MSG1);
                _db.OMSG.AddOrUpdate(oMSG);
                //先写入日志表MSG1_LOG,然后再保存（需要记录更新前的XML内容）
                var sqlLog1 = string.Format(@"INSERT INTO DBO.IF_MSG1_LOG( SequenceID,Content ,iLength ,UpdateTime ,UpdateUser)
                                            SELECT T0.SequenceID,T0.Content,T0.iLength,GETDATE(),'{0}'
                                            FROM DBO.IF_MSG1 T0
                                            WHERE T0.SequenceID = {1}", userid, oMSG.SequenceID);
                _db.Database.ExecuteSqlCommand(sqlLog1);
                _db.SaveChanges();
                returnModel.ErrMSG = "";
                returnModel.SequenceId = oMSG.SequenceID;
                return returnModel;

            }
            catch (Exception ex)
            {
                returnModel.ErrMSG = ex.Message + ex.InnerException.InnerException.Message;
                returnModel.SequenceId = MSG.OMSG.SequenceID;
                return returnModel;
            }

        }
        #endregion

        #region 重置消息方法
        ///// <summary>
        ///// 重置消息方法
        ///// </summary>
        ///// <param name="MSG"></param>
        ///// <returns></returns>
        [System.Web.Http.Route("ResetMessageBySequenceID")]
        [HttpPost]
        public async Task<MSGReturnModels> ResetMessageBySequenceID(MSGBindingModels MSG)
        {
            var returnModel = new MSGReturnModels();
            try
            {
                var oMSG = MSG.OMSG;
                var MSG1 = MSG.MSG1;
                var objModel = _db.OMSG.Find(oMSG.SequenceID);

                //if (_db.OMSG.Find(oMSG.SequenceID) == null)
                if (objModel == null)
                {
                    returnModel.ErrMSG = "找不到相应的消息对象，无法进行重置";
                    return returnModel;
                }

                oMSG.UpDateTime = DateTime.Now;
                oMSG.Status = 0;
                _db.MSG1.AddOrUpdate(MSG1);
                _db.OMSG.AddOrUpdate(oMSG);
                //先写入日志表MSG1_LOG,然后再保存（需要记录更新前的XML内容）
                var sqlLog1 = string.Format(@"INSERT INTO DBO.IF_MSG1_LOG( SequenceID,Content ,iLength ,UpdateTime ,UpdateUser)
                                            SELECT T0.SequenceID,T0.Content,T0.iLength,GETDATE(),'{0}'
                                            FROM DBO.IF_MSG1 T0
                                            WHERE T0.SequenceID = {1}", userid, oMSG.SequenceID);
                _db.Database.ExecuteSqlCommand(sqlLog1);
                _db.SaveChanges();
                returnModel.ErrMSG = "";
                returnModel.SequenceId = oMSG.SequenceID;
                return returnModel;

            }
            catch (Exception ex)
            {
                returnModel.ErrMSG = ex.Message + ex.InnerException.InnerException.Message;
                returnModel.SequenceId = MSG.OMSG.SequenceID;
                return returnModel;
            }

        }
        #endregion

        #region 根据SequenceID作废生产表
        [System.Web.Http.Route("DeleteMessageBySequenceID")]
        [HttpPost]
        public async Task<MSGReturnModels> DeleteMessageBySequenceID(int sequenceID)
        {

            var returnModel = new MSGReturnModels();
            try
            {
                var oMSG = _db.OMSG.Find(sequenceID);
                if (oMSG != null)
                {
                    oMSG.Status = -1;
                }
                else
                {
                    returnModel.ErrMSG = "找不到相应的数据";
                    return returnModel;
                }
                _db.SaveChanges();
                returnModel.ErrMSG = "";
                returnModel.SequenceId = sequenceID;
                return returnModel;

            }
            catch (Exception ex)
            {
                returnModel.ErrMSG = ex.Message + ex.InnerException.InnerException.Message;
                returnModel.SequenceId = sequenceID;
                return returnModel;
            }
        }

        #endregion



        #region 直接根据消息实体的SequenceID，获取SequenceId最小的消息内容
        ///// <summary>
        ///// 直接根据消息实体的SequenceID，获取SequenceId最小的消息内容
        ///// </summary>
        ///// <param name="sequenceID"></param>
        ///// <returns></returns>
        [System.Web.Http.Route("GetFirstMesssage")]
        [HttpGet]
        public async Task<MSGBindingModels> GetFirstMesssage()
        {
           
            try
            {
                userid = HttpContext.Current.User.Identity.GetUserId();
                var systemCode = UserManager.FindById(userid).SystemCode;
                //通过此SQL获取当前未处理的SequenceID最小的ID值
                var sqlcmd = string.Format(@"SELECT TOP 1 T0.SequenceID FROM IF_OMSG T0
                                              JOIN IF_OMSG3 T1 on T0.FromSystem = T1.FromSystem AND T0.FromCompany = T1.FromCompany AND T0.Flag = T1.Flag AND T0.ObjectType = T1.ObjectType AND T1.ToSystem = '{0}'
                                              LEFT JOIN IF_MSG2 T2 on T0.SequenceID = T2.SequenceID AND T2.targetsystem = '{0}'
                                            WHERE (T0.Status <> -1) AND (( T2.Status IS NULL) OR (T2.Status <>0 AND T2.Status <= T0.Status))  ORDER BY T0.SequenceID ", systemCode);
                var data = _db.Database.SqlQuery<int>(sqlcmd);
                if (data.Count() > 0)
                {
                    var sequenceId = data.First();
                    var oMSG = _db.OMSG.Find(sequenceId);
                    var MSG1 = _db.MSG1.Find(sequenceId);
                    var MSG = new MSGBindingModels();
                    MSG.OMSG = oMSG;
                    MSG.MSG1 = MSG1;
                    return MSG;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                //return null;
                Logger.Writer("GetFirstMessage：\r\n" + ex.Message);
                throw new MyHttpException("GetFirstMessage:", ex);
            }

        }
        #endregion

        #region GetMessageBySequenceID 根据传入的参数，获取消息实体表的主表和子表的内容,如果获取的system不一致，则返回为null
        /// <summary>
        /// 根据传入的参数，获取消息实体表的主表和子表的内容,如果获取的system不一致，则返回为null
        /// </summary>
        /// <param name="sequenceId"></param>
        /// <returns></returns>
        [System.Web.Http.Route("GetMessageBySequenceID")]
        [HttpGet]
        public async Task<MSGBindingModels> GetMessageBySequenceID()
        {
            var sequenceId = Convert.ToInt32( HttpContext.Current.Request["sequenceId"]);
            try
            {
                var oMSG = _db.OMSG.Find(sequenceId);
                var MSG1 = _db.MSG1.Find(sequenceId);
                var MSG = new MSGBindingModels();
                MSG.OMSG = oMSG;
                MSG.MSG1 = MSG1;
                return MSG;
            }
            catch (Exception ex)
            {
                var MSG = new MSGBindingModels();
                return MSG;
            }

        }
        #endregion

        #region 获取所有权限范围内的SequenceID清单
        /// <summary>
        /// 获取所有权限范围内的SequenceID清单
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.Route("GetMessageSequenceIDList")]
        [HttpGet]
        public async Task<List<int>> GetMessageSequenceIDList()
        {
           
            try
            {
                userid = HttpContext.Current.User.Identity.GetUserId();
                var systemCode = UserManager.FindById(userid).SystemCode;

                var sqlcmd = string.Format(@"SELECT  T0.SequenceID FROM IF_OMSG T0
                                              JOIN IF_OMSG3 T1 on T0.FromSystem = T1.FromSystem AND T0.FromCompany = T1.FromCompany AND T0.Flag = T1.Flag AND T0.ObjectType = T1.ObjectType AND T1.ToSystem = '{0}'
                                              LEFT JOIN IF_MSG2 T2 on T0.SequenceID = T2.SequenceID AND T2.targetsystem = '{0}'
                                            WHERE (T0.Status <> -1) AND
                                                  (( T2.Status IS NULL) OR (T2.Status <>0 AND T2.Status <= T0.Status))  ORDER BY T0.SequenceID ", systemCode);
                var data = _db.Database.SqlQuery<int>(sqlcmd);
                if (data.Count() > 0)
                {
                    var list = new List<int>();
                    foreach (var sequence in data)
                    {
                        if (!list.Contains(sequence))
                        {
                            list.Add(sequence);
                        }
                    }
                    return list;
                }
                return null;
            }
            catch (Exception)
            {
                return null;

            }

        }
        #endregion

        #region 通过传过来的idList来获取所有的消息实体
        /// <summary>
        /// 通过传过来的idList来获取所有的消息实体
        /// </summary>
        /// <param name="sequenceIdList"></param>
        /// <returns></returns>
        [System.Web.Http.Route("GetMessagesBySequenceIDList")]
        [HttpPost]
        public async Task<List<MSGBindingModels>> GetMessagesBySequenceIDList(List<int> sequenceIdList)
        {
            var list = new List<MSGBindingModels>();
            try
            {
                userid = HttpContext.Current.User.Identity.GetUserId();
                var systemCode = UserManager.FindById(userid).SystemCode;

                foreach (int sequenceId in sequenceIdList)
                {
                    var oMSG = _db.OMSG.Find(sequenceId);
                    if (oMSG.FromSystem == systemCode)
                    {
                        var MSG1 = _db.MSG1.Find(sequenceId);
                        var MSG = new MSGBindingModels();
                        MSG.OMSG = oMSG;
                        MSG.MSG1 = MSG1;
                        if (!list.Contains(MSG))
                        {
                            list.Add(MSG);
                        }
                    }

                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        #region 获取top数的符合条件的消息数据

        /// <summary>
        /// 获取top数的符合条件的消息数据
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        [System.Web.Http.Route("GetMessagesByTOPNumber")]
        [HttpGet]
        public async Task<List<MSGBindingModels>> GetMessagesByTOPNumber()
        {
            string top = HttpContext.Current.Request["top"];
            var list = new List<MSGBindingModels>();
            try
            {
                userid = HttpContext.Current.User.Identity.GetUserId();
                var systemCode = UserManager.FindById(userid).SystemCode;
                var sqlcmd = string.Format(@"SELECT TOP {0}  T0.SequenceID FROM IF_OMSG T0
                                            JOIN IF_OMSG3 T1 ON T0.FromSystem = T1.FromSystem AND T0.FromCompany = T1.FromCompany AND T0.Flag = T1.Flag AND T0.ObjectType = T1.ObjectType AND T1.ToSystem = '{1}'
                                            LEFT JOIN IF_MSG2 T2 ON T0.SequenceID = T2.SequenceID AND T2.targetsystem = '{1}'
                                            WHERE (T0.Status <> -1) AND (( T2.Status IS NULL) OR (T2.Status <>0 AND T2.Status <= T0.Status))  ORDER BY T0.SequenceID ", top, systemCode);
                var sequenceIdList = _db.Database.SqlQuery<int>(sqlcmd);
                if (sequenceIdList.Count() > 0)
                {
                    foreach (int sequenceId in sequenceIdList)
                    {
                        var oMSG = _db.OMSG.Find(sequenceId);
                        var MSG1 = _db.MSG1.Find(sequenceId);
                        var MSG = new MSGBindingModels();
                        MSG.OMSG = oMSG;
                        MSG.MSG1 = MSG1;
                        if (!list.Contains(MSG))
                        {
                            list.Add(MSG);
                        }
                    }
                }
                else
                {
                    return null;
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        #endregion

        #region 添加到消费表，并获取下一个处理SequenceID

        /// <summary>
        /// 添加消费表
        /// </summary>
        /// <param name="MSG"></param>
        /// <returns></returns>
        [System.Web.Http.Route("SetMessageOperationResult")]
        [HttpPost]
        public async Task<MSG2ReturnModels> SetMessageOperationResult(MSG2 MSG2)
        {
            var returnModel = new MSG2ReturnModels();
            try
            {
                if (MSG2 == null || MSG2.SequenceID <= 0)
                {
                    returnModel.ErrMSG = string.Format("Set结果设定异常：SequenceID={0}", MSG2 != null ? MSG2.SequenceID : -1);
                    returnModel.NextSequenceID = null;
                    return returnModel;
                }
                userid = HttpContext.Current.User.Identity.GetUserId();
                var systemCode = UserManager.FindById(userid).SystemCode;

                if (!string.IsNullOrEmpty(MSG2.ErrorMSG))
                {
                    var oMSG = _db.OMSG.Find(MSG2.SequenceID);
                    MSG2.Status = oMSG.Status + 1;
                }
                else
                {
                    MSG2.Status = 0;
                }



                var sqlcmdExists = string.Format(@"SELECT  T0.*  FROM  IF_MSG2 T0 WHERE T0.SequenceID = {0} AND T0.TargetSystem = '{1}'  ", MSG2.SequenceID, systemCode);
                var dataExists = _db.Database.SqlQuery<MSG2>(sqlcmdExists);
                if (dataExists.Count() == 0)
                {
                    MSG2.CreateTime = DateTime.Now;
                }
                else
                {
                    MSG2.CreateTime = dataExists.First().CreateTime;
                }
                MSG2.TargetSystem = systemCode;
                MSG2.UpdateTime = DateTime.Now;
                _db.MSG2.AddOrUpdate(MSG2);
                _db.SaveChanges();

                //写入日志表
                var sqlLog2 = string.Format(@"INSERT INTO dbo.IF_MSG2_Log( SequenceID ,TargetSystem ,Status ,ErrorMSG ,CreateTime ,MSGOriContent ,MSGJXContent ,TargetDB ,TargetType ,TargetValue ,Remark ,UpdateUser)
                                                SELECT T0.SequenceID,'{0}',T2.Status,T2.ErrorMSG,GETDATE(),T1.Content,'' AS MSGJXContent,T2.TargetDB,T2.TargetType,T2.TargetValue,N'' AS Remark,'{1}'
                                                FROM DBO.IF_OMSG T0
                                                   JOIN DBO.IF_MSG1 T1 ON T1.SequenceID = T0.SequenceID
                                                   JOIN DBO.IF_MSG2 T2 ON T2.SequenceID = T0.SequenceID AND T2.TargetSystem = '{2}'
                                                WHERE T2.SequenceID = {3}", systemCode, userid, systemCode, MSG2.SequenceID);
                _db.Database.ExecuteSqlCommand(sqlLog2);
                returnModel.ErrMSG = "";
                //成功后返回下一条SequenceID
                var sqlcmd = string.Format(@"SELECT TOP 1 T0.SequenceID 
                                            FROM IF_OMSG T0
                                            JOIN IF_OMSG3 T1 ON T0.FromSystem = T1.FromSystem AND T0.FromCompany = T1.FromCompany AND T0.Flag = T1.Flag AND T0.ObjectType = T1.ObjectType AND T1.ToSystem = '{0}'
                                            LEFT JOIN IF_MSG2 T2 ON T0.SequenceID = T2.SequenceID AND T2.targetsystem = '{0}'
                                            WHERE (T0.Status <> -1) AND  (( T2.status IS NULL) OR (T2.status <>0 AND T2.status <= T0.Status))
                                            ORDER BY T0.SequenceID
                  ", systemCode);
                var data = _db.Database.SqlQuery<int>(sqlcmd);
                if (data.Count() > 0)
                {
                    var nextsequenceId = _db.Database.SqlQuery<int>(sqlcmd).First();
                    returnModel.NextSequenceID = nextsequenceId;

                }
                else
                {
                    returnModel.NextSequenceID = null;

                }
                return returnModel;


            }
            catch (Exception ex)
            {
                returnModel.ErrMSG = ex.Message + ex.InnerException.InnerException.Message;
                returnModel.NextSequenceID = null;
                return returnModel;
            }
        }
        #endregion


        #region 获取错误的列表
        [System.Web.Http.Route("GetMessageErrorList")]
        [HttpGet]
        public async Task<List<OMSGError>> GetMessageErrorList()
        {
            List<OMSGError> list = new List<OMSGError>();
            try
            {
                userid = HttpContext.Current.User.Identity.GetUserId();
                var systemCode = UserManager.FindById(userid).SystemCode;
                var sqlcmd = string.Format(@"SELECT T0.SequenceID,T0.Timestamp,T0.FromCompany,T0.FromSystem,T0.Flag,T0.ObjectType,T2.ObjectName,T0.TransType,T0.FieldNames,T0.FieldValues,T0.Status ,T1.ErrorMsg
                                            FROM DBO.IF_OMSG T0
                                               JOIN dbo.IF_MSG2 T1 ON T1.SequenceID = T0.SequenceID
                                               LEFT JOIN (SELECT T2.FromCompany,T2.FromSystem,T2.ObjectType,MAX(T2.ObjectName) AS ObjectName
                                                          FROM dbo.IF_OMSG3 T2 GROUP BY   T2.FromCompany,T2.FromSystem,T2.ObjectType) AS T2
	                                            ON T0.FromCompany = T2.FromCompany AND T0.FromSystem = T2.FromSystem AND T0.ObjectType = T2.ObjectType
                                            WHERE T1.Status !=0  AND T0.Status != -1 AND T0.FromSystem = '{0}'", systemCode);
                var errList = _db.Database.SqlQuery<OMSGError>(sqlcmd);
                if (errList.Count() > 0)
                {
                    foreach (OMSGError oModel in errList)
                    {
                        OMSGError oErrorModel = new OMSGError();
                        oErrorModel.SequenceID = oModel.SequenceID;
                        oErrorModel.Timestamp = oModel.Timestamp;
                        oErrorModel.FromCompany = oModel.FromCompany;
                        oErrorModel.FromSystem = oModel.FromSystem;
                        oErrorModel.Flag = oModel.Flag;
                        oErrorModel.ObjectType = oModel.ObjectType;
                        oErrorModel.ObjectName = oModel.ObjectName;
                        oErrorModel.TransType = oModel.TransType;
                        oErrorModel.FieldNames = oModel.FieldNames;
                        oErrorModel.FieldValues = oModel.FieldValues;
                        oErrorModel.Status = oModel.Status;
                        oErrorModel.ErrorMsg = oModel.ErrorMsg;
                        list.Add(oErrorModel);
                    }
                }
                else
                {
                    return null;
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        #region 获取SAP的物料编号获取每个物料对应库存数据
        [System.Web.Http.Route("GetSAPOnHandForWarehouceByItemCode")]
        [HttpGet]
        public async Task<List<SKUOnHand>> GetSAPOnHandForWarehouceByItemCode()
        {
            string itemCode = HttpContext.Current.Request["itemCode"];
            //string whsCode = HttpContext.Current.Request["whsCode"];
            List<SKUOnHand> list = new List<SKUOnHand>();

            try
            {
                string oSqlText = @"SELECT T0.ItemCode,T0.ItemName,T3.Code AS LocationCode,T3.Name AS LocationName
                                    , T1.WhsCode AS WarehouseCode,T2.WhsName AS WarehouseName
                                    ,T1.OnHand , T0.InvntryUom
                                FROM dbo.OITM T0
                                   JOIN dbo.OITW T1 ON T0.ItemCode = T1.ItemCode
                                   JOIN dbo.OWHS T2 ON T1.WhsCode = T2.WhsCode
                                   LEFT JOIN dbo.[@U_DAOREW] T3 ON T2.U_Region1 = T3.Code
                                WHERE T0.ItemCode = '{0}' AND  T1.OnHand <> 0";
                DataSet dsOnHand = SqlHelper.GetDataSet(string.Format(oSqlText, itemCode));
                if (dsOnHand.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsOnHand.Tables[0].Rows)
                    {
                        SKUOnHand oModel = new SKUOnHand();
                        oModel.ItemCode = dr["ItemCode"].ToString();
                        oModel.LocationCode = dr["LocationCode"].ToString();
                        oModel.LocationName = dr["LocationName"].ToString();
                        oModel.WarehouseCode = dr["WarehouseCode"].ToString();
                        oModel.WarehouseName = dr["WarehouseName"].ToString();
                        if (string.IsNullOrEmpty(dr["OnHand"].ToString()))
                            oModel.OnHand = 0;
                        else
                            oModel.OnHand = Convert.ToDecimal(dr["OnHand"].ToString());
                        oModel.InvntryUom = dr["InvntryUom"].ToString();
                        list.Add(oModel);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {

                return null;
            }


        }
        #endregion

        #region 获取SAP的物料编号获取对应库存数据，不分仓库
        [System.Web.Http.Route("GetSAPOnHandByItemCode")]
        [HttpGet]
        public async Task<List<SKUOnHand>> GetSAPOnHandByItemCode()
        {
            string itemCode = HttpContext.Current.Request["itemCode"];
            List<SKUOnHand> list = new List<SKUOnHand>();

            try
            {
                string oSqlText = @"SELECT T0.ItemCode,T0.ItemName,T3.Code AS LocationCode,T3.Name AS LocationName
                                    , N'' AS WarehouseCode,N'' AS WarehouseName
                                    ,SUM(T1.OnHand) AS OnHand , MAX(T0.InvntryUom) AS InvntryUom
                                FROM dbo.OITM T0
                                   JOIN dbo.OITW T1 ON T0.ItemCode = T1.ItemCode
                                   JOIN dbo.OWHS T2 ON T1.WhsCode = T2.WhsCode
                                   LEFT JOIN dbo.[@U_DAOREW] T3 ON T2.U_Region1 = T3.Code
                                WHERE T0.ItemCode = '{0}' AND  T1.OnHand <> 0
                                GROUP BY T0.ItemCode,T0.ItemName,T3.Code ,T3.Name
                               ";
                DataSet dsOnHand = SqlHelper.GetDataSet(string.Format(oSqlText, itemCode));
                if (dsOnHand.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsOnHand.Tables[0].Rows)
                    {
                        SKUOnHand oModel = new SKUOnHand();
                        oModel.ItemCode = dr["ItemCode"].ToString();
                        oModel.LocationCode = dr["LocationCode"].ToString();
                        oModel.LocationName = dr["LocationName"].ToString();
                        oModel.WarehouseCode = dr["WarehouseCode"].ToString();
                        oModel.WarehouseName = dr["WarehouseName"].ToString();
                        if (string.IsNullOrEmpty(dr["OnHand"].ToString()))
                            oModel.OnHand = 0;
                        else
                            oModel.OnHand = Convert.ToDecimal(dr["OnHand"].ToString());
                        oModel.InvntryUom = dr["InvntryUom"].ToString();
                        list.Add(oModel);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {

                return null;
            }


        }
        #endregion


        #region 获取SAP物料编号获取对应库存数据，不分仓库
        [System.Web.Http.Route("GetSAPOnHandAllItemCode")]
        
        [HttpGet]

        public async Task<List<SKUOnHand>> GetSAPOnHandAllItemCode()
        {
            List<SKUOnHand> list = new List<SKUOnHand>();

            try
            {
                string oSqlText = @"SELECT T0.ItemCode,T0.ItemName,T3.Code AS LocationCode,T3.Name AS LocationName
                                    , T1.WhsCode AS WarehouseCode,T2.WhsName AS WarehouseName
                                    ,T1.OnHand , T0.InvntryUom
                                FROM dbo.OITM T0
                                   JOIN dbo.OITW T1 ON T0.ItemCode = T1.ItemCode
                                   JOIN dbo.OWHS T2 ON T1.WhsCode = T2.WhsCode
                                   LEFT JOIN dbo.[@U_DAOREW] T3 ON T2.U_Region1 = T3.Code
                                WHERE   T1.OnHand <> 0";
                DataSet dsOnHand = SqlHelper.GetDataSet(oSqlText.ToString());
                if (dsOnHand.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsOnHand.Tables[0].Rows)
                    {
                        SKUOnHand oModel = new SKUOnHand();
                        oModel.ItemCode = dr["ItemCode"].ToString();
                        oModel.LocationCode = dr["LocationCode"].ToString();
                        oModel.LocationName = dr["LocationName"].ToString();
                        oModel.WarehouseCode = dr["WarehouseCode"].ToString();
                        oModel.WarehouseName = dr["WarehouseName"].ToString();
                        if (string.IsNullOrEmpty(dr["OnHand"].ToString()))
                            oModel.OnHand = 0;
                        else
                            oModel.OnHand = Convert.ToDecimal(dr["OnHand"].ToString());
                        oModel.InvntryUom = dr["InvntryUom"].ToString();
                        list.Add(oModel);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {

                return null;
            }

        }
        #endregion


        #region 获取SAP物料编号获取对应库存数据，根据区域 
        [System.Web.Http.Route("GetSAPOnHandByLocationCode")]
        [HttpGet]
        public async Task<List<SKUOnHand>> GetSAPOnHandByLocationCode()
        {
            string locationCode = HttpContext.Current.Request["locationCode"];
            List<SKUOnHand> list = new List<SKUOnHand>();

            try
            {
                string oSqlText = @"SELECT T0.ItemCode,T0.ItemName,T3.Code AS LocationCode,T3.Name AS LocationName
                                    ,T1.WhsCode AS WarehouseCode,T2.WhsName AS WarehouseName
                                    ,T1.OnHand , T0.InvntryUom
                                FROM dbo.OITM T0
                                   JOIN dbo.OITW T1 ON T0.ItemCode = T1.ItemCode
                                   JOIN dbo.OWHS T2 ON T1.WhsCode = T2.WhsCode
                                   LEFT JOIN dbo.[@U_DAOREW] T3 ON T2.U_Region1 = T3.Code
                                WHERE  T2.U_Region1 = '{0}' AND T1.OnHand <> 0 ";
                DataSet dsOnHand = SqlHelper.GetDataSet(string.Format(oSqlText, locationCode));
                if (dsOnHand.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsOnHand.Tables[0].Rows)
                    {
                        SKUOnHand oModel = new SKUOnHand();
                        oModel.ItemCode = dr["ItemCode"].ToString();
                        oModel.LocationCode = dr["LocationCode"].ToString();
                        oModel.LocationName = dr["LocationName"].ToString();
                        oModel.WarehouseCode = dr["WarehouseCode"].ToString();
                        oModel.WarehouseName = dr["WarehouseName"].ToString();
                        if (string.IsNullOrEmpty(dr["OnHand"].ToString()))
                            oModel.OnHand = 0;
                        else
                            oModel.OnHand = Convert.ToDecimal(dr["OnHand"].ToString());
                        oModel.InvntryUom = dr["InvntryUom"].ToString();
                        list.Add(oModel);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {

                return null;
            }
            finally
            {
                list = null;
            }
        }
        #endregion

    }
    
}