<!DOCTYPE html>
<html>
<head>
    <title>创意活动管理</title>
     <link href="../../static/css/easyui.css" rel="stylesheet" type="text/css"/>
     <link href="../../static/css/kkpager.css" rel="stylesheet" type="text/css" />
       <link href="../../styles/common-layout.css" rel="stylesheet" type="text/css"  />
     <link href="../../styles/css/newYear/skin02.css" rel="stylesheet" type="text/css"  />
     <link href="css/creative.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="allPage" id="section" data-js="js/creative">
      <div class="contentArea_vipquery">
        <div class="navigation">
           <ul>
           <li class="one on" data-showpanel="nav01"><em>1</em>主题设置</li>
           <li data-showpanel="nav02"><em>2</em>风格设置</li>
           <li data-showpanel="nav03"><em>3</em>活动设置</li>
           <li data-showpanel="nav04"><em>4</em>推广设置</li>

           <img src="../../styles/images/newYear/nav/jt.png" alt="" />
           <img src="../../styles/images/newYear/nav/jton.png" class="hide" alt="" />

</ul>
         </div>
        <div class="panelDiv" data-panel="nav01">
        <form id="creative">
             <p class="title">主题类型</p>
               <div class="commonSelectWrap">
                  <em class="tit">选择类型：</em>
                   <div class="selectBox">
                     <input id="activityGroup" data-flag="" name="ActivityGroupId" type="text" value="" class="easyui-combobox" data-options="width:200,height:30,validType:'selectIndex'"/>
                   </div>
               </div>
             <p class="title">基础信息</p>
               <div class="commonSelectWrap">
                  <em class="tit">主题名称：</em>
                   <div class="searchInput">
                     <input  data-flag="" name="TemplateName" class="easyui-validatebox" data-options="required:true,validType:'maxLength[100]'"  type="text" value=""/>
                   </div>
               </div>
             <p class="title">活动图片</p>
               <div class="lineText">
                   <div class="inputBox">
                     <div class="logo"  data-name="ImageUrl"><img src="../../styles/images/newYear/imgDefault.png"  data-imgcode="imageId" ></div>
                     <div class="uploadTip">
                       <div class="uploadBtn btn">
                           <em class="upTip">上传</em>
                            <div class="jsUploadBtn" data-imgcode="imageId" data-msg="请上传一张活动图片" ></div>
                        </div><!--uploadBtn-->
                        <div class="tip">建议尺寸：265px*265px，大小：50K以内</div>
                        </div> <!--uploadTip-->
                   </div> <!--inputBox-->
               </div><!--lineText-->

               <div class="commonSelectWrap">
                  <em class="tit">显示顺序：</em>
                   <div class="searchInput">
                     <input  data-flag="" class="easyui-numberbox" data-options="min:0,precision:0,width:200,height:30" name="DisplayIndex" type="text" value=""/>
                   </div>
               </div>
         </form>
        </div>
      <div class="panelDiv" data-panel="nav02">
          <div class="optionBtn">
          <div class="r">
          <div  class="commonBtn icon icon_add" data-flag="style">新增</div>
          <div class="tip">最多只可以添加10条</div>
         </div>
</div>

  <div class="imgTable cursorDef nav02Table">
  <div class="dataTable" id="nav02Table">
           <div  class="loading">
                 <span><img src="../../static/images/loading.gif"></span>
            </div>
         </div>
  </div>
<div class="zsy"></div>
</div> <!-- data-panel="nav02"-->
      <div class="panelDiv" data-panel="nav03">
          <div class="optionBtn">
          <div class="r">
          <div  class="commonBtn icon icon_add" data-flag="activity">新增</div>
         </div>
</div>

  <div class="imgTable cursorDef nav03Table">
  <div class="dataTable" id="nav03Table">
           <div  class="loading">
                 <span><img src="../../static/images/loading.gif"></span>
            </div>
         </div>
  </div>

 <div id="pageContianer">
   <div id="kkpager" >    </div>
   </div>
<div class="zsy"></div>
</div> <!-- data-panel="nav03"-->
          <div class="panelDiv" data-panel="nav04">

           <div class="spread">
           <div class="spreadPanel">
           <p class="title">选择设置页面</p>
                <div class="contentDiv ">
                  <div class="tabDiv on"  data-showtab="tab01">
                    <span class="l"></span>
                    <em>微信图文设置</em>
</div> <!--tabDiv-->
                  <div class="tabDiv" data-showtab="tab02">
                    <span class="l"></span>
                    <em>微信朋友圈分享设置</em>
</div> <!--tabDiv-->
                  <div class="tabDiv" data-showtab="tab03">
                    <span class="l"></span>
                    <em>引导关注</em>
</div> <!--tabDiv-->
</div> <!--contentDiv-->
</div> <!--spreadPanel-->
           <div class="spreadPanel center attention" >
           <div class="phoneTitle">商户公众号名称</div>
           <div class="phoneWebDiv" data-flag="微信图文" data-tabname="tab01" data-type="Reg">
          <p class="txtTitle" data-view="Title">我是一个图文标题</p>
          <img src="images/tuwen.png" width="245" height="137"  data-imgcode="picText" >
          <span data-view="Summary" >这是一个图文素材图文素材这是一个图文素材图文素材这是一个图文素材图
          文素材这是一个图文素材图文素材这是一个图文素材图文素材
          这是一个图文素材图文素材这是一个图文素材图文素材这是一个图文素材图文素材
          这是一个图文素材图文素材这是一个图文素材图文素材这是一个图文素材图文素材</span>
