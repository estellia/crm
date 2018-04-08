using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using JIT.Utility.ExtensionMethod;
using JIT.MapAnalysis.v1.ComponentModel;
using JIT.Utility.MapAnalysis.v1.ComponentModel;
using System.Drawing;

namespace JIT.Utility.MapAnalysis.v1.Builder
{
    /// <summary>
    /// 生成饼图的Kpi数据
    /// </summary>
    public class KPIPieDataBuilder
    {
        /// <summary>
        /// 获取饼图Kpi数据
        /// </summary>
        /// <param name="pLevel">钻取层级</param>
        /// <param name="pKpiDataInfo">源数据</param>
        /// <param name="pPieBlockCount">饼的分块数</param>
        /// <param name="pKpiLabelPrefix">Kpi数据文本的前缀</param>
        /// <param name="pKpiLabelSuffix">Kpi数据文本的后缀</param>
        /// <param name="pDataValueFormat">数据显示格式</param>
        /// <param name="pLegendNames">图例显示文本</param>
        /// <param name="pStartColors">饼图分块渐变开始颜色</param>
        /// <param name="pEndColors">饼图分块渐变截止颜色</param>
        /// <param name="pStyleCode">渲染样式(SI:饼,SH:柱)</param>
        /// <returns>JSON格式的KPI数据</returns>
        public string GetKPIData(BoundLevel pLevel, List<KPIPieDataItem> pKpiDataInfo, int pPieBlockCount, string[] pKpiLabelPrefix, string[] pKpiLabelSuffix, DataValueFormat pDataValueFormat, string[] pLegendNames, Color[] pStartColors, Color[] pEndColors, string pStyleCode)
        {
            List<KPIPieData> lstKPIMultipleData = new List<KPIPieData>();
            #region 根据基数值设置饼的大小
            //找出最大最小值
            double dMaxValue, dMinValue;
            double dFirstValue = Convert.ToDouble(pKpiDataInfo[0].PieSizeData);
            dMaxValue = dFirstValue;
            dMinValue = dFirstValue; 
            foreach (var kpiDataItem in pKpiDataInfo)
            {
                double dCurrentValue = Convert.ToDouble(kpiDataItem.PieSizeData);
                if (dCurrentValue > dMaxValue)
                    dMaxValue = dCurrentValue;
                if (dCurrentValue < dMinValue)
                    dMinValue = dCurrentValue;
            }

            //#region    //按PieSizeData设置饼的大小
            double dInterval = (dMaxValue - dMinValue) / 10.0;

            foreach (var kpiDataItem in pKpiDataInfo)
            {
                if (kpiDataItem.Ignore)
                    continue;
                KPIPieData multipleKpiData = new KPIPieData();
                double dCurrentValue = Convert.ToDouble(kpiDataItem.PieSizeData);
                //当前分组级别（1，2，3，4，5）
                if (dCurrentValue <= (dMinValue + dInterval * 1))
                {
                    multipleKpiData.piesize = 1;
                }
                else if (dCurrentValue > (dMinValue + dInterval * 1) && dCurrentValue <= (dMinValue + dInterval * 2))
                {
                    multipleKpiData.piesize = 2;
                }
                else if (dCurrentValue > (dMinValue + dInterval * 2) && dCurrentValue <= (dMinValue + dInterval * 3))
                {
                    multipleKpiData.piesize = 3;
                }
                else if (dCurrentValue > (dMinValue + dInterval * 3) && dCurrentValue <= (dMinValue + dInterval * 4))
                {
                    multipleKpiData.piesize = 4;
                }
                else if (dCurrentValue > (dMinValue + dInterval * 4) && dCurrentValue <= (dMinValue + dInterval * 5))
                {
                    multipleKpiData.piesize = 5;
                }
                else if (dCurrentValue > (dMinValue + dInterval * 5) && dCurrentValue <= (dMinValue + dInterval * 6))
                {
                    multipleKpiData.piesize = 6;
                }
                else if (dCurrentValue > (dMinValue + dInterval * 6) && dCurrentValue <= (dMinValue + dInterval * 7))
                {
                    multipleKpiData.piesize = 7;
                }
                else if (dCurrentValue > (dMinValue + dInterval * 7) && dCurrentValue <= (dMinValue + dInterval * 8))
                {
                    multipleKpiData.piesize = 8;
                }
                else if (dCurrentValue > (dMinValue + dInterval * 8) && dCurrentValue <= (dMinValue + dInterval * 9))
                {
                    multipleKpiData.piesize = 9;
                }
                else if (dCurrentValue > (dMinValue + dInterval * 9) && dCurrentValue <= (dMinValue + dInterval * 10))
                {
                    multipleKpiData.piesize = 10;
                }
                multipleKpiData.piesize = 50 + multipleKpiData.piesize * 2;
                multipleKpiData.id = kpiDataItem.DataID;
                #region  生成Kpi数据
                //格式化Kpi数据值 格式化Kpi文本的前后缀
                //KPI1
                if (pPieBlockCount > 0)//
                {
                    multipleKpiData.kpi1 = kpiDataItem.KPIData1;
                    //multipleKpiData.kpilabel1 = multipleKpiData.kpi1;
                    multipleKpiData.kpilabel1 = KPIBuilderUtils.GetFormatedValue(multipleKpiData.kpi1, pDataValueFormat);
                    if (pKpiLabelPrefix != null && pKpiLabelPrefix.Length > 0)
                        multipleKpiData.kpilabel1 = pKpiLabelPrefix[0] + multipleKpiData.kpilabel1;
                    if (pKpiLabelSuffix != null && pKpiLabelSuffix.Length > 0)
                        multipleKpiData.kpilabel1 = multipleKpiData.kpilabel1 + pKpiLabelSuffix[0];
                }

                //KPI2
                if (pPieBlockCount > 1)
                {
                    multipleKpiData.kpi2 = kpiDataItem.KPIData2;
                    //multipleKpiData.kpilabel2 = multipleKpiData.kpi2;
                    multipleKpiData.kpilabel2 = KPIBuilderUtils.GetFormatedValue(multipleKpiData.kpi2, pDataValueFormat);
                    if (pKpiLabelPrefix != null && pKpiLabelPrefix.Length > 1)
                        multipleKpiData.kpilabel2 = pKpiLabelPrefix[1] + multipleKpiData.kpilabel2;
                    if (pKpiLabelSuffix != null && pKpiLabelSuffix.Length > 1)
                        multipleKpiData.kpilabel2 = multipleKpiData.kpilabel2 + pKpiLabelSuffix[1];
                }

                //KPI3
                if (pPieBlockCount > 2)
                {
                    multipleKpiData.kpi3 = kpiDataItem.KPIData3;
                    //multipleKpiData.kpilabel3 = multipleKpiData.kpi3;
                    multipleKpiData.kpilabel3 = KPIBuilderUtils.GetFormatedValue(multipleKpiData.kpi3, pDataValueFormat);
                    if (pKpiLabelPrefix != null && pKpiLabelPrefix.Length > 2)
                        multipleKpiData.kpilabel3 = pKpiLabelPrefix[2] + multipleKpiData.kpilabel3;
                    if (pKpiLabelSuffix != null && pKpiLabelSuffix.Length > 2)
                        multipleKpiData.kpilabel3 = multipleKpiData.kpilabel3 + pKpiLabelSuffix[2];
                }

                //KPI4
                if (pPieBlockCount > 3)
                {
                    multipleKpiData.kpi4 = kpiDataItem.KPIData4;
                    //multipleKpiData.kpilabel4 = multipleKpiData.kpi4;
                    multipleKpiData.kpilabel4 = KPIBuilderUtils.GetFormatedValue(multipleKpiData.kpi4, pDataValueFormat);
                    if (pKpiLabelPrefix != null && pKpiLabelPrefix.Length > 3)
                        multipleKpiData.kpilabel4 = pKpiLabelPrefix[3] + multipleKpiData.kpilabel4;
                    if (pKpiLabelSuffix != null && pKpiLabelSuffix.Length > 3)
                        multipleKpiData.kpilabel4 = multipleKpiData.kpilabel4 + pKpiLabelSuffix[3];
                }

                //KPI5
                if (pPieBlockCount > 4)
                {
                    multipleKpiData.kpi5 = kpiDataItem.KPIData5;
                    //multipleKpiData.kpilabel5 = multipleKpiData.kpi5;
                    multipleKpiData.kpilabel5 = KPIBuilderUtils.GetFormatedValue(multipleKpiData.kpi5, pDataValueFormat);
                    if (pKpiLabelPrefix != null && pKpiLabelPrefix.Length > 4)
                        multipleKpiData.kpilabel5 = pKpiLabelPrefix[4] + multipleKpiData.kpilabel5;
                    if (pKpiLabelSuffix != null && pKpiLabelSuffix.Length > 4)
                        multipleKpiData.kpilabel5 = multipleKpiData.kpilabel5 + pKpiLabelSuffix[4];
                }
                #endregion 生成Kpi数据
                lstKPIMultipleData.Add(multipleKpiData);
            }
            #endregion 根据基数值设置饼的大小

            #region 设置块的颜色
            List<PieThreshold> lstThreshold = new List<PieThreshold>();
            PieThreshold threshold = new PieThreshold();
            threshold.level = ((int)pLevel).ToString();
            List<PieThresholdItem> lstThresholdItem = new List<PieThresholdItem>();
            for (int i = 0; i < pPieBlockCount; i++)
            {
                PieThresholdItem thresholdItem1 = new PieThresholdItem();
                thresholdItem1.startcolor = System.Drawing.ColorTranslator.ToHtml(pStartColors[i]).Replace("#", "0x");
                thresholdItem1.endcolor = System.Drawing.ColorTranslator.ToHtml(pEndColors[i]).Replace("#", "0x");
                thresholdItem1.legendname = pLegendNames[i];
                lstThresholdItem.Add(thresholdItem1);
            }
            threshold.threshold = lstThresholdItem.ToArray();

            if (pStyleCode.ToUpper() == "SI")
                threshold.type = "10";
            else
                threshold.type = "11";
            lstThreshold.Add(threshold);
            #endregion  
            KPIInformation kpiInformation = new KPIInformation();
            kpiInformation.Data = lstKPIMultipleData.ToArray();
            kpiInformation.Thresholds = lstThreshold.ToArray();
            var tempJsonString = kpiInformation.ToJSON();
            return tempJsonString;
        } 
    }
}