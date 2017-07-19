<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="QC_TargetAnalyze_Edit.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_TargetAnalyze_Edit" %>

<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <asp:Label ID="lblName" runat="server" Width="350px"></asp:Label>
    <input type="hidden" runat="server" id="hidId" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" />

    <script src="../JS/jquery/jquery-1.4.2.js" type="text/javascript"></script>

    <script type="text/javascript">
        function FindDepManager(obj) {
            var Id = obj.value;
            //     alert(obj.parentNode.parentNode.getElementsByTagName("td")[4].getElementsByTagName("input")[0].value);
            if (Id == '00') {

                obj.parentNode.parentNode.getElementsByTagName("td")[4].getElementsByTagName("input")[0].value = "";
            }
            else {
                $.ajax({
                    url: 'QC_AjaxHandler.aspx',
                    data: "DepId=" + Id + "&method=FindDepManager",
                    type: "POST",
                    dataType: 'json',
                    success: function(data) {
                        if (data.Id != '0') {
                            obj.parentNode.parentNode.getElementsByTagName("td")[4].getElementsByTagName("input")[0].value = data.name;
                            obj.parentNode.parentNode.getElementsByTagName("td")[4].getElementsByTagName("input")[1].value = data.Id;
                        }


                    },
                    error: function() { alert("请检查网络，稍后再试！"); }
                });
            }

        }
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="96%">
                            <tr>
                                <td style="width: 18%" align="right">
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td align="right">
                                    <asp:TextBox ID="txtNum" runat="server" Width="27" Text="1" Height="17px"></asp:TextBox>
                                    <asp:Button ID="btnInsert" runat="server" Width="40" Text="插入" OnClick="btnInsert_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnDelete" runat="server" Width="40" Text="删除" OnClick="btnDelete_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnSave" runat="server" Width="40" Text="保存" OnClick="btnSave_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                CellPadding="2" ForeColor="#333333" Width="100%" OnRowDataBound="GridView1_RowDataBound">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Width="8px" CssClass="checkBoxCss" />
                        </ItemTemplate>
                        <ItemStyle Width="8px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="行号">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                            <%-- <asp:HiddenField ID="HIDXUHAO" Value='<%#Eval("TARGET_ID")%>' runat="server" />--%>
                        </ItemTemplate>
                        <HeaderStyle Wrap="false" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="部门" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlDepId" runat="server" onchange="FindDepManager(this)">
                            </asp:DropDownList>
                            <input type="hidden" id="txtDepId" runat="server" value='<%#Eval("TARGET_DEPID") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="体系" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlTiXi" runat="server" SelectedValue='<%#Eval("TARGET_TIXI") %>'>
                                <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                                <asp:ListItem Text="质量" Value="质量"></asp:ListItem>
                                <asp:ListItem Text="环境" Value="环境"></asp:ListItem>
                                <asp:ListItem Text="安全" Value="安全"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="负责人" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtManager" type="text" runat="server" value='<% #Eval("TARGET_MANAGER") %>'
                                style="width: 35px" />
                            <%--<input type="hidden" id="hidMarId" runat="server" value='<% Eval("TARGET_MANAGER") %>' />--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="分解目标" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input type="text" runat="server" value='<%# Eval("TARGET_MUBIAO") %>' id="txtMuBiao"
                                style="width: 300px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="一月" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtJan" type="text" runat="server" value='<%# Eval("TARGET_JAN") %>' style="width: 35px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="二月" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtFeb" type="text" runat="server" value='<%# Eval("TARGET_FEB") %>' style="width: 35px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="三月" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtMar" type="text" runat="server" value='<%# Eval("TARGET_MAR") %>' style="width: 35px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="四月" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtApr" type="text" runat="server" value='<%# Eval("TARGET_APR") %>' style="width: 35px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="五月" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtMay" type="text" runat="server" value='<%# Eval("TARGET_MAY") %>' style="width: 35px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="六月" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtJun" type="text" runat="server" value='<%# Eval("TARGET_JUN") %>' style="width: 35px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="七月" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtJuy" type="text" runat="server" value='<%# Eval("TARGET_JUL") %>' style="width: 35px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="八月" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtAug" type="text" runat="server" value='<%# Eval("TARGET_AUG") %>' style="width: 35px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="九月" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtSep" type="text" runat="server" value='<%# Eval("TARGET_SEP") %>' style="width: 35px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="十月" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtOct" type="text" runat="server" value='<%# Eval("TARGET_OCT") %>' style="width: 35px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="十一月" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtNov" type="text" runat="server" value='<%# Eval("TARGET_NOV") %>' style="width: 35px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="十二月" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input id="txtDec" type="text" runat="server" value='<%# Eval("TARGET_DEC") %>' style="width: 35px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <FixRowColumn TableHeight="100%" TableWidth="100%" />
            </yyc:SmartGridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
