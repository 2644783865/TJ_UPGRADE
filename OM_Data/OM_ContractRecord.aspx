<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="OM_ContractRecord.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_ContractRecord"
    Title="无标题页" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    合同签订记录
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ScriptManager ID="ScriptManagerOne" AsyncPostBackTimeout="10" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="ControlFinder" runat="server" Text="" Visible="false"></asp:Label>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td style="width: 20%;">
                                <asp:RadioButtonList runat="server" ID="rblStaffStatus" RepeatDirection="Horizontal"
                                    AutoPostBack="true" OnSelectedIndexChanged="Query">
                                    <asp:ListItem Text="全部"></asp:ListItem>
                                    <asp:ListItem Text="在职" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="离职" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="实习" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="width: 18%;">
                                <strong>按部门查：</strong>
                                <asp:DropDownList Width="100px" ID="ddlPartment" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="Query">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 18%;">
                                <strong>按合同主体查：</strong>
                                <asp:DropDownList Width="100px" ID="ddlContract" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="Query">
                                </asp:DropDownList>
                            </td>
                            <td colspan="2">
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
                            <td>
                                <asp:FileUpload ID="FileUpload1" Width="150px" runat="server" ToolTip="上传" />
                                <asp:Button ID="btnImport" runat="server" Text="导 入" OnClick="btnImport_Click" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnExport" runat="server" Text="导 出" OnClick="btnExport_Click" Visible="true" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="box-wrapper" id="datagrid">
        <div class="box-outer">
            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                没有记录!</asp:Panel>
            <asp:Panel ID="PanelBody" runat="server" Style="overflow-y: auto; overflow-x: scroll;"
                Width="100%" Height="420px">
                <asp:SmartGridView ID="SmartGridView1" Width="100%" CssClass="toptable grid " runat="server"
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnRowDataBound="SmartGridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="序号" HeaderStyle-Height="30px">
                            <ItemTemplate>
                                <asp:Label ID="lbXuhao" runat="server" Text="" Width="36px"></asp:Label>
                                <asp:Label ID="lbC_STID" runat="server" Text='<%#Eval("C_STID")%>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="C_STName" HeaderText="姓名" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        </asp:BoundField>
                        <asp:BoundField DataField="C_STDep" HeaderText="部门" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        </asp:BoundField>
                        <asp:BoundField DataField="C_STContract" HeaderText="合同主体" ItemStyle-Wrap="false"
                            HeaderStyle-Wrap="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="合同签订次数">
                            <ItemTemplate>
                                <asp:Label ID="lbTimes" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="最新合同" ItemStyle-Wrap="false" ItemStyle-BackColor="#ccffff"
                            HeaderStyle-Wrap="false"></asp:BoundField>
                        <asp:BoundField DataField="C_EditNote" HeaderText="备注" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        </asp:BoundField>
                        <asp:BoundField DataField="C_EditTime" HeaderText="编辑时间" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        </asp:BoundField>
                        <asp:BoundField DataField="C_EditPerson" HeaderText="编辑人" ItemStyle-Wrap="false"
                            HeaderStyle-Wrap="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="修改">
                            <ItemTemplate>
                                <asp:HyperLink ID="hplEdit" NavigateUrl='<%# edit(Eval("C_STID").ToString(),Eval("C_STName").ToString(),Eval("C_STContract").ToString()) %>'
                                    runat="server" ToolTip='<%#"点击修改【"+ Eval("C_STName")+"】的合同签订信息"%>'>
                                    <asp:Image ID="imgEdit" ImageUrl="~/assets/images/res.gif" hspace="2" align="absmiddle"
                                        runat="server" />修改</asp:HyperLink></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="White" Height="20px" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixRows="0" FixColumns="0,1,2,3" />
                </asp:SmartGridView>
            </asp:Panel>
            <div style="text-align: center; padding-top: 6px">
                总人数：
                <asp:Label ID="lbPeople" runat="server" ForeColor="Red" Font-Size="10pt"></asp:Label>&nbsp;人
            </div>
            <div style="float: right">
                <table>
                    <tr>
                        <td>
                            <asp:UCPaging ID="UCPaging1" runat="server" />
                        </td>
                        <td>
                            每页：<asp:DropDownList ID="ddl_pageno" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Query">
                                <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                <asp:ListItem Text="全部" Value="10000"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;行
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
