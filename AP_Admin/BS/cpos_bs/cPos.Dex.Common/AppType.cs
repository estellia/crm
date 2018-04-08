﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Dex.Common
{
    /// <summary>
    /// 平台类型
    /// </summary>
    public enum AppType { AP, BS, DEX, Client, MOBILE, POS }

    /// <summary>
    /// 用户凭证验证类型
    /// </summary>
    public enum CertType { POS, MOBILE }

    /// <summary>
    /// 上传盘点单类型
    /// </summary>
    public enum UploadCcOrderType { CC, MOBILE }
}