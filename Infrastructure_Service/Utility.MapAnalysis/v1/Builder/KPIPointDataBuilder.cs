using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.MapAnalysis.v1.ComponentModel;
using JIT.MapAnalysis.v1.ComponentModel;

namespace JIT.Utility.MapAnalysis.v1.Builder
{
    public class KPIPointDataBuilder
    {
        public string GetKpiTypeData(List<KPIPointDataItem> pKpiDataInfo, string pLabelPrefix, string pLabelSuffix, string pKpiText,int pTypeCount,string[] pTypeName,string[] pTypeLabel,string[] pTypeIcon,string pDefaultLegend,string pDefaultIcon)
        {
            var lstData = new List<KPIPointData>();
            foreach (var kpiDataItem in pKpiDataInfo)
            {//点 KPI 
                KPIPointData kpiData = new KPIPointData(); 
                if (!string.IsNullOrWhiteSpace(kpiDataItem.StoreID))
                {
                    kpiData.id = kpiDataItem.StoreID;
                }
                if (!string.IsNullOrWhiteSpace(kpiDataItem.StoreName))
                {
                    kpiData.title = kpiDataItem.StoreName; 
                }

                if (!string.IsNullOrWhiteSpace(kpiDataItem.StoreAddress))
                    kpiData.address = kpiDataItem.StoreAddress;
                kpiData.xytype = kpiDataItem.GPSType;
                kpiData.x = kpiDataItem.Longitude;
                kpiData.y = kpiDataItem.Latitude;
                if (kpiDataItem.KPIData != null)
                {
                    kpiData.kpi = kpiDataItem.KPIData;
                    var tempKpiLabel = kpiDataItem.KPILabel;

                    if (!string.IsNullOrWhiteSpace(pLabelPrefix))
                        kpiData.kpilabel = pLabelPrefix;
                    if (string.IsNullOrWhiteSpace(tempKpiLabel))
                        kpiData.kpilabel += kpiDataItem.KPIData;
                    else
                        kpiData.kpilabel += tempKpiLabel;

                    if (!string.IsNullOrWhiteSpace(pLabelSuffix))
                        kpiData.kpilabel += pLabelSuffix; ;
                }
                if (!string.IsNullOrWhiteSpace(kpiDataItem.KPIFilter1))
                    kpiData.kpifilter1 = kpiDataItem.KPIFilter1;
                if (!string.IsNullOrWhiteSpace(kpiDataItem.KPIFilter2))
                    kpiData.kpifilter2 = kpiDataItem.KPIFilter2;
                if (!string.IsNullOrWhiteSpace(kpiDataItem.KPIFilter3))
                    kpiData.kpifilter3 = kpiDataItem.KPIFilter3;
                if (!string.IsNullOrWhiteSpace(kpiDataItem.KPIFilter4))
                    kpiData.kpifilter4 = kpiDataItem.KPIFilter4;
                if (!string.IsNullOrWhiteSpace(kpiDataItem.KPIFilter5))
                    kpiData.kpifilter5 = kpiDataItem.KPIFilter5;

                //弹出框
                kpiData.poptype = kpiDataItem.PopWindowType;
                kpiData.popwidth = kpiDataItem.PopWindowWidth;
                kpiData.popheight = kpiDataItem.PopWindowHeight;
                kpiData.poptitle = kpiDataItem.PopWindowTitle;
                kpiData.popurl = kpiDataItem.PopWindowUrl;
                if (string.IsNullOrWhiteSpace(kpiData.popurl))
                {
                    //这些先写死
                    //kpiData.popwidth = "500";
                    //kpiData.popheight = "350";
                    //kpiData.poptitle = string.Empty;
                    kpiData.popurl = string.Format(@"/Module/MapAnalysis/Common/StoreInfo.aspx?sid={0}&kpilabel={1}&kpitext={2}", kpiData.id, kpiData.kpilabel, pKpiText);
                }
                lstData.Add(kpiData);
            }
            //设置阀值
            List<Threshold> lstThreshold = new List<Threshold>();
            Threshold threshold1 = new Threshold();
            threshold1.level = "4";
            threshold1.type = "6";
            threshold1.defaultimage = pDefaultIcon;
            threshold1.defaulttext = pDefaultLegend;
            List<ThresholdItem> lstThresholdItem = new List<ThresholdItem>();

            for (int i = 0; i < pTypeCount; i++)
            {
                ThresholdItem thresholdItem = new ThresholdItem();
                thresholdItem.image = pTypeIcon[i];
                thresholdItem.text = pTypeName[i];
                thresholdItem.textlabel = pTypeLabel[i];
                lstThresholdItem.Add(thresholdItem);
            } 
            threshold1.threshold = lstThresholdItem.ToArray();

            lstThreshold.Add(threshold1);
            var orderedItems = lstData.OrderBy(item => item.kpi);

            KPIInformation kpiInformation = new KPIInformation();
            kpiInformation.Data = lstData.ToArray();
            kpiInformation.Thresholds = lstThreshold.ToArray();
            var tempJsonString = kpiInformation.ToJSON();
            return tempJsonString; 
        }

