<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_KaoHeMuBan.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_KaoHeMuBan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    考核表模板
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
            }
        }

    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float: right">
                <asp:Button ID="btnsubmit" runat="server" Text="保 存" OnClick="btnsubmit_Click" />
                &nbsp;&nbsp;&nbsp
                <asp:Button ID="btnreturn" runat="server" Text="返 回" OnClientClick="window.location.href='OM_KaoHeMuBList.aspx';" />&nbsp;&nbsp;&nbsp;&nbsp;
       <input runat="server" id="hidAddPer" type="hidden" />
            </div>
            <div style="text-align: center; margin-top: 20px; padding-top: 10px">
                <h2>
                    考核模板</h2>
            </div>
            <br />
            <div style="width: 100%; text-align: center">
                <table width="1150px" style="margin-left: 10px">
                    <tr>
                        <td>
                            部门：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            类别：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" Width="100px">
                                <asp:ListItem Text="请选择" Value="00"></asp:ListItem>
                                <asp:ListItem Text="人员月度考核" Value="人员月度考核"></asp:ListItem>
                                <asp:ListItem Text="人员年度考核" Value="人员年度考核"></asp:ListItem>
                                <asp:ListItem Text="入职转正考核" Value="入职转正考核"></asp:ListItem>
                                <asp:ListItem Text="合同续订考核" Value="合同续订考核"></asp:ListItem>
                                <asp:ListItem Text="合同主体转移考核" Value="合同主体转移考核"></asp:ListItem>
                                <asp:ListItem Text="实习生实习期考核" Value="实习生实习期考核"></asp:ListItem>
                                <asp:ListItem Text="员工岗位调整考核" Value="员工岗位调整考核"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            模板名称：
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td>
                            所占比重&nbsp;&nbsp;&nbsp;&nbsp; 一级:
                            <asp:TextBox ID="txtBl1" runat="server" Width="30"></asp:TextBox>
                            二级:
                            <asp:TextBox ID="txtBl2" runat="server" Width="30"></asp:TextBox>
                            三级:
                            <asp:TextBox ID="txtBl3" runat="server" Width="30"></asp:TextBox>
                            四级:
                            <asp:TextBox ID="txtBl4" runat="server" Width="30"></asp:TextBox>
                        </td>
                        <td align="right">
                            增加行：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtNum" runat="server" Width="50px" onblur="CheckNum(this);" CssClass="center"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnadd" runat="server" Text="确 定" OnClick="btnadd_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="delete" runat="server" Text="删除行" OnClick="delete_Click"
                    Visible="false" />
                        </td>
                        
                      
                    </tr>
                    <tr>
                      <td align="right">评分标准:&nbsp;&nbsp;&nbsp;&nbsp;</td><td colspan="6" align="left"><asp:TextBox runat="server" Width="600px" Height="40px" ID="txtPFBZ" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                </table>
            </div>
            <br />
            <div>
                <hr />
            </div>
            <div style="text-align: center; overflow: auto; height: 300px">
                <table id="gr" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                    <asp:Repeater ID="Det_Repeater" runat="server">
                        <HeaderTemplate>
                            <tr class="tableTitle headcolor">
                                <td>
                                    <asp:Label ID="Label" Text="序 号" runat="server" Font-Bold="true" Width="50px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="kh_Col1" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="kh_Col2" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="kh_Col3" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="kh_Col4" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="kh_Col5" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                </td>
                                <%-- <td>
                                    <asp:TextBox ID="kh_Col6" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="kh_Col7" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="kh_Col8" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="kh_Col9" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="kh_Col10" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="kh_Col11" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="kh_Col12" runat="server" Width="100px" CssClass="center"></asp:TextBox>
                                </td>--%>
                                <td>
                                    <asp:Label ID="Label1" Text="权 重" runat="server" Font-Bold="true" Width="150px"></asp:Label>
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                <td>
                                    <div style="width: 50px">
                                        <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                        <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                        </asp:CheckBox></div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt1" runat="server" Width="150px" Height="50px" TextMode="MultiLine"
                                        Text='<%#Eval("kh_Cont1")  %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt2" runat="server" Width="150px" Height="50px" TextMode="MultiLine"
                                        Text='<%#Eval("kh_Cont2") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt3" runat="server" Width="150px" Height="50px" TextMode="MultiLine"
                                        Text='<%#Eval("kh_Cont3") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt4" runat="server" Width="150px" Height="50px" TextMode="MultiLine"
                                        Text='<%#Eval("kh_Cont4") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt5" runat="server" Width="150px" Height="50px" TextMode="MultiLine"
                                        Text='<%#Eval("kh_Cont5") %>'></asp:TextBox>
                                </td>
                                <%--       <td>
                                    <asp:TextBox ID="txt6" runat="server" Width="150px" Height="50px" TextMode="MultiLine"
                                        Text='<%#Eval("kh_Cont6") %>'></asp:TextBox>
                                </td>
                             <td>
                                    <asp:TextBox ID="txt7" runat="server" Width="150px" Height="50px" TextMode="MultiLine"
                                        Text='<%#Eval("kh_Cont7") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt8" runat="server" Width="150px" Height="50px" TextMode="MultiLine"
                                        Text='<%#Eval("kh_Cont8") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt9" runat="server" Width="150px" Height="50px" TextMode="MultiLine"
                                        Text='<%#Eval("kh_Cont9") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt10" runat="server" Width="150px" Height="50px" TextMode="MultiLine"
                                        Text='<%#Eval("kh_Cont10") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt11" runat="server" Width="150px" Height="50px" TextMode="MultiLine"
                                        Text='<%#Eval("kh_Cont11") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt12" runat="server" Width="150px" Height="50px" TextMode="MultiLine"
                                        Text='<%#Eval("kh_Cont12") %>'></asp:TextBox>
                                </td>--%>
                                <td>
                                    <asp:TextBox ID="txt6" runat="server" Width="150px" Height="50px" TextMode="MultiLine"
                                        Text='<%#Eval("kh_Weight") %>'></asp:TextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                    没有记录!</asp:Panel>
            </div>
            <div style="float: left; padding-top: 10px">
                &nbsp; &nbsp; &nbsp;</div>
            <br />
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
