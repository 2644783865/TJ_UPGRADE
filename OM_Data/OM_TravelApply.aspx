<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_TravelApply.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_TravelApply"
    Title="差旅申请" %>

<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    差旅申请
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <table width="100%">
        <tr>
            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <td colspan="5">
                <asp:RadioButtonList runat="server" ID="rblState" OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged"
                    RepeatColumns="7" AutoPostBack="true">
                    <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="未提交" Value="0"></asp:ListItem>
                    <asp:ListItem Text="审核中" Value="1"></asp:ListItem>
                    <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                    <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                    <asp:ListItem Text="已确认" Value="4"></asp:ListItem>
                    <asp:ListItem Text="我的审核任务" Value="5"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td align="right" width="15%">
                <asp:HyperLink ID="hpTask" NavigateUrl="OM_TravelApplyDetail.aspx?action=add" runat="server">
                    <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    差旅申请</asp:HyperLink>&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right" width="65px">
                <strong>条件选择 </strong>
            </td>
            <td style="width: 80px;" align="right">
                出差人：
            </td>
            <td colspan="4">
                <asp:TextBox runat="server" ID="txtTravelSQR" Width="100px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;出差时间 从：
                <asp:TextBox runat="server" ID="txtTravelStart" class="easyui-datebox" Width="100px"></asp:TextBox>
                &nbsp;&nbsp; 到：
                <asp:TextBox runat="server" ID="txtTravelEnd" class="easyui-datebox" Width="100px"></asp:TextBox>&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="查 询" OnClick="btnSearch_OnClick" />
            </td>
        </tr>
    </table>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="overflow-x: scroll">
                <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                    <asp:Repeater ID="rptTravel" runat="server" OnItemDataBound="rptTravel_ItemDataBound">
                        <HeaderTemplate>
                            <tr align="center" class="tableTitle headcolor" height="30px">
                                <td>
                                    <strong>序号</strong>
                                </td>
                                <td>
                                    <strong>查看/审核</strong>
                                </td>
                                <td>
                                    <strong>差旅单号</strong>
                                </td>
                                <td>
                                    <strong>出差人员</strong>
                                </td>
                                <td>
                                    <strong>所属部门</strong>
                                </td>
                                <td>
                                    <strong>预计时间</strong>
                                </td>
                                <td>
                                    <strong>预计天数</strong>
                                </td>
                                <td>
                                    <strong>出差事项</strong>
                                </td>
                                <td>
                                    <strong>出差地点</strong>
                                </td>
                                <td>
                                    <strong>实际时间</strong>
                                </td>
                                <td>
                                    <strong>实际天数</strong>
                                </td>
                                <td>
                                    <strong>备注</strong>
                                </td>
                                <td>
                                    <strong>审核状态</strong>
                                </td>
                                <td>
                                    <strong>编辑</strong>
                                </td>
                                <td>
                                    <strong>确认</strong>
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
                                    <asp:Label ID="lbTA_Code" runat="server" Text='<%#Eval("TA_Code")%>' Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:HyperLink runat="server" ID="HyperLink1">
                                        <asp:Image ID="ImgViewOrAudit" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                            runat="server" /><asp:Label ID="wordtext" runat="server" Text=""></asp:Label>
                                    </asp:HyperLink>
                                </td>
                                <td id="validcode" runat="server">
                                    <%#Eval("TA_Code")%>
                                </td>
                                <td>
                                    <%#Eval("TA_SQR")%>
                                </td>
                                <td>
                                    <%#Eval("TA_SQRDep")%>
                                </td>
                                <td>
                                    <%#Eval("TA_StartTimePlan")%>
                                </td>
                                <td>
                                    <%#Eval("TA_DaysPlan")%>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTA_Event" TextMode="MultiLine" Text='<%#Eval("TA_Event")%>' runat="server"
                                        Width="180px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTA_Place" TextMode="MultiLine" Text='<%#Eval("TA_Place")%>' runat="server"
                                        Width="120px"></asp:TextBox>
                                </td>
                                <td>
                                    <%#Eval("TA_StartTimeReal")%>
                                </td>
                                <td>
                                    <%#Eval("TA_DaysReal")%>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTA_Note" TextMode="MultiLine" Text='<%#Eval("TA_Note")%>' runat="server"
                                        Width="165px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lbTA_State" runat="server" Text='<%#Eval("TA_State").ToString()=="0"?"未提交":Eval("TA_State").ToString()=="4"?"已通过":Eval("TA_State").ToString()=="5"?"已驳回":Eval("TA_State").ToString()=="6"?"已确认":"审核中"%>'></asp:Label>
                                </td>
                                <td width="100px">
                                    <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_TravelApplyDetail.aspx?action=edit&key="+Eval("TA_Code")%>'
                                        runat="server" ID="HyperLink2">
                                        <asp:Image ID="ImgEdit" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                            runat="server" />编辑
                                    </asp:HyperLink>
                                </td>
                                <td width="100px">
                                    <asp:HyperLink NavigateUrl='<%#"~/OM_Data/OM_TravelApplyDetail.aspx?action=sure&key="+Eval("TA_Code")%>'
                                        runat="server" ID="HyperLink3">
                                        <asp:Image ID="ImgSure" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                            runat="server" />确认
                                    </asp:HyperLink>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_OnClick" runat="server" CommandArgument='<%# Eval("TA_Code")%>'
                                        OnClientClick="return confirm('确认删除吗?')">
                                        <asp:Image ID="Image2" ImageUrl="~/Assets/images/erase.gif" runat="server" border="0"
                                            hspace="2" align="absmiddle" />删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                没有记录!</asp:Panel>
            <asp:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
