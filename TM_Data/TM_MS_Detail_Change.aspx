<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_MS_Detail_Change.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MS_Detail_Change" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    制作明细变更       
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
<script language="javascript">
         function check()
        {
            var obj=document.getElementsByTagName("input");
            var objstr = '';
            for(var i=0;i<obj.length;i++)
            {
                if(obj[i].type.toLowerCase()=="checkbox" && obj[i].value!="")
                {
                   if( obj[i].checked)
                   {    
                      objstr="checked";
                      break;
                   }
                }
            }
            if(objstr=="checked")
            {
               var i=confirm('制作明细确定变更吗？');
               if(i==true)
               {
                    document.getElementById("<%=txtid.ClientID%>").value="1" ;
               }
               else
               {
                    document.getElementById("<%=txtid.ClientID%>").value="0" ;
               }
            }
            else            
            {
                alert("请选择要变更的制作明细！");
            }
        }
</script>

<div class="box-wrapper">
 <div style="height:6px" class="box_top"></div>
    <div class="box-outer">
        <table width="100%">
        <tr>
            <td colspan="8">
                <asp:Button ID="btnchange" runat="server" Text="变 更" OnClientClick="check()" 
                    onclick="btnchange_Click"/>
                 &nbsp;&nbsp;
                <asp:Button ID="btnreturn" runat="server" Text="返 回" onclick="btnreturn_Click"/>
            </td>
        </tr>
        <tr>
            <td style=" font-size:large; text-align:center;" colspan="8">制作明细变更</td>
        </tr>
        <tr>
            <td style="width: 8%" align="right">生产制号:</td>
            <td style="width: 15%"><asp:Label ID="tsa_id" runat="server" Width="100%" /></td>
            <td style="width: 8%" align="right">项目编号:</td>
            <td style="width: 25%"><asp:Label ID="ms_no" runat="server" Width="100%" /></td>
            <td style="width: 8%" align="right">项目名称:</td>
            <td style="width: 14%"><asp:Label ID="lab_proname" runat="server" Width="100%" /></td>
            <td style="width: 8%" align="right">工程名称:</td>
            <td style="width: 14%"><asp:Label ID="lab_engname" runat="server" Width="100%" /></td>
        </tr>
        </table>
    </div>
    <div class="box-outer">
        <asp:Panel ID="Panel1" runat="server"  style="height: 400px; width: 100%; position:static">
            <yyc:SmartGridView ID="GridView1" width="100%" CssClass="toptable grid" 
                runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
            
            <%--<asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" 
                runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">--%>
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" Width="10px" CssClass="checkBoxCss"/>
                        </ItemTemplate>
                        <ItemStyle Width="10px" />
                      </asp:TemplateField>
                 <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Width="30px" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="图号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="txt_tuhao" runat="server" Text='<%# Eval("MS_TUHAO") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="总序" ItemStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="txt_zongxu" runat="server" Text='<%# Eval("MS_ZONGXU") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <asp:Label ID="txt_name" runat="server" Width="100px" Text='<%# Eval("MS_NAME") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="规格" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:Label ID="txt_guige" runat="server" Width="80px" Text='<%# Eval("MS_GUIGE") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="材质" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:Label ID="txt_caizhi" runat="server" Width="60px" Text='<%# Eval("MS_CAIZHI") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:Label ID="txt_num" runat="server" Width="80px" Text='<%# Eval("MS_UNUM") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="单重" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:Label ID="txt_uwght" runat="server" Width="80px" Text='<%# Eval("MS_UWGHT") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="总重" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:Label ID="txt_tlwght" runat="server" Width="80px" Text='<%# Eval("MS_TLWGHT") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="工艺流程" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:Label ID="txt_process" runat="server" Width="80px" Text='<%# Eval("MS_PROCESS") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="箱号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <asp:Label ID="txt_caseno" runat="server" Text='<%# Eval("MS_TIMERQ") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="说明" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:Label ID="txt_explain" runat="server" Width="80px" Text='<%# Eval("MS_ENVREFFCT") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="txt_note" runat="server" Text='<%# Eval("MS_NOTE") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>  
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <FixRowColumn FixRowType="Header,Pager" TableHeight="400px" TableWidth="100%" FixColumns="0" />
            <%--</asp:GridView>--%>
            </yyc:SmartGridView>
            <input id="txtid" type="text"  runat="server" readonly="readonly" style="display: none" />
            <input id="eng_type" type="text" runat="server" readonly="readonly" value="" style="display:none"/>
            <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
            <input id="ms_state" type="text" runat="server" readonly="readonly" value="" style="display:none"/>
            </asp:Panel>
    </div>
    </div>
</asp:Content>
