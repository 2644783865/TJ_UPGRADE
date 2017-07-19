<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_KaoHe_JXGZ_Audit.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_KaoHe_JXGZ_Audit" %>

<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    部门绩效工资审核
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<table>
        <tr>
            <td colspan="3">
                <asp:RadioButtonList runat="server" ID="rblState" OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged"
                    RepeatColumns="6" AutoPostBack="true">
                    <asp:ListItem Text="全部" Value=""></asp:ListItem>
                    <asp:ListItem Text="审核中" Value="1"></asp:ListItem>
                    <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                    <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                    <asp:ListItem Text="我的审核任务" Value="4" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <div class="box-wrapper">
        <div class="box-outer">
            <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                <asp:Repeater ID="rep_Kaohe" runat="server">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle headcolor">
                            <td>
                                <strong>年</strong>
                            </td>
                            <td>
                                <strong>月</strong>
                            </td>
                            <td>
                                <strong>部门</strong>
                            </td>
                            <td>
                                <strong>制单人</strong>
                            </td>
                            <td>
                                <strong>制单时间</strong>
                            </td>
                            <td>
                                <strong>审核</strong>
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class='baseGadget' onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                            <td>
                                <%#Eval("Year") %>
                            </td>
                            <td>
                                <%#Eval("Month")%>
                            </td>
                            <td>
                                <%#Eval("DEP_NAME")%>
                            </td>
                            <td>
                                <%#Eval("ZDRNM")%>
                            </td>
                            <td>
                                <%#Eval("ZDTIME")%>
                            </td>
                            <td width="100px">
                                <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_KaoHe_JXGZ_Detail.aspx?action=audit&key="+Eval("Context")%>'
                                    runat="server" ID="HyperLink3">
                                    <asp:Image ID="Image3" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />审核
                                </asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                没有记录!</asp:Panel>
            <asp:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
