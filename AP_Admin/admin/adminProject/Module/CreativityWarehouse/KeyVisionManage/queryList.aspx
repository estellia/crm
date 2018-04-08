<!DOCTYPE html>
<html >
<head>
    <title>KV管理</title>
     <link href="../../static/css/easyui.css" rel="stylesheet" type="text/css" />
     <link href="../../static/css/kkpager.css" rel="stylesheet" type="text/css" />
       <link href="../../styles/common-layout.css" rel="stylesheet" type="text/css"  />
     <link href="../../styles/css/newYear/skin02.css" rel="stylesheet" type="text/css"  />
     <link href="css/queryList.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="section" class="page commonOutArea" data-js="js/queryList">

         <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                        <div class="item">
                          <form></form>
                          <form id="seach">
                              <div class="commonSelectWrap">
                                  <em class="tit">KV名称：</em>
                                  <label class="searchInput" >
                                      <input data-text="活动名称" id="name" name="name" type="text" value="">
                                  </label>
                              </div>
                              <div class="commonSelectWrap" >
                                  <em class="tit">KV状态：</em>
                                   <div class="selectBox" >
                                       <input  name="status" id="status" class="easyui-combobox" data-options="width:200,height:30"  />
                                  </div>
                              </div>
                              <div class="moreQueryWrap">
                                 <a href="javascript:;" class="commonBtn queryBtn">查询</a>
                              </div>
                          </form>

                        </div>

                    <!--<h2 class="commonTitle">会员查询</h2>-->

                </div>
                <div class="optionBtn">
                	<a class="commonBtn  icon w80 icon_add r" id="addKVBtn" href="javascript:;">新增</a>
                </div>
                <div class="tableWrap" id="tableWrap" style="display:inline-block;width:100%;">

                   <table class="dataTable" id="gridTable">
                         
                          <div  class="loading">
                                   <span>
                                 <img src="../../static/images/loading.gif" /></span>
                            </div>
                        

                   </table>
                    <div id="pageContianer">
                    <div class="dataMessage" >没有符合条件的查询记录</div>
                        <div id="kkpager">
                        </div>
                    </div>
                </div>
            </div>

    </div>

    <div style="display: none;">
        <div id="win" class="easyui-window" data-options="title:'KV新增',width:740,height:500,modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true">
            <div class="easyui-layout" data-options="fit:true">
                <div data-options="region:'center',border:false" style="padding: 10px; background: #fff; ">
                     <form></form>
                    <form id="addkvForm">
                     <div class="commonSelectWrap FullWidth">
                        <em class="tit ">显示顺序：</em>
                        <div class="searchInput" >
                            <input class="easyui-numberbox" data-options="width:200,height:32,required:true" name="DisplayIndex" style="border:none"/>
                            <div style="display:none;">
                                <input  class="easyui-validatebox" data-options="width:200,height:32" name="AdId" />
                            </div>
                        </div>
                    </div>
                    <div class="commonSelectWrap FullWidth">
                        <em class="tit ">主题类型：</em>
                        <div class="selectBox" >
                                <input id="ActivityGroup"  class="easyui-combobox" data-options="width:200,height:32,required:true"  name="ActivityGroupId" />
                        </div>
                    </div>

                    <div class="commonSelectWrap FullWidth kvActivity">
                        <em class="tit ">主题选择：</em>
                        <div class="selectBox" >
                                <input id="Activity"  class="easyui-combobox" data-options="width:200,height:32"  name="TemplateId" />
                        </div>
                    </div>

                    <div class="commonSelectWrap FullWidth kvname">
                        <em class="tit ">kv名称：</em>
                        <div class="searchInput" >
                            <input id="kvname" class="easyui-validatebox" data-options="width:200,height:32,required:true" name="BannerName" style="border:none"/>
                        </div>
                    </div>
                    
                    <div class="commonSelectWrap FullWidth linkaddress" style="display:none;">
                        <em class="tit ">链接地址：</em>
                        <div class="searchInput" >
                            <input id="linkaddress" class="easyui-validatebox" data-options="width:200,height:32" name="BannerUrl" style="border:none"/>
                        </div>
                    </div>

                    <div class="commonSelectWrap CheckResult">

                         <div class="lineText">
                              <div  style="display:none;"><input  id="ImageURL"  type="hidden" value="" /><input name="BannerImageId"  id="BannerImageId" type="hidden" value="" /></div>
                           <div class="inputBox">
                             <div class="logo"  data-name="WebLogo" style="width: 210px;height: 96px;"><img src="../../styles/images/newYear/imgDefault.png"></div>
                             <div class="uploadTip">
                               <div class="uploadBtn btn">
                                   <em class="upTip">上传</em>
                                    <div class="jsUploadBtn" ></div>
                                </div><!--uploadBtn-->
                                <div class="tip">图片格式：JPG,建议尺寸：840px*380px,大小10K以内</div>
                                </div> <!--uploadTip-->
                           </div> <!--inputBox-->
                       </div><!--lineText-->

                       
                    </div>
                        </form>
                </div>
                <div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
      				<a class="easyui-linkbutton commonBtn saveBtn" >确定</a>
      				<a class="easyui-linkbutton commonBtn cancelBtn"  href="javascript:void(0)" onclick="javascript:$('#win').window('close')" >取消</a>
      			</div>
            </div>
        </div>
    </div>

    <script type="text/javascript" src="../../static/js/lib/require.js"  defer  async="true" data-main="../../static/js/main.js"></script>

</body>
</html>
