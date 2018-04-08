/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/2/25 17:47:11
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.Web.ComponentModel
{
    /// <summary>
    /// 树节点组
    /// </summary>
    public class TreeNodes
    {
        #region 内部类
        /// <summary>
        /// ExtJS的树节点
        /// </summary>
        class ExtJSTreeNode
        {
            /// <summary>
            /// 节点的文本
            /// </summary>
            [JsonProperty(PropertyName = "text")]
            public string Text { get; set; }

            /// <summary>
            /// 节点的标识符
            /// </summary>
            [JsonProperty(PropertyName = "id")]
            public string ID { get; set; }

            /// <summary>
            /// 父节点的标识符
            /// </summary>
            //[JsonIgnore]
            [JsonProperty(PropertyName = "ParentID")]
            public string ParentID { get; set; }

            /// <summary>
            /// 父节点的标识符
            /// </summary>
            //[JsonIgnore]
            [JsonProperty(PropertyName = "Status")]
            public string Status { get; set; }



            /// <summary>
            /// 节点是否展开
            /// </summary>
            [JsonProperty(PropertyName = "expanded", NullValueHandling = NullValueHandling.Ignore)]
            public bool? IsExpanded { get; set; }

            /// <summary>
            /// 是否叶子节点
            /// </summary>
            [JsonProperty(PropertyName = "leaf")]
            public bool IsLeaf { get; set; }

            /// <summary>
            /// 是否父节点 for zTree
            /// </summary>
            [JsonProperty(PropertyName = "isParent")]
            public bool IsParent { get { return !IsLeaf; } }

            [JsonProperty(PropertyName = "create_time")]
            public string create_time { get; set; }

            [JsonProperty(PropertyName = "ImageUrl")]
            public string ImageUrl { get; set; }

            [JsonProperty(PropertyName = "PromotionItemCount")]
            public int PromotionItemCount { get; set; }

            /// <summary>
            /// 是否勾选
            /// </summary>
            [JsonProperty(PropertyName = "checked", NullValueHandling = NullValueHandling.Ignore)]
            public bool? IsChecked { get; set; }
            public int NodeLevel { get; set; }

            public int DisplayIndex { get; set; }
            /// <summary>
            /// 佣金比例
            /// </summary>
            [JsonProperty(PropertyName = "CommissionRate")]
            public int CommissionRate { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(PropertyName = "ItemGroupType")]
            public int ItemGroupType { get; set; }

            /// <summary>
            /// 分类颜色
            /// </summary>
            [JsonProperty(PropertyName = "Color")]
            public string Color { get; set; }

            /// <summary>
            /// 是否偏好
            /// </summary>
            [JsonProperty(PropertyName = "IsPreference")]
            public string IsPreference { get; set; }

            /// <summary>
            /// 子节点
            /// </summary>
            [JsonProperty(PropertyName = "children", NullValueHandling = NullValueHandling.Ignore)]
            public List<ExtJSTreeNode> Children { get; set; }

            /// <summary>
            /// 是否为 --请选择-- 项
            /// </summary>
            [JsonProperty(PropertyName = "isPleaseSelectItem", NullValueHandling = NullValueHandling.Ignore)]
            public bool? IsPleaseSelectItem { get; set; }

            /// <summary>
            /// 在节点及其子节点下找到指定ID的节点
            /// </summary>
            /// <param name="pID"></param>
            /// <returns></returns>
            public ExtJSTreeNode Find(string pID)
            {
                if (this.ID == pID)
                    return this;
                //
                ExtJSTreeNode node = null;
                if (this.Children != null && this.Children.Count > 0)
                {
                    foreach (var child in this.Children)
                    {
                        if (child != null)
                            node = child.Find(pID);
                        if (node != null)
                            break;
                    }
                }
                //
                return node;
            }
            /// <summary>
            /// 找到当前节点已经其子节点中的非叶节点
            /// </summary>
            /// <returns></returns>
            public ExtJSTreeNode[] FindNotLeafNodes()
            {
                List<ExtJSTreeNode> list = new List<ExtJSTreeNode>();
                //
                if (!this.IsLeaf)
                {
                    list.Add(this);
                }
                if (this.Children != null && this.Children.Count > 0)
                {
                    foreach (var item in this.Children)
                    {
                        list.AddRange(item.FindNotLeafNodes());
                    }
                }
                //
                return list.ToArray();
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TreeNodes()
        {
            this.InnerList = new List<TreeNode>();
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 内部的节点组
        /// </summary>
        protected List<TreeNode> InnerList { get; set; }
        #endregion

        #region 集合操作
        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="pTreeNodeInfo">节点信息</param>
        public void Add(TreeNode pTreeNodeInfo)
        {
            if (pTreeNodeInfo != null)
            {
                //验证节点信息
                this.Validate(pTreeNodeInfo);
                //添加节点
                this.InnerList.Add(pTreeNodeInfo);
            }
        }
        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="pID">节点ID</param>
        /// <param name="pText">节点文本</param>
        /// <param name="pParentID">父节点ID</param>
        /// <param name="pIsLeaf">是否为叶子节点</param>
        public void Add(string pID, string pText, string pParentID, bool pIsLeaf)
        {
            var node = new TreeNode() { ID = pID, Text = pText, ParentID = pParentID, IsLeaf = pIsLeaf };
            this.Add(node);
        }

        /// <summary>
        /// 插入节点
        /// </summary>
        /// <param name="pInsertedPositionIndex">插入的位置</param>
        /// <param name="pTreeNodeInfo">需要插入的节点信息</param>
        public void Insert(int pInsertedPositionIndex, TreeNode pTreeNodeInfo)
        {
            if (pTreeNodeInfo != null)
            {
                //验证节点信息
                this.Validate(pTreeNodeInfo);
                //插入节点
                this.InnerList.Insert(pInsertedPositionIndex, pTreeNodeInfo);
            }
        }
        /// <summary>
        /// 集合中的总数
        /// </summary>
        public int Count
        {
            get
            {
                return this.InnerList.Count;
            }
        }
        /// <summary>
        /// 清除集合中的所有元素
        /// </summary>
        public void Clear()
        {
            this.InnerList.Clear();
        }
        /// <summary>
        /// 移除集合中指定索引的元素
        /// </summary>
        /// <param name="pIndex"></param>
        public void RemoveAt(int pIndex)
        {
            this.InnerList.RemoveAt(pIndex);
        }

        /// <summary>
        /// 根据ID移除集合中的元素
        /// </summary>
        /// <param name="pID"></param>
        public void RemoveBy(string pID)
        {
            foreach (var item in this.InnerList)
            {
                if (item.ID == pID)
                {
                    this.InnerList.Remove(item);
                    break;
                }
            }
        }
        #endregion

        #region 工具方法
        /// <summary>
        /// 验证节点信息
        /// </summary>
        /// <param name="pTreeNodeInfo"></param>
        private void Validate(TreeNode pTreeNodeInfo)
        {
            //标识符不能为空
            if (string.IsNullOrEmpty(pTreeNodeInfo.ID))
            {
                throw new ArgumentNullException("pTreeNodeInfo.ID");
            }
            //文本不能为空或null
            if (string.IsNullOrEmpty(pTreeNodeInfo.Text))
            {
                throw new ArgumentNullException("pTreeNodeInfo.Text");
            }
            //父节点不能是自身
            if (pTreeNodeInfo.ID == pTreeNodeInfo.ParentID)
            {
                throw new ArgumentException("节点的父节点ID不能与自己的ID相同.");
            }
            //ID重复检查
            bool idRepeat = false;
            foreach (var item in this.InnerList)
            {
                if (item.ID == pTreeNodeInfo.ID)
                {
                    idRepeat = true;
                    break;
                }
            }
            if (idRepeat)
                throw new ArgumentException(string.Format("树节点的ID值重复.重复的ID值为:{0}", pTreeNodeInfo.ID));
        }
        #endregion 

        #region 转换成ExtJS的TreeStore的JSON
        /// <summary>
        /// 转换成ExtJS的TreeStore的JSON
        /// </summary>
        /// <param name="pIsMultiSelect">是否为多选,如果是,则所有节点都自动带checkbox</param>
        /// <param name="pIsSelectLeafOnly">是否为只能选择叶子节点,如果为是,则多选时的非叶节点不带checkbox</param>
        /// <param name="pSelectedNodes">选中的值,该项仅当多选模式时才会起作用</param>
        /// <returns></returns>
        public string ToTreeStoreJSON(bool pIsMultiSelect, bool pIsSelectLeafOnly, TreeNode[] pSelectedNodes)
        {
            List<ExtJSTreeNode> nodes = new List<ExtJSTreeNode>();
            foreach (var item in this.InnerList)
            {
                var node = new ExtJSTreeNode()
                {
                    ID = item.ID,
                    Text = item.Text,
                    ParentID = item.ParentID,
                    IsLeaf = item.IsLeaf,
                    IsPleaseSelectItem = item.IsPleaseSelectItem,
                    create_time = item.create_time,
                    ImageUrl = item.ImageUrl,
                    PromotionItemCount = item.PromotionItemCount,
                    Status = item.Status,
                    NodeLevel = item.NodeLevel,
                    DisplayIndex = item.DisplayIndex,
                    CommissionRate = item.CommissionRate,
                    ItemGroupType = item.ItemGroupType,
                    Color = item.Color,
                    IsPreference = item.IsPreference
                };
                if (pIsMultiSelect)
                {
                    node.IsChecked = false;//只要值不为null则就会带checkbox,但树项默认是不选中的
                    if (pSelectedNodes != null && pSelectedNodes.Length > 0)
                    {
                        foreach (var n in pSelectedNodes)
                        {
                            if (n != null && node.ID == n.ID)
                            {
                                node.IsChecked = true;
                                break;
                            }
                        }
                    }
                }
                //
                nodes.Add(node);
            }
            //根据节点的父节点，构建对象关系
            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                if (!string.IsNullOrEmpty(node.ParentID))    //如果有父节点
                {
                    //找到父亲节点
                    ExtJSTreeNode parent = null;
                    for (int j = 0; j < nodes.Count; j++)
                    {
                        parent = nodes[j].Find(node.ParentID);
                        if (parent != null)
                            break;
                    }
                    if (parent != null)
                    {
                        if (parent.Children == null)
                            parent.Children = new List<ExtJSTreeNode>();
                        parent.Children.Add(node);
                        parent.IsLeaf = false;
                        nodes.RemoveAt(i);
                        i--;
                    }
                }
            }
            //移除非叶节点的checkbox
            if (pIsMultiSelect && pIsSelectLeafOnly)
            {
                foreach (var node in nodes)
                {
                    var notLeafNodes = node.FindNotLeafNodes();
                    if (notLeafNodes != null && notLeafNodes.Length > 0)
                    {
                        foreach (var notLeafNode in notLeafNodes)
                        {
                            notLeafNode.IsChecked = null;   //非叶子节点不带checkbox
                        }
                    }
                }
            }
            //序列化
            return nodes.ToJSON();
        }
        #endregion
    }
}