        public string GetKpiValueData(List<KPIPointDataItem> pKpiDataInfo, string pLabelPrefix, string pLabelSuffix, string pKpiText, string[] pValueIcon, string pDefaultLegend, string pDefaultIcon)
        {
            var lstData = new List<KPIPointData>();
            foreach (var kpiDataItem in pKpiDataInfo)
            {//点 KPI 
                KPIPointData kpiData = new KPIPointData();
                if (!string.IsNullOrWhiteSpace(kpiDataItem.StoreID))
                {
                    kpiData.id = kpiDataItem.StoreID;
                }
                if (!string.IsNullOrWhiteSpace(kpiDataItem.StoreName))
                {
                    kpiData.title = kpiDataItem.StoreName;
                }

                if (!string.IsNullOrWhiteSpace(kpiDataItem.StoreAddress))
                    kpiData.address = kpiDataItem.StoreAddress;
                kpiData.xytype = kpiDataItem.GPSType;
                kpiData.x = kpiDataItem.Longitude;
                kpiData.y = kpiDataItem.Latitude;
                if (kpiDataItem.KPIData != null)
                {
                    kpiData.kpi = kpiDataItem.KPIData;
                    var tempKpiLabel = kpiDataItem.KPILabel;

                    if (!string.IsNullOrWhiteSpace(pLabelPrefix))
                        kpiData.kpilabel = pLabelPrefix;
                    if (string.IsNullOrWhiteSpace(tempKpiLabel))
                        kpiData.kpilabel += kpiDataItem.KPIData;
                    else
                        kpiData.kpilabel += tempKpiLabel;

                    if (!string.IsNullOrWhiteSpace(pLabelSuffix))
                        kpiData.kpilabel += pLabelSuffix; ;
                }
                if (!string.IsNullOrWhiteSpace(kpiDataItem.KPIFilter1))
                    kpiData.kpifilter1 = kpiDataItem.KPIFilter1;
                if (!string.IsNullOrWhiteSpace(kpiDataItem.KPIFilter2))
                    kpiData.kpifilter2 = kpiDataItem.KPIFilter2;
                if (!string.IsNullOrWhiteSpace(kpiDataItem.KPIFilter3))
                    kpiData.kpifilter3 = kpiDataItem.KPIFilter3;
                if (!string.IsNullOrWhiteSpace(kpiDataItem.KPIFilter4))
                    kpiData.kpifilter4 = kpiDataItem.KPIFilter4;
                if (!string.IsNullOrWhiteSpace(kpiDataItem.KPIFilter5))
                    kpiData.kpifilter5 = kpiDataItem.KPIFilter5;

                //这些先写死
                kpiData.popwidth = "500";
                kpiData.popheight = "350";
                kpiData.poptitle = string.Empty;
                kpiData.popurl = string.Format(@"/Module/MapAnalysis/Common/StoreInfo.aspx?sid={0}&kpilabel={1}&kpitext={2}", kpiData.id, kpiData.kpilabel, pKpiText);

                lstData.Add(kpiData);
            }
            //设置阀值
            List<Threshold> lstThreshold = new List<Threshold>();
            Threshold threshold1 = new Threshold();
            List<ThresholdItem> lstThresholdItem = new List<ThresholdItem>();

            //找出最大最小值
            double dMaxValue, dMinValue;
            double dFirstValue = Convert.ToDouble(pKpiDataInfo[0].KPIData);
            dMaxValue = dFirstValue;
            dMinValue = dFirstValue;
            foreach (var pointItem in pKpiDataInfo)
            {
                //KPIPointData pointItem = (KPIPointData)item;
                double dCurrentValue = Convert.ToDouble(pointItem.KPIData);
                if (dCurrentValue > dMaxValue)
                    dMaxValue = dCurrentValue;
                if (dCurrentValue < dMinValue)
                    dMinValue = dCurrentValue;
            }
            double dInterval = (dMaxValue - dMinValue) / 5; 
            for (int i = 0; i < 5; i++)
            {
                ThresholdItem thresholdItem = new ThresholdItem();
                thresholdItem.image =pValueIcon[i];
                thresholdItem.start = (dMinValue + dInterval * i).ToString("0.00");
                thresholdItem.startlabel = (dMinValue + dInterval * i).ToString("0.00");
                thresholdItem.end = (dMinValue + dInterval * (i + 1)).ToString("0.00");
                thresholdItem.endlabel = (dMinValue + dInterval * (i + 1)).ToString("0.00"); 
                lstThresholdItem.Add(thresholdItem);
            }

            //删除重复阀值
            List<ThresholdItem> lstNewThresholdItem = new List<ThresholdItem>();
            lstNewThresholdItem.Add(lstThresholdItem[0]);
            for (int i = 1; i < 5; i++)
            {
                if (lstThresholdItem[i - 1].start != lstThresholdItem[i].start || lstThresholdItem[i - 1].end != lstThresholdItem[i].end)
                {
                    lstNewThresholdItem.Add(lstThresholdItem[i]);
                }
            }

            threshold1.threshold = lstNewThresholdItem.ToArray();
            threshold1.level = "4";
            threshold1.type = "5";
            threshold1.defaultcolor = "0x000000";
            threshold1.defaultimage = pDefaultIcon;
            threshold1.defaulttext = pDefaultLegend; 

            lstThreshold.Add(threshold1);
            var orderedItems = lstData.OrderBy(item => item.kpi);
            KPIInformation kpiInformation = new KPIInformation();
            kpiInformation.Data = lstData.ToArray();
            kpiInformation.Thresholds = lstThreshold.ToArray();
            var tempJsonString = kpiInformation.ToJSON();
            return tempJsonString;
        }
    }
}