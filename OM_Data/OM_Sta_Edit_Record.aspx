<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_Sta_Edit_Record.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_Sta_Edit_Record"
    Title="无标题页" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    人员信息调整记录
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-wrapper">
        <div class="box-outer">
            <div>
                <table>
                    <tr>
                        <td align="right">
                            被编辑人:
                        </td>
                        <td>
                            <asp:TextBox ID="txtBEditPer" runat="server"></asp:TextBox>
                        </td>
                        <td align="right">
                            操作类型:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCAOZUOType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnSearch_Click">
                                <asp:ListItem Text="请选择" Value="00" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="修改" Value="修改"></asp:ListItem>
                                <asp:ListItem Text="添加" Value="添加"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            编辑人:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEdiePer" runat="server"></asp:TextBox>
                        </td>
                        <td align="right">
                            编辑时间 从:
                        </td>
                        <td>
                            <asp:TextBox ID="txtStart" runat="server" class="easyui-datebox"></asp:TextBox>
                        </td>
                        </td>
                        <td align="right">
                            到
                        </td>
                        <td>
                            <asp:TextBox ID="txtEnd" runat="server" class="easyui-datebox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="rptProNumCost" runat="server">
                        <HeaderTemplate>
                            <tr align="center" style="background-color: #B9D3EE;">
                                <td>
                                    序号
                                </td>
                                <td>
                                    被编辑人
                                </td>
                                <td>
                                    编辑表格
                                </td>
                                <td>
                                    操作类型
                                </td>
                                <td>
                                    编辑日期
                                </td>
                                <td>
                                    编辑人
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);">
                                <td>
                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false"
                                        Onclick="checkme(this)" />
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                </td>
                                <td id="Td1" runat="server" align="center">
                                    <asp:Label ID="bSTNAME" runat="server" Text='<%#Eval("bSTNAME")%>'></asp:Label>
                                </td>
                                <td id="Td2" runat="server" align="center">
                                    <asp:Label ID="Type" runat="server" Text='<%#Eval("Type")%>'></asp:Label>
                                </td>
                                <td id="Td3" runat="server" align="center">
                                    <asp:Label ID="lblPart" runat="server" Text='<%#Eval("Caozuo")%>'></asp:Label>
                                </td>
                                <td id="Td4" runat="server" align="center">
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("EditTime")%>'></asp:Label>
                                </td>
                                <td id="Td5" runat="server" align="center">
                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("EditPerName")%>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                    没有记录!<br />
                    <br />
                </asp:Panel>
            </div>
        </div>
    </div>
    <uc1:UCPaging ID="UCPaging1" runat="server" />
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lblTip" runat="server" Text="提示:" Visible="false" Font-Bold="true"></asp:Label>
                <asp:Image ID="imgloading" runat="server" Visible="false" />
                <asp:Label ID="lblTip2" runat="server" Visible="false" Text="数据处理中，请稍后..."></asp:Label>
                <asp:Label ID="LabelDate" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
