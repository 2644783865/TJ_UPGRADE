<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_YongYinList.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_YongYinList" %>

<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    用印管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <table width="100%">
        <tr style="width: 100%; height: 30px">
            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <td align="right" width="100%" colspan="5">
                <asp:HyperLink ID="HyperLink4" NavigateUrl="OM_YongYinDetial_cw_new1.aspx?action=add&type=6"
                    runat="server">
                    <asp:Image ID="Image4" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    公章申请</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="hpTask" NavigateUrl="OM_YongYinDetial_cw_new4.aspx?action=add&type=9"
                    runat="server">
                    <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    合同章申请</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="HyperLink5" NavigateUrl="OM_YongYinDetial_cw_new2.aspx?action=add&type=7"
                    runat="server">
                    <asp:Image ID="Image7" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    资质文件借阅</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="HyperLink1" NavigateUrl="OM_YongYinDetial_cw_new3.aspx?action=add&type=8"
                    runat="server">
                    <asp:Image ID="Image2" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    法人姓名章</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="HyperLink7" NavigateUrl="OM_YongYinDetial_cw_new5.aspx?action=add&type=10"
                    runat="server">
                    <asp:Image ID="Image9" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    总经理姓名章</asp:HyperLink>&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr style="width: 100%">
            <td align="left" width="28%">
                <strong>条件选择</strong> 申请单号：
                <asp:TextBox runat="server" ID="txt_sqdh"></asp:TextBox>
            </td>
            <td align="left" width="20%">
                申请人：
                <asp:TextBox runat="server" ID="txt_sqr"></asp:TextBox>
            </td>
            <td align="left" width="20%">
                所属部门：
                <asp:DropDownList runat="server" ID="ddlLZ_BUMEN" AutoPostBack="true" OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="left" width="32%">
                合同号/任务号、项目名称：
                <asp:TextBox runat="server" ID="txt_ht"></asp:TextBox>
            </td>

        </tr>
        <tr style="width: 100%">
            <td align="left" width="20%">
                文件用途：
                <asp:TextBox runat="server" ID="txt_yt"></asp:TextBox>
            </td>
            <td align="left" width="25%">
                制单时间 从：
                <asp:TextBox runat="server" ID="txtStart" class="easyui-datebox"></asp:TextBox>
            </td>
            <td align="left" width="20%">
                到：
                <asp:TextBox runat="server" ID="txtEnd" class="easyui-datebox"></asp:TextBox>
            </td>
            <td align="left" width="20%" style="display: none">
                类型：
                <asp:TextBox runat="server" ID="txt_nx"></asp:TextBox>
            </td>
            <td align="left" width="15%">
                <asp:Button ID="btnSearch" runat="server" Text="查 询" OnClick="ddl_Year_SelectedIndexChanged" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDaochu" runat="server" Text="导 出" OnClick="btnDaochu_OnClick"
                    Visible="false" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:RadioButtonList runat="server" ID="rblState" OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged"
                    RepeatColumns="7" AutoPostBack="true">
                    <asp:ListItem Text="全部" Value=""></asp:ListItem>
                    <asp:ListItem Text="未提交" Value="0"></asp:ListItem>
                    <asp:ListItem Text="审核中" Value="1"></asp:ListItem>
                    <asp:ListItem Text="未盖章" Value="2"></asp:ListItem>
                    <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                    <asp:ListItem Text="已盖章" Value="5"></asp:ListItem>
                    <asp:ListItem Text="我的审核任务" Value="4" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <div class="box-wrapper">
        <div class="box-outer">
            <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                <asp:Repeater ID="rep_Kaohe" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle headcolor">
                            <td>
                                单号
                            </td>
                            <td>
                                <strong>申请人</strong>
                            </td>
                            <td>
                                <strong>所在部门</strong>
                            </td>
                            <td>
                                <strong>类型</strong>
                            </td>
                            <td>
                                <strong>申请时间</strong>
                            </td>
                            <td>
                                合同号/任务号、项目名称
                            </td>
                            <td>
                                文件用途
                            </td>
                            <td>
                                数量
                            </td>
                            <td>
                                <strong>盖章人</strong>
                            </td>
                            <td id="tdView" runat="server">
                                <strong>查看</strong>
                            </td>
                            <td>
                                <strong>编辑</strong>
                            </td>
                            <td>
                                <strong>审核</strong>
                            </td>
                            <td>
                                <strong>盖章</strong>
                            </td>
                            <td>
                                <strong>删除</strong>
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class='baseGadget' onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                            <td>
                                <asp:Label ID="lblCode" Text='<%#Eval("Code")%>' runat="server"></asp:Label>
                                <asp:Label ID="lbl_splevel" Text='<%#Eval("SPLevel")%>' runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="Label1" Text='<%#Eval("SPLevel")%>' runat="server" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSQR" Text='<%#Eval("SQR")%>' runat="server"></asp:Label>
                                <asp:Label ID="lblSQRId" Text='<%#Eval("SQRId")%>' runat="server" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSQDEP" Text='<%#Eval("SqDep")%>' runat="server"></asp:Label>
                            </td>
                            <td>
                                <%# Eval("SPLevel").ToString().Contains("2") ? "合同章申请/文件借阅" : Eval("SPLevel").ToString().Contains("3") ? "公章申请" : Eval("SPLevel").ToString().Contains("4") ? "财务用印" : Eval("SPLevel").ToString().Contains("6") ? "公章申请" : Eval("SPLevel").ToString().Contains("7") ? "资质文件借阅" : Eval("SPLevel").ToString().Contains("8") ? "法人姓名章" : Eval("SPLevel").ToString().Contains("9") ? "合同章" : "总经理姓名章"%>
                            </td>
                            <td>
                                <%#Eval("SqTime")%>
                            </td>
                            <td>
                                <%#Eval("TaskId")%>
                            </td>
                            <td>
                                <%#Eval("Name")%>
                            </td>
                            <td>
                                <%#Eval("Num")%>
                            </td>
                            <td>
                                <%#Eval("GZR")%>
                            </td>
                            <td id="td1" runat="server">
                                <asp:HyperLink ID="HyperLink6" NavigateUrl='<%# viewSp(Eval("Context").ToString()) %>'
                                    runat="server">
                                    <asp:Image ID="Image8" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                        runat="server" />查看
                                </asp:HyperLink>
                            </td>
                            <td width="100px">
                                <asp:HyperLink NavigateUrl='<%# edit_yongyin_detail(Eval("Context").ToString()) %>'
                                    runat="server" ID="HyperLink2">
                                    <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />编辑
                                </asp:HyperLink>
                            </td>
                            <td width="100px">
                                <asp:HyperLink NavigateUrl='<%# aduit_yongyin_detail(Eval("Context").ToString()) %>'
                                    runat="server" ID="HyperLink3">
                                    <asp:Image ID="Image3" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />审核
                                </asp:HyperLink>
                            </td>
                            <td width="100px">
                                <asp:LinkButton runat="server" ID="hlGZ" OnClick="hlGZ_OnClick" CommandName='<%#Eval("Context") %>'
                                    OnClientClick="return confirm(&quot;确认盖章吗？&quot;)">
                                    <asp:Image ID="Image5" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />盖章</asp:LinkButton>
                            </td>
                            <td width="100px">
                                <asp:LinkButton runat="server" ID="hlDelete" OnClick="hlDelete_OnClick" CommandName='<%#Eval("Context") %>'
                                    OnClientClick="return confirm(&quot;确认删除吗？&quot;)">
                                    <asp:Image ID="Image6" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />删除</asp:LinkButton>
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
