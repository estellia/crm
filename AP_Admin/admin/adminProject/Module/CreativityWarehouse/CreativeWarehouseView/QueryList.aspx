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
        <div class="allPage" id="section" data-js="js/queryList.js?ver=0.4">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <div class="release"><a class="commonBtn r w80 releasebtn" href="javascript:void(0)">发布</a></div>
                <div class="contentleft">
                    <div class="thisseason spacing">
                        <div class="content">
                           
                            <div class="touchsliderthisseason">
                                <div class="thisseasontouchslider-viewport" style="width:790px;overflow:hidden;position: relative; height: 340px;">
                                    <div class="InSeasonList" style="width: 100000px; position: absolute; left: 0px; height:340px;">
                                        
                                    </div>
                                </div>
                                
                                <div class="thisseasontouchslider-nav">
                                    
                                </div>
                            </div>
                            
                        </div>
                    </div>
                </div>
                <div class="contentright ">
                    <div class="seasonlist spacing">
                         <div class="title">即将上线<span class="Annualplanbtn">查看全年计划></span></div>
                        <div class="content">
                            <ul class="seasonlist_ul">
                            </ul>

                        </div>
                    </div>
                    
                </div>

                <div class="ThemeType">
                    <div class="Theme" data-showclass="ProductLEventTemplatePreview">
                        <img data-onimg="images/ThemeType1on.png" data-img="images/ThemeType1.png" src="images/ThemeType1on.png" /></div>
                    <div class="Theme" data-showclass="HolidayLEventTemplatePreview">
                        <img data-onimg="images/ThemeType2on.png" data-img="images/ThemeType2.png" src="images/ThemeType2.png" /></div>
                    <div class="Theme" data-showclass="UnitLEventTemplatePreview">
                        <img data-onimg="images/ThemeType3on.png" data-img="images/ThemeType3.png" src="images/ThemeType3.png" /></div>
                </div>
                    <div class="nextseason spacing">
                          <div class="content  NextSeasonList ProductLEventTemplatePreview" >
                         
                        </div>
                        <div class="content  NextSeasonList HolidayLEventTemplatePreview" style="display:none;">
                         
                        </div>
                       
                        <div class="content  NextSeasonList UnitLEventTemplatePreview" style="display:none;">
                         
                        </div>
                    </div>
            </div>
        </div>
     <div id="Annualplanlayer" >
         <div class="planlayer">
             <img class="Annualplan" src="" />
             <img class="close" src="../../static/images/close.png" />
         </div>
        </div>
        
        <%-- 当季 --%>
    <script id="tpl_InSeasonList" type="text/html">
        <# for(i=0;i<BannerPreviewList.length;i++){ _data=BannerPreviewList[i]; #>
        <div class="thisseasontouchslider-item Seasondata"> <img  src="<#=_data.ImageUrl #>"  /></div>
            <#} #>
    </script>



       <%-- 主题 --%>
    <script id="tpl_NextSeasonList" type="text/html">
         <# for(i=0;i<activityList.length;i++){ _data=activityList[i]; #>

             <div class="Activity">
                    <div class="ActivityContent">
                        <div class="Activityimg"><img src="<#=_data.ImageUrl #>" /></div>
                        <div class="ActivityDesc"><span><#=_data.TemplateName #></span><span></span></div>
                    </div>
                    <div class="ActivityOpeartion"><div class="releasebtn">
                        <#if(_data.TemplateStatus==10) {#>
                        待上架
                         <#}else if(_data.TemplateStatus==20) { #>
                        待发布
                         <#}else if(_data.TemplateStatus==30) { #>
                        已发布
                         <#}else if(_data.TemplateStatus==40) { #>
                        已下架
                        <#}#>
                     </div></div>
                </div>
             <#} #>

    </script>
     <%-- 年度活动计划 --%>
    <script id="tpl_seasonlist" type="text/html">
         <# for(i=0;i<SeasonPlanPreviewList.length;i++){  _data=SeasonPlanPreviewList[i]; #>
              <li><span> <#=_data.PlanDate.substring(5) #></span> <#=_data.PlanName #></li>
            <#} #>

           
    </script>



    <script type="text/javascript" src="../../static/js/lib/require.js"  defer  async="true" data-main="../../static/js/main.js"></script>

</body>
</html>

