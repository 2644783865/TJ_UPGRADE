<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="TM_JICHUANGBASEDETAIL.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.TM_JICHUANGBASEDETAIL" Title="机床明细页面" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    机床明细页面
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
       <div style="width: 100%">
              <table width="100%">
                <tr>
                    <td align="right">
                        <a id="btnsave" class="easyui-linkbutton" runat="server" onserverclick="btnsave_click">保存</a>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
              </table>
         </div>
         <div>
                 <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1">
                        <tr>
                            <td align="center">
                                添加人：
                            </td>
                            <td align="center">
                                <asp:Label ID="lbaddper" runat="server"></asp:Label>
                                <asp:Label ID="lbaddperid" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td align="center">
                                添加时间：
                            </td>
                            <td align="center">
                                <asp:Label ID="lbaddtime" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                是否在用：
                            </td>
                            <td align="center">
                                <asp:RadioButtonList ID="rad_state" runat="server" RepeatColumns="2">
                                    <asp:ListItem Text="在用" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="停用" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                机床编号：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="jc_bh" runat="server"></asp:TextBox>
                            </td>
                            <td align="center">
                                机床类型：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="jc_type" runat="server"></asp:TextBox>
                            </td>
                            <td align="center">
                                适用工序：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="jc_gxtypeable" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                包含人员：
                            </td>
                            <td align="center">
                                <asp:TextBox ID="jc_containper" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                备注：
                            </td>
                            <td align="center" colspan="3">
                                <asp:TextBox ID="jc_note" runat="server" Width="90%" TextMode="MultiLine" Height="50px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
              </div>
</asp:Content>
