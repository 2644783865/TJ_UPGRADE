<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_MP_ExportExcel.aspx.cs"
    Inherits="ZCZJ_DPF.TM_Data.TM_MP_ExportExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>导出材料计划</title>
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
    <base id="goDownload" target="_self" />

    <script language="javascript" type="text/javascript">
        function MpExport(obj) {
            //document.getElementById("btnClose").disabled=true;
            document.getElementById("imgloading").className = "show";

            var downloadBase = document.getElementById("goDownload");
            var downloadBox = document.getElementById("download");
            download.src = document.URL;

            downloadBase.target = "download";
            setTimeout("CancelMsg()", 100);
            return true;
        }

        function CancelMsg() {
            var downloadBase = document.getElementById("goDownload");
            downloadBase.target = "_self";
            document.getElementById("btnMpExport").disabled = true;
        }
    </script>

</head>
<body>
    <iframe id="download" name="download" height="0px" width="0px"></iframe>
    <!--用iframe模拟文件下载-->
    <form id="form1" runat="server" style="width: 100%">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td style="width: 33%">
                            任务号：
                            <asp:Label ID="tsaid" runat="server"></asp:Label>
                        </td>
                        <td style="width: 33%">
                            项目名称：
                            <asp:Label ID="proname" runat="server"></asp:Label>
                            <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                        </td>
                        <td>
                            设备名称：
                            <asp:Label ID="engname" runat="server"></asp:Label>
                            <input id="eng_type" type="text" value="" readonly="readonly" runat="server" style="display: none" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper" style="text-align: center;">
        <div class="box-outer">
            <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1">
                <%--  <tr>
                    <td>
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="rblMpType" runat="server" AutoPostBack="true" RepeatColumns="2"
                            RepeatDirection="Horizontal" OnSelectedIndexChanged="ddlmptype_SelectedIndexChanged">
                            <asp:ListItem Text="正常计划" Value="TBPM_MPFORALLRVW"></asp:ListItem>
                          <asp:ListItem Text="取消计划" Value="TBPM_MPCHANGERVW"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>--%>
                <tr>
                    <td style="width: 10%;" class="tdleft1">
                        <strong>提交类别:</strong>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlmptype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmptype_SelectedIndexChanged">
                            <asp:ListItem Text="全部提交" Value="所有类型"></asp:ListItem>
                            <asp:ListItem Text="钢材" Value="钢材"></asp:ListItem>
                            <asp:ListItem Text="标准件" Value="采"></asp:ListItem>
                            <asp:ListItem Text="采购成品" Value="采购成品"></asp:ListItem>
                            <asp:ListItem Text="非金属" Value="非"></asp:ListItem>
                            <asp:ListItem Text="铸件" Value="铸件"></asp:ListItem>
                            <asp:ListItem Text="锻件" Value="锻件"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdleft1">
                        <strong>计划批号:</strong>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlLotNumList" runat="server" ToolTip="'全部'项为红色，即无可导出数据！">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Image ID="imgloading" CssClass="hidden" ImageUrl="~/Assets/images/PrgBar.gif"
                            border="0" hspace="2" align="absmiddle" runat="server" />
                    </td>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnMpExport" runat="server" Text="导出" OnClientClick="return MpExport(this);"
                            OnClick="btnMpExport_OnClick" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <input id="btnClose" type="button" runat="server" value="取消" onclick="window.close();" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
