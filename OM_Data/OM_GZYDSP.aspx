<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_GZYDSP.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_GZYDSP" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
薪酬异动审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
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
                        <strong>时间：</strong>
                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_OnSelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;年&nbsp;
                        <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_OnSelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;月&nbsp;
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="radio_all" runat="server" Text="全部" GroupName="shenhe" OnCheckedChanged="radio_all_CheckedChanged"
                                            AutoPostBack="True" />
                            <asp:RadioButton ID="radio_mytask" runat="server" Text="我的任务" GroupName="shenhe" OnCheckedChanged="radio_mytask_CheckedChanged"
                                            AutoPostBack="True"  Checked="true"/>
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        审批状态：<asp:DropDownList ID="drp_state" runat="server" OnSelectedIndexChanged="drp_state_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="">-请选择-</asp:ListItem>
                                            <asp:ListItem Value="0">初始化</asp:ListItem>
                                            <asp:ListItem Value="1">审核中</asp:ListItem>
                                            <asp:ListItem Value="2">已通过</asp:ListItem>
                                            <asp:ListItem Value="3">已驳回</asp:ListItem>
                                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   </td>
                   <td align="right">
                        <asp:HyperLink ID="hplfjcb" runat="server" NavigateUrl="~/OM_Data/OM_GZTZADD.aspx?" Target="_blank" Font-Underline="false"><asp:Image ID="AddImage2" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" ImageAlign="AbsMiddle" runat="server" Width="20px" />薪酬调整</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                            <asp:Repeater ID="rptscczsp" runat="server">
                                <HeaderTemplate>
                                    <tr style="background-color: #B9D3EE;" height="30px">
                                        <td align="center">
                                            序号
                                        </td>
                                        <td align="center">
                                            编号
                                        </td>
                                        <td align="center">
                                            年月
                                        </td>
                                        <td align="center">
                                            发起人
                                        </td>
                                        <td align="center">
                                            发起时间
                                        </td>
                                        <td align="center">
                                            审核状态
                                        </td>
                                        <td align="center">
                                            备注
                                        </td>
                                        <td align="center">
                                            查看/审核
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                        ondblclick="javascript:changeback(this);" height="30px">
                                        <td>
                                            <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false" Onclick="checkme(this)" />
                                            <asp:Label ID="lbSP_ID" runat="server" Text='<%#Eval("SP_ID")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                        </td>
                                        <td id="Td1" runat="server" align="center">
                                            <asp:Label ID="GZTZSP_SPBH" runat="server" Text='<%#Eval("GZTZSP_SPBH")%>'></asp:Label>
                                        </td>
                                        <td id="Td2" runat="server" align="center">
                                            <asp:Label ID="GZTZSP_YEARMONTH" runat="server" Width="90px" Text='<%#Eval("GZTZSP_YEARMONTH")%>'></asp:Label>
                                        </td>
                                        <td id="Td3" runat="server" align="center">
                                            <asp:Label ID="GZTZSP_SQRNAME" runat="server" Width="50px" Text='<%#Eval("GZTZSP_SQRNAME")%>'></asp:Label>
                                        </td>
                                        <td id="Td4" runat="server" align="center">
                                            <asp:Label ID="GZTZSP_SQTIME" runat="server" Width="120px" Text='<%#Eval("GZTZSP_SQTIME")%>'></asp:Label>
                                        </td>
                                        <td id="Td5" runat="server" align="center">
                                            <asp:Label ID="TOTALSTATE" runat="server" Text='<%#Eval("TOTALSTATE").ToString()=="0"?"初始化":Eval("TOTALSTATE").ToString()=="1"?"审核中":Eval("TOTALSTATE").ToString()=="2"?"通过":Eval("TOTALSTATE").ToString()=="3"?"驳回":"出错"%>'></asp:Label>
                                        </td>
                                        <td id="Td6" runat="server" align="center">
                                            <asp:Label ID="GZTZSP_SQRNOTE" runat="server" Text='<%#Eval("GZTZSP_SQRNOTE")%>'></asp:Label>
                                        </td>
                                        <td>
                                        <asp:HyperLink ID="hlContract3" Target="_blank" ToolTip="查看/审核" NavigateUrl='<%#"~/OM_Data/OM_GZTZSPdetail.aspx?spid="+Eval("GZTZSP_SPBH") %>'
                                    CssClass="link" runat="server">
                                    <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                    查看/审核
                                </asp:HyperLink>
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
