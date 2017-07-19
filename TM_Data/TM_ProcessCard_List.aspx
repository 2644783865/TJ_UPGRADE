<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="TM_ProcessCard_List.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_ProcessCard_List" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    工艺卡片表
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
        function openLink(url) {
            var returnVlue = window.showModalDialog("TM_ProcessCard_EditP.aspx?Id=" + url, '', "dialogHeight:270px;dialogWidth:500px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }
        function openLinkGen(url) {
            var returnVlue = window.showModalDialog("TM_ProcessCard_EditG.aspx?Id=" + url, '', "dialogHeight:270px;dialogWidth:500px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
   
        <ContentTemplate>
        
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
                                            <td align="right">
                                                <strong>审核状态:</strong>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="proRblstatus" RepeatColumns="6" runat="server" OnSelectedIndexChanged="Search_Click"
                                                    AutoPostBack="true">
                                                   
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="right">
                                                
                                                <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openLink(0)" CssClass="link">
                  <asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                                    align="absmiddle" runat="server" />新增工艺类卡片</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <yyc:SmartGridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333" DataKeyNames="PRO_ID"
                            OnRowDataBound="GridView1_RowDataBound">
                            <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1,2,3,4,5,6" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="行号">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                                        <asp:HiddenField ID="HIDXUHAO" Value='<%#Eval("PRO_ID")%>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                </asp:TemplateField>
                                  <asp:BoundField DataField="PRO_BANCI" HeaderText="版次">
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                </asp:BoundField>
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
                                            OnClick="download_OnClick" CommandArgument='<%#Eval("PRO_ID")%>' 
                                           >
                                           <%#Eval("fileName")%>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="PRO_ADATE" HeaderText="审核日期" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PRO_REVIEWANM" HeaderText="审核人" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PRO_REVIEWAADVC" HeaderText="意见" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" Visible="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PRO_REVIEWBNM" HeaderText="审核人" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PRO_REVIEWBADVC" HeaderText="意见" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" Visible="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PRO_REVIEWCNM" HeaderText="审核人" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PRO_REVIEWCADVC" HeaderText="意见" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Wrap="false" Visible="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="状态" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:Label ID="status" runat="server" Text='<%# Eval("PRO_STATE").ToString()=="1"?"未提交":Eval("PRO_STATE").ToString()=="2"||Eval("PRO_STATE").ToString()=="4"||Eval("PRO_STATE").ToString()=="6"?"审核中...":Eval("PRO_STATE").ToString()=="8"?"通过":Eval("PRO_STATE").ToString()=="9"?"已处理":"驳回" %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlTask4" CssClass="link" NavigateUrl='<%#"TM_ProcessCard_AuditP.aspx?Id="+Eval("PRO_ID")%>'
                                            runat="server">
                                            <asp:Image ID="Image6" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />
                                            <asp:Label ID="state4" runat="server" Text='<%# Eval("PRO_STATE").ToString()=="2"||Eval("PRO_STATE").ToString()=="4"||Eval("PRO_STATE").ToString()=="6"?"审核":"查看" %>'></asp:Label>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlTask5" CssClass="link" NavigateUrl='<%#"TM_ProcessCard_AuditP.aspx?auditId="+Eval("PRO_ID")%>'
                                            runat="server">
                                            <asp:Image ID="Image4" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />
                                            <asp:Label ID="state5" runat="server" Text='<%# Eval("PRO_STATE").ToString()=="2"||Eval("PRO_STATE").ToString()=="4"||Eval("PRO_STATE").ToString()=="6"?"审核":"查看" %>'></asp:Label>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操  作">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="hlDeleteGen" CssClass="link" runat="server" CausesValidation="False"
                                            OnClick="hlDelete_OnClick" CommandArgument='<%#Eval("PRO_ID")%>'
                                            OnClientClick="return confirm(&quot;确认删除该条工艺卡片么？&quot;)">
                                            <asp:Image ID="Image5" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />删除
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
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
                    </ContentTemplate>
   
</asp:Content>
