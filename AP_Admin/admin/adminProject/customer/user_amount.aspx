<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" Inherits="customer_user_amount" Codebehind="user_amount.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>会员充值</title>
    <style type="text/css">
*{margin:0;padding:0;}

.fe7c23{color:#fe7c23;}
.vip_recharge_area{padding:25px;}
.vip_recharge_area .tit_tag{margin-bottom:55px;padding:0 10px;border-left:3px solid #fe7c23;}
.vip_recharge_area .tit_tag span{display:inline-block;color:#fe7c23;}
.vip_recharge_area .tit_tag .on{color:#666;}

.phone_num_seaech{margin-bottom:55px;}
.phone_num_seaech em{display:inline-block;height:32px;line-height:32px;font-style:normal;color:#999;}
.phone_num_seaech .inputBox{display:inline-block;margin:0 20px 0 5px;}
.phone_num_seaech .inputBox input{border-radius:2px;height:29px;text-indent:5px;border:1px solid #ccc;background:#fff;}
.phone_num_seaech .queryBtn{display:inline-block;width:64px;height:32px;line-height:32px;text-align:center;border-radius:3px;background:#fe7c23;color:#fff; cursor:pointer}

.tit_tag_line{padding:8px 0;font-weight:bold;border-bottom:1px dotted #bebdbd;color:#666;}

.query_result_area{width:445px;margin:0 0 100px 65px;;padding:30px 0;font-size:14px;font-weight:bold;color:#666;}
.query_result_area .lineBox{height:38px;}
.query_result_area .leftBox{float:left;}
.query_result_area .rightBox{float:right;}
.query_result_area .inputBox{display:inline-block;margin:0 20px 0 5px;}
.query_result_area .inputBox input{border-radius:2px;width:340px;height:29px;text-indent:5px;border:1px solid #ccc;background:#fff;}
.query_result_area .queryBtn{display:inline-block;width:64px;height:32px;line-height:32px;text-align:center;border-radius:3px;background:#fe7c23;color:#fff;}
.query_result_area .notice{padding:10px 0 0 5px;color:#fe7c23;}
</style>
    <link href="../js/plugin/artDialog/artDialog.css" rel="stylesheet" type="text/css" />
    <script src="../js/plugin/artDialog/artDialog.js" type="text/javascript"></script>
    <script type="text/javascript">
        function loadItems() {
            var phoneNumber = $("#txt_phoneNumber").val();
            if (phoneNumber == "") {
                alert("请输入电话号码！");
                return;
            }
            if (!(/^1[3|4|5|8][0-9]\d{4,8}$/.test(phoneNumber))) {
                alert("不是完整的11位手机号或者正确的手机号前七位");               
                return;
            } 

            var reqContent = { "Parameters": { "Mobile": phoneNumber} };
            var jdata = { action: 'GetMember', ReqContent: JSON.stringify(reqContent) };
            $.ajax({
                url: "user_amount.aspx",
                type: 'post',
                data: jdata,
                dataType: 'json',
                success: function (msg) {
                    if (msg.ResultCode == 200) {
                        var jsonResult = msg.Data;
                        $("#dis_MemberID").text(jsonResult.MemberID);
                        $("#dis_UserName").text(jsonResult.UserName);
                        $("#dis_MemberNo").text(jsonResult.MemberNo);
                        $("#dis_ALDAmount").text(jsonResult.ALDAmount + "丁阿拉币");
                    } else {
                        $("#dis_MemberID").text("");
                        $("#dis_UserName").text("");
                        $("#dis_MemberNo").text("");
                        $("#dis_ALDAmount").text("");
                    }

                },
                error: function (err) {
                    alert(err);
                }
            });
        }

        function saveItems() {
            // var self = app;
            var memberID = $("#dis_MemberID").text();
            var amount = $("#txt_amount").val();
            if (isNaN(amount) || parseFloat(amount) < 0) {
                alert("充值数额必须是大于0的数字");
                return;
            }
            var reqContent = { "Parameters": { "MemberID": memberID, "Amount": amount * 100, "AmountSourceId": "12"} };
            var jdata = { action: 'AddAmountDetail', ReqContent: JSON.stringify(reqContent) };
            $.ajax({
                url: "user_amount.aspx",
                type: 'post',
                data: jdata,
                dataType: 'json',
                success: function (msg) {
                    if (msg.ResultCode == 200) {
                        debugger;
                        var money = $("#txt_amount").val()
                        var d = dialog({
                            title: "",
                            content: "您已充值成功：<br/>" + money + "元人民币" + "(" + parseFloat(money) * 100 + "阿拉丁币)",
                            fixed: true, okValue: '确定',
                            ok: function () {
                                $("#txt_amount").val("");
                                loadItems();
                            },
                            cancelValue: '继续充值',
                            cancel: function () {
                                $("#txt_amount").val("");
                                $("#dis_MemberID").text("");
                                $("#dis_UserName").text("");
                                $("#dis_MemberNo").text("");
                                $("#dis_ALDAmount").text("");
                                $("#txt_phoneNumber").val("");
                            }

                        });
                        d.showModal();
                    }
                },
                error: function (err) {
                    alert(err);
                }
            });
        }

        $(function () {
            $("#queryBtn").click(function () {
                loadItems();
            });
            $("#addAmount").click(function () {
                saveItems();
            });

        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="vip_recharge_area">
        <div class="tit_tag">
            <span>会员管理</span> <span>》</span> <span class="on">会员充值</span>
        </div>
        <div class="phone_num_seaech">
            <em>手机号：</em>
            <p class="inputBox">
                <input type="text" value="" id="txt_phoneNumber" /></p>
            <span class="queryBtn" id="queryBtn">查询</span>
        </div>
        <div class="tit_tag_line">
            查询结果</div>
        <div class="query_result_area">
            <div class="lineBox">
                <p class="leftBox">
                    会员名：<span id="dis_UserName"></span></p>
                <p class="rightBox">
                    手机号：<span id="dis_MemberNo"></span></p>
            </div>
            <div class="lineBox">
                <p class="">
                    当前余额：<span class="fe7c23" id="dis_ALDAmount"></span></p>
                <span id="dis_MemberID" style="display: none"></span>
            </div>
            <div class="lineBox">
                <p class="inputBox">
                    <input type="text" placeholder="请输入充值数额（元）" id="txt_amount" /></p>
                <span class="queryBtn" id="addAmount">充值</span>
                <p class="notice">
                    注：100丁阿拉币=1元人民币</p>
            </div>
        </div>
        <div class="tit_tag_line">
            充值历史</div>
    </div>
</asp:Content>
