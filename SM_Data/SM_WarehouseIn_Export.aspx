<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_WarehouseIn_Export.aspx.cs"
    Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseIn_Export" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

    <title>导出入库记录</title>
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
               <%-- <th>
                    匹配条件
                </th>--%>
                <th>
                    排序方式
                </th>
            </tr>
            <tr>
                <td>
                    入库单编号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                        <asp:ListItem Text="" Value="WG_CODE AS 入库单编号," Selected="True" Enabled="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_CODE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_CODE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    计划跟踪号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList24" runat="server">
                        <asp:ListItem Text="" Value="WG_PTCODE AS 计划跟踪号," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox24" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList24" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_PTCODE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_PTCODE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    物料编码
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList8" runat="server">
                        <asp:ListItem Text="" Value="WG_MARID AS 物料编码," Selected="True" Enabled="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList8" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_MARID ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_MARID DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    物料名称
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList9" runat="server">
                        <asp:ListItem Text="" Value="MNAME AS 物料名称," Selected="True" Enabled="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList9" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="MNAME ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="MNAME DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    规格型号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList12" runat="server">
                        <asp:ListItem Text="" Value="GUIGE AS 规格型号," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList12" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="GUIGE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="GUIGE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    材质
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList10" runat="server">
                        <asp:ListItem Text="" Value="CAIZHI AS 材质," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList10" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="CAIZHI ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="CAIZHI DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    国标
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList11" runat="server">
                        <asp:ListItem Text="" Value="GB AS 国标," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList11" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="GB ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="GB DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    是否定尺
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList13" runat="server">
                        <asp:ListItem Text="" Value="WG_FIXEDSIZE AS 是否定尺," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList13" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_FIXEDSIZE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_FIXEDSIZE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    长
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList14" runat="server">
                        <asp:ListItem Text="" Value="WG_LENGTH AS 长,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList14" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_LENGTH ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_LENGTH DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    宽
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList15" runat="server">
                        <asp:ListItem Text="" Value="WG_WIDTH AS 宽,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList15" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_WIDTH ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_WIDTH DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    批号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList16" runat="server">
                        <asp:ListItem Text="" Value="WG_LOTNUM AS 批号," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList16" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_LOTNUM ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_LOTNUM DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    单位
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList17" runat="server">
                        <asp:ListItem Text="" Value="CGDW AS 单位," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList17" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="CGDW ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="CGDW DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    实收重量
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList18" runat="server">
                        <asp:ListItem Text="" Value="cast(WG_RSNUM as float) AS 实收重量," Selected="True" Enabled="false"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList18" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_RSNUM ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_RSNUM DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    实收张（支）数
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList19" runat="server">
                        <asp:ListItem Text="" Value="WG_RSFZNUM AS 实收张数或支数," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList19" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_RSFZNUM ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_RSFZNUM DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    单价
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList20" runat="server">
                        <asp:ListItem Text="" Value="cast(WG_UPRICE as float) AS 单价," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList20" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_UPRICE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_UPRICE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    金额
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList21" runat="server">
                        <asp:ListItem Text="" Value="cast(WG_AMOUNT as float) AS 金额," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox21" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList21" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_AMOUNT ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_AMOUNT DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    收料仓库
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList22" runat="server">
                        <asp:ListItem Text="" Value="WS_NAME AS 收料仓库," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList22" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WS_NAME ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WS_NAME DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    收料仓位
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList23" runat="server">
                        <asp:ListItem Text="" Value="WL_NAME AS 收料仓位," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox23" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList23" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WL_NAME ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WL_NAME DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    订单编号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList25" runat="server">
                        <asp:ListItem Text="" Value="WG_ORDERID AS 订单编号," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox25" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList25" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_ORDERID ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_ORDERID DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    供应商
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList2" runat="server">
                        <asp:ListItem Text="" Value="SupplierName AS 供应商," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="SupplierName ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="SupplierName DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    部门
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList4" runat="server">
                        <asp:ListItem Text="" Value="DepName AS 部门,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="DepName ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="DepName DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    业务员
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList5" runat="server">
                        <asp:ListItem Text="" Value="ClerkName AS 业务员,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList5" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="ClerkName ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="ClerkName DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    制单人
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList6" runat="server">
                        <asp:ListItem Text="" Value="DocName AS 制单人," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList6" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="DocName ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="DocName DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    制单日期
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList3" runat="server">
                        <asp:ListItem Text="" Value="WG_DATE AS 制单日期," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_DATE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_DATE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    审核人
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList7" runat="server">
                        <asp:ListItem Text="" Value="VerfierName AS 审核人," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList7" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="VerfierName ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="VerfierName DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    审核日期
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList26" runat="server">
                        <asp:ListItem Text="" Value="left(WG_VERIFYDATE,10) AS 审核日期," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox26" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList26" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_VERIFYDATE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_VERIFYDATE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    条目备注
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList27" runat="server">
                        <asp:ListItem Text="" Value="WG_NOTE AS 条目备注," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox27" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList27" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_NOTE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_NOTE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    货单编号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList28" runat="server">
                        <asp:ListItem Text="" Value="WG_HDBH AS 货单编号,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox28" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList28" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_HDBH ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_HDBH DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
             <tr>
                <td>
                    标识号
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList30" runat="server" >
                        <asp:ListItem Text="" Value="WG_CGMODE AS 标识号," Selected="True"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox30" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList30" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_CGMODE ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_CGMODE DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    备注
                </td>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList29" runat="server">
                        <asp:ListItem Text="" Value="WG_ABSTRACT AS 备注,"></asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <%--<td>
                    <asp:TextBox ID="TextBox29" runat="server"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList29" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        RepeatLayout="Table">
                        <asp:ListItem Text="升序" Value="WG_ABSTRACT ASC,"></asp:ListItem>
                        <asp:ListItem Text="降序" Value="WG_ABSTRACT DESC,"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
