<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_SCCZSP.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_SCCZSP" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
生产操作人员岗位绩效
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
                        <asp:FileUpload ID="FileUpload1" Width="130px" runat="server" ToolTip="导 入(1)" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_importclient" runat="server" Text="导入(1)" OnClientClick="viewCondition()" />
                        <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btn_importclient"
                                    PopupControlID="PanelCondition" Drag="True" Enabled="True" DynamicServicePath=""
                                    Y="80" X="700">
                        </asp:ModalPopupExtender>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:FileUpload ID="FileUpload2" Width="130px" runat="server" ToolTip="导 入(2)" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_importbzz" runat="server" Text="导入(2)" OnClientClick="viewCondition()" />
                        <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btn_importbzz"
                                    PopupControlID="PanelCondition2" Drag="True" Enabled="True" DynamicServicePath=""
                                    Y="80" X="850">
                        </asp:ModalPopupExtender>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   </td>
                </tr>
            </table>
            <asp:Panel ID="PanelCondition" runat="server" Width="350px" Style="display: none">
                            <table width="90%" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                    <td colspan="8" align="center">
                                        <asp:Button ID="QueryButton" runat="server" OnClick="btn_import_Click" Text="确认" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="取消" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        月份
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_yearmonth" Width="200px" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月"
                ID="CalendarExtender1" runat="server" Format="yyyy-MM" TargetControlID="tb_yearmonth">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        备注
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_NOTE" Width="200px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
           </asp:Panel>
           <asp:Panel ID="PanelCondition2" runat="server" Width="350px" Style="display: none">
                            <table width="90%" style="background-color: #CCCCFF; border: solid 1px black;">
                                <tr>
                                    <td colspan="8" align="center">
                                        <asp:Button ID="QueryButton2" runat="server" OnClick="QueryButton2_Click" Text="确认" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnClose2" runat="server" OnClick="btnClose2_Click" Text="取消" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        月份
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_yearmonth2" Width="200px" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月"
                ID="CalendarExtender2" runat="server" Format="yyyy-MM" TargetControlID="tb_yearmonth2">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        备注
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_NOTE2" Width="200px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
           </asp:Panel>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
                                            制单人
                                        </td>
                                        <td align="center">
                                            制单时间
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
                                            <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="SCCZTOL_BH" runat="server" Text='<%#Eval("SCCZTOL_BH")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="SCCZTOL_NY" runat="server" Width="90px" Text='<%#Eval("SCCZTOL_NY")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="SCCZTOL_ZDRNAME" runat="server" Width="50px" Text='<%#Eval("SCCZTOL_ZDRNAME")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="SCCZTOL_ZDTIME" runat="server" Width="90px" Text='<%#Eval("SCCZTOL_ZDTIME")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="SCCZTOL_TOLSTATE" runat="server" Text='<%#Eval("SCCZTOL_TOLSTATE").ToString()=="0"?"初始化":Eval("SCCZTOL_TOLSTATE").ToString()=="1"?"审核中":Eval("SCCZTOL_TOLSTATE").ToString()=="2"?"通过":Eval("SCCZTOL_TOLSTATE").ToString()=="3"?"驳回":"出错"%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="SCCZTOL_NOTE" runat="server" Text='<%#Eval("SCCZTOL_NOTE")%>'></asp:Label>
                                        </td>
                                        <td>
                                        <asp:HyperLink ID="hlContract3" Target="_blank" ToolTip="查看/审核" NavigateUrl='<%#"~/OM_Data/OM_SCCZSP_DETAIL.aspx?bh="+Eval("SCCZTOL_BH") %>'
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
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
