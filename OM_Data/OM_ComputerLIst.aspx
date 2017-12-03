<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_ComputerLIst.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_ComputerLIst"
    Title="无标题页" %>

<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    办公设备报修
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <table width="100%">
        <tr>
            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <td style="width: 7%;">
                <strong>条件选择</strong>
            </td>
            <td>
                制单时间 从：&nbsp;&nbsp;
                <asp:TextBox runat="server" ID="txtStart" class="easyui-datebox"></asp:TextBox>&nbsp;&nbsp;
                到：
                <asp:TextBox runat="server" ID="txtEnd" class="easyui-datebox"></asp:TextBox>&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="查 询" OnClick="ddl_Year_SelectedIndexChanged" />
            </td>
            <td align="right">
                <asp:HyperLink ID="hpTask" NavigateUrl="OM_ComputerDetail.aspx?action=add" runat="server">
                    <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    报修申请</asp:HyperLink>&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:RadioButtonList runat="server" ID="rblState" OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged"
                    RepeatColumns="7" AutoPostBack="true">
                    <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="未提交" Value="0"></asp:ListItem>
                    <asp:ListItem Text="审核中" Value="1"></asp:ListItem>
                    <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                    <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                    <asp:ListItem Text="已确认" Value="5"></asp:ListItem>
                    <asp:ListItem Text="我的审核任务" Value="4"></asp:ListItem>
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
                                <strong>申请人</strong>
                            </td>
                            <td>
                                <strong>所在部门</strong>
                            </td>
                            <td>
                                <strong>申请时间</strong>
                            </td>
                            <td>
                                <strong>反馈人</strong>
                            </td>
                            <td>
                                <strong>查看</strong>
                            </td>
                            <td>
                                <strong>编辑</strong>
                            </td>
                            <td>
                                <strong>审核</strong>
                            </td>
                            <td>
                                <strong>确认维修</strong>
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class='baseGadget' onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                            <td>
                                <%#Eval("SQR")%>
                            </td>
                            <td>
                                <%#Eval("SqDep")%>
                            </td>
                            <td>
                                <%#Eval("SqTime")%>
                            </td>
                            <td>
                                <%#Eval("GZR")%>
                            </td>
                            <td>
                                <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_ComputerDetail.aspx?action=view&key="+Eval("Context")%>'
                                    runat="server" ID="HyperLink1">
                                    <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />查看
                                </asp:HyperLink>
                            </td>
                            <td width="100px">
                                <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_ComputerDetail.aspx?action=edit&key="+Eval("Context")%>'
                                    runat="server" ID="HyperLink2">
                                    <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />编辑
                                </asp:HyperLink>
                            </td>
                            <td width="100px">
                                <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_ComputerDetail.aspx?action=audit&key="+Eval("Context")%>'
                                    runat="server" ID="HyperLink3">
                                    <asp:Image ID="Image3" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />审核
                                </asp:HyperLink>
                            </td>
                            <td width="100px">
                                <asp:LinkButton runat="server" ID="hlGZ" OnClick="hlGZ_OnClick" CommandName='<%#Eval("Context") %>'
                                    OnClientClick="return confirm(&quot;确认维修吗？&quot;)">
                                    <asp:Image ID="Image5" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />确认维修</asp:LinkButton>
                                <asp:Label ID="lbState" runat="server" Text='<%#Eval("State") %>' Visible="false"></asp:Label>
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
