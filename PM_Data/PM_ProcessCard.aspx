<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PM_ProcessCard.aspx.cs"
    MasterPageFile="~/Masters/RightCotentMaster.Master" Inherits="ZCZJ_DPF.PM_Data.PM_ProcessCard" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    工艺卡片
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        function OnTxtPersonInfoKeyDown() {
            var dep = document.getElementById('<%=ddlSearch.ClientID%>');
            var acNameClientId = "<%=aceProcessCard.ClientID %>";
            var acName = $find(acNameClientId);
            if (acName != null) {
                acName.set_contextKey(dep.options[dep.selectedIndex].value);
            }
        }
        //        function openLink(url) {
        //            var returnVlue = window.showModalDialog("TM_ProcessCard_EditP.aspx?Id=" + url, '', "dialogHeight:240px;dialogWidth:500px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        //        }
        //        function openLinkGen(url) {
        //            var returnVlue = window.showModalDialog("TM_ProcessCard_EditG.aspx?Id=" + url, '', "dialogHeight:240px;dialogWidth:500px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        //        }
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="box-inner">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%">
                    <tr>
                        <td align="right">
                            分类查询:
                            <asp:DropDownList ID="ddlSearch" runat="server" Height="20px">
                                <asp:ListItem Text="设备名称" Value="PRO_ENGNAME"></asp:ListItem>
                                <asp:ListItem Text="设备型号" Value="PRO_ENGMODEL"></asp:ListItem>
                                <asp:ListItem Text="部件名称" Value="PRO_PARTNAME"></asp:ListItem>
                                <asp:ListItem Text="图号" Value="PRO_TUHAO"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtSearch" onkeydown="return OnTxtPersonInfoKeyDown()"
                                Width="100px" Height="15px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="aceProcessCard" runat="server" CompletionSetCount="15"
                                MinimumPrefixLength="1" ServiceMethod="SearchProcessCard" FirstRowSelected="True"
                                ServicePath="~/Ajax.asmx" TargetControlID="txtSearch" CompletionInterval="500"
                                DelimiterCharacters="" Enabled="True">
                            </cc1:AutoCompleteExtender>
                            <asp:Button ID="btnSearch" runat="server" Text="查 询" OnClick="Search_Click" CommandName="ProcessCard" />
                        </td>
                        <td>
                            <asp:Button runat="server" Text="制图员处理" ID="btnZTSEE" BackColor="Yellow" OnClick="btnZTSEE_OnClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" Text="调度员处理" ID="btnDDSEE" BackColor="Green" OnClick="btnDDSEE_OnClick" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <yyc:SmartGridView ID="GridView1" Width="100%" Height="50%" CssClass="toptable grid"
                runat="server" AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333"
                DataKeyNames="PRO_ID" OnRowDataBound="GridView1_RowDataBound">
                <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1,2,3,4,5,6" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="行号">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cbxXuHao" />
                            <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                            <asp:HiddenField ID="HIDXUHAO" Value='<%#Eval("PRO_ID")%>' runat="server" />
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="制图员">
                        <ItemTemplate>
                            <asp:Label ID="lbPRO_IFZTSEE" runat="server" Width="100%" Height="100%"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="调度员">
                        <ItemTemplate>
                            <asp:Label ID="lbPRO_IFDDSEE" runat="server" Width="100%" Height="100%"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PRO_ENGNAME" HeaderText="设备名称">
                        <HeaderStyle Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRO_ENGMODEL" HeaderText="设备型号">
                        <HeaderStyle Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRO_PARTNAME" HeaderText="部件名称">
                        <HeaderStyle Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRO_TUHAO" HeaderText="图号">
                        <HeaderStyle Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRO_BANCI" HeaderText="版次">
                        <HeaderStyle Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRO_SUBMITNM" HeaderText="提交人" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRO_ISSUEDTIME" HeaderText="提交日期">
                        <HeaderStyle Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="附 件">
                        <ItemTemplate>
                            <asp:LinkButton ID="download" CssClass="link" runat="server" CausesValidation="False"
                                OnClick="download_OnClick" CommandArgument='<%#Eval("PRO_ID")%>'>
                                           <%#Eval("fileName")%>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="PRO_ADATE" HeaderText="下发日期" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRO_NOTE" HeaderText="备注" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <HeaderStyle Wrap="False" BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" Wrap="False" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </yyc:SmartGridView>
            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" Font-Size="Large">
                没有记录!</asp:Panel>
            <uc1:UCPaging ID="UCPagingPro" runat="server" />
        </div>
    </div>
</asp:Content>
