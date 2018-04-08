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
    public class KPIFlatDataBuilder
    {
        private Color[] GetRenderColors(string pStyleCode,string pStyleSubCode)
        {
            if (pStyleCode.ToUpper() == "SA")
            {//面渲染色系
                switch (pStyleSubCode)
                {
                    case "1":
                    default:
                        Color[] flatColors1 = new Color[5] { "#EDF8E9".HexToColor().Value, "#BAE4B3".HexToColor().Value, "#74C476".HexToColor().Value, "#31A354".HexToColor().Value, "#006D2C".HexToColor().Value };
                        return flatColors1;
                        break;
                    case "2":
                        Color[] flatColors2 = new Color[5] { "#EFF3FF".HexToColor().Value, "#BDD7E7".HexToColor().Value, "#6BAED6".HexToColor().Value, "#3182BD".HexToColor().Value, "#08519C".HexToColor().Value };
                        return flatColors2;
                        break;
                    case "3":
                        Color[] flatColors3 = new Color[5] { "#FEE5D9".HexToColor().Value, "#FCAE91".HexToColor().Value, "#FB6A4A".HexToColor().Value, "#DE2D26".HexToColor().Value, "#A50F15".HexToColor().Value };
                        return flatColors3;
                        break;
                }
            }
            else
            {//气泡色系
                Color[] flatColors3 = new Color[5] { "#FEE5D9".HexToColor().Value, "#FCAE91".HexToColor().Value, "#FB6A4A".HexToColor().Value, "#DE2D26".HexToColor().Value, "#A50F15".HexToColor().Value };
                return flatColors3;
            }
        }
        public string GetKpiData(BoundLevel pLevel, List<KPIFlatDataItem> pKpiDataInfo, string pKpiLabelPrefix, string pKpiLabelSuffix, DataValueFormat pDataValueFormat , string pStyleCode,string pStyleSubCode)
        {
            Color[] pFlatColors = GetRenderColors(pStyleCode,pStyleSubCode);
            pKpiDataInfo.Sort(KpiDataCompare);
            double dMinValue = 0;// Convert.ToDouble(pKpiDataInfo[0].KPIData);
            for (int i = 0; i < pKpiDataInfo.Count; i++)
            {
                if (pKpiDataInfo[i].KPIData != null)
                {
                    dMinValue = Convert.ToDouble(pKpiDataInfo[i].KPIData);
                    break;
                }
            }
            double dMaxValue = Convert.ToDouble(pKpiDataInfo[pKpiDataInfo.Count - 1].KPIData);

            List<Threshold> lstThreshold = new List<Threshold>();
            //分成5段
            double dInterval = (dMaxValue - dMinValue) / 5.0;
            if (dInterval < 0.01 && dInterval > 0)//最小间隔为0.1
                dInterval = 0.01;

            Threshold threshold1 = new Threshold();
            List<ThresholdItem> lstThresholdItem = new List<ThresholdItem>();
            for (int i = 0; i < 5; i++)
            {
                double startValue;
                double endValue;
                if (i == 0)
                {//起始值使用最小值
                    startValue = dMinValue;
                }
                else
                {
                    startValue = dMinValue + dInterval * i;
                }
                if (i == 4)
                {//结束值使用最大值
                    endValue = dMaxValue;
                }
                else
                {
                    endValue = dMinValue + dInterval * (i + 1);
                }
                if (startValue == 0 && endValue == 0)//忽略无数据的KPI
                    continue;
                string startLabel = KPIBuilderUtils.GetFormatedValue(startValue.ToString(), pDataValueFormat);
                string endLabel = KPIBuilderUtils.GetFormatedValue(endValue.ToString(), pDataValueFormat);
                //阀值处理        
                ThresholdItem thresholdItem = new ThresholdItem();
                if (pFlatColors.Length > i)
                    thresholdItem.color = System.Drawing.ColorTranslator.ToHtml(pFlatColors[i]).Replace("#", "0x");
                else
                    thresholdItem.color = "0xFF0000";
                thresholdItem.start = startValue.ToString();
                thresholdItem.end = endValue.ToString();
                if ((pDataValueFormat & DataValueFormat.Int) != 0)
                {
                    if (thresholdItem.start.IndexOf(".") > -1)
                        thresholdItem.start = thresholdItem.start.Substring(0, thresholdItem.start.IndexOf("."));
                    if (thresholdItem.end.IndexOf(".") > -1)
                        thresholdItem.end = thresholdItem.end.Substring(0, thresholdItem.end.IndexOf("."));
                }
                thresholdItem.startlabel = startLabel;
                thresholdItem.endlabel = endLabel;
                thresholdItem.size = (20 + i*2).ToString();
                if (Convert.ToDouble(thresholdItem.start) >= dMaxValue && lstThresholdItem.Count > 0)
                    continue;//由于误差原因，可能超过最大值。
                lstThresholdItem.Add(thresholdItem);
            }
            //删除重复的分段阀值
            List<ThresholdItem> lstNewThresholdItem = new List<ThresholdItem>();
            lstNewThresholdItem.Add(lstThresholdItem[0]);
            for (int i = 1; i < lstThresholdItem.Count; i++)
            {
                if (lstThresholdItem[i - 1].start != lstThresholdItem[i].start || lstThresholdItem[i - 1].end != lstThresholdItem[i].end)
                {
                    lstNewThresholdItem.Add(lstThresholdItem[i]);
                }
            }
            threshold1.threshold = lstNewThresholdItem.ToArray();
            if (pStyleCode.ToUpper() == "SB")
            {
                threshold1.type = "3";
                //气泡渲染要添加透明效果
                foreach (var item in threshold1.threshold)
                {
                    item.alpha = "0.8";
                }

                threshold1.defaultalpha = "0.8";
            }
            else
            {
                threshold1.type = "1";
            }
            threshold1.level = ((int)pLevel).ToString();
            threshold1.defaultcolor = "0xFFFFFF";
            threshold1.defaultimage = "";
            threshold1.defaultsize = "20";
            threshold1.defaulttext = "无数据";
            lstThreshold.Add(threshold1);

            //计算气泡大小
            var lstData = new List<KPIFlatData>();
            foreach (var kpiDataItem in pKpiDataInfo)
            {
                //过滤无效的数据
                if (string.IsNullOrWhiteSpace(kpiDataItem.KPIData) || kpiDataItem.Ignore)
                    continue;
                //lstThreshold
                double dValue = Convert.ToDouble(kpiDataItem.KPIData);
                //过滤太小的数值
                //if (dValue < 0.01)
                //    continue;
                //省市县KPI 
                KPIFlatData kpiData = new KPIFlatData();
                if (!string.IsNullOrWhiteSpace(kpiDataItem.DataID))
                    kpiData.id = kpiDataItem.DataID;
                kpiData.kpi = kpiDataItem.KPIData;
                kpiData.kpilabel = KPIBuilderUtils.GetFormatedValue(kpiData.kpi, pDataValueFormat);
                kpiData.kpimark = kpiData.kpilabel;
                if (!string.IsNullOrWhiteSpace(pKpiLabelPrefix))
                    kpiData.kpilabel = pKpiLabelPrefix + kpiData.kpilabel;

                if (!string.IsNullOrWhiteSpace(pKpiLabelSuffix))
                    kpiData.kpilabel = kpiData.kpilabel + pKpiLabelSuffix;                
                lstData.Add(kpiData);
            }
            KPIInformation jsonKpiData = new KPIInformation();
            jsonKpiData.Data = lstData.ToArray();
            jsonKpiData.Thresholds = lstThreshold.ToArray();
            return jsonKpiData.ToJSON();
        }

        /// <summary>
        /// Kpi数据的数值大小比较
        /// </summary>
        /// <param name="obj1">比较对象1</param>
        /// <param name="obj2">比较对象2</param>
        /// <returns>0:相等，1:前面的值大-1:前面的值小</returns>
        private static int KpiDataCompare(KPIFlatDataItem obj1, KPIFlatDataItem obj2)
        {
            int res = 0;
            //空引用
            if ((obj1 == null) && (obj2 == null))
            {
                return 0;
            }
            else if ((obj1 != null) && (obj2 == null))
            {
                return 1;
            }
            else if ((obj1 == null) && (obj2 != null))
            {
                return -1;
            }

            //空值
            if (string.IsNullOrWhiteSpace(obj1.KPIData) && string.IsNullOrWhiteSpace(obj2.KPIData))
            {
                return 0;
            }
            else if (!string.IsNullOrWhiteSpace(obj1.KPIData) && string.IsNullOrWhiteSpace(obj2.KPIData))
            {
                return 1;
            }
            else if (string.IsNullOrWhiteSpace(obj1.KPIData) && !string.IsNullOrWhiteSpace(obj2.KPIData))
            {
                return -1;
            }

            if (Convert.ToDouble(obj1.KPIData) > Convert.ToDouble( obj2.KPIData) )
            {
                res = 1;
            }
            else if (Convert.ToDouble(obj1.KPIData) < Convert.ToDouble( obj2.KPIData))
            {
                res = -1;
            }
            return res;
        }
    } 
}