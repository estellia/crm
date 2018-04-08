using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cPos.Dex.Model;
using cPos.Dex.ContractModel;
using cPos.Model;
using cPos.Dex.Common;
using ICSharpCode.SharpZipLib.Zip;
using System.Configuration;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;

namespace cPos.Dex.WcfServiceCommon
{
    public static class Common
    {
        #region ToItem
        public static Item ToItem(ItemInfo item)
        {
            var obj = new Item();
            obj.item_id = item.Item_Id;
            obj.item_category_id = item.Item_Category_Id;
            obj.item_code = item.Item_Code;
            obj.item_name = item.Item_Name;
            obj.item_name_en = item.Item_Name_En;
            obj.item_name_short = item.Item_Name_Short;
            obj.item_status = item.Status;
            obj.item_remark = item.Item_Remark;
            obj.pyzjm = item.Pyzjm;
            obj.create_user_id = item.Create_User_Id;
            obj.create_time = item.Create_Time;
            obj.modify_user_id = item.Modify_User_Id;
            obj.modify_time = item.Modify_Time;
            obj.if_gifts = Utils.GetStrVal(item.ifgifts);
            obj.if_often = Utils.GetStrVal(item.ifoften);
            obj.if_service = Utils.GetStrVal(item.ifservice);
            obj.isgb = Utils.GetStrVal(item.isGB);
            obj.data_from = Utils.GetStrVal(item.data_from);
            obj.display_index = Utils.GetStrVal(item.display_index);
            obj.item_props = new List<ItemProp>();
            if (item.ItemPropList != null)
            {
                foreach (var itemProp in item.ItemPropList)
                {
                    var objProp = new ItemProp();
                    objProp.item_id = itemProp.Item_Id;
                    objProp.item_property_id = itemProp.Item_Property_Id;
                    objProp.property_code_group_id = itemProp.PropertyCodeGroupId;
                    objProp.property_code_id = itemProp.PropertyCodeId;
                    objProp.property_detail_id = itemProp.PropertyDetailId;
                    objProp.property_code_value = itemProp.PropertyCodeValue;
                    objProp.status = Utils.GetStrVal(itemProp.Status);
                    objProp.property_code_group_name = itemProp.PropertyCodeGroupName;
                    objProp.property_code_name = itemProp.PropertyCodeName;
                    objProp.create_user_id = itemProp.Create_User_id;
                    objProp.create_time = itemProp.Create_Time;
                    obj.item_props.Add(objProp);
                }
            }
            return obj;
        }
        #endregion

        #region ToSku
        public static Sku ToSku(SkuInfo sku)
        {
            var obj = new Sku();
            obj.sku_id = sku.sku_id;
            obj.item_id = sku.item_id;
            obj.prop_1_detail_id = sku.prop_1_detail_id;
            obj.prop_1_detail_code = sku.prop_1_detail_code;
            obj.prop_1_detail_name = sku.prop_1_detail_name;
            obj.prop_2_detail_id = sku.prop_2_detail_id;
            obj.prop_2_detail_code = sku.prop_2_detail_code;
            obj.prop_2_detail_name = sku.prop_2_detail_name;
            obj.prop_3_detail_id = sku.prop_3_detail_id;
            obj.prop_3_detail_code = sku.prop_3_detail_code;
            obj.prop_3_detail_name = sku.prop_3_detail_name;
            obj.prop_4_detail_id = sku.prop_4_detail_id;
            obj.prop_4_detail_code = sku.prop_4_detail_code;
            obj.prop_4_detail_name = sku.prop_4_detail_name;
            obj.prop_5_detail_id = sku.prop_5_detail_id;
            obj.prop_5_detail_code = sku.prop_5_detail_code;
            obj.prop_5_detail_name = sku.prop_5_detail_name;
            obj.prop_1_id = sku.prop_1_id;
            obj.prop_1_code = sku.prop_1_code;
            obj.prop_1_name = sku.prop_1_name;
            obj.prop_2_id = sku.prop_2_id;
            obj.prop_2_code = sku.prop_2_code;
            obj.prop_2_name = sku.prop_2_name;
            obj.prop_3_id = sku.prop_3_id;
            obj.prop_3_code = sku.prop_3_code;
            obj.prop_3_name = sku.prop_3_name;
            obj.prop_4_id = sku.prop_4_id;
            obj.prop_4_code = sku.prop_4_code;
            obj.prop_4_name = sku.prop_4_name;
            obj.prop_5_id = sku.prop_5_id;
            obj.prop_5_code = sku.prop_5_code;
            obj.prop_5_name = sku.prop_5_name;
            obj.barcode = sku.barcode;
            obj.status = sku.status;
            obj.create_time = sku.create_time;
            obj.create_user_id = sku.create_user_id;
            obj.modify_time = sku.modify_time;
            obj.modify_user_id = sku.modify_user_id;
            return obj;
        }
        #endregion

        #region ZipFile
        /// <summary>
        /// 压缩文件，路径均为FTP相对路径
        /// </summary>
        /// <param name="pFileToZip">要压缩的文件</param>
        /// <param name="pZipedFile">压缩之后的路径</param>       
        /// <returns></returns>
        public static bool ZipFile(string[] pFileToZips, string pZipedFile, string folderPath, ref string error)
        {
            foreach (string file in pFileToZips)
            {
                //如果文件没有找到，则报错
                if (!File.Exists(folderPath + file))
                {
                    throw new System.IO.FileNotFoundException("指定要压缩的文件: " + file + " 不存在!");
                }
            }

            Crc32 crc = new Crc32();
            ZipOutputStream ZipStream = new ZipOutputStream(File.Create(pZipedFile));

            bool res = true;
            try
            {
                foreach (string file in pFileToZips)
                {
                    //打开压缩文件
                    FileStream fs = File.OpenRead(folderPath + file);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    ZipEntry entry = new ZipEntry(file.Substring(file.LastIndexOf('\\') + 1));

                    //entry.DateTime = DateTime.Now;

                    entry.Size = fs.Length;
                    //fs.Close();

                    //crc.Reset();
                    //crc.Update(buffer);

                    // entry.Crc = crc.Value;

                    ZipStream.PutNextEntry(entry);
                    ZipStream.Write(buffer, 0, buffer.Length);
                    ZipStream.CloseEntry();

                }
            }
            catch (Exception ex)
            {
                res = false;
                error = ex.ToString();
            }
            finally
            {
                if (ZipStream != null)
                {
                    ZipStream.Finish();
                    ZipStream.Close();
                }
                GC.Collect();
                GC.Collect(1);
            }
            return res;
        } 
        #endregion

    }
}