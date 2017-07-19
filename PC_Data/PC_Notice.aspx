<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_Notice.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_Notice" Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    采购部公告
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />

    <script src="../JS/DatePicker.js" language="javascript" type="text/javascript"></script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="box-inner" style="vertical-align: top">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 88%">
                    <tr>
                        <td style="width: 16%;">
                            <b>采购部公告</b>
                        </td>
                        <td align="right">
                            项目名称:
                        </td>
                        <td style="width: 18%; height: 42px" valign="top">
                            <cc1:ComboBox ID="ddlProName" runat="server" AutoPostBack="true" Height="15px" Width="90px"
                                AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" onselectedindexchanged="ddlProName_SelectedIndexChanged">
                            </cc1:ComboBox>
                           
                        </td>
                        <td align="right">
                            部门来源:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddldepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            状 态:
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblstatus" RepeatColumns="2" runat="server" AutoPostBack="true"
                                Height="15px" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged">
                                <asp:ListItem Text="未读" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="已阅" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="联系单号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px">
                                <ItemTemplate>
                                    <asp:Label ID="id" runat="server" Text='<%# Eval("DCS_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DCS_PROJECTID" HeaderText="项目编号" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" />
                            <asp:BoundField DataField="DCS_PROJECT" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" />
                            <asp:BoundField DataField="DCS_DEPNAME" HeaderText="来源部门" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" />
                            <asp:TemplateField HeaderText="类别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:Label ID="type" runat="server" Text='<%# Eval("DCS_TYPE").ToString()=="1"?"合同":Eval("DCS_TYPE").ToString()=="2"?"任务单":"其他" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="DCS_TYPE" HeaderText="类别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px" />--%>
                            <asp:BoundField DataField="DCS_EDITOR" HeaderText="委托人" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" />
                            <asp:BoundField DataField="DCS_DATE" HeaderText="提交时间" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="80px" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="状态" ItemStyle-Width="48px">
                                <ItemTemplate>
                                    <asp:Label ID="status" runat="server" Text='<%# Eval("DCS_STATE").ToString()=="1"?"已阅":"未读"%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="详细信息" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="56px">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlTask1" CssClass="link" runat="server" NavigateUrl='<%#"~/CM_Data/CM_Notice.aspx?action="+Eval("ID") %>'>
                                        <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />查看
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <asp:Panel ID="NoDataPanel" runat="server">
                        没有任务!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
