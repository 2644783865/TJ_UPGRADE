<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_SHBXBLJL.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_SHBXBLJL" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<strong>社会保险比例记录</strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
                 <div>
                    <div>
                        <div>
                           <table width="100%" id="table2">
                             <tr>
                               <td align="left">
                                    &nbsp;&nbsp;&nbsp;
                                    <strong>修改日期:</strong>
                                    从<asp:TextBox ID="txtStartTime" Width="100px" class="easyui-datebox" runat="server" onClick="setday(this)"></asp:TextBox>
                                    到
                                    <asp:TextBox ID="txtEndTime" runat="server" Width="100px" class="easyui-datebox" onClick="setday(this)"></asp:TextBox> 
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btnQuery" Text="查询" OnClick="btnQuery_OnClick" />
                                    &nbsp;&nbsp;&nbsp;
                              </td>
                            </tr>
                          </table>
                        </div>
                        <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                            <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                                border="1" width="100%">
                                <asp:Repeater ID="rptGDGZrecord" runat="server">
                                    <HeaderTemplate>
                                        <tr align="center" style="background-color: #B9D3EE;">
                                            <td align="center">
                                                序号
                                            </td>
                                            <td align="center">
                                                企业养老保险（单位）
                                            </td>
                                            <td align="center">
                                                失业保险（单位）
                                            </td>
                                            <td align="center">
                                                基本医疗保险门诊大额医疗（单位）
                                            </td>
                                            <td align="center">
                                                工伤保险（单位）
                                            </td>
                                            <td align="center">
                                                生育保险（单位）
                                            </td>
                                            <td align="center">
                                                企业养老保险（个人）
                                            </td>
                                            <td align="center">
                                                失业保险（个人）
                                            </td>
                                            <td align="center">
                                                基本医疗保险门诊大额医疗（个人）
                                            </td>
                                            <td align="center">
                                                在职大额医疗救助（个人）
                                            </td>
                                            <td align="center">
                                                修改时间
                                            </td>
                                            <td align="center">
                                                修改人
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                            ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                            <td align="center">
                                                <asp:Label ID="lbID" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:Label ID="BL_QYYLBX" runat="server" Text='<%#Eval("BL_QYYLBX")%>'></asp:Label>%
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:Label ID="BL_SYBX" runat="server" Text='<%#Eval("BL_SYBX")%>'></asp:Label>%
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:Label ID="BL_JBYL" runat="server" Text='<%#Eval("BL_JBYL")%>'></asp:Label>%
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:Label ID="BL_GSBX" runat="server" Text='<%#Eval("BL_GSBX")%>'></asp:Label>%
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:Label ID="BL_SHENGYU" runat="server" Text='<%#Eval("BL_SHENGYU")%>'></asp:Label>%
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:Label ID="BL_QYYLGR" runat="server" Text='<%#Eval("BL_QYYLGR")%>'></asp:Label>%
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:Label ID="BL_SYBXGR" runat="server" Text='<%#Eval("BL_SYBXGR")%>'></asp:Label>%
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:Label ID="BL_JBYLGR" runat="server" Text='<%#Eval("BL_JBYLGR")%>'></asp:Label>%
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:Label ID="BL_ZZDEYL" runat="server" Text='<%#Eval("BL_ZZDEYL")%>'></asp:Label>%
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:Label ID="BL_XGSJ" runat="server" Text='<%#Eval("BL_XGSJ")%>'></asp:Label>
                                            </td>
                                            <td runat="server" align="center">
                                                <asp:Label ID="BL_XGR" runat="server" Text='<%#Eval("BL_XGR")%>'></asp:Label>
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
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
</asp:Content>
