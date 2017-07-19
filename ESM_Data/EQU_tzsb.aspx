<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EQU_tzsb.aspx.cs" Inherits="ZCZJ_DPF.ESM.EQU_tzsb"
    Title="特种设备管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="uc1" Src="~/Controls/UCPaging.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
            ActiveTabIndex="0" Height="500px">
            <asp:TabPanel ID="Tab_equipment" runat="server" Width="100%" HeaderText="特种设备管理">
                <HeaderTemplate>
                    特种设备管理</HeaderTemplate>
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <asp:Label ID="label2" runat="server" Text="按设备名称查看："></asp:Label>
                                <asp:TextBox ID="name" runat="server"></asp:TextBox>
                                <asp:Button ID="search" runat="server" Text="查看" OnClick="search_Click" />
                            </td>
                            <td align="right">
                                <asp:HyperLink ID="HyperLink3" NavigateUrl="javascript:window:showModalDialog('EQU_tzsbop.aspx?action=add','','dialogWidth=800px dialogHeight=700px');"
                                    runat="server"><img src="../Assets/images/Add_new_img.gif" />添加特种设备</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                    <div class="box-wrapper">
                        <div class="box-outer">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                        border="1" style="cursor: pointer">
                                        <asp:Repeater ID="tzsbRepeater" runat="server">
                                            <HeaderTemplate>
                                                <tr align="center" class="tableTitle">
                                                    <td>
                                                        <strong>内部编号 </strong>
                                                    </td>
                                                    <td>
                                                        <strong>设备名称 </strong>
                                                    </td>
                                                   <%-- <td visible="false">
                                                        <strong>设备类型 </strong>
                                                    </td>--%>
                                                    <td>
                                                        <strong>型号规格 </strong>
                                                    </td>
                                                    <td>
                                                        <strong>出厂编号 </strong>
                                                    </td>
                                                    <td>
                                                        <strong>注册编号 </strong>
                                                    </td>
                                                    <td>
                                                        <strong>使用证号 </strong>
                                                    </td>
                                                    <td>
                                                        <strong>制造商 </strong>
                                                    </td>
                                                    <td>
                                                        <strong>安置位置 </strong>
                                                    </td>
                                                    <td>
                                                        <strong>使用状态 </strong>
                                                    </td>
                                                    <td>
                                                        <strong>再检日期 </strong>
                                                    </td>
                                                    <td>
                                                        <strong>备注 </strong>
                                                    </td>
                                                    </td>
                                                    <td>
                                                        <strong>修改</strong>
                                                    </td>
                                                    <td>
                                                        <strong>删除</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="baseGadget" onmouseover="this.CName='highlight'" onmouseout="this.CName='baseGadget'">
                                                    <asp:Label ID="Id" runat="server" Visible="false" Text='<%#Eval("Id")%>'></asp:Label>
                                                    <td>
                                                        <%#Eval("Code")%>&nbsp;
                                                    </td>
                                                    <td>
                                                        <%#Eval("Name")%>&nbsp;
                                                    </td>
                                                    <%--<td>
                                                        <%#Eval("Type")%>&nbsp;
                                                    </td>--%>
                                                    <td>
                                                        <%#Eval("Specification")%>&nbsp;
                                                    </td>
                                                    <td>
                                                        <%#Eval("Ocode")%>&nbsp;
                                                    </td>
                                                    <td>
                                                        <%#Eval("Rcode")%>&nbsp;
                                                    </td>
                                                    <td>
                                                        <%#Eval("Ucode")%>&nbsp;
                                                    </td>
                                                    <td>
                                                        <%#Eval("Manufa")%>&nbsp;
                                                    </td>
                                                    <td>
                                                        <%#Eval("Position")%>&nbsp;
                                                    </td>
                                                    <td>
                                                        <%#Eval("Ustate")%>&nbsp;
                                                    </td>
                                                    <td>
                                                        <%#Eval("Redate")%>&nbsp;
                                                    </td>
                                                    <td>
                                                        <%#Eval("Remark")%>&nbsp;
                                                    </td>
                                                    <td title="点击修改">
                                                        <asp:HyperLink ID="HyperLink2" NavigateUrl='<%# editYg(Eval("Id").ToString()) %>'
                                                            runat="server">
                                                            <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                                                runat="server" />修改</asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="checkboxstaff" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                                        </asp:CheckBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Panel ID="NoDataPane" runat="server">
                                        </asp:Panel>
                                    </table>
                                     <div class="PageChange">
                                        <asp:Button ID="deletebt" runat="server" Text="删除" OnClick="deletebt_Click"/>
                                    </div>
                                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                                    <div align="center">
                                        <asp:Label ID="Lnumber" runat="server" Text=""></asp:Label></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
    </form>
</body>
</html>
