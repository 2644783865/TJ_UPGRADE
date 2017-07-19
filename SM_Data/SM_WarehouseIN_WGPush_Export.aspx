<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_WarehouseIN_WGPush_Export.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseIN_WGPush_Export" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <base target="download" />

    <script src="javascripts/MicrosoftAjax.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/MSAjaxDownload.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/Jquery.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/jquery.blockUI.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/jquery.cookie.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">


     function DownloadFile() {
        var val = "SM_WarehouseIn_Export.aspx?file=Files";
        
//        alert(val);
        var dn = new AjaxDownload(val);
        dn.EnableTrace(true);
        //fires before download, 
        dn.add_onBeginDownload(BeginDownload);
        dn.add_onEndDownload(EndDownload);
        dn.add_onError(DownloadError);
        dn.Download();
        return true;
    }
     function BeginDownload() {
        $.blockUI(); 
    }
    
    function EndDownload() {
        $.unblockUI();
    }
    
    
    function DownloadError() {
        var errMsg = AjaxDownload.ErrorMessage;
//        var errCk = $.cookie('downloaderror');
//        
//        if (errCk) {
//            errMsg += ", Error from server = " + errCk;
//        }
        alert(errMsg);
    }
    
   

    function closewin() {
        window.close();
    }
    </script>

    <title>导出订单记录</title>
</head>
<body>
    <form id="form1" runat="server">
    <iframe id="download" name="download" height="0px" width="0px"></iframe>
    <div align="center">
        <div>
            <asp:Button ID="Comfirm" runat="server" Text="确定" OnClick="Confirm_Click" OnClientClick="DownloadFile();" />&nbsp;&nbsp;&nbsp;
            <input id="Cancel" type="button" value="关闭" onclick="closewin()" />&nbsp;&nbsp;&nbsp;</div>
        <table>
            <tr>
                <th>
                    字段名称
                </th>
                <th>
                    是否选择
                </th>
                <th>
                    匹配条件
                </th>
                <th>
                    排序方式
                </th>
            </tr>
            <tr>
                <td>
                    供应商
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                        <asp:ListItem Text="" Value="suppliernm AS 供应商," Selected="True" Enabled="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="suppliernm ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="suppliernm DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    订单编号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList2" runat="server">
                        <asp:ListItem Text="" Value="orderno AS 订单编号," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="orderno ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="orderno DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    计划跟踪号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList3" runat="server">
                        <asp:ListItem Text="" Value="ptcode AS 计划跟踪号," Selected="True" Enabled="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="ptcode ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="ptcode DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    物料代码
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList4" runat="server">
                        <asp:ListItem Text="" Value="marid AS 物料代码," Selected="True" Enabled="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="marid ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="marid DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    物料名称
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList5" runat="server">
                        <asp:ListItem Text="" Value="marnm AS 物料名称," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList5" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="marnm ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="marnm DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    规格型号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList6" runat="server">
                        <asp:ListItem Text="" Value="margg AS 规格型号," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList6" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="margg ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="margg DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    材质
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList7" runat="server">
                        <asp:ListItem Text="" Value="marcz AS 材质," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList7" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="marcz ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="marcz DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    国标
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList8" runat="server">
                        <asp:ListItem Text="" Value="margb AS 国标," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList8" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="margb ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="margb DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    长
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList9" runat="server">
                        <asp:ListItem Text="" Value="length AS 长,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList9" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="length ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="length DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    宽
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList10" runat="server">
                        <asp:ListItem Text="" Value="width AS 宽,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList10" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="width ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="width DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    单位
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList11" runat="server">
                        <asp:ListItem Text="" Value="marunit AS 单位," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList11" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="marunit ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="marunit DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            
            <tr>
                <td>
                    订货数量
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList12" runat="server"  >
                        <asp:ListItem Text="" Value="cast(zxnum as float) AS 订货数量," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList12" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="zxnum ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="zxnum DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    到货数量
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList13" runat="server">
                        <asp:ListItem Text="" Value="cast(recgdnum as float) AS 到货数量," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList13" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="recgdnum ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="recgdnum DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    辅助数量
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList14" runat="server">
                        <asp:ListItem Text="" Value="cast(zxfznum as float) AS 辅助数量,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList14" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="zxfznum ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="zxfznum DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
             <tr>
                <td>
                    辅助单位
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList15" runat="server">
                        <asp:ListItem Text="" Value="marfzunit AS 辅助单位,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList15" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="marfzunit ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="marfzunit DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            
            <tr>
                <td>
                    交货日期
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList16" runat="server">
                        <asp:ListItem Text="" Value="recdate AS 交货日期," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList16" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="recdate ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="recdate DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    单价
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList17" runat="server">
                        <asp:ListItem Text="" Value="cast(price as float) AS 单价," Selected="True" Enabled="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList17" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="price ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="price DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    税率
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList18" runat="server">
                        <asp:ListItem Text="" Value="taxrate AS 税率," Selected="True" Enabled="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList18" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="taxrate ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="taxrate DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    含税单价
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList19" runat="server" >
                        <asp:ListItem Text="" Value="cast(ctprice as float) AS 含税单价," Selected="True" Enabled="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList19" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="ctprice ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="ctprice DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    金额
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList20" runat="server">
                        <asp:ListItem Text="" Value="cast(amount as float) AS 金额," Selected="True" Enabled="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList20" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="amount ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="amount DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    含税金额
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList21" runat="server">
                        <asp:ListItem Text="" Value="cast(ctamount as float) AS 含税金额," Selected="True" Enabled="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox21" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList21" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="ctamount ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="ctamount DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    下单日期
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList22" runat="server">
                        <asp:ListItem Text="" Value="shtime AS 下单日期," Selected="True" ></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList22" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="shtime ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="shtime DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    部门
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList23" runat="server">
                        <asp:ListItem Text="" Value="depnm AS 部门," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox23" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList23" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="depnm ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="depnm DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    业务员
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList24" runat="server" >
                        <asp:ListItem Text="" Value="ywynm AS 业务员," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox24" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList24" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="ywynm ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="ywynm DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    订单状态
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList25" runat="server">
                        <asp:ListItem Text="" Value="totalstate AS 订单状态," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox25" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList25" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="totalstate ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="totalstate DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    行业务状态
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList26" runat="server">
                        <asp:ListItem Text="" Value="detailstate AS 行业务状态," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox26" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList26" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="detailstate ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="detailstate DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    计划类型
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList27" runat="server">
                        <asp:ListItem Text="" Value="PO_MASHAPE AS 计划类型," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox27" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList27" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="PO_MASHAPE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="PO_MASHAPE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    标识号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList28" runat="server">
                        <asp:ListItem Text="" Value="PO_TUHAO AS 标识号," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox28" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList28" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="PO_TUHAO ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="PO_TUHAO DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    质检结果
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList29" runat="server">
                        <asp:ListItem Text="" Value="PO_CGFS AS 质检结果," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox29" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList29" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="PO_CGFS ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="PO_CGFS DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    备注
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList30" runat="server"  >
                        <asp:ListItem Text="" Value="detailnote AS 备注," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox30" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList30" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="detailnote ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="detailnote DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
         </table>
     </div>
    </form>
</body>
</html>
