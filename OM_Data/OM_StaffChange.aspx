<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_StaffChange.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_StaffChange"
    Title="人员增减统计" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    人员增减统计
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        function viewCondition() {
            document.getElementById('<%=select.ClientID%>').style.display = 'block';         
            return false;
        }

        function Close() {
            document.getElementById('<%=select.ClientID%>').style.display = 'none';
        }
    </script>

    <asp:ScriptManager ID="ScriptManagerOne" AsyncPostBackTimeout="10" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="ControlFinder" runat="server" Text="" Visible="false"></asp:Label>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td style="width: 18%;">
                                <strong>按部门查：</strong>
                                <asp:DropDownList Width="100px" ID="ddlPartment" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="Query">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <strong>时间:</strong>
                                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Query">
                                </asp:DropDownList>
                                &nbsp;年&nbsp;
                                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Query">
                                </asp:DropDownList>
                                &nbsp;月&nbsp;
                                <asp:LinkButton ID="lkbTime" CssClass="hand" runat="server" OnClientClick="return  viewCondition()">
                                    <asp:Image ID="Image1" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2"
                                        align="absmiddle" runat="server" />&nbsp;时间区间</asp:LinkButton>
                                <asp:ModalPopupExtender ID="ModalPopupExtender1" X="200" Y="600" runat="server" TargetControlID="lkbTime"
                                    PopupControlID="select" CancelControlID="close">
                                </asp:ModalPopupExtender>
                            </td>
                            <td>
                                <strong>姓名：</strong>
                                <asp:TextBox ID="txtName" runat="server" Width="80px"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtName"
                                    ServicePath="~/OM_Data/OM_Data_Autocomplete.asmx" CompletionSetCount="100" MinimumPrefixLength="1"
                                    CompletionInterval="100" ServiceMethod="Getdata" FirstRowSelected="true" CompletionListCssClass="completionListElement"
                                    CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="highlightedListItem"
                                    UseContextKey="false" EnableCaching="false">
                                </asp:AutoCompleteExtender>
                                <asp:Button ID="btnSearch" runat="server" Text="查 看" OnClick="Query" />
                            </td>
                            <td colspan="2" align="left">
                                <asp:Button ID="btnSave" runat="server" Text="保 存" OnClick="btnSave_Click" ToolTip="点击保存增减原因说明和备注信息，不改变其他数据" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnUpdate" runat="server" Text="更 新"
                                    OnClick="btnUpdate_Click" OnClientClick="return confirm('点击更新当月人员增减信息，确认更新？')">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="select" Style="display: none; border-style: solid; border-width: 1px;
                        border-color: blue; background-color: Menu;" runat="server">
                        <table width="300px;">
                            <tr>
                                <td>
                                    <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 13px;
                                        font-size: 12px; font-weight: bold; position: absolute; top: 6px; right: 5px;">
                                        <a id="close" style="background-color: #6699CC; cursor: pointer; color: #FFFFFF;
                                            text-align: center; text-decoration: none; padding: 3px;" title="关闭">x</a>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbtips" runat="server" Text="截止年月:" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; height: 30px">
                                    <asp:TextBox ID="txtEndTime" runat="server" data-options="formatter:function(date){var y=date.getFullYear();var m=(date.getMonth()+1).toString();var lenth=m.length;if(lenth<2){m='0'+m;}; return y+'-'+m;}"
                                        class="easyui-datebox" Style="width: 80px" editable="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:Button ID="btnReset" runat="server" Text="重 置" OnClick="btnReset_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnFind" runat="server" Text="搜 索" OnClick="Query" />&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <div style="width: 100%; overflow-x: scroll">
        <table width="100%" id="tab" align="center" cellpadding="2" cellspacing="1" class=" grid "
            border="1" style="overflow: auto;">
            <asp:Repeater ID="rptstaffchange" runat="server" OnItemDataBound="rptstaffchange_ItemDataBound">
                <HeaderTemplate>
                    <tr style="background-color: #B9D3EE; height: 35px">
                        <td align="center">
                            <strong>序号</strong>
                        </td>
                        <td align="center" style="width: 7%">
                            <strong>部门</strong>
                        </td>
                        <td align="center">
                            <strong>年月</strong>
                        </td>
                        <td align="center">
                            <strong>上月人数</strong>
                        </td>
                        <td align="center">
                            <strong>当月人数</strong>
                        </td>
                        <td align="center">
                            <strong>新增人数</strong>
                        </td>
                        <td align="center">
                            <strong>减少人数</strong>
                        </td>
                        <td align="center">
                            <strong>新增人员信息</strong>
                        </td>
                        <td align="center">
                            <strong>减少人员信息</strong>
                        </td>
                        <td align="center">
                            <strong>增减原因说明</strong>
                        </td>
                        <td align="center">
                            <strong>备注</strong>
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                        ondblclick="javascript:changeback(this);">
                        <td align="center">
                            <asp:Label ID="lbXuHao" runat="server" Text=""></asp:Label><asp:CheckBox ID="chk"
                                runat="server" />
                            <asp:Label ID="lbSC_STDepID" runat="server" Text='<%#Eval("SC_STDepID")%>' Visible="false"></asp:Label>
                        </td>
                        <td align="center" id="tdmerge" runat="server" style="overflow: hidden;" nowrap='nowrap'>
                            <%#Eval("SC_STDep")%>
                        </td>
                        <td align="center">
                            <asp:Label ID="lbSC_YearMonth" runat="server" Text='<%#Eval("SC_YearMonth")%>' Width="70px"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="lbLasDeptNum" runat="server" Text="" Width="60px"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="lbSC_DepNum" runat="server" Text='<%#Eval("SC_DepNum")%>' Width="60px"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="lbSC_IncNum" runat="server" Text='<%#Eval("SC_IncNum")%>' Width="60px"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="lbSC_DecNum" runat="server" Text='<%#Eval("SC_DecNum")%>' Width="60px"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtSC_IncPersonInfo" runat="server" Text='<%#Eval("SC_IncPersonInfo")%>'
                                Width="240px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtSC_DecPersonInfo" runat="server" Text='<%#Eval("SC_DecPersonInfo")%>'
                                Width="240px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSC_ChangeInfo" runat="server" TextMode="MultiLine" Text='<%#Eval("SC_ChangeInfo")%>'></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSC_Note" runat="server" TextMode="MultiLine" Text='<%#Eval("SC_Note")%>'></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr>
                        <td colspan="3" align="center">
                            合计：
                        </td>
                        <td align="center">
                            <asp:Label ID="lb_lastnum" runat="server"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="lb_nownum" runat="server"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="lb_incnum" runat="server"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="lb_decnum" runat="server"></asp:Label>
                        </td>
                        <td colspan="4">
                        </td>
                    </tr>
                </FooterTemplate>
            </asp:Repeater>
        </table>
    </div>
    <asp:Panel ID="palNoData" runat="server" Visible="False" ForeColor="Red" HorizontalAlign="Center">
        没有记录!<br />
        <br />
    </asp:Panel>
    <div style="float: right">
        <table>
            <tr>
                <td>
                    <asp:UCPaging ID="UCPaging1" runat="server" />
                </td>
                <td>
                    <p id="prow" runat="server">
                        每页：<asp:DropDownList ID="ddl_pageno" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Query">
                            <asp:ListItem Text="50" Value="50">
                            </asp:ListItem>
                            <asp:ListItem Text="100" Value="100">
                            </asp:ListItem>
                            <asp:ListItem Text="全部" Value="10000">
                            </asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;行</p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
