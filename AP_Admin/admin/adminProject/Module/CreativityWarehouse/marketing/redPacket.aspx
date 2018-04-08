<!DOCTYPE html>
<html>
<head>
    <title>创意活动管理</title>
     <link href="../../static/css/easyui.css" rel="stylesheet" type="text/css"/>
     <link href="../../static/css/kkpager.css" rel="stylesheet" type="text/css" />
       <link href="../../styles/common-layout.css" rel="stylesheet" type="text/css"  />
     <link href="../../styles/css/newYear/skin02.css" rel="stylesheet" type="text/css"  />
     <link href="css/queryList.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <div class="allPage" id="section" data-js="js/redPacket">
          <div class="optionBtn">
          <div class="commonBtn w80" id="getBack">返回</div>

</div><!--optionBtn-->
<div class="spreadPanel" >
<div class="phonePanel"  data-imgcode="BackGroundImageUrl">
   <div class="draggable"><img src="images/logo.png" data-imgcode="LogoImageUrl" width="39" height="39"></div>

   <div class="action"> <div><img src="images/packetClose.png" width="182"   data-imgcode="NotReceiveImageUrl"/>  <img src="images/packetOpen.png" width="182" class="hide" data-imgcode="ReceiveImageUrl" /> <em> 活动规则 <i>&gt;</i> </em></div> </div>

</div>


</div> <!--spreadPanel-->
<form id="optionForm">
<div class="subMitPanel">
                    <div class="commonSelectWrap">
                  <em class="tit">活动名称：</em>
                   <div class="searchInput">
                     <input   name="Title" class="easyui-validatebox"  data-options="required:true" type="text" value=""/>
                   </div>
               </div>
                    <div class="lineText">
                   <div class="inputBox">
                   <em class="tit">LOGO图片：</em>

                     <div class="uploadTip" style="left: 78px;bottom:10px;">
                       <div class="uploadBtn btn">
                           <em class="upTip">上传图片</em>
                            <div class="jsUploadBtn"  data-imgcode="LogoImageUrl" data-msg="LOGO图片" data-batid="Logo"></div>
                        </div><!--uploadBtn-->
                        <div class="tip" >图片格式：PNG，建议尺寸：90px*90px，大小10K以内</div>
                        </div> <!--uploadTip-->
                   </div> <!--inputBox-->
               </div><!--lineText-->
                    <div class="lineText">
                   <div class="inputBox">
                   <em class="tit">背景图片：</em>

                     <div class="uploadTip" style="left: 78px;bottom:10px;">
                       <div class="uploadBtn btn">
                           <em class="upTip">上传图片</em>
                            <div class="jsUploadBtn"  data-imgcode="BackGroundImageUrl"  data-msg="背景图片" data-batid="BackGround" ></div>
                        </div><!--uploadBtn-->
                        <div class="tip" >图片格式：JPG，建议尺寸640px*1008px：，大小10K以内


                        </div>
                        </div> <!--uploadTip-->
                   </div> <!--inputBox-->
               </div><!--lineText-->
                    <div class="lineText">
                   <div class="inputBox">
                   <em class="tit">未领取图片：</em>

                     <div class="uploadTip" style="left: 78px;bottom:10px;">
                       <div class="uploadBtn btn">
                           <em class="upTip">上传图片</em>
                            <div class="jsUploadBtn"  data-imgcode="NotReceiveImageUrl"  data-msg="未领取的图片" data-batid="NotReceive" ></div>
                        </div><!--uploadBtn-->
                        <div class="tip" >图片格式：JPG，建议尺寸：420px*600px，大小10K以内</div>
                        </div> <!--uploadTip-->
                        <div class="btnText"  data-imgcode="NotReceiveImageUrl">查看未领取图片</div>
                   </div> <!--inputBox-->
               </div><!--lineText-->
                    <div class="lineText">
                   <div class="inputBox">
                   <em class="tit" >已领取图片：</em>

                     <div class="uploadTip" style="left: 78px;bottom:10px;">
                       <div class="uploadBtn btn">
                           <em class="upTip">上传图片</em>
                            <div class="jsUploadBtn"  data-imgcode="ReceiveImageUrl"  data-msg="已领取的图片" data-batid="Receive" ></div>
                        </div><!--uploadBtn-->
                        <div class="tip" >图片格式：PNG，建议尺寸：420px*700px，大小10K以内</div>
                        </div> <!--uploadTip-->
                       <div class="btnText" data-imgcode="ReceiveImageUrl">查看已领取图片</div>
                   </div> <!--inputBox-->
               </div><!--lineText-->

                    <div class="commonSelectWrap" style="display: none">
                  <em class="tit">规则内容：</em>
                   <div class="selectBox" style="height: 120px;">
                         <input id="picText" class="easyui-combobox" data-options="
                         		valueField: 'label',
                         		textField: 'value',
                         		width:200,
                         		height:30,
                         		data: [{
                         			label: '1',
                         			value: '文字',
                         			selected:true
                         		},{
                         			label: '2',
                         			value: '图片'
                         		}]" />

                    <div class="picTextPanel">
                    <textarea name="Description" class="easyui-validatebox"  data-options="required:true,validType:'maxLength[5000]'"  >详细规则请咨询正念官方。</textarea>
                     <div class="lineText">
                         <div class="uploadBtn btn">
                               <em class="upTip">上传图片</em>
                                     <div class="jsUploadBtn"  data-imgcode="RuleImageUrl"  data-msg="规则图片" data-batid="Rule" ></div>
                                </div><!--uploadBtn-->
                                                                    <div class="tip" >建议图片格式：PNG，大小10K以内</div>
                        </div><!--lineText-->
                    </div>
                   </div>
               </div>
</div>
<div class="optionBtn" >
 <div class="commonBtn" style="margin-left: 560px;" id="subMitBtn">保存</div>
</div>


</form>
 </div>
    <script type="text/javascript" src="../../static/js/lib/require.js"  defer  async="true" data-main="../../static/js/main.js"></script>

</body>
</html>
