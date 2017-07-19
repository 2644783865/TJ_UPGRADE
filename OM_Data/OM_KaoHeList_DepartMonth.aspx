<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_KaoHeList_DepartMonth.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_KaoHeList_DepartMonth" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
部门月度考核
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<table>
        <tr width="100%">
            <td style="width: 10%;">
                <strong>条件选择</strong>
            </td>
        
            <td style="width: 25%;">
                <strong>时间：</strong>
                <asp:DropDownList ID="dplYear" runat="server">
                </asp:DropDownList>
                &nbsp;年&nbsp;
                <asp:DropDownList ID="dplMoth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;月&nbsp;
            </td>
            <td align="right" width="80px">
                制单时间 从：
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtStart" class="easyui-datebox"></asp:TextBox>
            </td>
            <td align="right" width="30px">
                到：
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtEnd" class="easyui-datebox"></asp:TextBox>
            </td>
            <td>
            <asp:Button ID="btnSearch" runat="server" Text="查 询" OnClick="ddl_Year_SelectedIndexChanged" />&nbsp;&nbsp;&nbsp;
              <asp:Button ID="btnDaochu" runat="server" Text="导 出" OnClick="btnDaochu_OnClick" />
            </td>
            <td align="right" width="15%">
                <asp:HyperLink ID="hpTask" NavigateUrl="OM_KaoHeDetail_DepartMonth.aspx?action=add" runat="server">
                    <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    添加记录</asp:HyperLink>&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:RadioButtonList runat="server" ID="rblState" OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged"
                    RepeatColumns="6" AutoPostBack="true">
                    <asp:ListItem Text="全部" Value="" ></asp:ListItem>
                    <asp:ListItem Text="未提交" Value="0"></asp:ListItem>
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
                                <strong>制单人</strong>
                            </td>
                            <td>
                                <strong>制单时间</strong>
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
                                <%#Eval("ZDRNM")%>
                            </td>
                            <td>
                                <%#Eval("ZDTIME")%>
                            </td>

                            <td>
                                <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_KaoHeDetail_DepartMonth.aspx?action=view&key="+Eval("Context")%>'
                                    runat="server" ID="HyperLink1">
                                    <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />查看
                                </asp:HyperLink>
                            </td>
                            <td width="100px">
                                <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_KaoHeDetail_DepartMonth.aspx?action=edit&key="+Eval("Context")%>'
                                    runat="server" ID="HyperLink2">
                                    <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />编辑
                                </asp:HyperLink>
                            </td>
                            <td width="100px">
                                <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_KaoHeDetail_DepartMonth.aspx?action=audit&key="+Eval("Context")%>'
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
