using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JIT.TradeCenter.Entity.HuiFuPayEntity
{
    public enum HuiFuInterfaceEnum
    {
        #region 用户管理类
        /// <summary>
        /// 持卡人登录
        /// </summary>
        [Description("持卡人登录")]
        cardHolderLogon,
        #endregion

        #region 查询类
        /// <summary>
        /// 会员绑定预付卡列表
        /// </summary>
        [Description("会员绑定预付卡列表")]
        userRelCardsQuery,

        /// <summary>
        /// 预付卡查询
        /// </summary>
        [Description("预付卡查询")]
        cardDetailQuery,

        /// <summary>
        /// 商户登录
        /// </summary>
        [Description("商户登录")]
        merchantLogon,

        /// <summary>
        /// 交易查询
        /// </summary>
        [Description("交易查询")]
        transQuery,

        ///// <summary>
        ///// 会员绑定预付卡列表
        ///// </summary>
        //[Description("会员绑定预付卡列表")]
        //userRelCardsQuery,

        /// <summary>
        /// 会员卡片支持门店列表
        /// </summary>
        [Description("会员卡片支持门店列表")]
        userCardSupprotShopQuery,

        /// <summary>
        /// 查询交易信息
        /// </summary>
        [Description("查询交易信息")]
        queryTrans,

        /// <summary>
        /// 查询单笔交易状态
        /// </summary>
        [Description("查询单笔交易状态")]
        qryTxnState,


        #endregion

        #region 管理类
        /// <summary>
        /// 卡密修改
        /// </summary>
        [Description("卡密修改")]
        chgCardPwd,

        /// <summary>
        /// 重置密码
        /// </summary>
        [Description("重置密码")]
        resetCardPwd,

        /// <summary>
        /// 虚拟卡申请
        /// </summary>
        [Description("虚拟卡申请")]
        appVirtualCard,

        /// <summary>
        /// 会员录入
        /// </summary>
        [Description("会员录入")]
        member,

        /// <summary>
        /// 会员登录密码修改
        /// </summary>
        [Description("会员登录密码修改")]
        mobileAppVirCard,

        /// <summary>
        /// 会员登录密码修改
        /// </summary>
        [Description("会员登录密码修改")]
        loginPwdUpd,

        /// <summary>
        /// 会员登陆
        /// </summary>
        [Description("会员登陆")]
        userLogon,

        #endregion

        #region 交易类
        /// <summary>
        /// 充值
        /// </summary>
        [Description("充值")]
        recharge,
        /// <summary>
        /// 消费
        /// </summary>
        [Description("消费")]
        consumption,

        /// <summary>
        /// 卡慧充值
        /// </summary>
        [Description("卡慧充值")]
        rechargeRequest,
        #endregion
        
        #region 辅助类
        /// <summary>
        /// 短信动态码申请
        /// </summary>
        [Description("短信动态码申请")]
        getVerificationCode,
        #endregion
    }
}
