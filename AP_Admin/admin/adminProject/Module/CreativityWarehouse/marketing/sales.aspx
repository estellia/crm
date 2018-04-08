<!DOCTYPE html>
<html>
<head>
    <title>创意活动管理</title>
     <link href="../../static/css/easyui.css" rel="stylesheet" type="text/css"/>
     <link href="../../static/css/kkpager.css" rel="stylesheet" type="text/css" />
       <link href="../../styles/common-layout.css" rel="stylesheet" type="text/css"  />
     <link href="../../styles/css/newYear/skin02.css" rel="stylesheet" type="text/css"  />
</head>
<body>
    <div class="allPage" id="section" data-js="js/sales">
          <div class="optionBtn">
          <div class="commonBtn w80" id="getBack">返回</div>

</div><!--optionBtn-->
 <form id="optionForm">
    <div class="spreadPanel">
                  <div class="commonSelectWrap">
                     <em class="tit" data-tipname="EvnetName">促销名称：</em>
                      <div class="searchInput">
                        <input  data-flag="" name="EventName"  class="easyui-validatebox"  data-options="required:true" type="text" value=""/>
                      </div>
                  </div>
                  <div class="lineText">
                       <em class="tit" data-tipname="ImageUrl">促销kv图片：</em>
                      <div class="inputBox">
                        <div class="logo"  data-name="WebLogo"  style="width:320px;height: 185px;"><img data-imgcode="imageId" src="../../styles/images/newYear/imgDefault.png"></div>
                        <div class="uploadTip" style="left:372px">
                          <div class="uploadBtn btn">
                              <em class="upTip">上传图片</em>
                               <div class="jsUploadBtn" data-imgcode="imageId" data-msg="促销kv图片" ></div>
                           </div><!--uploadBtn-->
                           <div class="tip">建议尺寸：640px*370px，大小：10K以内</div>
                           </div> <!--uploadTip-->
                      </div> <!--inputBox-->
                  </div><!--lineText-->
                  </div>  <!--spreadPanel-->

<form>
<div class="optionBtn" >
 <div class="commonBtn" style="margin-left: 560px;" id="subMitBtn">保存</div>
</div>


  </div>
    <script type="text/javascript" src="../../static/js/lib/require.js"  defer  async="true" data-main="../../static/js/main.js"></script>

</body>
</html>
