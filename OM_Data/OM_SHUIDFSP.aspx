<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_SHUIDFSP.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_SHUIDFSP" Title="住宿水电费审批" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    住宿水电费审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
      $(function() {
            $("#tab tr:not(:first)").dblclick(function() {
                var id = $(this).find("input[name*=spbh]").val();
                window.open("OM_SHUIDFSP_detail.aspx?action=read&spbh=" + id);
            });
        })   
    </script>

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../FM_Data/FM_JS/SelectCondition.js" type="text/javascript"></script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper">
        <div class="box-outer">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:RadioButton ID="radio_all" runat="server" Text="全部" GroupName="shenhe" OnCheckedChanged="Query"
                            AutoPostBack="True" />
                        <asp:RadioButton ID="radio_mytask" runat="server" Text="我的任务" GroupName="shenhe"
                            OnCheckedChanged="Query" AutoPostBack="True" Checked="true" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 审批状态：<asp:DropDownList ID="drp_state" runat="server"
                            OnSelectedIndexChanged="Query" AutoPostBack="True">
                            <asp:ListItem Value="">-请选择-</asp:ListItem>
                            <asp:ListItem Value="0">初始化</asp:ListItem>
                            <asp:ListItem Value="1">待审批</asp:ListItem>
                            <asp:ListItem Value="2">已通过</asp:ListItem>
                            <asp:ListItem Value="3">已驳回</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        审批编号：<asp:TextBox runat="server" ID="txtSPBH" Width="80px"></asp:TextBox>
                        <%-- <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSPBH"
                            ServicePath="OM_Data_Autocomplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                            CompletionInterval="10" ServiceMethod="GetSPBH" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                        </asp:AutoCompleteExtender>--%>
                        <asp:Button runat="server" ID="btnChakan" Text="查询" OnClick="Query" BackColor="#98FB98" />
                        <asp:Button ID="btn_Reset" runat="server" Text="重置" BackColor="#98FB98" OnClick="btn_Reset_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="rptsushe" runat="server" OnItemDataBound="rptsushe_OnItemDataBound">
                        <HeaderTemplate>
                            <tr style="background-color: #B9D3EE;" height="30px">
                                <td align="center">
                                    序号
                                </td>
                                <td align="center">
                                    审批编号
                                </td>
                                <td align="center">
                                    发起人
                                </td>
                                <td align="center">
                                    发起时间
                                </td>
                                <td align="center">
                                    审核人
                                </td>
                                <%--<td align="center">
                                    起始时间
                                </td>
                                <td align="center">
                                    截止时间
                                </td>--%>
                                <td align="center">
                                    审核状态
                                </td>
                                <td align="center">
                                    修改
                                </td>
                                <td align="center">
                                    审核
                                </td>
                                <td align="center">
                                    删除
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" title="双击查看详细信息">
                                <td>
                                    <asp:Label ID="lbXuHao" runat="server" Text=""></asp:Label>
                                    <input type="hidden" runat="server" id="hidspbh" value=' <%#Eval("spbh")%>' name="spbh" />
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbspbh" runat="server" Text='<%#Eval("spbh")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbcreatstname" runat="server" Width="50px" Text='<%#Eval("creatstname")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbcreattime" runat="server" Width="90px" Text='<%#Eval("creattime")%>'></asp:Label>
                                    <asp:Label ID="lbcreatstid" runat="server" Width="90px" Visible="false" Text='<%#Eval("creatstid")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbshrname" runat="server" Width="90px" Text='<%#Eval("shrname")%>'></asp:Label>
                                    <asp:Label ID="lbshrid" runat="server" Width="90px" Visible="false" Text='<%#Eval("shrid")%>'></asp:Label>
                                </td>
                                <%--<td align="center">
                                    <asp:Label ID="lbstartdate" runat="server" Width="50px" Text='<%#Eval("startdate")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbenddate" runat="server" Text='<%#Eval("enddate")%>'></asp:Label>
                                </td>--%>
                                <td align="center">
                                    <asp:Label ID="lbstate" runat="server" Text='<%#Eval("state").ToString()=="1"?"待审批":Eval("state").ToString()=="2"?"已通过":Eval("state").ToString()=="3"?"已驳回":"初始化"%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HyperLink runat="server" ID="hlXiuGai" NavigateUrl='<%#"OM_SHUIDFSP_detail.aspx?action=alert&spbh="+DataBinder.Eval(Container.DataItem,"spbh").ToString()%>'>
                                        <asp:Image runat="server" ID="imgXiuGai" ImageUrl="~/Assets/images/res.gif" Height="20px"
                                            ImageAlign="AbsMiddle" />
                                        修改
                                    </asp:HyperLink>
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlContract" runat="server" NavigateUrl='<%#"OM_SHUIDFSP_detail.aspx?action=check&spbh="+DataBinder.Eval(Container.DataItem,"spbh").ToString()%>'>
                                        <asp:Image ID="imgShenHe" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                        审核
                                    </asp:HyperLink>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_OnClick" runat="server" CommandArgument='<%# Eval("spbh")%>'
                                        CommandName="Del" OnClientClick="return confirm('确认删除吗?')">
                                        <asp:Image ID="Image2" ImageUrl="~/Assets/images/erase.gif" runat="server" border="0"
                                            hspace="2" align="absmiddle" />删除</asp:LinkButton>
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
</asp:Content>
