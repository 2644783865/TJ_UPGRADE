<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="PM_ProjectPlan_View.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_ProjectPlan_View"
    Title="项目计划管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagName="UCPaging" TagPrefix="uc1" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    项目计划
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <script src="../JS/DatePicker.js" type="text/javascript" ></script>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="RightContent">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        项目计划
                                    </td>
                                    <td align="right" runat="server">
                                        <asp:Button ID="btn_plan" runat="server" Text="提交项目计划" OnClick="btn_plan_click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <div>
                            <table width="100%">
                                <tr>
                                    <td style="font-size: big; text-align: center;" colspan="4">
                                        生产项目计划
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;" align="left">
                                        &nbsp;&nbsp;&nbsp;合&nbsp;&nbsp;&nbsp;同&nbsp;&nbsp;&nbsp;号：
                                        <asp:Label ID="contact" Text="" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 25%;" align="left">
                                        任&nbsp;&nbsp;&nbsp;务&nbsp;&nbsp;&nbsp;号：
                                        <asp:Label ID="projid" Text="" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 25%;" align="left">
                                        批&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：
                                        <asp:Label ID="pid" Text="" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 25%;" align="left">
                                        交&nbsp;&nbsp;&nbsp;货&nbsp;&nbsp;&nbsp;期：
                                        <asp:Label ID="endtime" Text="" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                            ActiveTabIndex="0">
                            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="项目计划" TabIndex="0">
                                <ContentTemplate>
                                    <div style="height: 450px; overflow: auto; width: 100%">
                                        <div class="cpbox1 xscroll">
                                            <table id="tab" class="nowrap cptable fullwidth" align="center" width="100%">
                                                <asp:Repeater ID="pm_projectplan_view_repeater" runat="server" >
                                                    <HeaderTemplate>
                                                        <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                                            <td>
                                                                <strong>行号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>图号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>设备名称</strong>
                                                            </td>
                                                            <td>
                                                                <strong>数量</strong>
                                                            </td>
                                                            <td>
                                                                <strong>下料</strong>
                                                            </td>
                                                            <td>
                                                                <strong>机加工</strong>
                                                            </td>
                                                            <td>
                                                                <strong>结构</strong>
                                                            </td>
                                                            <td>
                                                                <strong>装配</strong>
                                                            </td>
                                                             <td>
                                                                <strong>喷漆</strong>
                                                            </td>
                                                             <td>
                                                                <strong>包装入库</strong>
                                                            </td>
                                                             <td>
                                                                <strong>备注</strong>
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                            <td>
                                                                <asp:Label ID="ROWSNUM" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                 <asp:Label ID="MS_ID" runat="server" Visible="false" Text='<%#Eval("MS_ID")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="MS_TUHAO" runat="server" Text='<%#Eval("MS_TUHAO")%>'></asp:Label>
                                                            </td>
                                                             <td>
                                                                <asp:Label ID="MS_NAME" runat="server" Text='<%#Eval("MS_NAME")%>'></asp:Label>
                                                            </td>
                                                             <td>
                                                                <asp:Label ID="MS_UNUM" runat="server" Text='<%#Eval("MS_UNUM")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                 <asp:TextBox ID="xialiaotime" Text='<%#Eval("PM_XIALIAO")%>'   runat="server" onclick="setday(this)" />
                                                            </td>
                                                            <td>
                                                                 <asp:TextBox ID="jijiatime" Text='<%#Eval("PM_JIJIA")%>'  runat="server" onclick="setday(this)" />
                                                            </td>
                                                            <td>
                                                                 <asp:TextBox ID="jiegoutime" Text='<%#Eval("PM_JIEGOU")%>'  runat="server" onclick="setday(this)" />
                                                            </td>
                                                            <td>
                                                                 <asp:TextBox ID="zhuangpeitime" Text='<%#Eval("PM_ZHUANGPEI")%>'  runat="server" onclick="setday(this)" />
                                                            </td>
                                                            <td>
                                                                 <asp:TextBox ID="pengqitime" Text='<%#Eval("PM_PENGQI")%>'  runat="server" onclick="setday(this)" />
                                                            </td>
                                                            <td>
                                                                 <asp:TextBox ID="rukutime" Text='<%#Eval("PM_RUKU")%>'  runat="server" onclick="setday(this)" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_beizhu" Text='<%#Eval("PM_NOTE")%>' runat="server" ></asp:TextBox>
                                                            </td>
                                                       </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td colspan="19" align="center" >
                                                        <asp:Panel ID="NoDataPane1" runat="server" Visible="false">
                                                            没有数据！</asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                     <uc1:UCPaging ID="UCPaging1" runat="server" />
                                </ContentTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