</div> <!--phoneWebDiv-->

 <div class="phoneWebDiv share" style="display: none" data-flag="分享设置" data-tabname="tab02" data-type="Share">
                <img src="images/icon.png" class="shareIcon"/>
                <em class="jt_tip"></em>
                        <div class="share">
                             <p class="txtTitle" data-view="Title">我是一个图文标题</p>

                            <div >
                             <div  class="shareImg" style="float:left"><img src="../../styles/images/newYear/imgDefault.png" data-imgcode="shareImg">

                             </div>
                              <span data-view="Summary" style="float: right; width: 120px; padding-right: 10px;" >我是一个分享的描述</span>
                              </div>
                        </div> <!--share-->

              </div> <!--phoneWebDiv share-->
 <div class="phoneWebDiv attention" data-flag="引导" data-tabname="tab03"  style="display: none; text-align: center" data-type="Focus">
            <img src="images/bgPhone.png" class="bgPhone" data-imgcode="bgPhone" >
           <div class="erweiMaPanel">
           <p data-view="PromptText">长按此二维码进行关注</p>
             <div class="erWeiMa">
             <img src="../../styles/images/newYear/imgDefault.png" >
            </div>
           </div>
 </div> <!--attention-->


</div> <!--spreadPanel-->
           <div class="spreadPanel setSpread" style="width:290px;padding-top: 15px;">
            <div  data-tabname="tab01" data-type="Reg" data-msg="图文设置" >
                    <div class="commonSelectWrap">
                  <em class="tit">图文标题：</em>
                   <div class="searchInput">
                     <input  data-flag="" name="Title" type="text" value=""/>
                   </div>
               </div>
                    <div class="lineText">
                   <div class="inputBox">
                   <em class="tit">图文图片：</em>

                     <div class="uploadTip" style="left: 78px;bottom:10px;">
                       <div class="uploadBtn btn">
                           <em class="upTip">上传图片</em>
                            <div class="jsUploadBtn" data-imgcode="picText" data-msg="图文图片" ></div>
                        </div><!--uploadBtn-->
                        <div class="tip" style="font-size: 12px; text-align: left; background: none; padding-left: 0;" >建议尺寸：900px*500px，大小：50K以内</div>
                        </div> <!--uploadTip-->
                   </div> <!--inputBox-->
               </div><!--lineText-->
                    <div class="commonSelectWrap">
                  <em class="tit">图文摘要：</em>
                   <div class="searchInput" style="height: 120px;">
                    <textarea name="Summary"></textarea>
                   </div>
               </div>
            </div>
               <div  data-tabname="tab02"  style="display: none;" data-type="Share" data-msg="微信朋友圈分享设置">
                                                                    <div class="commonSelectWrap">
                                                                  <em class="tit">分享标题：</em>
                                                                   <div class="searchInput">
                                                                     <input  data-flag="" name="Title" type="text" value=""/>
                                                                   </div>
                                                               </div>
                                                                    <div class="lineText">
                                                                   <div class="inputBox">
                                                                   <em class="tit">分享图片：</em>

                                                                     <div class="uploadTip" style="left: 78px;bottom:10px;">
                                                                       <div class="uploadBtn btn">
                                                                           <em class="upTip">上传图片</em>
                                                                            <div class="jsUploadBtn"  data-imgcode="shareImg" data-msg="分享图片"></div>
                                                                        </div><!--uploadBtn-->
                                                                        <div class="tip" style="font-size: 12px; text-align: left; background: none; padding-left: 0;" >建议尺寸：120px*120px，大小：50K以内</div>
                                                                        </div> <!--uploadTip-->
                                                                   </div> <!--inputBox-->
                                                               </div><!--lineText-->
                                                                    <div class="commonSelectWrap">
                                                                  <em class="tit">分享摘要：</em>
                                                                   <div class="searchInput" style="height: 120px;">
                                                                     <textarea name="Summary"></textarea>
                                                                   </div>
                                                               </div>
                                                            </div>
                        <div  data-tabname="tab03"  style="display: none;" data-type="Focus" data-msg="引导关注">
                                <div class="lineText">
                               <div class="inputBox">
                               <em class="tit">引导背景：</em>

                                 <div class="uploadTip" style="left: 78px;bottom:10px;">
                                   <div class="uploadBtn btn">
                                       <em class="upTip">上传图片</em>
                                        <div class="jsUploadBtn" data-imgcode="bgPhone"  data-msg="引导背景"></div>
                                    </div><!--uploadBtn-->
                                    <div class="tip" style="font-size: 12px; text-align: left; background: none; padding-left: 0;" >建议尺寸：640px*1008px，大小：50K以内</div>
                                    </div> <!--uploadTip-->
                               </div> <!--inputBox-->
                           </div><!--lineText-->
                                <div class="commonSelectWrap">
                              <em class="tit">提示文字：</em>
                               <div class="searchInput" >
                                 <input type="text" name="PromptText" value="" placeholder="长按次二维码进行关注"/>
                               </div>
                           </div>
                        </div>

</div>   <!--spreadPanel-->
</div>

</div> <!-- data-panel="nav04"-->
      </div>
    <div class="btnopt" data-falg="nav01">
                        <div class=" commonBtn bgWhite prevStepBtn" data-flag="nav01" style="float: left; display: none">
                            上一步</div>
                        <div class=" commonBtn nextStepBtn" id="submitBtn" data-submitindex="1" data-flag="nav02" style="float:left;">下一步</div>
                    </div>
    </div>
 <div style="display: none">
      <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
      		<div class="easyui-layout" data-options="fit:true" id="panlconent">

      			<div data-options="region:'center'" style="padding:10px;">

      			</div>
      			<div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
      				<a class="easyui-linkbutton commonBtn saveBtn" >确定</a>

      			</div>
      		</div>

      	</div>
      </div>
    <script type="text/javascript" src="../../static/js/lib/require.js"  defer  async="true" data-main="../../static/js/main.js"></script>


</body>
</html>
