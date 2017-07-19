﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_WarehouseOut_Export4.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseOut_Export4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="download" />

    <script src="javascripts/MicrosoftAjax.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/MSAjaxDownload.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/Jquery.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/jquery.blockUI.js" language="javascript" type="text/javascript"></script>

    <script src="javascripts/jquery.cookie.js" language="javascript" type="text/javascript"></script>

    <link href="../SM_Data/StyleFile/Style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">

      function DownloadFile() {
        var val = "SM_WarehouseOut_Export4.aspx?file=Files";
        
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

    <title>导出其他出库记录</title>
</head>
<body>
    <form id="form1" runat="server">
    <iframe id="download" name="download" height="0px" width="0px"></iframe>
    <div align="center">
        <table class="edittable">
            <caption>
                导出其他出库记录</caption>
            <thead>
                <tr>
                    <th>
                        字段名称
                    </th>
                    <th>
                        是否选择
                    </th>
                   
                    <th>
                        排序方式
                    </th>
                </tr>
            </thead>
            <tbody>
            <tr>
             <td colspan="3" align="center">
                  <asp:RadioButtonList ID="RadioButtonListStyle" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True">其他出库明细</asp:ListItem>
                      <%--  <asp:ListItem Value="1">物料名称汇总</asp:ListItem>
                        <asp:ListItem Value="2">生产制号汇总</asp:ListItem>
                        <asp:ListItem Value="3">辅材班组汇总</asp:ListItem>
                        <asp:ListItem Value="4">生产制号物料代码汇总</asp:ListItem>--%>
                    </asp:RadioButtonList></td>
            </tr>
                <tr>
                    <td>
                        出库单编号
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                            <asp:ListItem Text="" Value="cast(id as varchar(50)) AS 出库单编号," Selected="True" Enabled="false"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="OutCode ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="OutCode DESC," Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        计划跟踪号
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList24" runat="server">
                            <asp:ListItem Text="" Value="PTC AS 计划跟踪号,"  Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList24" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="PTC ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="PTC DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        物料编码
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList8" runat="server">
                            <asp:ListItem Text="" Value="MaterialCode AS 物料编码," Selected="True" Enabled="false"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                    
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList8" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="MaterialCode ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="MaterialCode DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        物料名称
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList9" runat="server">
                            <asp:ListItem Text="" Value="MaterialName AS 物料名称," Selected="True" Enabled="false"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList9" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="MaterialName ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="MaterialName DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        规格型号
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList12" runat="server">
                            <asp:ListItem Text="" Value="Standard AS 规格型号," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList12" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="Standard ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="Standard DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        材质
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList10" runat="server">
                            <asp:ListItem Text="" Value="Attribute AS 材质," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList10" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="Attribute ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="Attribute DESC,"></asp:ListItem>
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
                            <asp:ListItem Text="" Value="Fixed AS 是否定尺," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                    
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList13" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="Fixed ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="Fixed DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        批号
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList16" runat="server">
                            <asp:ListItem Text="" Value="LotNumber AS 批号," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList16" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="LotNumber ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="LotNumber DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        长
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList14" runat="server">
                            <asp:ListItem Text="" Value="cast(Length as varchar(50)) AS 长," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList14" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="Length ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="Length DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        宽
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList15" runat="server">
                            <asp:ListItem Text="" Value="cast(Width as varchar(50)) AS 宽," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList15" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="Width ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="Width DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        单位
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList17" runat="server">
                            <asp:ListItem Text="" Value="Unit AS 单位," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList17" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="Unit ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="Unit DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        实际发料重量
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList18" runat="server" >
                            <asp:ListItem Text="" Value="cast(RealNumber as varchar(50)) AS 实际发料重量," Selected="True" Enabled="false"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                  
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList18" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="RealNumber ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="RealNumber DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        实际发料张（支）数
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList19" runat="server">
                            <asp:ListItem Text="" Value="cast(RealSupportNumber as varchar(50)) AS 实际发料张数或支数," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList19" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="RealSupportNumber ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="RealSupportNumber DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        单价
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList20" runat="server">
                            <asp:ListItem Text="" Value="cast(UnitPrice as varchar(50)) AS 单价," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList20" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="UnitPrice ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="UnitPrice DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        金额
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList21" runat="server">
                            <asp:ListItem Text="" Value="cast(Amount as varchar(50)) AS 金额," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList21" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="Amount ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="Amount DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                  <tr>
                    <td>
                        含税单价
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList28" runat="server">
                            <asp:ListItem Text="" Value="cast(round(UnitPrice*1.17,4) as varchar(50)) AS 含税单价," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList28" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="UnitPrice ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="UnitPrice DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        含税金额
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList29" runat="server">
                            <asp:ListItem Text="" Value="cast(round(Amount*1.17,2) as varchar(50)) AS 含税金额," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList29" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="Amount ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="Amount DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        发料仓库
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList22" runat="server">
                            <asp:ListItem Text="" Value="Warehouse AS 发料仓库," Selected="True" Enabled="false" ></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList22" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="Warehouse ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="Warehouse DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        发料仓位
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList23" runat="server">
                            <asp:ListItem Text="" Value="Location AS 发料仓位," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                 
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList23" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="Location ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="Location DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        领料部门
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList2" runat="server">
                            <asp:ListItem Text="" Value="Dep AS 领料部门," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                  
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="Dep ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="Dep DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        制作班组
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList25" runat="server">
                            <asp:ListItem Text="" Value="ZZBZNM AS 制作班组," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList25" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="ZZBZNM ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="ZZBZNM DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        生产制号
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList26" runat="server">
                            <asp:ListItem Text="" Value="right(TSAID,charindex('-',REVERSE(TSAID))-1) AS 生产制号," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList26" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="TSAID ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="TSAID DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                 <tr>
                    <td>
                        中文生产制号
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList30" runat="server">
                            <asp:ListItem Text="" Value="TSAID AS 中文生产制号," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList30" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="TSAID ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="TSAID DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                 <tr>
                    <td>
                        子项名称
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList27" runat="server">
                            <asp:ListItem Text="" Value="OP_ZXMC AS 子项名称," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                  
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList27" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="OP_ZXMC ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="OP_ZXMC DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        发料人
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList4" runat="server">
                            <asp:ListItem Text="" Value="Sender AS 发料人," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList4" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="Sender ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="Sender DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        制单人
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList5" runat="server">
                            <asp:ListItem Text="" Value="Doc AS 制单人," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList5" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="Doc ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="Doc DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        制单日期
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList3" runat="server">
                            <asp:ListItem Text="" Value="Date AS 制单日期," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                  
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="Date ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="Date DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        审核人
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList6" runat="server">
                            <asp:ListItem Text="" Value="Verifier AS 审核人," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                  
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList6" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="Verifier ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="Verifier DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        审核日期
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList7" runat="server">
                            <asp:ListItem Text="" Value="left(ApprovedDate,10) AS 审核日期," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList7" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="ApprovedDate ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="ApprovedDate DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        条目备注
                    </td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList31" runat="server">
                            <asp:ListItem Text="" Value="DetailNote AS 条目备注," Selected="True"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                   
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList31" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Table">
                            <asp:ListItem Text="升序" Value="DetailNote ASC,"></asp:ListItem>
                            <asp:ListItem Text="降序" Value="DetailNote DESC,"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="3" align="center">
                        <asp:Button ID="Comfirm" runat="server" Text="确定" OnClick="Confirm_Click" OnClientClick="DownloadFile();"
                            CssClass="button" />&nbsp;&nbsp;&nbsp;
                        <input id="Cancel" type="button" value="关闭" onclick="closewin()" class="button" />&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
    </form>
</body>
</html>
