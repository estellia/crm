<!DOCTYPE html>
<html>
<head>
    <title>创意主题管理</title>
     <link href="../../static/css/easyui.css" rel="stylesheet" type="text/css"/>
     <link href="../../static/css/kkpager.css" rel="stylesheet" type="text/css" />
       <link href="../../styles/common-layout.css" rel="stylesheet" type="text/css"  />
     <link href="../../styles/css/newYear/skin02.css" rel="stylesheet" type="text/css"  />
     <link href="css/queryList.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="allPage" id="section" data-js="js/queryList">
                     <!-- 内容区域 -->
                     <div class="contentArea_vipquery">
                         <!--个别信息查询-->
                         <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                                 <div class="item">
                                   <form id="seach">
                                                           <div class="commonSelectWrap">
                                                               <em class="tit">主题名称：</em>
                                                               <div class="searchInput">
                                                                   <input  data-flag="item_name" name="TemplateName" type="text"
                                                                       value=""/>
                                                               </div>
                                                           </div>
                                                           <div class="commonSelectWrap">
                                                               <em class="tit">主题类型：</em>
                                                               <div class="selectBox">
                                                                         <input id="MarketingType" name="ActivityGroupId" class="easyui-combobox" data-options="width:200,height:30"  />
                                                               </div>
                                                           </div>
                                                           <div class="commonSelectWrap">
                                                               <em class="tit">主题状态：</em>
                                                               <div class="selectBox">
                                                                         <input id="item_status" name="TemplateStatus" class="easyui-combobox" data-options="width:200,height:30"  />
                                                               </div>
                                                           </div>
                                                           <div class="moreQueryWrap">
                                                                                      <a href="javascript:;" class="commonBtn queryBtn">查询</a>
                                                                                    </div>




                                                           </form>

                                 </div>

                             <!--<h2 class="commonTitle">会员查询</h2>-->

                         </div>
                         <div class="optionBtn" id="opt">

                                                <div class="commonBtn icon w80 icon_add r" data-flag="add"  >新增</div>


                         </div>
                         <div class="tableWrap" id="tableWrap">

                            <div class="cursorDef"> <div class="dataTable" id="gridTable">
                                                        <div  class="loading">
                                                                 <span>
                                                               <img src="../../static/images/loading.gif"></span>
                                                          </div>
         </div>  </div>
                             <div id="pageContianer">
                             <div class="dataMessage" >没有符合条件的查询记录</div>
                                 <div id="kkpager" >
                                 </div>
                             </div>
                         </div>
                     </div>
   </div>


    <script type="text/javascript" src="../../static/js/lib/require.js"  defer  async="true" data-main="../../static/js/main.js"></script>

</body>
</html>
