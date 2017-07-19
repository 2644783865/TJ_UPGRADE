<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_TravelDelay.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_TravelDelay"
    Title="差旅延期" %>

<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    差旅延期
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <table width="100%">
        <tr>
            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <td colspan="5">
                <asp:RadioButtonList runat="server" ID="rblState" OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged"
                    RepeatColumns="6" AutoPostBack="true">
                    <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="未提交" Value="0"></asp:ListItem>
                    <asp:ListItem Text="审核中" Value="1"></asp:ListItem>
                    <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                    <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                    <asp:ListItem Text="我的审核任务" Value="4"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td align="right" width="15%">
                <asp:HyperLink ID="hpTask" NavigateUrl="OM_TravelDelayDetail.aspx?action=add" runat="server">
                    <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    差旅延期</asp:HyperLink>&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right" width="65px">
                <strong>条件选择 </strong>
            </td>
            <td style="width: 80px;" align="right">
                制单人：
            </td>
            <td colspan="4">
                <asp:TextBox runat="server" ID="txtTravelZDR" Width="80px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;制单时间 从：
                <asp:TextBox runat="server" ID="txtTravelStart" class="easyui-datebox" Width="100px"></asp:TextBox>
                &nbsp;&nbsp; 到：
                <asp:TextBox runat="server" ID="txtTravelEnd" class="easyui-datebox" Width="100px"></asp:TextBox>&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="查 询" OnClick="btnSearch_OnClick" />
            </td>
        </tr>
    </table>
    <div class="box-wrapper">
        <div class="box-outer">
            <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                <asp:Repeater ID="rptTravelDelay" runat="server" OnItemDataBound="rptTravelDelay_ItemDataBound">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle headcolor">
                            <td>
                                <strong>序号</strong>
                            </td>
                            <td>
                                <strong>差旅延期单号</strong>
                            </td>
                            <td>
                                <strong>制单人</strong>
                            </td>
                            <td>
                                <strong>制单时间</strong>
                            </td>
                            <td>
                                <strong>审核级别</strong>
                            </td>
                            <td>
                                <strong>延期审核</strong>
                            </td>
                            <td>
                                <strong>查看</strong>
                            </td>
                            <td>
                                <strong>审核</strong>
                            </td>
                            <td>
                                <strong>编辑</strong>
                            </td>
                            <td>
                                <strong>删除</strong>
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class='baseGadget' onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                            <td>
                                <asp:Label ID="lbXuhao" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lbTD_Code" runat="server" Text='<%#Eval("TD_Code")%>' Visible="false"></asp:Label>
                            </td>
                            <td>
                                <%#Eval("TD_Code")%>
                            </td>
                            <td>
                                <%#Eval("TD_ZDR")%>
                            </td>
                            <td>
                                <%#Eval("TD_ZDTime")%>
                            </td>
                            <td>
                                <%#Eval("TD_SHLevel")%>
                            </td>
                            <td>
                                <asp:Label ID="lbTD_State" runat="server" Text='<%#Eval("TD_State").ToString()=="0"?"未提交":Eval("TD_State").ToString()=="4"?"已通过":Eval("TD_State").ToString()=="5"?"已驳回":"审核中"%>'></asp:Label>
                            </td>
                            <td>
                                <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_TravelDelayDetail.aspx?action=view&key="+Eval("TD_Code")%>'
                                    runat="server" ID="HyperLink1">
                                    <asp:Image ID="ImageView" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />查看
                                </asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_TravelDelayDetail.aspx?action=audit&key="+Eval("TD_Code")%>'
                                    runat="server" ID="HyperLink2">
                                    <asp:Image ID="ImgAudit" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />审核
                                </asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_TravelDelayDetail.aspx?action=edit&key="+Eval("TD_Code")%>'
                                    runat="server" ID="HyperLink3">
                                    <asp:Image ID="ImgEdit" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />编辑
                                </asp:HyperLink>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_OnClick" runat="server" CommandArgument='<%# Eval("TD_Code")%>'
                                    OnClientClick="return confirm('确认删除吗?')">
                                    <asp:Image ID="Image2" ImageUrl="~/Assets/images/erase.gif" runat="server" border="0"
                                        hspace="2" align="absmiddle" />删除</asp:LinkButton>
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
