<!DOCTYPE html>
<html >
<head>
    <title>计划活动管理</title>
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
                 <div class="panlDiv FullWidth">
                    <div class="lineText">
                        <div  style="display:none;"><input  id="ImageURL"  type="hidden" value="" /><input name="BannerImageId"  id="BannerImageId" type="hidden" value="" /></div>
                   <em class="tit">全年计划：</em>
                   <div class="inputBox">
                     <div class="uploadTip">
                       <div class="uploadBtn btn">
                           <em class="upTip">上传</em>
                            <div class="jsUploadBtn" ></div>
                        </div><!--uploadBtn-->
                        <div class="tip">图片格式：jpg, 建议尺寸0px*0px，50KB以内</div>
                        </div> <!--uploadTip-->
                   </div> <!--inputBox-->
               </div><!--lineText-->

                     </div>
                <br />
                <div class="panlDiv FullWidth">
                            <div class="title">下季计划</div>
                
                </div>

                
                <div class="tableWrap" id="tableWrap" style="display:inline-block;width:100%;">

                   <table class="dataTable" id="gridTable">
                        
                   </table>
                    <div id="pageContianer">
                    <div class="dataMessage" >没有符合条件的查询记录</div>
                       
                    </div>
                </div>
                <div class="panlDiv FullWidth">
                          <div id="addbtn" class="commonBtn icon w80  icon_add l" data-flag="add">新增</div>
                
                </div>
            </div>

    </div>

    

    <script type="text/javascript" src="../../static/js/lib/require.js"  defer  async="true" data-main="../../static/js/main.js"></script>

</body>
</html>
