<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="CM_ServiceResult.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_ServiceResult"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    顾客服务信息反馈表
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="float: right">
        <asp:Button ID="btnsubmit" runat="server" Text="提 交" OnClick="btnsubmit_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnreturn" runat="server" Text="返 回" OnClick="btnreturn_Click" />
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="text-align: center">
            <div style="text-align: center; margin-top: 25px">
                <h2>
                    顾客服务信息反馈表</h2>
            </div>
            <table width="800px">
                <tr>
                    <td style="text-align: right; font-size: medium">
                        文件号：TJZJ-R-M-13
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; font-size: medium">
                        编号：GKFWXXFK11001&nbsp;&nbsp;&nbsp;&nbsp;版 本：1
                    </td>
                </tr>
            </table>
            <asp:Panel ID="panel" runat="server">
                <table width="800px" cellpadding="4" cellspacing="1" class="grid" border="1">
                    <tr>
                        <td width="100px">
                            合同号：
                        </td>
                        <td>
                            <asp:Label ID="CM_CONTR" runat="server"></asp:Label>
                        </td>
                        <td width="100px">
                            项目名称：
                        </td>
                        <td>
                            <asp:Label ID="CM_PJNAME" runat="server" Width="400px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            设备图号：
                        </td>
                        <td>
                            <asp:TextBox ID="CM_EQUIPMAP" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            设备名称：
                        </td>
                        <td>
                            <asp:Label ID="CM_EQUIP" runat="server" Width="400px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left">
                            现场实际问题描述：<br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="CM_QUESTION" runat="server" TextMode="MultiLine" Width="500px" Height="100px"></asp:TextBox>
                            <br />
                            <p style="float: right">
                                签字：<asp:Label ID="CM_SIGN1" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left">
                            原因分析：<br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="CM_REASON" runat="server" TextMode="MultiLine" Width="500px" Height="100px"></asp:TextBox>
                            <br />
                            <p style="float: right">
                                签字：<asp:Label ID="CM_SIGN2" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left">
                            现场处理过程及结果：<br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="CM_PRO" runat="server" TextMode="MultiLine" Width="500px" Height="100px"></asp:TextBox>
                            <br />
                            <p style="float: right">
                                签字：<asp:Label ID="CM_SIGN3" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left">
                            业主意见：<br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="CM_YEZHU" runat="server" TextMode="MultiLine" Width="500px" Height="100px"></asp:TextBox>
                            <br />
                            <p style="float: right">
                                签字：<asp:Label ID="CM_SIGN4" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;</p>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
        </div>
    </div>
</asp:Content>
