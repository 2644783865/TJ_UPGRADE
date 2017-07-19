<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="OM_CarAddSafe.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CarAddSafe" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    新增车辆维修记录
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnOK" runat="server" Text="确 定" OnClick="btnOK_OnClick" Width="100px"  CssClass="button-outer" /> 
                                 &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReturn" runat="server" Text="返 回" Width="100px"  OnClick="btnReturn_OnClick" CausesValidation="False" CssClass="button-outer"/>
                                 &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
            <table id="Table1" align="center" cellpadding="4" cellspacing="1" runat="server" class="toptable grid" border="1">
                <tr>
                    <td width="100px">车牌号：</td>
                    <td>
                    <asp:DropDownList ID="ddlcarnum" runat="server" Width="300px" AutoPostBack="true" />
                    <asp:TextBox ID="txtCarNum" runat="server" Enabled="false" Visible="false" Width="300px"></asp:TextBox>
                    <span id="span1" runat="server" class="Error">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写车牌号！"
                         ControlToValidate="txtCarNum" Display="Dynamic"></asp:RequiredFieldValidator></td>
                </tr>
                
                <tr>
                    <td width="100px">维护时间：</td>
                    <td>
                    <asp:TextBox ID="txtSafeTime" runat="server" Width="300px" class="easyui-datebox" editable="false"></asp:TextBox>
                    <%--<asp:TextBox ID="txtSafeTime" runat="server" Width="300px"></asp:TextBox>--%></td>
                </tr>
                
                <tr>
                    <td width="100px">维护内容：</td>
                    <td><asp:TextBox ID="txtSafeText" runat="server" Width="300px" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                
                <tr>
                    <td width="100px">维护厂家或地点：</td>
                    <td><asp:TextBox ID="txtSafeSite" runat="server" Width="300px" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                
                <tr>
                    <td width="100px">维护费用：</td>
                    <td><asp:TextBox ID="txtSafeCost" runat="server" Width="300px"></asp:TextBox>元（￥）</td>
                </tr>
                
                <tr>
                    <td width="100px">经办人：</td>
                    <td>
                    <asp:DropDownList ID="txtSafer" runat ="server" Width="300px" />
                    <%--<asp:TextBox ID="txtSafer" runat="server" Width="300px"></asp:TextBox>--%></td>
                </tr>
                
                <tr>
                    <td width="100px">填表时间：</td>
                    <td>
                        <asp:Label ID="lblAddtime" runat="server" Width="300px"></asp:Label></td>
                </tr>
            </table>
</asp:Content>