<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_MP_Require_Change.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MP_Require_Change" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    材料需用计划变更      
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
               var i=confirm('材料计划确定变更吗？');
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
                alert("请选择要变更的材料！");
            }
        }
</script>

<div class="box-wrapper">
 <div style="height:6px" class="box_top"></div>
    <div class="box-outer">
        <table width="100%">
        <tr>
            <td colspan="8">
                <asp:Button ID="btnchange" runat="server" Text="变 更" OnClientClick="check()" onclick="btnchange_Click"/>
                 &nbsp;&nbsp;
                <asp:Button ID="btnreturn" runat="server" Text="返 回" onclick="btnreturn_Click"/>
            </td>
        </tr>
        <tr>
            <td style=" font-size:large; text-align:center;" colspan="8">材料需用计划变更</td>
        </tr>
        <tr>
            <td style="width: 8%" align="right">生产制号:</td>
            <td style="width: 15%"><asp:Label ID="tsa_id" runat="server" Width="100%" /></td>
            <td style="width: 8%" align="right">项目编号:</td>
            <td style="width: 25%"><asp:Label ID="mp_no" runat="server" Width="100%" /></td>
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
                <asp:TemplateField HeaderText="总序" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="txt_zongxu" runat="server" Text='<%# Eval("MP_ZONGXU") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="物料编码" ItemStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="txt_mpid" runat="server" Text='<%# Eval("MP_MARID") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="助记码" ItemStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="txt_hmcode" runat="server" Text='<%# Eval("MP_HRCODE") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="材料名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <asp:Label ID="txt_name" runat="server" Width="100px" Text='<%# Eval("MP_NAME") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="规格" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:Label ID="txt_guige" runat="server" Width="80px" Text='<%# Eval("MP_GUIGE") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="材质" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:Label ID="txt_caizhi" runat="server" Width="60px" Text='<%# Eval("MP_CAIZHI") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="单位" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:Label ID="txt_unit" runat="server" Width="60px" Text='<%# Eval("MP_UNIT") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="需用数量" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:Label ID="txt_num" runat="server" Width="80px" Text='<%# Eval("MP_NUMBER") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:Label ID="txt_fixsize" runat="server" Width="80px" Text='<%# Eval("MP_FIXEDSIZE").ToString()=="1"?"定尺":Eval("MP_FIXEDSIZE").ToString()=="0"?"非定尺":"" %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用途" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:Label ID="txt_use" runat="server" Width="80px" Text='<%# Eval("MP_USAGE") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="材料类别" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:Label ID="txt_type" runat="server" Width="80px" Text='<%# Eval("MP_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="时间要求" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <asp:Label ID="txt_time" runat="server" Text='<%# Eval("MP_TIMERQ") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="环保影响" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:Label ID="txt_influence" runat="server" Width="80px" Text='<%# Eval("MP_ENVREFFCT") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="txt_note" runat="server" Text='<%# Eval("MP_NOTE") %>'></asp:Label>
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
            <input id="mp_state" type="text" runat="server" readonly="readonly" value="" style="display:none"/>
            </asp:Panel>
    </div>
    </div>
</asp:Content>
